using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidator
	{
		public static bool VisitAndValidateProperties(object dto, List<ArgumentError> argumentErrors, string rootName = null)
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
					valid = VisitAndValidateProperties(enumerator.Current, argumentErrors, rootName) && valid;
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
							valid = VisitAndValidateProperties(val, argumentErrors, (rootName != null ? rootName + "." : "") + propertyInfo.Name) && valid;
						}
					}
				}
			}

			foreach (var validationResult in validationResults)
			{
				foreach (var memberName in validationResult.MemberNames)
				{
					string argumentPath = (rootName != null ? rootName + "." : "") + memberName;
					ArgumentError argumentError = CreateArgumentError(argumentPath, validationResult.ErrorMessage);
					argumentErrors.Add(argumentError);
				}
			}

			return valid;
		}


		// Full list is here
		// http://stackoverflow.com/questions/6121650/where-is-the-whole-list-of-default-error-messages-for-dataannotations-at-mvc-3
		private const string DAErrorMessage_Required = "The .* field is required.";
		
		private static ArgumentError CreateArgumentError(string argumentName, string daErrorMessage)
		{
			ArgumentError argumentError;
			if (Regex.IsMatch(daErrorMessage, DAErrorMessage_Required))
				argumentError =new RequiredArgumentError();
			else
				throw new ArgumentOutOfRangeException("daErrorMessage", "Data Annotations Error Match not found: " + daErrorMessage);

			return argumentError;

		}
	}
}
