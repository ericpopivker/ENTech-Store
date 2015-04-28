using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.External
{
	public class ExternalCommandFactory
	{
		public TCommand Create<TCommand>() where TCommand : IExternalCommand
		{
			var command = IoC.Resolve<TCommand>();
			return command;
		}
	}
}
