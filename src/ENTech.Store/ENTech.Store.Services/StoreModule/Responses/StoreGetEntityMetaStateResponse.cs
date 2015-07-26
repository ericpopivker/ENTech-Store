using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.StoreModule.Responses
{

	public class StoreGetEntityMetaStateResponse : IResponse
	{
		public EntityMetaState EntityMetaState { get; set; }
	}
}