using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Errors.ArgumentErrors;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.UnitTests.ProductModule.Validators.EntityValidators
{
	[TestFixture]
	public class StoretValidatorTest
	{
		private Mock<IInternalCommandService> _internalCommandService = new Mock<IInternalCommandService>();
		
		private const int StoreIdFake = 123;

		private StoreValidator _storeValidator;

		[SetUp]
		public void SetUp()
		{
			_internalCommandService.ResetCalls();
			
			_storeValidator= new StoreValidator(_internalCommandService.Object);
		}

		[Test]
		public void ValidateId_When_called_Then_calls_StoreGetEntityMetaStateCommand()
		{
			_internalCommandService.Setup(ics => ics.Execute<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse, StoreGetEntityMetaStateCommand>(It.IsAny<StoreGetEntityMetaStateRequest>()))
										.Returns(new StoreGetEntityMetaStateResponse{EntityMetaState = EntityMetaState.Exists});

			_storeValidator.ValidateId(StoreIdFake);


			_internalCommandService.Verify(ics => ics.Execute<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse, StoreGetEntityMetaStateCommand>(It.Is<StoreGetEntityMetaStateRequest>(r => r.Id == StoreIdFake)), Times.Once);
		}



		[Test]
		public void ValidateId_When_exists_Then_returns_valid()
		{
			_internalCommandService.Setup(ics => ics.Execute<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse, StoreGetEntityMetaStateCommand>(It.IsAny<StoreGetEntityMetaStateRequest>()))
										.Returns(new StoreGetEntityMetaStateResponse { EntityMetaState = EntityMetaState.Exists });

			var result = _storeValidator.ValidateId(StoreIdFake);
			Assert.IsTrue(result.IsValid);
		}


		[Test]
		public void ValidateId_When_not_found_Then_returns_EntityWithIdDoesNotExistError()
		{
			_internalCommandService.Setup(ics => ics.Execute<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse, StoreGetEntityMetaStateCommand>(It.IsAny<StoreGetEntityMetaStateRequest>()))
										.Returns(new StoreGetEntityMetaStateResponse { EntityMetaState = EntityMetaState.NotFound });

			var result = _storeValidator.ValidateId(StoreIdFake);

			Assert.IsFalse(result.IsValid);
			Assert.IsTrue(result.ArgumentError is EntityWithIdDoesNotExist);
		}



		[Test]
		public void ValidateId_When_entity_deleted_Then_returns_EntityWithIdIsDeletedError()
		{
			_internalCommandService.Setup(ics => ics.Execute<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse, StoreGetEntityMetaStateCommand>(It.IsAny<StoreGetEntityMetaStateRequest>()))
										.Returns(new StoreGetEntityMetaStateResponse { EntityMetaState = EntityMetaState.Deleted });

			var result = _storeValidator.ValidateId(StoreIdFake);

			Assert.IsFalse(result.IsValid);
			Assert.IsTrue(result.ArgumentError is EntityWithIdIsDeletedArgumentError);
		}
	}
}
