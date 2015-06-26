using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	
	public sealed class CommonArgumentErrorCode : ErrorCodeBase
	{
		public const int Required = 100001;

		public const int PositiveInt = 100002;
	}
}