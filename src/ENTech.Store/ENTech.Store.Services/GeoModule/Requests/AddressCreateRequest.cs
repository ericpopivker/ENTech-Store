using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.GeoModule.Dtos;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressCreateRequest : IRequest
	{
		public AddressCreateOrUpdateDto Address { get; set; }
	}
}