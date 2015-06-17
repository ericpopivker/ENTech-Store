using System.Linq;
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

		public virtual RequestValidatorResult ValidateRequest(TRequest request)
		{
			return RequestValidatorResult.Valid();
		}


		public virtual void NotifyExecuted(TRequest request, TResponse response)
		{
		}
	}
}