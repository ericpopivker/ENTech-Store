using System;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;

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

		protected void AfterExecute<TResponse>(IRequest<TResponse> request, TResponse response, CommandFacade<TResponse> commandFacade)
			where TResponse : IResponse, new()
		{
			commandFacade.NotifyExecuted(request, response);
		}

		protected IResponseStatus<TResponse> TryExecute<TResponse>(IRequest<TResponse> request, CommandFacade<TResponse> commandFacade)
			where TResponse : IResponse, new()
		{
			var response = new TResponse();
			
			var validateResult = commandFacade.Validate(request);

			if (validateResult.IsValid)
			{
				try
				{
					response = commandFacade.Execute(request);
				}
				catch (Exception e)
				{
					//ErrorLogUtils.AddError(e);

					var error = new InternalServerResponseError();
					return new ErrorResponseStatus<TResponse>(error);
				}
			}
			else
			{
				return new ErrorResponseStatus<TResponse>(validateResult.ResponseError);
			}

			return new OkResponseStatus<TResponse>(response);
		}

		
	}
}