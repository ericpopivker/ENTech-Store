using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.AuthenticationModule.Errors
{
	public class AuthenticationResponseErrorCode : ErrorCodeBase
	{
		public const int ApiKeyRequired = 60001;

		public const int ApiKeyInvalid = 60002;
	}
}