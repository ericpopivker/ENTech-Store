using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.Shared
{
	public abstract class CommandTestsBase<TRequest, TResponse> where TRequest : IInternalRequest where TResponse : InternalResponse
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

		protected Mock<IMapper> MapperMock { get { return _mapperMock; } }
		protected Mock<IUnitOfWork> UnitOfWorkMock { get { return _unitOfWorkMock; } }

		protected ICommand<TRequest, TResponse> Command { get { return CreateCommand(); } }

		[TearDown]
		public void TearDown()
		{
			_mapperMock.ResetCalls();
			_unitOfWorkMock.ResetCalls();
			TearDownInternal();
		}

		protected virtual void TearDownInternal()
		{
			
		}

		protected abstract ICommand<TRequest, TResponse> CreateCommand();
	}
}