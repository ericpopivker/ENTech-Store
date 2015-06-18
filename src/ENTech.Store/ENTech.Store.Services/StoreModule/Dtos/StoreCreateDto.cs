namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class StoreCreateDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Logo { get; set; }
		public string Phone { get; set; }
		public AddressCreateDto Address { get; set; }
		public string Timezone { get; set; }
	}

	public class AddressCreateDto
	{
		public string City { get; set; }
		public int CountryId { get; set; }
		public int? StateId { get; set; }
		public string StateOther { get; set; }
		public string Street { get; set; }
		public string Street2 { get; set; }
		public string Zip { get; set; }
	}
}