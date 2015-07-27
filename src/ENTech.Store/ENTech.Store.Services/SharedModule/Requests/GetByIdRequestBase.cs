using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class GetByIdRequestBase<TResponse> : IRequest<TResponse>
		where TResponse : IResponse
	{
		public int Id { get; set; }
	}



	public abstract class GetByIdRequestBase<TResponse, TLoadOptionEnum> : GetByIdRequestBase<TResponse>
		where TResponse : IResponse
		where TLoadOptionEnum : struct
	{
		public ICollection<TLoadOptionEnum> LoadOptions { get; set; }
	}
}
