using System.Linq;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.AuthenticationModule.Dtos;
using ENTech.Store.Services.AuthenticationModule.ModuleLogic;
using ENTech.Store.Services.AuthenticationModule.Responses;

namespace ENTech.Store.Services.AuthenticationModule.Commands
{
	public class AuthenticateApiKeyCommand<TRequest> : CommandBase<TRequest, AuthenticateApiKeyResponse> where TRequest : IInternalRequest
	{
		private readonly IRepository<Partner> _partnerRepository;
		private readonly IPartnerQuery _partnerQuery;
		private readonly IMapper _mapper;

		public AuthenticateApiKeyCommand(IRepository<Partner> partneRepository, IMapper mapper, IPartnerQuery partnerQuery)
			: base(false)
		{
			_partnerRepository = partneRepository;
			_mapper = mapper;
			_partnerQuery = partnerQuery;
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

			var partnerId = _partnerQuery.GetByApiKey(apiKey);

			if (partnerId.HasValue == false)
				return new AuthenticateApiKeyResponse
				{
					IsSuccess = false,
					Error = new Error(AuthenticationErrorCode.InvalidApiKey, "Not authorized")
				};

			var partner = _partnerRepository.GetById(partnerId.Value);

			return new AuthenticateApiKeyResponse
			{
				IsSuccess = true,
				Partner = _mapper.Map<Partner, PartnerDto>(partner)
			};
		}
	}
}