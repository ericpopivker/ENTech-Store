using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Text;

namespace ENTech.Store.Migrations.Migrations
{

	public class SqlGenerator : SqlServerMigrationSqlGenerator
	{
		private string _defaultSchema = "dbo.";

		#region Foreign Keys

		protected override void Generate(DropForeignKeyOperation dfo)
		{
			if (dfo.HasDefaultName)
				dfo.Name = GetForeignKeyName(dfo.DependentTable, dfo.PrincipalTable,dfo.DependentColumns);
			base.Generate(dfo);
		}

		protected override void Generate(AddForeignKeyOperation afo)
		{
			if (afo.HasDefaultName)
				afo.Name = GetForeignKeyName(afo.DependentTable, afo.PrincipalTable, afo.DependentColumns);
			base.Generate(afo);
		}

		#endregion

		#region PrimaryKeys

		protected override void Generate(AddPrimaryKeyOperation apo)
		{
			apo.Name = GetPrimaryKeyName(apo.Table);
			base.Generate(apo);
		}

		protected override void Generate(DropPrimaryKeyOperation apo)
		{
			apo.Name = GetPrimaryKeyName(apo.Table);
			base.Generate(apo);
		}

		protected override void Generate(CreateTableOperation cto)
		{
			if (cto.PrimaryKey != null)
			{
				cto.PrimaryKey.Name = GetPrimaryKeyName(cto.PrimaryKey.Table);
			}
			base.Generate(cto);
		}

		#endregion


		#region Indexes

		protected override void Generate(CreateIndexOperation cio)
		{
			if (cio.HasDefaultName)
				cio.Name = GetIndexName(cio.Table, cio.Columns, cio.IsUnique);
			base.Generate(cio);
		}

		protected override void Generate(DropIndexOperation dio)
		{
			if (dio.HasDefaultName)
				dio.Name = GetIndexName(dio.Table, dio.Columns, null);
			base.Generate(dio);
		}

		#endregion

		private string GetForeignKeyName(string dependentTable, string principalTable, IList<string> dependentColumns)
		{
			var keyName = String.Format("FK_{0}_{1}", StripDbo(dependentTable), StripDbo(principalTable));

			foreach (var dependentColumn in dependentColumns)
			{
				keyName += "_" + dependentColumn;
			}

			return keyName;
		}

		private string GetPrimaryKeyName(string table)
		{
			return String.Format("PK_{0}", StripDbo(table));
		}

		private string GetIndexName(string table, IEnumerable<string> columns, bool? isUnique)
		{
			var sb = new StringBuilder();
			sb.Append(String.Format("{0}_", isUnique.GetValueOrDefault(false) ? "UQ" : "IX"));
			sb.Append(StripDbo(table));
			foreach (var column in columns)
			{
				sb.Append(String.Format("_{0}", column));
			}
			return sb.ToString();
		}

		private string StripDbo(string table)
		{
			return table.StartsWith(_defaultSchema) ? table.Replace(_defaultSchema, "") : table;
		}
	}
}