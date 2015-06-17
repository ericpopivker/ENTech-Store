using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Commands;

namespace ENTech.Store.Services.CommandService
{
	public class CommandFactory : ICommandFactory
	{
		public TCommand Create<TCommand>()
			where TCommand : ICommand
		{
			return IoC.Resolve<TCommand>();
		}
	}
}