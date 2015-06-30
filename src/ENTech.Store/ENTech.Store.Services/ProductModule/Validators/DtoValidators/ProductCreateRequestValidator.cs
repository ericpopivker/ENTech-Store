using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using FluentValidation;

namespace ENTech.Store.Services.ProductModule.Validators.DtoValidators
{
	public class ProductCreateRequestValidator : DtoValidatorBase<ProductCreateRequest>
	{
		public ProductCreateRequestValidator()
		{
			RuleFor(dto => dto.Product).NotNull();
		}
	}
}
