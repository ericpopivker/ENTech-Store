using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Database.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductGetByIdCommand : CommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
		private IRepository<Product> _productRepository;
		public ProductGetByIdCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory, IRepository<Product> productRepository)
			: base(dtoValidatorFactory, false)
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