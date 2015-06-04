using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class FindResponse<TEntity> : Response
	{
		public IEnumerable<TEntity> Items { get; set; }
	}
}
