using System;
using System.Collections;
using System.Security.AccessControl;
using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.ProductModule.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using Newtonsoft.Json.Schema;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand3 : DbContextCommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		public ProductCreateCommand3(IUnitOfWork unitOfWork) : base(unitOfWork.DbContext, false)
		{
		}

		protected RequestValidatorResult ValidateRequest(ProductCreateRequest request)
		{
			var validator = new ProductCreateRequestValidator2();
			var fluentValidationResults = validator.Validate(request);

			//if (fluentValidationResults.IsValid)
			return RequestValidatorResult.Valid();
		
			//convert Fluent result to ValidatorResult
			//var argumentErrors = new ArgumentErrorsCollection();
			//foreach (var fluentError in fluentValidationResults.Errors)
			//{
			//	argumentErrors.Add(new ArgumentError{ArgumentName = fluentError.PropertyName, ErrorCode = int.Parse(fluentError.ErrorCode)});
			//}
		
			//return new RequestValidatorResult{IsValid = true};
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
