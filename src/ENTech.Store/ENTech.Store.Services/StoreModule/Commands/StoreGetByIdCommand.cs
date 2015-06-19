using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Projections;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : DbContextCommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IQueryExecuter _queryExecuter;
		private readonly IMapper _mapper;

		public StoreGetByIdCommand(IUnitOfWork unitOfWork, IQueryExecuter queryExecuter, IMapper mapper)
			: base(unitOfWork.DbContext, false)
		{
			_queryExecuter = queryExecuter;
			_mapper = mapper;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var criteria = new GetByIdCriteria<StoreProjection>(request.Id);

			var projection = _queryExecuter.Execute(criteria);
			
			var result = _mapper.Map<StoreProjection, StoreDto>(projection);

			return new StoreGetByIdResponse
			{
				IsSuccess = true,
				Item = result
			};
		}
	}
}