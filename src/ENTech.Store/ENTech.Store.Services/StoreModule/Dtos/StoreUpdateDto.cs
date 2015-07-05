using ENTech.Store.Services.GeoModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class StoreUpdateDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public AddressCreateOrUpdateDto Address { get; set; }
		public string Logo { get; set; }
		public string TimezoneId { get; set; }
	}
}