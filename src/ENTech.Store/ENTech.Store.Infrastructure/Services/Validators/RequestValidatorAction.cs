using System;
using System.Linq.Expressions;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public sealed class RequestValidatorAction<TEntity, TMember>
	{
		public Expression<Func<TEntity, TMember>> ForMember { get; set; }

		public Expression<Func<TMember, bool>> ValidationExpression { get; set; }

		public int ValidationErrorCode { get; set; }

		public RequestValidatorAction(Expression<Func<TEntity, TMember>> forMember, 
			Expression<Func<TMember, bool>> validationExpression, 
			int validationErrorCode)
		{
			ForMember = forMember;
			ValidationExpression = validationExpression;
			ValidationErrorCode = validationErrorCode;
		}
	}
}
