using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.AuthenticationModule.ModuleLogic
{
	public class AuthenticationErrorCode : ErrorCodeBase
	{
		[StringValue("Invalid Api Key")]
		public const int InvalidApiKey = 60001;
	}
}