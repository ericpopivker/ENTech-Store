﻿namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class AddressDto
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