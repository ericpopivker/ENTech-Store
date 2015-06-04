using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services
{
	public class CommonErrorCode : ErrorCodeBase
	{
		[StringValue("Internal server error")]
		public const int InternalServerError = 10000;

		[StringValue("User is not authorized to perform this action")]
		public const int UserNotAuthorized = 10001;

		[StringValue("User is not authenticated")]
		public const int UserNotAuthenticated= 10002;

		[StringValue("Token is not correct")]
		public const int InvalidToken = 10003;

		[StringValue("Invalid arguments")]
		public const int ArgumentErrors = 10004;
	}
}
