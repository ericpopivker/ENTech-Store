using System.ComponentModel.DataAnnotations;

namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Dtos
{
	public class CustomerDto
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

	}
}
