using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services
{
	public class CommonArgumentErrorCode : ErrorCodeBase
	{
		[StringValue("Required")]
		public const int Required = 20000;
	}
}
