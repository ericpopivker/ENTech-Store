using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public class ValidateRequestResult : IValidatorResult
	{

		private List<ArgumentError> _argumentErrors;

		
		public bool IsValid { get { return _argumentErrors.Count == 0; } }


		public List<ArgumentError> ArgumentErrors { get { return _argumentErrors; } }

		public ValidateRequestResult()
		{
			_argumentErrors= new List<ArgumentError>();
		}
	}
}