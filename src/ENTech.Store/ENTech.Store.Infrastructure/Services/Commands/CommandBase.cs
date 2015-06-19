using System.Linq;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public abstract class CommandBase<TRequest, TResponse> : ICommand<TRequest, TResponse>
		where TRequest : IInternalRequest
		where TResponse : InternalResponse, new()
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

		public ArgumentErrorsCollection Validate(TRequest request)
		{
			ArgumentErrorsCollection result = new ArgumentErrorsCollection();

			DtoValidator.VisitAndValidateProperties(request, result);
			if (result.Any())
				return result;
			
			return ValidateInternal(request);
		}

		protected virtual ArgumentErrorsCollection ValidateInternal(TRequest request)
		{
			return new ArgumentErrorsCollection();
		}

		public virtual void NotifyExecuted(TRequest request, TResponse response)
		{
			//
		}

		protected TResponse InternalServerError()
		{
			return new TResponse
			{
				IsSuccess = false,
				Error = new Error(CommonErrorCode.InternalServerError)
			};
		} 
	}
}