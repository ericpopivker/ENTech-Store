using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressCreateRequest : IRequest<AddressCreateResponse>
	{
		public AddressCreateOrUpdateDto Address { get; set; }
	}
}