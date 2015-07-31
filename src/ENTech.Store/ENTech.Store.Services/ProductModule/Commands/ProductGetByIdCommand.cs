using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
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
		private IMapper _mapper;
		public ProductGetByIdCommand(IDtoValidatorFactory dtoValidatorFactory, IRepository<Product> productRepository, IMapper mapper)
			: base(dtoValidatorFactory, false)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			var product = _productRepository.GetById(request.Id);
			
			var productDto = _mapper.Map<Product, ProductDto>(product);
			
			return new ProductGetByIdResponse{Item = productDto};
		}
	}
}