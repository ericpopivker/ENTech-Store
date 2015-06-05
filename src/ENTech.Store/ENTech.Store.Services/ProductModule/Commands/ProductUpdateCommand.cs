using ENTech.Store.Entities;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductUpdateCommand : DbContextCommandBase<ProductUpdateRequest, ProductUpdateResponse>
	{
		public ProductUpdateCommand(IDbContext dbContext, bool requiresTransaction) : base(dbContext, requiresTransaction)
		{
		}

		public override ProductUpdateResponse Execute(ProductUpdateRequest request)
		{
			return new ProductUpdateResponse
			{
				IsSuccess = true
			};
		}
	}
}