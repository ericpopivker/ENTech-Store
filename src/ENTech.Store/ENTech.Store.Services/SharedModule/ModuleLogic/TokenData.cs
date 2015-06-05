using System;

namespace ENTech.Store.Services.SharedModule.ModuleLogic
{
	public class TokenData
	{
		public string Token { get; set; }

		public DateTime ExpiresAt { get; set; }
	}
}