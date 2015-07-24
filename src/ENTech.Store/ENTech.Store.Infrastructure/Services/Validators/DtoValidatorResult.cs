using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class DtoValidatorResult<TArgumentOwner> : ValidateArgumentsResult<TArgumentOwner>, IDtoValidatorResult
	{
		public void AddArgumentErrors(List<ResponseArgumentError> argumentErrors, bool insertFirst = false)
		{
			if (insertFirst)
			{
				ArgumentErrors.InsertRange(0, argumentErrors);
			}
			else
			{
				ArgumentErrors.AddRange(argumentErrors);
			}
			
		}

		public void AddPrefixToArgumentErrorNames(string prefix)
		{
			foreach (var resArgumentError in this.ArgumentErrors)
			{
				resArgumentError.ArgumentName = prefix + resArgumentError.ArgumentName;
			}
		}
	}
}