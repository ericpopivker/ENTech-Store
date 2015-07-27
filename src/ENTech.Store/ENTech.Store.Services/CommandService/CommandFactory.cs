using System;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.CommandService
{
	public class CommandFactory : ICommandFactory
	{
		public CommandFacade<TResponse> Create<TResponse>(IRequest<TResponse> request) where TResponse : IResponse
		{
			var handlerType = typeof(ICommand<,>).MakeGenericType(request.GetType(), typeof(TResponse));
			var wrapperType = typeof(CommandFacade<,>).MakeGenericType(request.GetType(), typeof(TResponse));
			object handler;
			try
			{
				handler = IoC.Resolve(handlerType);

				if (handler == null)
					throw BuildException(request);
			}
			catch (Exception e)
			{
				throw BuildException(request, e);
			}
			var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
			return (CommandFacade<TResponse>)wrapperHandler;
		}

		private static InvalidOperationException BuildException(object message, Exception inner = null)
		{
			return new InvalidOperationException("Command was not found for request of type " + message.GetType() + ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", inner);
		}
	}
}