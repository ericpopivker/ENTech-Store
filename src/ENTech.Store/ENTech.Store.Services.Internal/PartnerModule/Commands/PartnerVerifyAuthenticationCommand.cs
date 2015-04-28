using ENTech.Store.Entities;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.Internal.PartnerModule.Dtos;
using ENTech.Store.Services.Internal.PartnerModule.Requests;
using ENTech.Store.Services.Internal.PartnerModule.Responses;
using ENTech.Store.Services.Internal.SharedModule;

namespace ENTech.Store.Services.Internal.PartnerModule.Commands
{
	public class PartnerVerifyAuthenticationCommand :
		InternalCommandBase
			<PartnerVerifyAuthenticationRequest, PartnerVerifyAuthenticationResponse>
	{


		private readonly IUnitOfWork _unitOfWork;
		
		private readonly ITokenService _tokenService;

		public PartnerVerifyAuthenticationCommand(IUnitOfWork unitOfWork, ITokenService tokenService)
		{
			_unitOfWork = unitOfWork;
			_tokenService = tokenService;
		}

		public override void ValidateRequestCustom()
		{
		}

		protected override void Execute()
		{
			var authData = _tokenService.GetByToken<PartnerAuthenticationDto>(Request.PartnerToken);

			if (authData == null)
			{
				FillError();
				return;
			}

			//var partner = DbContext.Partners.SingleOrDefault(p => p.Id == authData.PartnerId);

			//if (partner == null)
			//{
			//	FillError();
			//	return;
			//}

			Response.PartnerId = authData.PartnerId;
		}

		private void FillError()
		{
			Response.IsSuccess = false;
			Response.ArgumentErrors["PartnerToken"] = new Error(RequestValidatorErrorCode.InvalidToken,
																RequestValidatorErrorMessagesDictionary.Instance[RequestValidatorErrorCode.InvalidToken]);
			Response.Error = Response.ArgumentErrors["PartnerToken"];
		}
	}
}
