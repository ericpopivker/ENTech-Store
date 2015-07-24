using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ValidateArgumentResult : IValidatorResult
	{
		private ArgumentError _argumentError;

		
		public bool IsValid { get { return _argumentError == null; } }


		public ArgumentError ArgumentError { get { return _argumentError; } }

		private ValidateArgumentResult()
		{
		}


		public static ValidateArgumentResult Valid()
		{
			var result = new ValidateArgumentResult();

			result._argumentError = null;

			return result;
		}


		public static ValidateArgumentResult Invalid(ArgumentError error)
		{
			var result = new ValidateArgumentResult();

			result._argumentError = error;
	
			return result;
		}
	}
}