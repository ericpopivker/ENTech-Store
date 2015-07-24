using System;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Helpers;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public class ResponseArgumentError
	{
		public string ArgumentName { get; internal set; }


		public ArgumentError ArgumentError { get; internal set; }

		public ResponseArgumentError(IArgumentName argumentName, ArgumentError argumentError)
		{
			ArgumentName = argumentName.Value;
			ArgumentError = argumentError;
		}

		internal ResponseArgumentError(string argumentName, ArgumentError argumentError)
		{
			ArgumentName = argumentName;
			ArgumentError = argumentError;
		}
	}
}