namespace ENTech.Store.Infrastructure.Services.Dtos
{
	public class ReportRowCellDto<TColumnEnum>
	{
		public TColumnEnum Column { get; set; }
	}
	public class CustomFieldReportRowCellDto<TColumnEnum> : ReportRowCellDto<TColumnEnum>
	{
		public int CustomFieldId { get; set; }
		public string FormattedValue { get; set; }
		public string Label { get; set; }
	}

	public class ReportRowCellDto<TColumnEnum, TValue> : ReportRowCellDto<TColumnEnum>
	{
		public TValue Value { get; set; }
	}
}