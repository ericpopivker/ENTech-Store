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


		//public ovveride ValidateInternal()
		//{
		//	_storeValidator.Exists(dto => dto.StoreId, dto.StoreId);
		
		//	_storeValidator.NotDeleted();
		
		//	if(!DtoValidatorResult.HasArgumentError(dto => dto.Name)) &&
		//		!DtoValidatorResult.HasArgumentError(dto => dto.StoreId))

		//		_productValidator.NameIsUnique(dto => dto.PropertyName,  dto.Name, dto.StoreId);
		
		//}
	}
}
