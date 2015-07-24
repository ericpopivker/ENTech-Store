using System;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.ProductModule.Errors.ResponseErrors
{
	public class ProductOverMaxStoreLimitError : ResponseError
	{
		private const string _errorMessageTemplate = "Store limit for number of products {0} was already reached";

		private int _maxProducts;
		
		public  int MaxProducts { get { return _maxProducts; } }
		
		public ProductOverMaxStoreLimitError(int maxProducts) 
					: base(CommonArgumentErrorCode.Required)
		{
			_maxProducts = maxProducts;
		}
		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}


		public override string ErrorMessage
		{
			get { return String.Format(_errorMessageTemplate, _maxProducts); }
		}
	}
}
