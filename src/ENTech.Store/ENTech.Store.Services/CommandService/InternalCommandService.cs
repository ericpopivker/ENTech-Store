using System;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Services.CommandService
{
	internal class InternalCommandService :
		CommandServiceBase,
		IInternalCommandService
	{
		public InternalCommandService(ICommandFactory commandFactory)
			: base(commandFactory)
		{
			
		}
		
		public TResponse Execute<TRequest, TResponse, TCommand>(TRequest request) 
			where TRequest : IRequest 
			where TResponse : ResponseBase, new() 
			where TCommand : ICommand<TRequest, TResponse>
		{
			var command = CommandFactory.Create<TCommand>();

			var responseStatus = TryExecute<TRequest, TResponse, TCommand>(request, command);
			if (responseStatus is ErrorResponseStatus<TResponse>)
				throw new InvalidOperationException(); //serialize Errors to Json

			return ((OkResponseStatus<TResponse>)responseStatus).Response;
		}
	}
}