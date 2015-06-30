using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Dtos;
using FluentValidation;

namespace ENTech.Store.Services.ProductModule.Validators.DtoValidators
{
	public class ProductCreateDtoValidator : DtoValidatorBase<ProductCreateDto>
	{
		public ProductCreateDtoValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty();
		}
	}
}
