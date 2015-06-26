using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors
{
	public class PositiveIntArgumentError : ArgumentError
	{
		private const string _errorMessageTemplate = "Value should be positive integer";

		public PositiveIntArgumentError(string argumentName)
			: base(argumentName, CommonArgumentErrorCode.PositiveInt)
		{
		}

		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
