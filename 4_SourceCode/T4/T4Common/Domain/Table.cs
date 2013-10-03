using System.Collections.Generic;
using System;

namespace T4Common.Domain
{
	/// <summary>
	/// Defines a database table entity.
	/// </summary>
	[Serializable]
	public class Table
	{
		private string name;

		public Table()
		{
			ForeignKeys = new List<ForeignKey>();
			Columns = new List<Column>();
			HasManyRelationships = new List<HasMany>();
		}

		public string Name
		{
			get
			{
				return string.IsNullOrWhiteSpace(EntityName) ? name : EntityName;
			}
			set { name = value; }
		}

		public EnumTableType TableType { get; set; }
		public string Owner { get; set; }
		public PrimaryKey PrimaryKey { get; set; }
		public IList<ForeignKey> ForeignKeys { get; set; }
		public IList<Column> Columns { get; set; }
		public IList<HasMany> HasManyRelationships { get; set; }
		public string EntityName { get; set; }
		public string Remark { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	[Serializable]
	public class HasMany
	{
		public string Reference { get; set; }
		public string ReferenceColumn { get; set; }
	}

	/// <summary>
	/// Defines a database column entity;
	/// </summary>
	[Serializable]
	public class Column
	{
		/// <summary>
		/// 数据库字段名称
		/// </summary>
		public string Name { get; set; }
		public string Remark { get; set; }
		public string DataType { get; set; }
		public int? DataLength { get; set; }
		public bool IsNullable { get; set; }
		public bool IsPrimaryKey { get; set; }
		public bool IsForeignKey { get; set; }
		public bool IsUnique { get; set; }
		public string MappedDataType { get; set; }
		public bool IsIdentity { get; set; }
		/// <summary>
		/// 如果Name是数据库关键字，则对其进行处理，加_Keyword
		/// </summary>
		public string LegalName
		{
			get
			{
				if (DataTypeMapper.CSharpKeywords.Contains(Name))
					return Name + "_Keyword";
				return Name;
			}
		}
		public string TableName { get; set; }
	}

	public class ForeignKeyColumn : Column
	{
		public string References { get; set; }
	}

	public interface IPrimaryKey
	{
		PrimaryKeyType KeyType { get; }
		IList<Column> Columns { get; set; }
	}

	public abstract class AbstractPrimaryKey : IPrimaryKey
	{
		protected AbstractPrimaryKey()
		{
			Columns = new List<Column>();
		}

		#region IPrimaryKey Members

		public abstract PrimaryKeyType KeyType { get; }
		public IList<Column> Columns { get; set; }

		#endregion
	}

	/// <summary>
	/// Defines a primary key entity.
	/// </summary>
	[Serializable]
	public class PrimaryKey
	{
		public PrimaryKey()
		{
			Columns = new List<Column>();
		}

		public PrimaryKeyType Type { get; set; }
		public IList<Column> Columns { get; set; }
		public bool IsGeneratedBySequence { get; set; } // Oracle only.
		public bool IsSelfReferencing { get; set; }
	}

	/// <summary>
	/// Defines a composite key entity.
	/// </summary>
	[Serializable]
	public class CompositeKey : AbstractPrimaryKey
	{
		public override PrimaryKeyType KeyType
		{
			get { return PrimaryKeyType.CompositeKey; }
		}
	}

	/// <summary>
	/// Defines a foreign key entity.
	/// </summary>
	[Serializable]
	public class ForeignKey
	{
		/// <summary>
		/// Foreign key column name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Defines what table the foreign key references.
		/// </summary>
		public string References { get; set; }
	}

	[Serializable]
	public enum EnumTableType
	{
		Table,
		View
	}

}