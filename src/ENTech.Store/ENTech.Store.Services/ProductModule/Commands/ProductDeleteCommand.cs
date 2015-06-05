using ENTech.Store.Entities;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductDeleteCommand : DbContextCommandBase<ProductDeleteRequest, ProductDeleteResponse>
	{
		public ProductDeleteCommand(IDbContext dbContext, bool requiresTransaction)
			: base(dbContext, requiresTransaction)
		{
		}

		public override ProductDeleteResponse Execute(ProductDeleteRequest request)
		{
			return new ProductDeleteResponse
			{
				IsSuccess = true
			};
		}
	}
}