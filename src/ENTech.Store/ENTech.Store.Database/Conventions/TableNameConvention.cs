using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text.RegularExpressions;

namespace ENTech.Store.Database.Conventions
{
	public class TableNameConvention : Convention
	{
		public TableNameConvention()
		{
			Types().Configure(x => x.ToTable(GetTableName(x.ClrType)));
		}

		private string GetTableName(Type type)
		{
			var truncatedName = type.Name.Replace("DbEntity", string.Empty);

			var result = Regex.Replace(truncatedName, ".[A-Z]", m => string.Format("{0}{1}", m.Value[0], m.Value[1]));

			return result;
		}
	}
}