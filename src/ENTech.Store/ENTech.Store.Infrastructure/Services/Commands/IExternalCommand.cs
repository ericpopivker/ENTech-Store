using System;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface IExternalCommand
	{

	}

	[Obsolete]
	public interface IExternalCommand<in TExternalRequest, out TExternalResponse, TInternalRequest, TInternalResponse> : IExternalCommand
		where TExternalRequest:IExternalRequest
		where TExternalResponse:ExternalResponse
		where TInternalRequest:class, IInternalRequest
		where TInternalResponse:InternalResponse
	{
		TExternalResponse Execute(TExternalRequest request);

		TInternalResponse ExecuteInternal(TInternalRequest internalRequest);
	}
}