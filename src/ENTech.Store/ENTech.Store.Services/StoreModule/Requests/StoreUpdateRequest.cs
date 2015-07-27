using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreUpdateRequest : IRequest<StoreUpdateResponse>
	{
		[Required]
		public StoreUpdateDto Store { get; set; }
		public int StoreId { get; set; }
	}
}