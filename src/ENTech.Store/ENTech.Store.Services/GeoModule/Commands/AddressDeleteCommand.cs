using System;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressDeleteCommand : CommandBase<AddressDeleteRequest, AddressDeleteResponse>
	{
		public AddressDeleteCommand(bool requiresTransaction) : base(requiresTransaction)
		{
		}

		public override AddressDeleteResponse Execute(AddressDeleteRequest request)
		{
			throw new NotImplementedException();
		}
	}
}