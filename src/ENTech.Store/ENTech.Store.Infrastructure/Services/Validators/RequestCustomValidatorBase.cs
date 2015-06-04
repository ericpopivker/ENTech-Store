using ENTech.Store.Infrastructure.Helpers;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public abstract class RequestCustomValidatorBase<TErrorCode> : IRequestValidator where TErrorCode : ErrorCodeBase
	{
		public bool TryValidate<TEntity, TMember>(RequestValidatorAction<TEntity, TMember> action, TEntity entity,
			ArgumentErrorsCollection existingErrors)
		{
			var memberName = PropertyHelper.GetName(action.ForMember);

			if (existingErrors[memberName] == null)
			{
				var memberValue = action.ForMember.Compile().Invoke(entity);
				var result = action.ValidationExpression.Compile().Invoke(memberValue);
				if (!result)
				{
					var errorMessage = RequestValidatorErrorMessagesDictionary.Instance[action.ValidationErrorCode];
					existingErrors[memberName] = new Error(action.ValidationErrorCode, errorMessage);
				}
				return result;
			}
			return false;
		}
	}
}
