using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;

namespace ENTech.Store.Services.CommandService.Definition
{
	public interface IExternalCommandService
	{
		IResponseStatus<TResponse> Execute<TRequest, TResponse, TCommand>(TRequest request)
			where TCommand : ICommand<TRequest, TResponse>
			where TRequest : IRequest
			where TResponse : IResponse, new();
	}
}