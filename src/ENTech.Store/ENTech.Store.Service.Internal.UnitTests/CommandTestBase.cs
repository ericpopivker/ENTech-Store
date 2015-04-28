using ENTech.Store.Core;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using Moq;
using Moq.Protected;

namespace ENTech.Store.Services.Internal.UnitTests
{
	public abstract class CommandTestBase<TDbContext,TCommand, TRequest, TResponse, TValidator, TErrorCode>
		where TRequest : class, IInternalRequest
		where TResponse : class, IResponse, new()
		where TValidator : RequestCustomValidatorBase<TErrorCode>
		where TCommand : InternalCommandBase<TDbContext,TRequest, TResponse, TValidator, TErrorCode>
		where TErrorCode : ErrorCodeBase
	{
		internal Mock<ArgumentErrorsCollection> ErrorsMock = null;
		internal Mock<TValidator> ValidatorMock = null;
		internal Mock<IUnitOfWork> UnitOfWorkMock = null;
		internal Mock<TCommand> CommandMock = null;
		internal Mock<TResponse> ResponseMock = null;
		internal Mock<IDbContext> DbContextMock = null; 

		internal void SetupMocks(TRequest request)
		{
			ErrorsMock = new Mock<ArgumentErrorsCollection> {CallBase = true};

			ValidatorMock = new Mock<TValidator> {CallBase = true};

			ResponseMock = new Mock<TResponse> {CallBase = true};
			ResponseMock.SetupGet(r => r.ArgumentErrors).Returns(ErrorsMock.Object);

			DbContextMock = GetDbContextMock();
			UnitOfWorkMock = GetUnitOfWorkMock();

			CommandMock = GetCommandMock(request);
		}

		private Mock<TCommand> GetCommandMock(TRequest request)
		{

			var commandMock =
				new Mock<TCommand>(GetCommandConstructorParameters()) {CallBase = true};
			commandMock.Protected().SetupGet<TRequest>("Request").Returns(request);
			commandMock.Protected().SetupGet<TResponse>("Response").Returns(ResponseMock.Object);
			commandMock.Protected().SetupGet<TValidator>("Validator").Returns(ValidatorMock.Object);
			commandMock.Protected().SetupGet<IDbContext>("DbContext").Returns(UnitOfWorkMock.Object.DbContext);
			return commandMock;
		}

		protected virtual object[] GetCommandConstructorParameters()
		{
			return new object[] {UnitOfWorkMock.Object, ValidatorMock.Object};
		}

		internal abstract Mock<IDbContext> GetDbContextMock();

		internal virtual Mock<IUnitOfWork> GetUnitOfWorkMock()
		{
			var uow = new Mock<IUnitOfWork>();
			uow.Setup(u => u.DbContext).Returns(DbContextMock.Object);
			return uow;
		}
	}
}
