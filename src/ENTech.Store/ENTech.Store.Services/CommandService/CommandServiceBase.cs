using System;
using System.Linq;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.CommandService
{
	public abstract class CommandServiceBase
	{
		private readonly ICommandFactory _commandFactory;

		protected ICommandFactory CommandFactory {
			get
			{
				return _commandFactory;
			}
		}

		protected CommandServiceBase(ICommandFactory commandFactory)
		{
			_commandFactory = commandFactory;
		}

		protected void AfterExecute<TRequest, TResponse, TCommand>(TRequest request, TResponse response, TCommand command)
			where TRequest : IInternalRequest
			where TResponse : InternalResponse, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			command.NotifyExecuted(request, response);
		}

		protected TResponse TryExecute<TRequest, TResponse, TCommand>(TRequest request, TCommand command)
			where TRequest : IInternalRequest
			where TResponse : InternalResponse, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			var response = new TResponse();

				response.ArgumentErrors = new ArgumentErrorsCollection();

				var requestValidationResult = command.Validate(request);

				if (requestValidationResult.Any() == false)
				{
					try
					{
						response = command.Execute(request);
					}
					catch (Exception e)
					{
						//ErrorLogUtils.AddError(e);
						return ErrorResponse<TResponse>(new Error(CommonErrorCode.InternalServerError, e.Message));
					}
				}
				else
				{
					return ErrorResponse<TResponse>(new Error(CommonErrorCode.ArgumentErrors), requestValidationResult);
				}

				if (!response.IsSuccess)
				{
					return response;
				}

			return response;
		}

		protected TResponse ErrorResponse<TResponse>(Error error, ArgumentErrorsCollection argumentErrors = null,
			string extraMessage = null)
			where TResponse : InternalResponse, new()
		{
			var response = new TResponse { IsSuccess = false, Error = error };


			if (argumentErrors != null)
			{
				foreach (var argumentError in argumentErrors)
				{
					response.ArgumentErrors[argumentError.ArgumentName] = argumentError;
				}
			}

			if (extraMessage != null)
				response.Error.ErrorMessage += Environment.NewLine + extraMessage;

			return response;
		}
	}
}