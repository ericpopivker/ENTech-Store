using System;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Helpers;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public abstract class ArgumentError : Error
	{
		public string ArgumentName { get; set; }

		public ArgumentError(string argumentName, int errorCode)
			: base(errorCode)
		{
			ArgumentName = argumentName;
		}
		

		//public static ArgumentError For<TObj>(Expression<Func<TObj, object>> expression, string errorMessage, int errorCode = 0)
		//{
		//	return new ArgumentError(PropertyHelper.GetName(expression), errorCode, errorMessage);
		//}
	}



	//public class ProductQuantityOverInventoryArgumentError : ArgumentError
	//{
	//	int Actual { get; }
	//	int MaxValue { get; }

	//	void ProductQuantityOverInventoryArgumentError(int actual, int maxValue)
	//	{

	//		_errorCode = ProductArgumetnErrorCode.ProductQuantityOverInventory;
	//		_errorMessage = ProductArgumetnErrorCode.GEtErrorMessageTempate(..).Format(actual, maxValue);

	//	}
	//}
}