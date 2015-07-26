using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.SharedModule.Requests
{
	public abstract class GetByIdRequestBase : IRequest
	{
		public int Id { get; set; }
	}



	public abstract class GetByIdRequestBase<TLoadOptionEnum> : GetByIdRequestBase
		where TLoadOptionEnum : struct
	{
		public ICollection<TLoadOptionEnum> LoadOptions { get; set; }
	}
}
