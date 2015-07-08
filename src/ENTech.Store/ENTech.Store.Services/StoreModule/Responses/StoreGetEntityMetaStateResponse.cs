using ENTech.Store.Infrastructure.Services.Repositories;
using ENTech.Store.Infrastructure.Services.Responses;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Responses
{

	public class StoreGetEntityMetaStateResponse : IResponse
	{
		public EntityMetaState EntityMetaState { get; set; }
	}
}