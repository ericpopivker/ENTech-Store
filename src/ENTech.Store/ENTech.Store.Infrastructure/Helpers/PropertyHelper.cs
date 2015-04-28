using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ENTech.Store.Infrastructure.Helpers
{
	public class PropertyHelper
	{
		public static string GetName<T>(Expression<Func<T>> expression)
		{
			return GetName(expression.Body);
		}

		public static string GetName<T>(Expression<Func<T, object>> expression)
		{
			return GetName(expression.Body);
		}

		public static string GetName<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return GetName(expression.Body);
		}

		public static Type GetType<T>(Expression<Func<T, object>> expression)
		{
			return GetMemberExpression(expression.Body).Type;
		}

		public static Type GetType<T, TProperty>(Expression<Func<T, TProperty>> expression)
		{
			return GetMemberExpression(expression.Body).Type;
		}

		private static MemberExpression GetMemberExpression(Expression expression)
		{
			var getMemberExpression = expression as MemberExpression;

			if (getMemberExpression != null)
				return getMemberExpression;

			if (IsConversion(expression))
			{
				var unaryExpression = expression as UnaryExpression;

				if (unaryExpression != null)
					return GetMemberExpression(unaryExpression.Operand);
			}

			return null;
		}

		private static string GetName(Expression expression)
		{
			return string.Join(".", GetNames(expression));
		}

		private static IEnumerable<string> GetNames(Expression expression)
		{
			var memberExpression = GetMemberExpression(expression);

			if (memberExpression == null)
			{
				//fix for indexes
				if (expression is MethodCallExpression)
				{
					foreach (var argument in ((MethodCallExpression) expression).Arguments)
					{
						var memberArgument = argument as MemberExpression;

						if (memberArgument != null && memberArgument.Member is FieldInfo)
						{
							var field = memberArgument.Member as FieldInfo;
							if (field.FieldType == typeof(System.Int32))
							{
								//acessing Dtos possible only via properties. If was field accessed then it must be index [i]
								yield break;
							}
						}
						else
							foreach (var argumentName in GetNames(argument))
								yield return argumentName;
					}
				}
				yield break;
			}

			var names = GetNames(memberExpression.Expression).ToList();

			foreach (var memberName in names)
				yield return memberName;

			yield return memberExpression.Member.Name;
		}

		private static bool IsConversion(Expression expression)
		{
			return (expression.NodeType == ExpressionType.Convert
					|| expression.NodeType == ExpressionType.ConvertChecked);
		}
	}
}