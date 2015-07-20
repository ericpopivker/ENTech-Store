using System;
using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductGetByIdCommand : CommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
		public ProductGetByIdCommand()
			: base(false)
		{
		}

		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			throw new NotImplementedException();
		}
	}
}