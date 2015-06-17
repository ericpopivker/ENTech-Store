using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Extensions;
using ENTech.Store.Services.ProductModule.Requests;
using FluentValidation;

namespace ENTech.Store.Services.ProductModule.Validators
{
	public class ProductCreateRequestValidator2 : AbstractValidator<ProductCreateRequest>
	{
		public ProductCreateRequestValidator2()
		{

			// First set the cascade mode
			CascadeMode = CascadeMode.StopOnFirstFailure;

			// Rule definitions follow
			RuleFor(r => r.Product.Id).NotEqual(0);
			RuleFor(r => r.Product.Name).NotEmpty();

		}
	}
}
