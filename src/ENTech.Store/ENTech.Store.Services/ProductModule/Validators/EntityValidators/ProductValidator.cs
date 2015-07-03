﻿using System.Collections.ObjectModel;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure;
using ENTech.Store.Infrastructure.Services.Errors;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Errors.ArgumentErrors;
using ENTech.Store.Services.ProductModule.Errors.ResponseErrors;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.StoreModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.ProductModule.Validators.EntityValidators
{
	public class ProductValidator : IProductValidator
	{
		private IUnitOfWork _unitOfWork;
		private IInternalCommandService _internalCommandService;
		private IProductQuery _productQuery;

		public ProductValidator(IUnitOfWork unitOfWork, IInternalCommandService internalCommandService, IProductQuery productQuery)
		{
			_unitOfWork = unitOfWork;
			_internalCommandService = internalCommandService;
			_productQuery = productQuery;
		}


		public ValidateArgumentResult NameMustBeUnique(string argumentName, string productName)
		{
			var query = new ProductExiststByNameQuery();
			var critera = new ProductExiststByNameQuery.Criteria { Name = productName };
			var exists = _unitOfWork.Query(query, critera);

			if (exists)
			{
				var error = new ProductNameMustBeUniqueArgumentError(argumentName);
				return ValidateArgumentResult.Invalid(error);
			}

			return ValidateArgumentResult.Valid();
		}



		public ValidateOperationResult IsOverMaxProductsLimit(int storeId)
		{
			StoreGetByIdRequest storeGetByIdRequest = new StoreGetByIdRequest();
			storeGetByIdRequest.LoadOptions = new Collection<StoreLoadOption> { StoreLoadOption.Settings };
			var response = _internalCommandService.Execute<StoreGetByIdRequest, StoreGetByIdResponse, StoreGetByIdCommand>(storeGetByIdRequest);

			var storeSettings = response.Item.Settings;
			var totalProducts = _productQuery.GetTotalByStoreId(storeId);

			Verify.Argument.IsTrue(storeSettings.MaxProducts >= totalProducts, "totalProducts");

			if (totalProducts == storeSettings.MaxProducts)
			{
				var error = new ProductOverMaxStoreLimitError(storeSettings.MaxProducts);
				return ValidateOperationResult.Invalid(error);
			}

			return ValidateOperationResult.Valid();
		}
	}
}
