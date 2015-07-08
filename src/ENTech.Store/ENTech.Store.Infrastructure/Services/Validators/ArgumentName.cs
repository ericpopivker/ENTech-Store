using System;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Helpers;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class ArgumentName<TArgumentOwner> : IArgumentName
	{
		private Expression<Func<TArgumentOwner, object>> _expression;
		private string _value;
		private ArgumentName()
		{
			
		}

		public static ArgumentName<TArgumentOwner> For(Expression<Func<TArgumentOwner, object>> expression)
		{
			Verify.Argument.IsNotNull(expression, "expression");

			var argumentName = new ArgumentName<TArgumentOwner>();
			argumentName._expression = expression;
			return argumentName;
		}

		public string Value
		{
			get
			{
				if (_value == null)
					_value = PropertyHelper.GetName(_expression);

				return _value;
			}
		}
	}
}
