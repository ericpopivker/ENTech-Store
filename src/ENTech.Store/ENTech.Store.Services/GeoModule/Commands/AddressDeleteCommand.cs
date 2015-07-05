using ENTech.Store.Entities;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressDeleteCommand : DbContextCommandBase<AddressDeleteRequest, AddressDeleteResponse>
	{
		public AddressDeleteCommand(IDbContext dbContext, bool requiresTransaction) : base(dbContext, requiresTransaction)
		{
		}

		public override AddressDeleteResponse Execute(AddressDeleteRequest request)
		{
			throw new System.NotImplementedException();
		}
	}
}