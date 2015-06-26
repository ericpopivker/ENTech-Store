using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ValidatorResult
	{
		private bool _isValid;

		private Error _error;

		
		public bool IsValid { get { return _isValid; } }

		
		public Error Error { get { return _error; } }

		private ValidatorResult()
		{
		}


		public static ValidatorResult Valid()
		{
			var result = new ValidatorResult();
			result._isValid = true;

			return result;
		}


		public static ValidatorResult Invalid(List<ArgumentError> argumentErrors)
		{
			var result = new ValidatorResult();

			result._isValid = false;
			result._error= new InvalidArgumentsResponseError(argumentErrors);

			return result;
		}


		public static ValidatorResult Invalid(ArgumentError argumentError)
		{
			var result = new ValidatorResult();

			result._isValid = false;
			result._error = new InvalidArgumentsResponseError(new List<ArgumentError> {argumentError});

			return result;
		}

		public static ValidatorResult Invalid(Error responseError)
		{
			var result = new ValidatorResult();

			result._isValid = false;
			result._error = responseError;
	
			return result;
		}
	}
}