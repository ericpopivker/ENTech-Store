using ENTech.Store.Services.AuthenticationModule.Dtos;

namespace ENTech.Store.Services.Misc
{
	public struct AuthenticateResult
	{
		public bool IsSuccess { get; set; }
		public string ErrorMessage { get; set; }

		public PartnerDto Partner {get;set;}

		public static AuthenticateResult Success()
		{
			return new AuthenticateResult
			{
				IsSuccess = true
			};
		}

		public static AuthenticateResult Fail(string errorMessage)
		{
			return new AuthenticateResult
			{
				IsSuccess = false,
				ErrorMessage = errorMessage
			};
		}
	}
}