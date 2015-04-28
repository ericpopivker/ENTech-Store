using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class RequestValidatorResult : Error
	{
		public RequestValidatorResult(bool isSuccess, int errorCode, string errorMessage)
			: base(errorCode, errorMessage)
		{
			IsSuccess = isSuccess;
		}

		public bool IsSuccess { get; set; }
	}
}
