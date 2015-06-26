
namespace ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors
{
	public class RequiredArgumentError : ArgumentError
	{
		private const string _errorMessageTemplate = "Required";

		public RequiredArgumentError(string argumentName) 
					: base(argumentName, CommonArgumentErrorCode.Required)
		{
		}
		
		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
