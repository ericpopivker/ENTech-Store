using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IDtoValidatorResult
	{
		bool IsValid { get; }

		List<ResponseArgumentError> ArgumentErrors { get; }

		void AddPrefixToArgumentErrorNames(string prefix);

		void AddArgumentErrors(List<ResponseArgumentError> argumentErrors, bool insertFirst = false);
	}
}