using System;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.Internal.PartnerModule.Responses
{
	public class PartnerAuthenticateResponse : InternalResponse
	{
		public string Token { get; set; }

		public DateTime ExpireAt { get; set; }
	}
}
