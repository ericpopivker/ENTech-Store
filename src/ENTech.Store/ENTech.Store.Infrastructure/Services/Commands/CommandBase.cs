using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public abstract class CommandBase<TRequest, TResponse> : ICommand<TRequest, TResponse>
		where TRequest : IRequest
		where TResponse : ResponseBase
	{
		private readonly bool _requiresTransaction;

		protected CommandBase(bool requiresTransaction)
		{
			_requiresTransaction = requiresTransaction;
		}

		public abstract TResponse Execute(TRequest request);

		public bool RequiresTransaction
		{
			get { return _requiresTransaction; }
		}

		public ValidatorResult Validate(TRequest request)
		{
			var argErrors = new List<ArgumentError>();

			DtoValidator.VisitAndValidateProperties(request, argErrors);
			if (argErrors.Any())
				return ValidatorResult.Invalid(argErrors);
			
			return ValidateInternal(request);
		}

		protected virtual ValidatorResult ValidateInternal(TRequest request)
		{
			return ValidatorResult.Valid();
		}

		public virtual void NotifyExecuted(TRequest request, TResponse response)
		{
			//
		}
	}
}