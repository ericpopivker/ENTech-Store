using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Errors;
using ENTech.Store.Services.ProductModule.Errors.ArgumentErrors;
using ENTech.Store.Services.ProductModule.Errors.ResponseErrors;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.ProductModule.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand : DbContextCommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		private IUnitOfWork _unitOfWork ;
		private IInternalCommandService _internalCommandService;
		private IProductQuery _productQuery;
		private IProductValidator _productValidator;

		public ProductCreateCommand(IUnitOfWork unitOfWork, IInternalCommandService internalCommandService, IProductQuery productQuery, IProductValidator productValidator)
			: base(unitOfWork.DbContext, false)
		{
			_unitOfWork = unitOfWork;
			_internalCommandService = internalCommandService;
			_productQuery = productQuery;
			_productValidator = productValidator;
		}

		protected override ValidatorResult ValidateInternal(ProductCreateRequest request)
		{
			var argValidatorResult = _productValidator.NameMustBeUnique("Product.Name", request.Product.Name);
			if (!argValidatorResult.IsValid)
				return ValidatorResult.Invalid(argValidatorResult.Error);


			var validatorResult = _productValidator.IsOverMaxProductsLimit(request.Product.StoreId);
			if (!validatorResult.IsValid)
				return validatorResult;
				
			return  ValidatorResult.Valid();
		}

		public override ProductCreateResponse Execute(ProductCreateRequest request)
		{
			var response = new ProductCreateResponse();

			//Check store limit of total products.

			return new ProductCreateResponse();
		}

		
	}
}