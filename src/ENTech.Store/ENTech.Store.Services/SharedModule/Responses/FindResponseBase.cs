using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.SharedModule.Responses
{
	public class FindResponseBase<TDto> : IResponse
	{
		public int TotalPages { get; set; }

		public int TotalItems { get; set; }

		public ICollection<TDto> Items { get; set; }
	}
}
