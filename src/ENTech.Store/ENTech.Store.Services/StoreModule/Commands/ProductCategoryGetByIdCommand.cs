using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class ProductCategoryGetByIdCommand : CommandBase<ProductCategoryGetByIdRequest, ProductCategoryGetByIdResponse>
	{
		private readonly IRepository<ProductCategory> _productCategoryRepository;
		private readonly IMapper _mapper;

		public ProductCategoryGetByIdCommand(IRepository<ProductCategory> productCategoryRepository, IMapper mapper, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_productCategoryRepository = productCategoryRepository;
			_mapper = mapper;

		}

		public override ProductCategoryGetByIdResponse Execute(ProductCategoryGetByIdRequest request)
		{
			var entity = _productCategoryRepository.GetById(request.Id);

			var dto = _mapper.Map<ProductCategory, ProductCategoryDto>(entity);

			return new ProductCategoryGetByIdResponse
			{
				Item = dto
			};
		}
	}

	public class ProductCategoryGetByIdRequest : GetByIdRequestBase<ProductCategoryGetByIdResponse>
	{
	}

	public class ProductCategoryGetByIdResponse : GetByIdResponse<ProductCategoryDto>
	{
	}
}