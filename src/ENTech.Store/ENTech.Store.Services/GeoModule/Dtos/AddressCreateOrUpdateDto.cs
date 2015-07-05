namespace ENTech.Store.Services.GeoModule.Dtos
{
	public class AddressCreateOrUpdateDto
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