using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.CommandService
{
	public abstract class CommandFacade<TResult> where TResult : IResponse
	{
		public abstract TResult Execute(IRequest<TResult> message);
		public abstract bool RequiresTransaction { get; }
		public abstract ValidateCommandResult Validate(IRequest<TResult> request);

		public abstract void NotifyExecuted(IRequest<TResult> request, TResult response);
	}
	
	public class CommandFacade<TRequest, TResponse> : CommandFacade<TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : IResponse
	{
		private readonly ICommand<TRequest, TResponse> _inner;

		public CommandFacade(ICommand<TRequest, TResponse> inner)
		{
			_inner = inner;
		}

		public override TResponse Execute(IRequest<TResponse> message)
		{
			return _inner.Execute((TRequest)message);
		}

		public override bool RequiresTransaction
		{
			get { return _inner.RequiresTransaction; }
		}

		public override ValidateCommandResult Validate(IRequest<TResponse> request)
		{
			return _inner.Validate((TRequest)request);
		}

		public override void NotifyExecuted(IRequest<TResponse> request, TResponse response)
		{
			_inner.NotifyExecuted((TRequest)request, response);
		}
	}
}