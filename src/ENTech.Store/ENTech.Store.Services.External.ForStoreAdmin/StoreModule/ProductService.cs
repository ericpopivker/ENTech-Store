using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Commands;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Requests;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Responses;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule
{
	public class ProductService
	{
		public ProductCreateResponse Create(ProductCreateRequest request)
		{
			var factory = new ExternalCommandFactory();
			var command = factory.Create<ProductCreateCommand>();
			return command.Execute(request);
		}
	}
}
