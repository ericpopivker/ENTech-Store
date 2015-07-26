namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class StoreCreateDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Logo { get; set; }
		public string Phone { get; set; }
		public AddressDto Address { get; set; }
		public string Timezone { get; set; }
	}
}