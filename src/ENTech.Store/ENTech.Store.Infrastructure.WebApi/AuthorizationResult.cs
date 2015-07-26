namespace ENTech.Store.Infrastructure.WebApi
{
	public class AuthorizationResult
	{
		public bool IsSuccessful { get; private set; }
		public IAuthorizationCredential Credential { get; private set; }

		private AuthorizationResult()
		{
			
		}

		public static AuthorizationResult Success(IAuthorizationCredential credential = null)
		{
			return new AuthorizationResult
			{
				IsSuccessful = true,
				Credential = credential
			};
		}

		public static AuthorizationResult Failure()
		{
			return new AuthorizationResult();
		}
	}
}