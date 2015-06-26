using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Errors.ResponseErrors
{
	public class UserUserNotAuthorizedResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "User is not authorized";

		public UserUserNotAuthorizedResponseError()
			: base(CommonResponseErrorCode.UserNotAuthorized)
		{
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
