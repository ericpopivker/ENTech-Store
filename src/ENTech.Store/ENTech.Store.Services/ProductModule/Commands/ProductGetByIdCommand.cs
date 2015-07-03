using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Queries;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.ProductModule.Commands
{
	public class ProductGetByIdCommand : DbContextCommandBase<ProductGetByIdRequest, ProductGetByIdResponse>
	{
		public ProductGetByIdCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
		}

		public override ProductGetByIdResponse Execute(ProductGetByIdRequest request)
		{
			var query = new ProductGetByIdQuery();
			var result = query.Execute(DbContext, new ProductGetByIdQuery.Criteria
			{
				Id = request.Id.Value
			});
			return new ProductGetByIdResponse
			{
				Item = result
			};
		}
	}
}