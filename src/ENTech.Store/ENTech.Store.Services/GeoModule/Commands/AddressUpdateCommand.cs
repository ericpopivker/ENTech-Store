using System;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressUpdateCommand : CommandBase<AddressUpdateRequest, AddressUpdateResponse>
	{
		public AddressUpdateCommand(bool requiresTransaction) : base(requiresTransaction)
		{
		}

		public override AddressUpdateResponse Execute(AddressUpdateRequest request)
		{
			throw new NotImplementedException();
		}
	}
}