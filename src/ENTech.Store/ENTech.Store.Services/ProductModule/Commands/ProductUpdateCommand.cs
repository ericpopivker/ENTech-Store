﻿using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductUpdateCommand : DbContextCommandBase<ProductUpdateRequest, ProductUpdateResponse>
	{
		public ProductUpdateCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override ProductUpdateResponse Execute(ProductUpdateRequest request)
		{
			return new ProductUpdateResponse
			{
				IsSuccess = true
			};
		}
	}
}