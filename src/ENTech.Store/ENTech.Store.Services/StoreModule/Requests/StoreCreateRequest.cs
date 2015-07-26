using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreCreateRequest : IRequest
	{
		public StoreCreateDto Store { get; set; }
	}
}