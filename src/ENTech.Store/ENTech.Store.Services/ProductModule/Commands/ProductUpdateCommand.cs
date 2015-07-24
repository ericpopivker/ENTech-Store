using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductUpdateCommand : CommandBase<ProductUpdateRequest, ProductUpdateResponse>
	{
		public ProductUpdateCommand(IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
		}

		public override ProductUpdateResponse Execute(ProductUpdateRequest request)
		{
			return new ProductUpdateResponse();
		}
	}
}