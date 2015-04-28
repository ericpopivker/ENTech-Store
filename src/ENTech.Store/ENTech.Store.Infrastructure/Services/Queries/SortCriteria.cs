using System;
using ENTech.Store.Infrastructure.Attributes;
using ENTech.Store.Infrastructure.Enums;

namespace ENTech.Store.Infrastructure.Services.Queries
{
	public class SortCriteria
	{
		private SortCriteria()
		{
			
		}

		public string Field { get; private set; }

		public object SortField { get; private set; }

		public SortDirection Direction { get; private set; }

		public static SortCriteria Create<TSortField>(TSortField sortField, SortDirection direction) where TSortField : struct, IConvertible
		{
			if(!typeof(TSortField).IsEnum) throw new InvalidOperationException("TSortField is not enum");

			var criteria = new SortCriteria();
			criteria.Direction = direction;
			criteria.Field = sortField.GetStringValue();
			criteria.SortField = sortField;
			return criteria;
		}
	}
}