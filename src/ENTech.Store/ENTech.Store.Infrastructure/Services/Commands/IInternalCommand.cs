using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface IInternalCommand
	{
		
	}
	public interface IInternalCommand<in TInternalRequest, out TInternalResponse> : IInternalCommand
		where TInternalRequest : class, IInternalRequest
		where TInternalResponse : class, IResponse
	{

		TInternalResponse Execute(TInternalRequest request);
	}
}