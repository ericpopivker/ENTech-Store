using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreDeleteCommand : DbContextCommandBase<StoreDeleteRequest, StoreDeleteResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _repository;

		public StoreDeleteCommand(IUnitOfWork unitOfWork, IRepository<Entities.StoreModule.Store> repository)
			: base(unitOfWork.DbContext, false)
		{
			_repository = repository;
		}

		public override StoreDeleteResponse Execute(StoreDeleteRequest request)
		{
			return new StoreDeleteResponse
			{
				IsSuccess = true
			};
		}
	}
}