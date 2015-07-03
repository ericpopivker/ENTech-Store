using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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


		//private static string GetName(Expression expression)
		//{
		//	return string.Join(".", GetNames(expression));
		//}

		public static string GetName(Expression expression)
		{
			var pathElements = new List<string>();

			while (true)
			{
				var memberExpression = GetMemberExpression(expression);

				if (memberExpression == null)
				{
					//fix for indexes
					if (expression is MethodCallExpression)
					{
						var methodCallExpression = expression as MethodCallExpression;

						if (methodCallExpression.Method.Name == "get_Item")
						{
							var indexerText = "[" + ((ConstantExpression) methodCallExpression.Arguments[0]).Value + "]";

							TryAddMemberSeparator(pathElements);

							pathElements.Add(indexerText);
						}

						//Go to parent
						expression = methodCallExpression.Object;
					}
					else
					{
						break;
					}
				}
				else
				{
					TryAddMemberSeparator(pathElements);

					pathElements.Add(memberExpression.Member.Name);

					//Go to parent
					expression = memberExpression.Expression;
				}
			}


			var sb = new StringBuilder();
			

			for (int index = pathElements.Count - 1; index >= 0; index--)
				sb.Append(pathElements[index]);
			
			return sb.ToString();
		}

		private static void TryAddMemberSeparator(List<string> pathElements)
		{
			if (pathElements.Count > 0 && !pathElements.Last().StartsWith("["))
				pathElements.Add(".");
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


		private static bool IsConversion(Expression expression)
		{
			return (expression.NodeType == ExpressionType.Convert
					|| expression.NodeType == ExpressionType.ConvertChecked);
		}
	}
}