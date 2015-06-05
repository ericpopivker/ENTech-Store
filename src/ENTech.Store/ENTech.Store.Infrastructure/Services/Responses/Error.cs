using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class Error
	{
		public Error(int errorCode, string errorMessage)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}

		//SashaT: for support exisiting code. can be removed in future
		public Error()
		{
			
		}

		public Error(int errorCode)
		{
			ErrorCode = errorCode;
			ErrorMessage = RequestValidatorErrorMessagesDictionary.Instance[errorCode];
		}

		public int ErrorCode { get; set; }

		public string ErrorMessage { get; set; }
	}
}
