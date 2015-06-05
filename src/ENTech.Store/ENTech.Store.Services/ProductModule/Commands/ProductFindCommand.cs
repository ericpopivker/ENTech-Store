using System.Linq;
using ENTech.Store.Entities;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductFindCommand : DbContextCommandBase<ProductFindRequest, ProductFindResponse>
	{
		public ProductFindCommand(IDbContext dbContext, bool requiresTransaction) : base(dbContext, requiresTransaction)
		{
		}

		public override ProductFindResponse Execute(ProductFindRequest request)
		{
			return new ProductFindResponse {IsSuccess = true, Items = Enumerable.Empty<ProductDto>()};
		}
	}
}