using System;
using System.Linq;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.CommandService
{
	public abstract class CommandServiceBase
	{
		private readonly ICommandFactory _commandFactory;

		protected ICommandFactory CommandFactory
		{
			get { return _commandFactory; }
		}

		protected CommandServiceBase(ICommandFactory commandFactory)
		{
			_commandFactory = commandFactory;
		}

		protected void AfterExecute<TRequest, TResponse, TCommand>(TRequest request, TResponse response, TCommand command)
			where TRequest : IRequest
			where TResponse : ResponseBase, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			command.NotifyExecuted(request, response);
		}

		protected TResponse TryExecute<TRequest, TResponse, TCommand>(TRequest request, TCommand command)
			where TRequest : IRequest
			where TResponse : ResponseBase, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			var response = new TResponse();

			response.ArgumentErrors = new ArgumentErrorsCollection();

			var requestValidatorResult = command.ValidateRequest(request);

			if (requestValidatorResult.IsValid)
			{
				try
				{
					response = command.Execute(request);
				}
				catch (Exception e)
				{
					//ErrorLogUtils.AddError(e);
					response.IsSuccess = false;

					string errorMessage = ErrorCodeUtils.GetErrorMessage<CommonErrorCode>(CommonErrorCode.InternalServerError);
					errorMessage += e.Message;
					response.Error = new ResponseError(CommonErrorCode.InternalServerError, errorMessage);
				}
			}
			else
			{
				response.IsSuccess = false;
				response.Error = new ResponseError(CommonErrorCode.ArgumentErrors,
					ErrorCodeUtils.GetErrorMessage<CommonErrorCode>(CommonErrorCode.ArgumentErrors));
				response.ArgumentErrors = requestValidatorResult.ArgumentErrors;
			}

			if (!response.IsSuccess)
			{
				return response;
			}

			return response;
		}
	}
}