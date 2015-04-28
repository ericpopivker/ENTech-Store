using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.Internal.PartnerModule.Validators
{
	public class PartnerRequestCustomValidatorErrorCode : ErrorCodeBase
	{
		[StringValue("Partner not found.")]
		public const int PartnerNotFound = 11000;
	}
}