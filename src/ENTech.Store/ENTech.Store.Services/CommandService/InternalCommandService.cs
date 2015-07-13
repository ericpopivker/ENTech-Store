using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
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
		
		public TResponse Execute<TRequest, TResponse, TCommand>(TRequest request) 
			where TRequest : IInternalRequest 
			where TResponse : InternalResponse, new() 
			where TCommand : ICommand<TRequest, TResponse>
		{
			var command = CommandFactory.Create<TCommand>();

			var response = TryExecute<TRequest, TResponse, TCommand>(request, command);

			return response;
		}
	}
}