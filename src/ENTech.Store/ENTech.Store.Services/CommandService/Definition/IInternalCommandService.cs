using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.CommandService.Definition
{
	public interface IInternalCommandService 
	{
		TResponse Execute<TRequest, TResponse, TCommand>(TRequest request)
			where TCommand : ICommand<TRequest, TResponse>
			where TRequest : IInternalRequest
			where TResponse : InternalResponse, new();
	}
}