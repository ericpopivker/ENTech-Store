using System;
using System.Collections.Generic;
using System.Linq;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class ArgumentErrorsCollection : List<ArgumentError>
	{
		public ArgumentErrorsCollection()
		{
			
		}

		public ArgumentErrorsCollection(IEnumerable<ArgumentError> argumentErrors)
		{
			AddRange(argumentErrors);
		}

		public ResponseError this[string key]
		{
			get
			{
				var error = this.SingleOrDefault(e => e.ArgumentName == key);
				return error;
			}

		}

		public bool HasArgument(string key)
		{
			return this[key] != null;
		}
	}
}
