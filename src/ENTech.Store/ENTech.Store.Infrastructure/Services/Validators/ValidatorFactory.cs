using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ValidatorFactory : IValidatorFactory
	{
		public TValidator Create<TValidator>()
									 where TValidator : new()
		{
			return new TValidator();
		}
	}
}
