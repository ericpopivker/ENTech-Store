using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.AuthenticationModule.Errors.ResponseErrors
{
	public class ApiKeyRequiredResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "API Key is required, but wasn't passed";

		public ApiKeyRequiredResponseError()
			: base(AuthenticationResponseErrorCode.ApiKeyRequired)
		{
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
