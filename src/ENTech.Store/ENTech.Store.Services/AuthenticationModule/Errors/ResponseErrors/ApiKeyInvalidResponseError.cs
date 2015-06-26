using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Services.AuthenticationModule.Errors.ResponseErrors
{
	public class ApiKeyInvalidResponseError : ResponseError
	{
		private const string _errorMessageTemplate = "API Key is not valid";

		public ApiKeyInvalidResponseError()
			: base(AuthenticationResponseErrorCode.ApiKeyInvalid)
		{
		}

		protected override string ErrorMessageTemplate
		{
			get { return _errorMessageTemplate; }
		}
	}
}
