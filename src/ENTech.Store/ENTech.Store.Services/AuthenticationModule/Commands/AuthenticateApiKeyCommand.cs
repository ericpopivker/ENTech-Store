using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.AuthenticationModule.Dtos;
using ENTech.Store.Services.AuthenticationModule.Errors.ResponseErrors;
using ENTech.Store.Services.AuthenticationModule.Responses;

namespace ENTech.Store.Services.AuthenticationModule.Commands
{
	public class AuthenticateApiKeyCommand<TRequest> : CommandBase<TRequest, AuthenticateApiKeyResponse> where TRequest : IRequest
	{
		private readonly IRepository<Partner> _partnerRepository;
		private readonly IPartnerQuery _partnerQuery;
		private readonly IMapper _mapper;

		public AuthenticateApiKeyCommand(IRepository<Partner> partnerRepository, IMapper mapper, IPartnerQuery partnerQuery, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_partnerRepository = partnerRepository;
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
					IsAuthenticated = false,
					Error = new ApiKeyRequiredResponseError()
				};
			}

			var partnerId = _partnerQuery.GetByApiKey(apiKey);

			if (partnerId.HasValue == false)
				return new AuthenticateApiKeyResponse
				{
					IsAuthenticated = false,
					Error = new ApiKeyInvalidResponseError()
				};

			var partner = _partnerRepository.GetById(partnerId.Value);

			return new AuthenticateApiKeyResponse
			{
				Partner = _mapper.Map<Partner, PartnerDto>(partner),
				IsAuthenticated = true
			};
		}
	}
}