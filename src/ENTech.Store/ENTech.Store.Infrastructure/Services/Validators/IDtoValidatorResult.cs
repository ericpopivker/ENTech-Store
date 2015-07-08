using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IDtoValidatorResult
	{
		bool IsValid { get; }

		List<ResponseArgumentError> ArgumentErrors { get; }
	}
}