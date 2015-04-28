using System.ComponentModel.DataAnnotations;
using ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Dtos;

namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Requests
{
	public class CustomerCreateRequest : ExternalRequestBase
	{
		[Required]
		public CustomerDto Customer { get; set; }
	}
}