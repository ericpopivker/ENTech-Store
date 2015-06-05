using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand : DbContextCommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		public ProductCreateCommand(IUnitOfWork unitOfWork) : base(unitOfWork.DbContext, false)
		{
		}

		public override ProductCreateResponse Execute(ProductCreateRequest request)
		{
			return new ProductCreateResponse
			{
				IsSuccess = true
			};
		}
	}
}