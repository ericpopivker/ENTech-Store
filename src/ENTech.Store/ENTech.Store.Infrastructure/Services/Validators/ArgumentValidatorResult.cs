using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ArgumentValidatorResult
	{
		private bool _isValid;

		private ArgumentError _error;

		
		public bool IsValid { get { return _isValid; } }


		public ArgumentError Error { get { return _error; } }

		private ArgumentValidatorResult()
		{
		}


		public static ArgumentValidatorResult Valid()
		{
			var result = new ArgumentValidatorResult();
			result._isValid = true;

			return result;
		}


		public static ArgumentValidatorResult Invalid(ArgumentError error)
		{
			var result = new ArgumentValidatorResult();

			result._isValid = false;
			result._error = error;
	
			return result;
		}
	}
}