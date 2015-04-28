using System.Linq;
using ENTech.Store.Core;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.PartnerModule.Requests;

namespace ENTech.Store.Services.Internal.PartnerModule.Validators
{
	public class PartnerRequestCustomValidator : RequestCustomValidatorBase<PartnerRequestCustomValidatorErrorCode>
	{
		public virtual void ValidatePartner(IDbContext dbContext, PartnerAuthenticateRequest request,
											ArgumentErrorsCollection errors)
		{
			var action = new RequestValidatorAction<PartnerAuthenticateRequest, string>(r => r.Key,
																						(key) => IsPartnerExists(dbContext, key) && IsPartnerSecretMatches(dbContext, key, request.Secret),
																						PartnerRequestCustomValidatorErrorCode.PartnerNotFound);
			TryValidate(action, request, errors);
		}

		private bool IsPartnerSecretMatches(IDbContext dbContext, string key, string secret)
		{
			var keyMatches = dbContext.Partners.Count(s => s.Key == key && s.Secret == secret)==1;

			return keyMatches;
		}

		private bool IsPartnerExists(IDbContext dbContext, string key)
		{
			var partnerExists = dbContext.Partners.Count(s => s.Key == key)==1;

			return partnerExists;
		}
	}
}
