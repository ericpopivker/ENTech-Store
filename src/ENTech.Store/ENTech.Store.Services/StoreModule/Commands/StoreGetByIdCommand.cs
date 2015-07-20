using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : DbContextCommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IStoreQuery _query;
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;

		public StoreGetByIdCommand(IRepository<Entities.StoreModule.Store> storeRepository, IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork.DbContext, false)
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