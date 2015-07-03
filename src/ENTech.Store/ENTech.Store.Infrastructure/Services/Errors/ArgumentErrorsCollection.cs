using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Helpers;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public class ArgumentErrorsCollection : List<ArgumentError>
	{
		private List<ArgumentError> _argumentErrors;

		public ArgumentErrorsCollection()
		{
			_argumentErrors = new List<ArgumentError>();
		}
		
		public List<ArgumentError> FindByArgument(Expression<Func<object, object>> expression)
		{
			string argumentName = PropertyHelper.GetName(expression);
			return _argumentErrors.Where(ae => ae.ArgumentName == argumentName).ToList();

		}
		

		public bool ExistsForArgument(Expression<Func<object, object>> expression)
		{
			string argumentName = PropertyHelper.GetName(expression);
			return _argumentErrors.Any(ae => ae.ArgumentName == argumentName);
		}
	}
}
