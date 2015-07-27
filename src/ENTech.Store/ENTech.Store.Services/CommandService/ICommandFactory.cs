using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.CommandService
{
	public interface ICommandFactory
	{
		CommandFacade<TResponse> Create<TResponse>(IRequest<TResponse> request) 
			where TResponse : IResponse;
	}
}