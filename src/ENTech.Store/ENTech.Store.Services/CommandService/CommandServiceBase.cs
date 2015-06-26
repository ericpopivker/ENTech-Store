using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Services;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Infrastructure.Services.Validators;

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
			where TRequest : IRequest
			where TResponse : ResponseBase, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			command.NotifyExecuted(request, response);
		}

		protected IResponseStatus<TResponse> TryExecute<TRequest, TResponse, TCommand>(TRequest request, TCommand command)
			where TRequest : IRequest
			where TResponse : ResponseBase, new()
			where TCommand : ICommand<TRequest, TResponse>
		{
			var response = new TResponse();
			
			var validatorResult = command.Validate(request);

			if (validatorResult.IsValid)
			{
				try
				{
					response = command.Execute(request);
				}
				catch (Exception e)
				{
					//ErrorLogUtils.AddError(e);

					var error = new InternalResponseError();
					return new ErrorResponseStatus<TResponse>(error);
				}
			}
			else
			{
				return new ErrorResponseStatus<TResponse>(validatorResult.Error);
			}
			

			return new OkResponseStatus<TResponse>(response);
		}

		
	}
}