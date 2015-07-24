using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductDeleteCommand : CommandBase<ProductDeleteRequest, ProductDeleteResponse>
	{
		public ProductDeleteCommand(IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
		}

		public override ProductDeleteResponse Execute(ProductDeleteRequest request)
		{
			return new ProductDeleteResponse();
		}
	}
}