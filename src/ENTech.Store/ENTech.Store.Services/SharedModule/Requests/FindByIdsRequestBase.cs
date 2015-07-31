using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class FindByIdsRequestBase<TResponse> : IRequest<TResponse>
		where TResponse : IResponse
	{
		public IEnumerable<int> Ids { get; set; }
	}
}