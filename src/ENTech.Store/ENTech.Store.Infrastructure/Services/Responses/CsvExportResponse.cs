using System;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class CsvExportResponse : Response
	{
		public Guid CsvExportTaskId { get; set; }
	}
}