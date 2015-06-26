﻿using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Errors.ResponseErrors
{
	public class InvalidArgumentsResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "Invalid arguments";

		public List<ArgumentError> ArgumentErrors {get; set; } 

		public InvalidArgumentsResponseError() 
				: base(CommonResponseErrorCode.InvalidArguments)
		{
			ArgumentErrors= new List<ArgumentError>();
		}

		public InvalidArgumentsResponseError(IEnumerable<ArgumentError> argumentErrors) : this()
		{
			ArgumentErrors.AddRange(argumentErrors);
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
		
	}
}
