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

		public Error this[string key]
		{
			get
			{
				var error = this.SingleOrDefault(e => e.ArgumentName == key);
				return error;
			}
			set
			{
				{
					var error = this.SingleOrDefault(e => e.ArgumentName == key);
					if (error != null)
					{
						error.ErrorMessage += Environment.NewLine + value.ErrorMessage;
						return;
					}
					Add(new ArgumentError(key,value.ErrorCode,value.ErrorMessage));
				}
			}
		}

		public bool HasArgument(string key)
		{
			return this[key] != null;
		}
	}
}
