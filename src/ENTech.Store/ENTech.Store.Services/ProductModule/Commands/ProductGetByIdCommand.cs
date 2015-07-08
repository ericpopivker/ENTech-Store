using ENTech.Store.Entities.ProductModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Repositories;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductGetByIdCommand : DbContextCommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
		private IRepository<Product> _productRepository;
		public ProductGetByIdCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory, IRepository<Product> productRepository)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
			_productRepository = productRepository;
		}

		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			var product = _productRepository.GetById(request.Id);
			var productDto = new ProductDto();
			
			//TO DO: map product to productDto
			
			return new ProductGetByIdResponse{Item = productDto};
			
		}
	}
}