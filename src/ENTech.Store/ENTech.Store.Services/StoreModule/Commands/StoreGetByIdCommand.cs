using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : CommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IStoreQuery _query;
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;

		public StoreGetByIdCommand(IRepository<Entities.StoreModule.Store> storeRepository, IMapper mapper)
			: base(false)
		{
			_storeRepository = storeRepository;
			_mapper = mapper;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var entity = _storeRepository.GetById(request.Id);
			
			var result = _mapper.Map<Entities.StoreModule.Store, StoreDto>(entity);

			return new StoreGetByIdResponse
			{
				IsSuccess = true,
				Item = result
			};
		}
	}
}