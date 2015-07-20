using System;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductFindCommand : CommandBase<ProductFindRequest, ProductFindResponse>
	{
		public ProductFindCommand()
			: base(false)
		{
		}

		public override ProductFindResponse Execute(ProductFindRequest request)
		{
			throw new NotImplementedException();
		}
	}
}