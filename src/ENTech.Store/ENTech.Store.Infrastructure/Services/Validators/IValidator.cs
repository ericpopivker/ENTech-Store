using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IValidator<TValidatorResult, TValidatorContext> : IValidator
	{
		TValidatorResult Validate(TValidatorContext context);

	}

	public interface IValidator
	{
		IValidatorResult Validate(IValidatorContext context);
	}


}
