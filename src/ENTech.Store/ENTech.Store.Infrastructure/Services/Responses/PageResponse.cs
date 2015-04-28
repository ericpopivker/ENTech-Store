using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class FindResponse<TDto> : InternalResponse
	{
		public int TotalPages { get; set; }

		public int TotalItems { get; set; }

		public ICollection<TDto> Items { get; set; }
	}
}
