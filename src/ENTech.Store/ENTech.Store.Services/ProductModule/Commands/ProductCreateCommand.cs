using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand : CommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		private IInternalCommandService _internalCommandService;
		private IProductQuery _productQuery;
		private IProductValidator _productValidator;
		private IStoreValidator _storeValidator;

		public ProductCreateCommand(IDtoValidatorFactory dtoValidatorFactory, IInternalCommandService internalCommandService, IProductQuery productQuery, IProductValidator productValidator, IStoreValidator storeValidator)
			: base(dtoValidatorFactory, false)
		{
			_internalCommandService = internalCommandService;
			_productQuery = productQuery;
			_productValidator = productValidator;
			_storeValidator = storeValidator;
		}

		protected override void ValidateRequestInternal(ProductCreateRequest request, ValidateRequestResult<ProductCreateRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.Product.StoreId))
			{
				var result = _storeValidator.ValidateId(request.Product.StoreId);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.Product.StoreId, result.ArgumentError);
			}

			if (validateRequestResult.NoErrorsForArgument(req => req.Product.Name) &&
			    validateRequestResult.NoErrorsForArgument(req => request.Product.StoreId))
			{

				var result = _productValidator.NameMustBeUnique(request.Product.Name, request.Product.StoreId);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.Product.Name, result.ArgumentError);
			}
		}
		
		protected override ValidateOperationResult ValidateOperationInternal(ProductCreateRequest request)
		{
			var result = _productValidator.IsOverMaxProductsLimit(request.Product.StoreId);
			if (!result.IsValid)
				return result;

			return null;
		}

		public override ProductCreateResponse Execute(ProductCreateRequest request)
		{
			var response = new ProductCreateResponse();

			//Check store limit of total products.

			return new ProductCreateResponse();
		}

		
	}
}