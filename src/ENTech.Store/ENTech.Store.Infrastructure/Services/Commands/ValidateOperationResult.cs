using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Errors.ResponseErrors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ValidateOperationResult : IValidatorResult
	{
		private ResponseError _responseError;
		
		public bool IsValid { get { return _responseError == null; } }
		
		public ResponseError ResponseError { get { return _responseError; } }


		private ValidateOperationResult()
		{
		}


		public static ValidateOperationResult Valid()
		{
			var result = new ValidateOperationResult();
			result._responseError = null;

			return result;
		}


		public static ValidateOperationResult Invalid(ResponseError responseError)
		{
			Verify.Argument.IsFalse(responseError is InvalidArgumentsResponseError, "responseError");

			var result = new ValidateOperationResult();

			result._responseError = responseError;
	
			return result;
		}
	}
}