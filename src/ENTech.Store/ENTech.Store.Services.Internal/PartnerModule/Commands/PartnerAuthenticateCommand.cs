using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.Internal.PartnerModule.Requests;
using ENTech.Store.Services.Internal.PartnerModule.Responses;
using ENTech.Store.Services.Internal.SharedModule;

namespace ENTech.Store.Services.Internal.PartnerModule.Commands
{
	class PartnerAuthenticateCommand : 
			InternalCommandBase<PartnerAuthenticateRequest, PartnerAuthenticateResponse>
	{
		private readonly IUnitOfWork _unitOfWork;
		
		private readonly ITokenService _tokenService;

		public PartnerAuthenticateCommand(IUnitOfWork unitOfWork, ITokenService tokenService)
		{
			_unitOfWork = unitOfWork;
			_tokenService = tokenService;
		}

		public override void ValidateRequestCustom()
		{
			//Validator.ValidatePartner(DbContext, Request, Response.ArgumentErrors);
		}

		protected override void Execute()
		{
			//var partner = DbContext.Partners.Single(p => p.Key == Request.Key && p.Secret == Request.Secret);

			//var data = new PartnerAuthenticationDto {PartnerId = partner.Id};

			//var token = _tokenService.Create(data);

			//Response.Token = token.Token;
			//Response.ExpireAt = token.ExpireAt;
		}
	}
}
