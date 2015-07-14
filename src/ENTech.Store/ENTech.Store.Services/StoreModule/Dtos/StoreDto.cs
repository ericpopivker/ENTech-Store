namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class StoreDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Logo { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public AddressDto Address { get; set; }
	}
}