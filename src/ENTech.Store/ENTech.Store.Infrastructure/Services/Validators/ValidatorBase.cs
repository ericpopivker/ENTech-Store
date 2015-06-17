using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public abstract class ValidatorBase<TValidatorResult, TValidatorContext> : IValidator<TValidatorResult, TValidatorContext>
	{
		TValidatorResult IValidator<TValidatorResult, TValidatorContext>.Validate(TValidatorContext context)
		{
			return Validate(context);
		}

		IValidatorResult IValidator.Validate(IValidatorContext validatorContext)
		{
			return Validate((TValidatorContext)validatorContext) as IValidatorResult;
		}

		public abstract TValidatorResult Validate(TValidatorContext validatorContext);
	}

}
