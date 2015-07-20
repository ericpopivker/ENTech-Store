using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductUpdateCommand : CommandBase<ProductUpdateRequest, ProductUpdateResponse>
	{
		public ProductUpdateCommand()
			: base(false)
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