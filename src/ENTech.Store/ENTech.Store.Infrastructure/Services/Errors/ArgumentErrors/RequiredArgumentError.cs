
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors
{
	public class RequiredArgumentError : ArgumentError
	{
		private const string _errorMessageTemplate = "Required";


		internal RequiredArgumentError()
			: base(CommonArgumentErrorCode.Required)
		{
		}


		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
