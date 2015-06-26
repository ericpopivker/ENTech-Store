using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Errors.ResponseErrors
{
	public class UserNotAuthenticatedResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "User is not authenticated";

		public UserNotAuthenticatedResponseError()
			: base(CommonResponseErrorCode.UserNotAuthenticated)
		{
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
