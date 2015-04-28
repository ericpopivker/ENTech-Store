using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Responses;

namespace ENTech.Store.Services.External
{
	public class FindResponse<TDto> : ExternalResponse
	{
		public int TotalPages { get; set; }

		public int TotalItems { get; set; }

		public ICollection<TDto> Items { get; set; }
	}
}