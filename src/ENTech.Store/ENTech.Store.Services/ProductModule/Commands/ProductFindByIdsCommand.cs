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
	public class ProductFindByIdsCommand : CommandBase<ProductFindByIdsRequest, ProductFindByIdsResponse>
	{
		private IRepository<Product> _productRepository;
		private IMapper _mapper;
		public ProductFindByIdsCommand(IDtoValidatorFactory dtoValidatorFactory, IRepository<Product> productRepository, IMapper mapper)
			: base(dtoValidatorFactory, false)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public override ProductFindByIdsResponse Execute(ProductFindByIdsRequest request)
		{
			var products = _productRepository.FindByIds(request.Ids);

			var dtos = _mapper.MapCollection<Product, ProductDto>(products);

			return new ProductFindByIdsResponse { Items = dtos };
		}
	}
}