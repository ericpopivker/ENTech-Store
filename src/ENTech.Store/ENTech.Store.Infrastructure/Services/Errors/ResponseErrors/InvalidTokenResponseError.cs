using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Errors.ResponseErrors
{
	public class InvalidTokenResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "Invalid token";

		public InvalidTokenResponseError()
			: base(CommonResponseErrorCode.InvalidToken)
		{
		}
		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
