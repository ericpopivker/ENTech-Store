using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Services.Dtos
{
	public class ReportRowDto<TColumnEnum>
	{
		public IEnumerable<ReportRowCellDto<TColumnEnum>> Cells { get; set; }
	}
}