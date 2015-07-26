using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.GeoModule.Dtos;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressUpdateRequest : IRequest
	{
		public int AddressId { get; set; }
		public AddressCreateOrUpdateDto Address { get; set; }
	}
}