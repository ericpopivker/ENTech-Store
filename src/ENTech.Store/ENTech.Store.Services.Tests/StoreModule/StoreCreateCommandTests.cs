using ENTech.Store.Entities.GeoModule;
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
		readonly Mock<IRepository<Entities.StoreModule.Store>> _storeRepositoryMock = new Mock<IRepository<Entities.StoreModule.Store>>();
			
		[Test]
		public void Execute_When_called_Then_calls_repository_create()
		{
			var command = new StoreCreateCommand(_unitOfWorkMock.Object, 
				_storeRepositoryMock.Object);

			var request = new StoreCreateRequest
			{
				Store = new StoreCreateDto
				{
					Name = "test store name"
				}
			};

			command.Execute(request);

			_storeRepositoryMock.Verify(x => x.Add(It.Is<Entities.StoreModule.Store>(y => y.Name == request.Store.Name)),
				Times.Once);
		}
	}
}