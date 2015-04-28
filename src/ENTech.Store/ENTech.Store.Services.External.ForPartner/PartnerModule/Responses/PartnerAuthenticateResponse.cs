using System;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule.Responses
{
	public class PartnerAuthenticateResponse:ExternalResponse
	{
		public string Token { get; set; }

		public DateTime ExpireAt { get; set; }
	}
}