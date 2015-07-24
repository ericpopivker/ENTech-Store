
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.ProductModule.Errors.ArgumentErrors
{
	public class ProductNameMustBeUniqueArgumentError : ArgumentError
	{
		private const string _errorMessageTemplate = "Must be unique";

		public ProductNameMustBeUniqueArgumentError() 
					: base(ProductArgumentErrorCode.NameMustBeUnique)
		{
		}
		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
