using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Responses.Statuses;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.CommandService.Definition
{
	public interface IExternalCommandService<TSecurity>
			where TSecurity : ISecurityInformation
	{
		IResponseStatus<TResponse> Execute<TRequest, TResponse, TCommand>(TRequest request)
			where TCommand : ICommand<TRequest, TResponse>
			where TRequest : SecureRequestBase<TSecurity>
			where TResponse : IResponse, new();
	}
}