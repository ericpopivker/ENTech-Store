using System;

namespace ENTech.Store.Infrastructure.Services.Requests
{
	public interface IRequest
	{
		[Obsolete("Hack")]
		string UserToken { get; set; }
		string ApiKey { get; set; }	 
	}
}