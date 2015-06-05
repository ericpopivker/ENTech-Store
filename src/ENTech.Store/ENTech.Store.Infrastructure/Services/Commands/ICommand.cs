using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface ICommand<in TRequest, TResponse> : IInternalCommand
		where TRequest : IInternalRequest
		where TResponse : InternalResponse
	{
		TResponse Execute(TRequest request);

		bool RequiresTransaction { get; }

		ArgumentErrorsCollection Validate(TRequest request);

		void NotifyExecuted(TRequest request, TResponse response);
	}
}