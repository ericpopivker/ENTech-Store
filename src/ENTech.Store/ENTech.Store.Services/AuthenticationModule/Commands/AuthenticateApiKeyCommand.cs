using System.Linq;
using AutoMapper;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.AuthenticationModule.Dtos;
using ENTech.Store.Services.AuthenticationModule.ModuleLogic;
using ENTech.Store.Services.AuthenticationModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.AuthenticationModule.Commands
{
	public class AuthenticateApiKeyCommand<TRequest> : DbContextCommandBase<TRequest, AuthenticateApiKeyResponse> 
		where TRequest : IInternalRequest
	{
		public AuthenticateApiKeyCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override AuthenticateApiKeyResponse Execute(TRequest request)
		{
			var apiKey = request.ApiKey;
			if (string.IsNullOrEmpty(apiKey))
			{
					return new AuthenticateApiKeyResponse
					{
						IsSuccess = false,
						Error = new Error(AuthenticationErrorCode.InvalidApiKey, "Api Key not provided")
					};
			}

			var partner =  DbContext.Partners.FirstOrDefault(x => x.Key == apiKey);
			if (partner == null)
				return new AuthenticateApiKeyResponse
				{
					IsSuccess = false,
					Error = new Error(AuthenticationErrorCode.InvalidApiKey, "Not authorized")
				};

			return new AuthenticateApiKeyResponse
			{
				IsSuccess = true,
				Partner = Mapper.Map<PartnerDto>(partner)
			};
		}
	}
}