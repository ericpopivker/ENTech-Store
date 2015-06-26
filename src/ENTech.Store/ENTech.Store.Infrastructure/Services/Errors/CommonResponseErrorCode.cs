using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public class CommonResponseErrorCode : ErrorCodeBase
	{
		public const int InternalServerError = 10000;

		public const int UserNotAuthorized = 10001;

		public const int UserNotAuthenticated= 10002;

		public const int InvalidToken = 10003;

		public const int InvalidArguments = 10004;
	}
}
