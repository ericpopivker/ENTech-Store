using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public interface IValidatorResult
	{
		bool IsValid { get; }

		int ErrorCode { get; }

		string ErrorMessage { get; }

	}
}
