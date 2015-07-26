using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.GeoModule.Responses
{
	public class AddressGetEntityMetaStateResponse : IResponse
	{
		public EntityMetaState EntityMetaState { get; set; }
	}
}