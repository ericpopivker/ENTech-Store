using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidatorResult
	{
		public Boolean IsValid { get { return ArgumentErrors == null || ArgumentErrors.Count == 0; }}

		public List<ArgumentError> ArgumentErrors { get; internal set; } 
	}
}