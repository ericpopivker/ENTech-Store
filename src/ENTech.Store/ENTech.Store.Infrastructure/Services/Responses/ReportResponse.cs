using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Dtos;

namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class ReportResponse<TColumnEnum> : IResponse
	{
		public IEnumerable<TColumnEnum> Columns { get; set; }
		public IEnumerable<AdditionalColumn> CustomFieldColumns { get; set; }
		public IEnumerable<AdditionalColumn> ScheduleItemColumns { get; set; }
		public IEnumerable<ReportRowDto<TColumnEnum>> Rows { get; set; }
		public DateTime WasRunAt { get; set; }
		public int TotalRowCount { get; set; }
	}

	public class AdditionalColumn
	{
		public int Id { get; set; }
		public string Label { get; set; }
	}
}