using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public abstract class Error
	{
		protected int _errorCode;

		protected Error(int errorCode)
		{
			_errorCode = errorCode;
		}

		public int ErrorCode
		{
			get { return _errorCode; }
		}

		protected abstract string ErrorMessageTemplate { get; }


		public virtual string ErrorMessage 
		{
			get
			{
				string errorMessageTemplate = this.ErrorMessageTemplate;

				Verify.Argument.IsNotEmpty(errorMessageTemplate, "ErrorMessageTemplate");
				Verify.Argument.IsFalse(errorMessageTemplate.Contains("{0}"), "ErrorMessageTemplate");

				return ErrorMessageTemplate; 
			} 
		}
	}
}
