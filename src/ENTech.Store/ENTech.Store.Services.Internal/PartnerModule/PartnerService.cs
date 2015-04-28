using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Factories;
using ENTech.Store.Services.Internal.PartnerModule.Commands;
using ENTech.Store.Services.Internal.PartnerModule.Requests;
using ENTech.Store.Services.Internal.PartnerModule.Responses;

namespace ENTech.Store.Services.Internal.PartnerModule
{
	public class PartnerService : IPartnerService
	{
		public PartnerAuthenticateResponse Authenticate(PartnerAuthenticateRequest request)
		{
			var factory = new InternalCommandFactory();
			var command = factory.Create<PartnerAuthenticateCommand>();
			var response = command.Execute(request);
			return response;
		}

		public PartnerVerifyAuthenticationResponse VerifyAuthentication(PartnerVerifyAuthenticationRequest request)
		{
			var command = IoC.Resolve<PartnerVerifyAuthenticationCommand>();
			return command.Execute(request);
		}
	}
}