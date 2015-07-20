using ENTech.Store.Infrastructure.Database.EF6.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductCreateCommand : CommandBase<ProductCreateRequest, ProductCreateResponse>
	{
		public ProductCreateCommand() : base(false)
		{
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