using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using FluentValidation;
using ValidationContext = FluentValidation.ValidationContext;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public abstract class DtoValidatorBase<TDto> : IDtoValidator<TDto>
	{
		private FluentDtoValidator<TDto> _fluentDtoValidator;
	
		public FluentDtoValidator<TDto> FluentValidator { get { return _fluentDtoValidator; } }

		public DtoValidatorBase()
		{
			_fluentDtoValidator = new FluentDtoValidator<TDto>();
		}

		
		IDtoValidatorResult IDtoValidator.Validate(object dto)
		{
			return Validate((TDto)dto);
		}


		public DtoValidatorResult<TDto> Validate(TDto dto)
		{
			var result = ValidateFluent(dto);
			ValidateInternal(dto, result);

			return result;
		}

		protected virtual void ValidateInternal(TDto dto  , DtoValidatorResult<TDto> dtoValidatorResult)
		{
		}

		private DtoValidatorResult<TDto> ValidateFluent(TDto dto)
		{
			var fluentValidatorResult = _fluentDtoValidator.Validate(dto);

			var result = new DtoValidatorResult<TDto>();

			var argumentErrors = new List<ResponseArgumentError>();

			foreach (var validationFailure in fluentValidatorResult.Errors)
			{
				string argumentName = validationFailure.PropertyName;
				var argumentError = FluentValidationResources.GetArgumentError(validationFailure.ErrorMessage, argumentName);

				argumentErrors.Add(new ResponseArgumentError(argumentName, argumentError));
			}

			result.ArgumentErrors = argumentErrors;
			return result;
		}


		protected IRuleBuilderInitial<TDto, TProperty> RuleFor<TProperty>(Expression<Func<TDto, TProperty>> expression)
		{
			return _fluentDtoValidator.RuleFor(expression);
		}

		protected IRuleBuilderInitial<TDto, TProperty> RuleForEach<TProperty>(Expression<Func<TDto, IEnumerable<TProperty>>> expression)
		{
			return _fluentDtoValidator.RuleForEach(expression);
		}


		protected void When(Func<TDto, bool> predicate, Action action)
		{
			_fluentDtoValidator.When(predicate, action);
		}


		protected void Unless(Func<TDto, bool> predicate, Action action) 
		{
			_fluentDtoValidator.Unless(predicate, action);
		}


		protected IArgumentName ArgumentName(Expression<Func<TDto, object>> expresison)
		{
			return ArgumentName<TDto>.For(expresison);
		}
	}


}
