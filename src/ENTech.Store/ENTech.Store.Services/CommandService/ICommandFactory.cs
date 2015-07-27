using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.CommandService
{
	public interface ICommandFactory
	{
		CommandFacade<TResponse> Create<TResponse>(IRequest<TResponse> request) 
			where TResponse : IResponse;
	}
}