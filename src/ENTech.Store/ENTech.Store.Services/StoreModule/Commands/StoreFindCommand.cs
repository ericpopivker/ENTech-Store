using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : DbContextCommandBase<StoreFindRequest, StoreFindResponse>
	{
		private readonly IStoreQueryExecuter _queryExecuter;
		private readonly IMapper _mapper;

		public StoreFindCommand(IStoreQueryExecuter queryExecuter, IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork.DbContext, false)
		{
			_queryExecuter = queryExecuter;
			_mapper = mapper;
		}

		public override StoreFindResponse Execute(StoreFindRequest request)
		{
			var criteria = _mapper.Map<StoreFindCriteriaDto, StoreFindCriteria>(request.Criteria);

			var result = _queryExecuter.Find(criteria);

			return new StoreFindResponse
			{
				Items = result.Select(x=>new StoreDto()).ToList(),
				IsSuccess = true
			};
		}
	}
}