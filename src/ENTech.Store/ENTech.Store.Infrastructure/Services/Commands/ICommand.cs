using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface ICommand<in TRequest, TResponse> : IInternalCommand
		where TRequest : IRequest
		where TResponse : ResponseBase
	{
		TResponse Execute(TRequest request);

		bool RequiresTransaction { get; }

		ValidatorResult Validate(TRequest request);

		void NotifyExecuted(TRequest request, TResponse response);
	}
}