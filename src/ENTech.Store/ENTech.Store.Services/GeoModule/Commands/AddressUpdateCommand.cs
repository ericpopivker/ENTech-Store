using System;
using ENTech.Store.Entities;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressUpdateCommand : DbContextCommandBase<AddressUpdateRequest, AddressUpdateResponse>
	{
		public AddressUpdateCommand(IDbContext dbContext, bool requiresTransaction) : base(dbContext, requiresTransaction)
		{
		}

		public override AddressUpdateResponse Execute(AddressUpdateRequest request)
		{
			throw new NotImplementedException();
		}
	}
}