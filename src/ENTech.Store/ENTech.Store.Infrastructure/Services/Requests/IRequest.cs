using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Requests
{
	public interface IRequest<TResponse>
		where TResponse : IResponse
	{
	}
}