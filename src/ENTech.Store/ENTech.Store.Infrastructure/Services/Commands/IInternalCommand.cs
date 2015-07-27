using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface IInternalCommand
	{
		
	}
	public interface IInternalCommand<in TRequest, out TResponse> : IInternalCommand
		where TRequest : class, IRequest<TResponse>
		where TResponse : class, IResponse
	{
		TResponse Execute(TRequest request);
	}
}