using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductGetByIdCommand : DbContextCommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
		public ProductGetByIdCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			return new ProductGetByIdResponse
			{
				IsSuccess = true,
				Item = new ProductDto
				{
					Id = 1,
					Name = "Test"
				}
			};
		}
	}
}