using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class ResponseError
	{
		private int _errorCode;
		private string _errorMessage;

		public ResponseError(int errorCode, string errorMessage)
		{
			_errorCode = errorCode;
			_errorMessage = errorMessage;
		}

		//SashaT: for support exisiting code. can be removed in future
		public ResponseError()
		{
			
		}

		public int ErrorCode
		{
			get { return _errorCode; }
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
		}
	}
}
