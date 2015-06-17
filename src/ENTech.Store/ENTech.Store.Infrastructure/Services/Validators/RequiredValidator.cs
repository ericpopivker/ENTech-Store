using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class RequiredValidator : ValidatorBase<RequiredValidator.Result, RequiredValidator.Context>
	{
		//internal RequiredValidator()
		//{
		//} 

		public class Context : IValidatorContext
		{
			public Object Value { get; set; }
		}

		public class Result : IValidatorResult
		{
			private bool _isValid;
			private int _errorCode;
			private string _errorMessage;

			public Result(bool isValid)
			{
				if (isValid)
					Valid();
				else
					Invalid();
			}

			public bool IsValid
			{
				get { return _isValid; }
			}

			public int ErrorCode
			{
				get { return _errorCode; }
			}

			public string ErrorMessage
			{
				get { return _errorMessage; }
			}

			private void Valid()
			{
				_isValid = true;
			}


			private void Invalid()
			{
				_isValid = false;
				_errorCode = CommonArgumentErrorCode.Required;
				_errorMessage = ErrorCodeUtils.GetErrorMessage<CommonArgumentErrorCode>(CommonArgumentErrorCode.Required);
			}
		}

		public override Result Validate(Context context)
		{
			if (context.Value == null)
				return new Result(false);

			if (context.Value is string)
			{
				bool isValid = !String.IsNullOrWhiteSpace(context.Value as string);
				return new Result(isValid);
			}

			if (context.Value is IEnumerable)
			{
				var enumerbale = (context.Value as IEnumerable);

				IEnumerator enumerator = enumerbale.GetEnumerator();
				bool isValid = enumerator.MoveNext();
				return new Result(isValid);
			}

			return new Result(true);
		}
	}
}
