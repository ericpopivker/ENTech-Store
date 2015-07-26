using System;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductFindCommand : CommandBase<ProductFindRequest, ProductFindResponse>
	{
		public ProductFindCommand(IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
		}

		public override ProductFindResponse Execute(ProductFindRequest request)
		{
			throw new NotImplementedException();
		}
	}
}