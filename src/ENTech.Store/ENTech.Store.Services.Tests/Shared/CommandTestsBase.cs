using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.CommandService.Definition;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.Shared
{
	public abstract class CommandTestsBase<TRequest, TResponse> where TRequest : IInternalRequest where TResponse : InternalResponse
	{
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
		private readonly Mock<IInternalCommandService> _internalCommandServiceMock = new Mock<IInternalCommandService>();

		protected Mock<IMapper> MapperMock { get { return _mapperMock; } }
		protected Mock<IInternalCommandService> InternalCommandServiceMock { get { return _internalCommandServiceMock; } }

		protected ICommand<TRequest, TResponse> Command { get { return CreateCommand(); } }

		[TearDown]
		public void TearDown()
		{
			_mapperMock.ResetCalls();
			TearDownInternal();
		}

		protected virtual void TearDownInternal()
		{
			
		}

		protected abstract ICommand<TRequest, TResponse> CreateCommand();
	}
}