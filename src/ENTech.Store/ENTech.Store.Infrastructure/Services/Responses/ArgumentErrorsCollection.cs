using System.Collections.Generic;
using System.Linq;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class ArgumentErrorsCollection : List<ArgumentError>
	{
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
						error.ErrorMessage += value;
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
