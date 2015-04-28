using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Enums
{
	/// <summary>Current application environment.</summary>
	public enum EnvironmentType
	{
		Dev = 1,
		Qa = 2,
		Stable = 3,
		Prod = 4,
		Local = 5
	}
}
