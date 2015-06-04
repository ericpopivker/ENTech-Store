using ENTech.Store.Entities;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.CommandService.Concrete
{
	public class PublicExternalCommandService : ExternalCommandService<AnonymousSecurityInformation>
	{
		public PublicExternalCommandService(ICommandFactory commandFactory) : base(commandFactory)
		{
		}

		protected override void LimitDbContext(SecureRequestBase<AnonymousSecurityInformation> request, IDbContext dbContext)
		{
			//
		}
	}
}