using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Criterias;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Projections;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreFindCommand : DbContextCommandBase<StoreFindRequest, StoreFindResponse>
	{
		private readonly IStoreQuery _query;
		private readonly IMapper _mapper;

		public StoreFindCommand(IStoreQuery query, IUnitOfWork unitOfWork, IMapper mapper)
			: base(unitOfWork.DbContext, false)
		{
			_query = query;
			_mapper = mapper;
		}

		public override StoreFindResponse Execute(StoreFindRequest request)
		{
			var criteria = _mapper.Map<StoreFindCriteriaDto, StoreFindCriteria>(request.Criteria);

			var result = _query.Find(criteria);

			var mappedResult = _mapper.Map<IEnumerable<StoreProjection>, IEnumerable<StoreDto>>(result);

			return new StoreFindResponse
			{
				Items = mappedResult.ToList(),
				IsSuccess = true
			};
		}
	}
}