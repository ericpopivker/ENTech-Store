using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.CommandService.Definition
{
	public interface IInternalCommandService 
	{
		TResponse Execute<TResponse>(IRequest<TResponse> request)
			where TResponse : IResponse, new();
	}
}