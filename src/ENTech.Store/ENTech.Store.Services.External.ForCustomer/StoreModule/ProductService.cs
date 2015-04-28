using ENTech.Store.Services.External.ForCustomer.StoreModule.Commands;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Requests;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Responses;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule
{
	public class ProductService
	{
		public ProductFindResponse Find(ProductFindRequest request)
		{
			var factory = new ExternalCommandFactory();
			var command = factory.Create<ProductFindCommand>();

			return command.Execute(request);
		}

		public ProductGetByIdResponse GetById(ProductGetByIdRequest request)
		{
			var factory = new ExternalCommandFactory();
			var command = factory.Create<ProductGetByIdCommand>();
			return command.Execute(request);
		}
	}
}
