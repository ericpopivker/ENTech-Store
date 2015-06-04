using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class FindResponse<TEntity> : InternalResponse
	{
		public IEnumerable<TEntity> Items { get; set; }
	}
}
