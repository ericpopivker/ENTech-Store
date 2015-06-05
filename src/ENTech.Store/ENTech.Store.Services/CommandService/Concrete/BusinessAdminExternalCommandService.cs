using ENTech.Store.Entities;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.CommandService.Concrete
{
	public class BusinessAdminExternalCommandService : ExternalCommandService<BusinessAdminSecurityInformation>
	{
		public BusinessAdminExternalCommandService(ICommandFactory commandFactory)
			: base(commandFactory)
		{
		}

		protected override void LimitDbContext(SecureRequestBase<BusinessAdminSecurityInformation> request, IDbContext dbContext)
		{
			//
		}
	}
}