using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;

namespace ENTech.Store.Services.CommandService.Definition
{
	public interface IExternalCommandService
	{
		IResponseStatus<TResponse> Execute<TResponse>(IRequest<TResponse> request)
			where TResponse : IResponse, new();
	}
}