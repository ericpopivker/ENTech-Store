using System.Web.Util;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Commands
{
	public interface ICommand
	{
}

	public interface ICommand<in TRequest, TResponse> : ICommand
		where TRequest : IRequest
		where TResponse : ResponseBase
	{
		TResponse Execute(TRequest request);

		bool RequiresTransaction { get; }

		RequestValidatorResult ValidateRequest(TRequest request);

		void NotifyExecuted(TRequest request, TResponse response);
	}
}