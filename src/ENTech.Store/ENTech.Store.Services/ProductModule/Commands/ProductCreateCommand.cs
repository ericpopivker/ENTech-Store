using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand : DbContextCommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		private IValidatorFactory _validatorFactory;

		public ProductCreateCommand(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory) : base(unitOfWork.DbContext, false)
		{
			_validatorFactory = validatorFactory;
		}

		public override RequestValidatorResult ValidateRequest(ProductCreateRequest request)
		{
			var requestValidator = new RequestValidator<ProductCreateRequest>(request);
			
			requestValidator.Add(p => request.Product, _validatorFactory.Create<RequiredValidator>());

			if (request.Product != null)
			{
				requestValidator.Add(p => request.Product.Name, _validatorFactory.Create<RequiredValidator>());
			}

			var result = requestValidator.Validate();
			return result;
		}

		
		public override ProductCreateResponse Execute(ProductCreateRequest request)
		{
			return new ProductCreateResponse
			{
				IsSuccess = true
			};
		}
	}

	
}