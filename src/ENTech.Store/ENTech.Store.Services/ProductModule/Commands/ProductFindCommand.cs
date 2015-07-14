using System;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductFindCommand : DbContextCommandBase<ProductFindRequest, ProductFindResponse>
	{
		public ProductFindCommand(IUnitOfWork unitOfWork)
			: base(unitOfWork.DbContext, false)
		{
		}

		public override ProductFindResponse Execute(ProductFindRequest request)
		{
			throw new NotImplementedException();
		}
	}
}