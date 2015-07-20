using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : CommandBase<StoreFindRequest, StoreFindResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IStoreQuery _query;
		private readonly IMapper _mapper;

		public StoreFindCommand(IRepository<Entities.StoreModule.Store> storeRepository, IStoreQuery query, IMapper mapper)
			: base(false)
		{
			_storeRepository = storeRepository;
			_query = query;
			_mapper = mapper;
		}

		public override StoreFindResponse Execute(StoreFindRequest request)
		{
			var criteria = _mapper.Map<StoreFindCriteriaDto, StoreFindCriteria>(request.Criteria);

			var itemIds = _query.Find(criteria);

			var items = _storeRepository.FindByIds(itemIds); //expand == projection?

			var mappedResult = _mapper.Map<IEnumerable<Entities.StoreModule.Store>, IEnumerable<StoreDto>>(items);

			return new StoreFindResponse
			{
				Items = mappedResult.ToList(),
				IsSuccess = true
			};
		}
	}
}