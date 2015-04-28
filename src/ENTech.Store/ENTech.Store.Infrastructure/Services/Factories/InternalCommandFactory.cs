using ENTech.Store.Infrastructure.Services.Commands;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Infrastructure.Services.Factories
{
	public class InternalCommandFactory
	{
		public TCommand Create<TCommand>()
			where TCommand : IInternalCommand
		{
			return IoC.Container.Resolve<TCommand>();
		}
	}
}
