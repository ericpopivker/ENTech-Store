using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using Moq;
using NUnit.Framework;

namespace ENTech.Store.Services.Tests.StoreModule
{
	public class StoreCreateCommandTests
	{
		readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
		Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoMock = new Mock<IRepository<Entities.StoreModule.Store>>();
			
		[Test]
		public void Execute_When_called_Then_calls_repository_create()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object);
			var request = new StoreCreateRequest
			{
				Store = new StoreCreateDto
				{

				}
			};

			command.Execute(request);

			_storeRepositoMock.Verify(x=>x.Add(It.IsAny<Entities.StoreModule.Store>()), Times.Once);
		}
	}
}