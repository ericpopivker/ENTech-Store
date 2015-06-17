using ENTech.Store.Infrastructure.Services.Commands;

namespace ENTech.Store.Services.CommandService
{
	public interface ICommandFactory
	{
		TCommand Create<TCommand>() where TCommand : ICommand;
	}
}