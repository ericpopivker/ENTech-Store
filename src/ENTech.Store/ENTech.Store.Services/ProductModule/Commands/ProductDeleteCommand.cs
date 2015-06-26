using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductDeleteCommand : DbContextCommandBase<ProductDeleteRequest, ProductDeleteResponse>
	{
		public ProductDeleteCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override ProductDeleteResponse Execute(ProductDeleteRequest request)
		{
			return new ProductDeleteResponse();
		}
	}
}