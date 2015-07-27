using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreCreateRequest : IRequest<StoreCreateResponse>
	{
		public StoreCreateDto Store { get; set; }
	}
}