using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Services.Internal.StoreModule.Dtos;

namespace ENTech.Store.Services.Internal.UnitTests.Builders
{
	class SortFieldDtoBuilder : BuilderBase<SortFieldDto, SortFieldDtoBuilder>
	{
		public SortFieldDtoBuilder WithField(string name)
		{
			this.SortField = name;
			return this;
		}

		public SortFieldDtoBuilder Descending()
		{
			this.IsDescending = true;
			return this;
		}

		public override SortFieldDto Build()
		{
			return new SortFieldDto()
				{
					SortField = SortField,
					IsDescending = IsDescending
				};
		}

		protected bool IsDescending { get; set; }

		protected string SortField { get; set; }
	}
}
