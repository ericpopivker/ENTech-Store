using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Enums;
using ENTech.Store.Infrastructure.Extensions;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.AuthenticationModule.Commands;
using ENTech.Store.Services.AuthenticationModule.Dtos;
using ENTech.Store.Services.AuthenticationModule.Responses;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;
using Newtonsoft.Json;

namespace ENTech.Store.Services.CommandService
{
	public abstract class ExternalCommandService<TSecurity> :
		CommandServiceBase,
		IExternalCommandService<TSecurity>
			where TSecurity : ISecurityInformation
	{
		private readonly IInternalCommandService _internalCommandService;

		protected IInternalCommandService InternalCommandService { get { return _internalCommandService; } }

		protected ExternalCommandService(ICommandFactory commandFactory)
			: base(commandFactory)
		{
			_internalCommandService = new InternalCommandService(commandFactory);
		}

		public TResponse Execute<TRequest, TResponse, TCommand>(TRequest request)
			where TRequest : SecureRequestBase<TSecurity> 
			where TResponse : ResponseBase, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			var unitOfWork = IoC.Resolve<IUnitOfWork>();

			var stopwatch = new Stopwatch();

			TResponse response = new TResponse();

			AuthenticateResult authenticateResult = new AuthenticateResult();

			try
			{
				stopwatch.Start();

				var command = CommandFactory.Create<TCommand>();

				if (command.RequiresTransaction)
				{
					unitOfWork.BeginTransaction();
				}

				authenticateResult = Authenticate(unitOfWork, request);

				if (authenticateResult.IsSuccess == false)
				{
					response.IsSuccess = false;
					response.Error = new ResponseError(CommonErrorCode.UserNotAuthorized, ErrorCodeUtils.GetErrorMessage<CommonErrorCode>(CommonErrorCode.UserNotAuthorized));
					return response;
				}

				LimitDbContext(request, unitOfWork.DbContext);
				try
				{
					response = TryExecute<TRequest, TResponse, TCommand>(request, command);

					unitOfWork.SaveChanges();

					if (command.RequiresTransaction)
					{					
						unitOfWork.CompleteTransaction();
					}

					AfterExecute(request, response, command);
				}
				catch (Exception e)
				{
					if (command.RequiresTransaction)
					{
						unitOfWork.RollbackTransaction();
					}

					//ErrorLogUtils.AddError(e);

					response.IsSuccess = false;

					string errorMessage = ErrorCodeUtils.GetErrorMessage<CommonErrorCode>(CommonErrorCode.InternalServerError);
					errorMessage += e.Message;
					response.Error = new ResponseError(CommonErrorCode.InternalServerError, errorMessage);
				}

				unitOfWork.Dispose();
				
				return response;
			}
			catch (Exception e)
			{
				//ErrorLogUtils.AddError(e);

				response.IsSuccess = false;

				string errorMessage = ErrorCodeUtils.GetErrorMessage<CommonErrorCode>(CommonErrorCode.InternalServerError);
				errorMessage += e.Message;
				response.Error = new ResponseError(CommonErrorCode.InternalServerError, errorMessage);
				return response;
			}
			finally
			{
				if (unitOfWork != null)
					unitOfWork.Dispose();

				stopwatch.Stop();

				SaveApiLogEntry(stopwatch.ElapsedMilliseconds, response, request, authenticateResult.Partner);
			}
		}

		protected AuthenticateResult Authenticate(IUnitOfWork unitOfWork, SecureRequestBase<TSecurity> request)
		{
			var result = _internalCommandService
				.Execute
				<SecureRequestBase<TSecurity>, AuthenticateApiKeyResponse,
					AuthenticateApiKeyCommand<SecureRequestBase<TSecurity>>>(request);

			if (result.IsSuccess)
			{
				var partner = result.Partner;
				var internalAuthenticateResult = AuthenticateInternal(unitOfWork, request);
				return new AuthenticateResult
				{
					IsSuccess = internalAuthenticateResult.IsSuccess,
					ErrorMessage = internalAuthenticateResult.ErrorMessage,
					Partner = internalAuthenticateResult.IsSuccess ? partner : null
				};
			}

			return new AuthenticateResult
			{
				IsSuccess = false,
				ErrorMessage = result.Error.Decode(x => x.ErrorMessage)
			};
		}


		protected virtual InternalAuthenticateResult AuthenticateInternal(IUnitOfWork unitOfWork, SecureRequestBase<TSecurity> request)
		{
			return new InternalAuthenticateResult
			{
				IsSuccess = true
			};
		}

		protected abstract void LimitDbContext(SecureRequestBase<TSecurity> request, IDbContext dbContext);

		private void SaveApiLogEntry<TRequest, TResponse>(decimal duration, TResponse response, TRequest request, PartnerDto partner)
			where TResponse : ResponseBase, new()
			where TRequest : SecureRequestBase<TSecurity> 
		{
			//try
			//{
			//	string fullUrl = String.Empty;
			//	string route = String.Empty;
			//	string query = String.Empty;
			//	string headers = String.Empty;
			//	string jsonRequest = String.Empty;
			//	string callerIp = String.Empty;

			//	string jsonResponse = JsonConvert.SerializeObject(response);

			//	if (HttpContext.Current != null)
			//	{
			//		fullUrl = HttpContext.Current.Request.Url.OriginalString;

			//		route = HttpContext.Current.Request.Url.AbsolutePath;

			//		var sb = new StringBuilder();

			//		for (int i = 0; i < HttpContext.Current.Request.Headers.Count; i++)
			//			sb.AppendFormat("{0}={1};", HttpContext.Current.Request.Headers.Keys[i],
			//				HttpContext.Current.Request.Headers[i].ToString());

			//		headers = sb.ToString();

			//		query = HttpContext.Current.Request.QueryString.ToString();

			//		using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
			//		{
			//			try
			//			{
			//				HttpContext.Current.Request.InputStream.Position = 0;
			//				jsonRequest = reader.ReadToEnd();
			//			}
			//			catch (Exception ex)
			//			{
			//			}
			//			finally
			//			{
			//				HttpContext.Current.Request.InputStream.Position = 0;
			//			}
			//		}

			//		callerIp = HttpContext.Current.Request.UserHostAddress;
			//	}


			//	if (HttpContext.Current == null || !HttpContext.Current.Request.Url.OriginalString.Contains(EnvSettings.ApiUrl))
			//		jsonRequest = JsonConvert.SerializeObject(request);


			//	int? partnerId = null;
			//	string partnerName = String.Empty;

			//	if (partner != null)
			//	{
			//		partnerId = partner.Id;
			//		partnerName = partner.Name;
			//	}

			//	ErrorLogUtils.AddApiLogEntry(partnerId, partnerName, (int) EnvSettings.EnvType, EnvSettings.EnvType.GetStringValue(),
			//		fullUrl, route, query, headers, jsonRequest, jsonResponse, duration, callerIp);
			//}
			//catch (Exception ex)
			//{
			//	ErrorLogUtils.AddError(ex);
			//}
		}


		protected class InternalAuthenticateResult
		{
			public bool IsSuccess { get; set; }
			public string ErrorMessage { get; set; }
		}
	}
}