using System;
using System.Runtime.Serialization;

namespace ENTech.Store.Infrastructure.Services.Filters
{
	public class ListReportFilter<TColumnEnum>
	{
		[DataMember]
		public TColumnEnum Column { get; set; }

		public object Value { get; set; }
	}
	
	public class DateTimeRange
	{
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
	}
}