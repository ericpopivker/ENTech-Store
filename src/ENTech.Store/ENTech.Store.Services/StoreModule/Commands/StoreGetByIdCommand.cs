using ENTech.Store.Entities.UnitOfWork;
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
		private readonly IStoreQuery _query;
		private readonly IMapper _mapper;

		public StoreGetByIdCommand(IUnitOfWork unitOfWork, IStoreQuery query, IMapper mapper)
			: base(unitOfWork.DbContext, false)
		{
			_query = query;
			_mapper = mapper;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var projection = _query.GetById(request.Id);
			
			var result = _mapper.Map<StoreProjection, StoreDto>(projection);

			return new StoreGetByIdResponse
			{
				IsSuccess = true,
				Item = result
			};
		}
	}
}