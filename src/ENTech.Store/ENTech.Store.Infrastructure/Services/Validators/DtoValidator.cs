using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidator
	{
		public static bool VisitAndValidateProperties(object dto, ArgumentErrorsCollection errors, string rootName = null)
		{
			var valid = true;

			var validationResults = new List<ValidationResult>();

			var validationContext = new ValidationContext(dto, serviceProvider: null, items: null);

			valid = Validator.TryValidateObject(dto, validationContext, validationResults, true);
			//check if o is collection
			if (dto.GetType().GetInterface("IEnumerable") != null)
			{
				var enumerator = ((IEnumerable)dto).GetEnumerator();
				while (enumerator.MoveNext())
				{
					valid = VisitAndValidateProperties(enumerator.Current, errors, rootName) && valid;
				}
			}
			else
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
						if (val != null)
						{
							valid = VisitAndValidateProperties(val, errors, (rootName != null ? rootName + "." : "") + propertyInfo.Name) && valid;
						}
					}
				}
			}

			foreach (var validationResult in validationResults)
			{
				foreach (var memberName in validationResult.MemberNames)
				{
					errors[(rootName != null ? rootName + "." : "") + memberName] =
						new Error(RequestValidatorErrorCode.DtoValidationFailed, validationResult.ErrorMessage);
				}
			}

			return valid;
		}
	}
}
