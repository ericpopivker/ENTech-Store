using System;

namespace ENTech.Store.Infrastructure.Services.Requests
{
	public interface IInternalRequest
	{
		[Obsolete("Hack")]
		string UserToken { get; set; }
		string ApiKey { get; set; }	 
	}
}