using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;
using FluentValidation.Results;

namespace ENTech.Store.Services.ProductModule.Validators.DtoValidators
{
	public static class FluentValidationResources
	{
		//email_error
		//equal_error
		//exact_length_error
		//exclusivebetween_error
		//greaterthan_error
		//greaterthanorequal_error
		//inclusivebetween_error
		//length_error
		//lessthan_error
		//lessthanorequal_error
		//notempty_error				DONE
		//notequal_error
		//notnull_error
		//predicate_error
		//regex_error

		public static string notempty_error
		{
			get
			{
				return CommonArgumentErrorCode.Required.ToString();
			}
		}

		public static string notnull_error
		{
			get
			{
				return CommonArgumentErrorCode.Required.ToString();
			}
		}

		public static ArgumentError GetArgumentError(string fluentValidationFailureErrorMessage, string argumentName)
		{
			string[] vals = fluentValidationFailureErrorMessage.Split(new[] { ',' });
			int errorCode;

			if (!Int32.TryParse(vals[0], out errorCode))
			{
				throw new ArgumentOutOfRangeException(vals[0] + " is not supported.");
			}

			switch (errorCode)
			{
				case CommonArgumentErrorCode.Required:
					return new RequiredArgumentError();
				default:
					throw new ArgumentOutOfRangeException(vals[0] + " is not supported.");
			}

		}
		
	}
}
