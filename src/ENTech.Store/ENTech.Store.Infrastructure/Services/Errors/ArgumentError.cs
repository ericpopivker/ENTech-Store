using System;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Helpers;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Errors
{
	public abstract class ArgumentError : Error
	{
		protected ArgumentError(int errorCode) : base(errorCode)
		{
		}
	}

}