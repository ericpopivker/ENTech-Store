using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Attributes
{
	public class StringValueAttribute : Attribute
	{

		#region Properties

		/// <summary>
		/// Holds the stringvalue for a value in an enum.
		/// </summary>
		public string StringValue { get; protected set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor used to init a StringValue Attribute
		/// </summary>
		/// <param name="value"></param>
		public StringValueAttribute(string value)
		{
			StringValue = value;
		}

		#endregion
	}


	//http://weblogs.asp.net/stefansedich/archive/2008/03/12/enum-with-string-values-in-c.aspx
	//http://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
	public static class StringValueUtils
	{
		public static string GetStringValue<T>(this T value) where T : struct,IConvertible
		{
			//if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type"); ;
			// Get the type
			var type = value.GetType();
			// Get fieldinfo for this type
			var fieldInfo = type.GetField(value.ToString());

			// Get the stringvalue attributes
			var attribs = fieldInfo.GetCustomAttributes(
				typeof(StringValueAttribute), false) as StringValueAttribute[];

			// Return the first if there was a match.
			return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : fieldInfo.Name;
		}
	}
}
