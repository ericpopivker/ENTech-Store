using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class RequestValidatorResult
	{
		private bool _isValid;
		//private ResponseError _error;
		private ArgumentErrorsCollection _argumentErrors;

		public bool IsValid { get { return _isValid; } }

		//public ResponseError Error { get { return _error; } }

		public ArgumentErrorsCollection ArgumentErrors { get { return _argumentErrors; } }

		private RequestValidatorResult()
		{
		}


		public static RequestValidatorResult Valid()
		{
			var result = new RequestValidatorResult();
			result._isValid = true;

			return result;
		}


		//public static ValidatorResult Invalid(ResponseError error)
		//{
		//	Verify.Argument.AreNotEqual(CommonErrorCode.ArgumentErrors, error.ErrorCode, "error.ErrorCode");

		//	var result = new ValidatorResult();

		//	result._isValid = false;
		//	result._error = error;

		//	return result;
		//}


		public static RequestValidatorResult Invalid(ArgumentErrorsCollection argumentErrors)
		{
			var result = new RequestValidatorResult();

			result._isValid = false;
			result._argumentErrors = argumentErrors;

			return result;
		}
	}
}
