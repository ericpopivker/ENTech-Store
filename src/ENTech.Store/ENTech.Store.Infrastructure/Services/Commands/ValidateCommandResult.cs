using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ValidateCommandResult : IValidatorResult
	{
		private ResponseError _responseError;

		
		public bool IsValid { get { return _responseError == null; } }

		
		public ResponseError ResponseError { get { return _responseError; } }

		private ValidateCommandResult()
		{
		}


		public static ValidateCommandResult Valid()
		{
			var result = new ValidateCommandResult();
			result._responseError = null;
			return result;
		}


		public static ValidateCommandResult Invalid(ResponseError responseError)
		{
			var result = new ValidateCommandResult();
			result._responseError = responseError;
			return result;
		}
	}
}