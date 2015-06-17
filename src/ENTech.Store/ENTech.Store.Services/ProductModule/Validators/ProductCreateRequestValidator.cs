using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Extensions;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;

namespace ENTech.Store.Services.ProductModule.Validators
{
	public class ProductCreateRequestValidator : RequestValidator<ProductCreateRequest>
	{

		public ProductCreateRequestValidator(ProductCreateRequest request) : base(request) 
		{
			//Add(r => p.Product, _validatorFactory.Create<RequiredValidator>());

			//if (request.Product != null)
			//{
			//	Add(p => request.Product.Name, _validatorFactory.Create<RequiredValidator>());
			//}
		
		}
	}
}
