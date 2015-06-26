
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.ProductModule.Errors.ArgumentErrors
{
	public class ProductNameMustBeUniqueArgumentError : ArgumentError
	{
		private const string _errorMessageTemplate = "Must be unique";

		public ProductNameMustBeUniqueArgumentError(string argumentName) 
					: base(argumentName, ProductArgumentErrorCode.NameMustBeUnique)
		{
		}
		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
