using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Services.ProductModule.Validators.DtoValidators;
using FluentValidation;
using ValidationContext = FluentValidation.ValidationContext;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidatoResult
	{
		public Boolean IsValid { get; internal set; }

		public List<ArgumentError> ArgumentErrors { get; internal set; } 
	}


	public abstract class DtoValidatorBase<TDto> : IDtoValidator<TDto>
	{
		public class FluentDtoValidator<TDto> : AbstractValidator<TDto>
		{
		}

		private FluentDtoValidator<TDto> _fluentDtoValidator;
		private DtoValidatorFactory _dtoValidatorFactory;

		public FluentDtoValidator<TDto> FluentValidator { get { return _fluentDtoValidator; } }

		public DtoValidatorBase()
		{
			_fluentDtoValidator = new FluentDtoValidator<TDto>();
			_dtoValidatorFactory = new DtoValidatorFactory();
		}

		
		DtoValidatoResult IDtoValidator.Validate(object dto, string propertyParentPath = null)
		{
			return Validate((TDto)dto, propertyParentPath);
		}


		public virtual DtoValidatoResult Validate(TDto dto, string propertyParentPath=null)
		{
			var fluentValidatorResult = _fluentDtoValidator.Validate(dto);
			var result = new DtoValidatoResult();

			var argumentErrors = new List<ArgumentError>();
			
			foreach (var validationFailure in fluentValidatorResult.Errors)
			{
				string argumentName = propertyParentPath != null ? propertyParentPath + "." + validationFailure.PropertyName : validationFailure.PropertyName;
				var argumentError = FluentValidationResources.GetArgumentError(validationFailure.ErrorMessage, argumentName);
				argumentErrors.Add(argumentError);
			}

			VisitAndValidateDependents(dto, argumentErrors, propertyParentPath);

			if (argumentErrors.Count > 0)
			{
				result.IsValid = false;
				result.ArgumentErrors = argumentErrors;
			}
			else
			{
				result.IsValid = true;
			}

			return result;
		}



		public void VisitAndValidateDependents(object dto, List<ArgumentError> argumentErrors, string propertyParentPath = null)
		{
			
			foreach (var propertyInfo in dto.GetType().GetProperties())
			{
				if (!(propertyInfo.PropertyType.IsPrimitive ||
					propertyInfo.PropertyType == typeof(String) ||
					propertyInfo.PropertyType == typeof(Decimal) ||
					propertyInfo.PropertyType == typeof(Decimal?) ||
					propertyInfo.PropertyType == typeof(DateTime) ||
					propertyInfo.PropertyType == typeof(DateTime?)))
				{
					var val = propertyInfo.GetValue(dto);
					var propertyName = propertyInfo.Name;
					var propertyPath = (propertyParentPath != null ? propertyParentPath + "." : "") + propertyName;

					if (val != null)
					{
						IDtoValidator dtoValidator = null;
							
						if (val.GetType().GetInterface("IEnumerable") != null)
						{
							var enumerator = ((IEnumerable)dto).GetEnumerator();
							int index = 0;
							while (enumerator.MoveNext())
							{
								if (index == 0)
								{
									dtoValidator = _dtoValidatorFactory.TryCreate(enumerator.Current.GetType());
									if (dtoValidator == null)  
										break; //For now assume that types in Enumrable are all of same type
								}
								
								propertyPath += "[" + index + "]";
								var result = dtoValidator.Validate(enumerator.Current, propertyPath);
								if (!result.IsValid)
									argumentErrors.AddRange(result.ArgumentErrors);

								index++;
							}
						}
						else
						{
								dtoValidator = _dtoValidatorFactory.TryCreate(val.GetType());

								if (dtoValidator != null)
								{
									var result = dtoValidator.Validate(val, propertyPath);

									if (!result.IsValid)
										argumentErrors.AddRange(result.ArgumentErrors);
								}
							
						}
					}
				}
			}
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

	
	}


}
