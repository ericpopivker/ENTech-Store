using ENTech.Store.Services.External.ForPartner.PartnerModule.Commands;
using ENTech.Store.Services.External.ForPartner.PartnerModule.Requests;
using ENTech.Store.Services.External.ForPartner.PartnerModule.Responses;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule
{
	public class PartnerService
	{
		public PartnerAuthenticateResponse Authenticate(PartnerAuthenticateRequest request)
		{
			var factory = new ExternalCommandFactory();
			var command = factory.Create<PartnerAuthenticateCommand>();
			return command.Execute(request);
		}
	}
}
