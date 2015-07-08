using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Errors;

namespace ENTech.Store.Infrastructure.Services.Validators
{
	public abstract class ValidateArgumentsResult<TArgumentOwner>
	{
		public Boolean IsValid { get { return ArgumentErrors.Count == 0; } }

		public List<ResponseArgumentError> ArgumentErrors { get; internal set; }

		public ValidateArgumentsResult()
		{
			ArgumentErrors = new List<ResponseArgumentError>();
		}

		public void AddArgumentError(Expression<Func<TArgumentOwner, object>> expression, ArgumentError argumentError)
		{
			ArgumentErrors.Add(new ResponseArgumentError(ArgumentName(expression), argumentError));
		}


		public List<ArgumentError> FindErrorsByArgument(Expression<Func<TArgumentOwner, object>> expression)
		{
			string argumentName = ArgumentName(expression).Value;
			return ArgumentErrors.Where(rae => rae.ArgumentName == argumentName).Select(rae => rae.ArgumentError).ToList();

		}

		public bool AnyErrorsForArgument(Expression<Func<TArgumentOwner, object>> expression)
		{
			string argumentName = ArgumentName(expression).Value;
			return ArgumentErrors.Any(rae => rae.ArgumentName == argumentName);
		}


		public bool NoErrorsForArgument(Expression<Func<TArgumentOwner, object>> expression)
		{
			return !AnyErrorsForArgument(expression);
		}


		protected IArgumentName ArgumentName(Expression<Func<TArgumentOwner, object>> expresison)
		{
			return ArgumentName<TArgumentOwner>.For(expresison);
		}
	}
}
