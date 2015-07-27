using System;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.CommandService.Definition;

namespace ENTech.Store.Services.CommandService
{
	public class InternalCommandService :
		CommandServiceBase,
		IInternalCommandService
	{
		public InternalCommandService(ICommandFactory commandFactory)
			: base(commandFactory)
		{
			
		}
		
		public TResponse Execute<TResponse>(IRequest<TResponse> request) 
			where TResponse : IResponse, new() 
		{
			var command = CommandFactory.Create(request);

			var responseStatus = TryExecute(request, command);

			if (responseStatus is ErrorResponseStatus<TResponse>)
				throw new InvalidOperationException(); //serialize Errors to Json

			return ((OkResponseStatus<TResponse>)responseStatus).Response;
		}
	}
}