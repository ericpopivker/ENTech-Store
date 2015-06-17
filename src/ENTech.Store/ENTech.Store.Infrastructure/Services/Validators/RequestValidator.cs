using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Helpers;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public class RequestValidator<TRequest>
	{
		private List<ChainItem> _chainItems = new List<ChainItem>();

		public class ChainItem
		{
			public IValidator Validator;
			public IValidatorContext Context;
			public Expression<Func<TRequest, object>> PropertyExpression;
		}


		private TRequest _request;
		public RequestValidator(TRequest request)
		{
			_request = request;
		}

		public void Add(Expression<Func<TRequest, object>> expression, RequiredValidator requiredValidator)
		{
			var groupItem = new ChainItem();
			groupItem.Validator = requiredValidator;
			groupItem.Context = new RequiredValidator.Context { Value = PropertyHelper.GetValue(expression, _request) };
			groupItem.PropertyExpression = expression;

			_chainItems.Add(groupItem);
		}


		public RequestValidatorResult Validate()
		{
			var argumentErrors = new ArgumentErrorsCollection();
			foreach (var groupItem in _chainItems)
			{
				var validator = groupItem.Validator;
				var context = groupItem.Context;

				var validatorResult = validator.Validate(context);
				if (!validatorResult.IsValid)
				{
					
					

					string propertyName = PropertyHelper.GetName(groupItem.PropertyExpression);
					propertyName = propertyName.Substring(propertyName.IndexOf('.') + 1);
					argumentErrors.Add(
						new ArgumentError
							(
							propertyName,
							validatorResult.ErrorCode,
							validatorResult.ErrorMessage
							));
				}
			}

			if (argumentErrors.Count > 0)
				return RequestValidatorResult.Invalid(argumentErrors);
			return RequestValidatorResult.Valid();
		}
	}
}
