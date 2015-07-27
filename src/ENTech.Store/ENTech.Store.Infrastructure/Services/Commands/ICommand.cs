using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface ICommand<in TRequest, TResponse> : IInternalCommand
		where TRequest : IRequest<TResponse>
		where TResponse : IResponse
	{
		TResponse Execute(TRequest request);

		bool RequiresTransaction { get; }

		ValidateCommandResult Validate(TRequest request);

		void NotifyExecuted(TRequest request, TResponse response);
	}
}