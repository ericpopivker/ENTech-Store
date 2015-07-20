using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductDeleteCommand : CommandBase<ProductDeleteRequest, ProductDeleteResponse>
	{
		public ProductDeleteCommand()
			: base(false)
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