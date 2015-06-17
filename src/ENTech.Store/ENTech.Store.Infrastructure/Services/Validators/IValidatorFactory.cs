using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IValidatorFactory
	{
		TValidator Create<TValidator>() where TValidator : new();
	}
}
