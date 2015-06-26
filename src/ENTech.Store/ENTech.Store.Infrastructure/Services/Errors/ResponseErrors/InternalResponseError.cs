using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Errors.ResponseErrors
{
	public class InternalResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "Internal server error";

		private string _debugInfo;
		
		public InternalResponseError()
			: base(CommonResponseErrorCode.InternalServerError)
		{
		}

		public InternalResponseError(string debugInfo) : this()
		{
			_debugInfo = debugInfo;
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}


		public override string ErrorMessage
		{
			get
			{
				string errorMessage = _errorMessageTemplate;
				if (_debugInfo != null)
					errorMessage = errorMessage + ". " + _debugInfo;
				return errorMessage; 
			}
		}
	}
}
