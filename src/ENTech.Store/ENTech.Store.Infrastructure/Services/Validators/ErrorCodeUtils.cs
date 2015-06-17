using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Attributes;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ErrorCodeUtils
	{
		public static string GetErrorMessage<TErrorCode>(int errorCode)
		{
			var type = typeof(TErrorCode);
			var consts = type
					.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
					.Where(f => f.FieldType == typeof(int)).ToList();

			foreach (var codeConst in consts)
			{
				var code = (int) codeConst.GetValue(null);
				if (code == errorCode)
					return codeConst.GetCustomAttribute<StringValueAttribute>().StringValue;
			}

			throw new ArgumentOutOfRangeException("errorCode", "Value " + errorCode + " not found");
		}
	}
}
