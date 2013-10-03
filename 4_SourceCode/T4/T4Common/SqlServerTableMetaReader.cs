using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using T4Common;
using T4Common.Domain;
using T4Common.Properties;

namespace T4Common
{
	public class SqlServerTableMetaReader : IMetadataReader
	{
		private readonly string connectionStr;

		public SqlServerTableMetaReader(string conStr)
		{
			this.connectionStr = conStr;
		}

		#region IMetadataReader Members

		public IList<Column> GetTableDetails(Table table, string owner)
		{
			var columns = new List<Column>();
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				using (SqlCommand tableDetailsCommand = conn.CreateCommand())
				{
					tableDetailsCommand.CommandText = string.Format(
						@"
							SELECT c.column_name, c.data_type, c.is_nullable, tc.constraint_type, c.numeric_precision, c.numeric_scale, c.character_maximum_length,
								COLUMNPROPERTY(object_id('[' + '{1}' + '].[' + '{0}' + ']'), c.column_name, 'IsIdentity') as 'IsIdentity',t1.value as 'remark'
							from information_schema.columns c
								join syscolumns on (
									id = object_id('[' + '{1}' + '].[' + '{0}' + ']')
									and syscolumns.name = c.column_name
								)
								left outer join (
									information_schema.constraint_column_usage ccu
									join information_schema.table_constraints tc on (
										tc.table_schema = ccu.table_schema
										and tc.constraint_name = ccu.constraint_name
										and tc.constraint_type <> 'CHECK'
									)
								) on (
									c.table_schema = ccu.table_schema and ccu.table_schema =  '{1}'
									and c.table_name = ccu.table_name
									and c.column_name = ccu.column_name
								)
								left join  sys.extended_properties t1
									on (
										t1.major_id = object_id(c.table_name) 
										and syscolumns.colid = t1.minor_id
									)
							where c.table_name = '{0}'
							order by c.table_name, c.ordinal_position",
						table.Name, owner);
					using (SqlDataReader sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
					{
						while (sqlDataReader.Read())
						{
							string columnName = sqlDataReader.GetString(0);
							string dataType = sqlDataReader.GetString(1);
							bool isNullable = sqlDataReader.GetString(2).Equals("YES",
																				StringComparison.
																					CurrentCultureIgnoreCase);
							var characterMaxLenth = sqlDataReader["character_maximum_length"] as int?;
							var numericPrecision = sqlDataReader["numeric_precision"] as int?;
							var numericScale = sqlDataReader["numeric_scale"] as int?;
							var remark = sqlDataReader["remark"] as string;
							bool isPrimaryKey =
								(!sqlDataReader.IsDBNull(3)
									 ? sqlDataReader.GetString(3).Equals(
										 SqlServerConstraintType.PrimaryKey.ToString(),
										 StringComparison.CurrentCultureIgnoreCase)
									 : false);
							bool isForeignKey =
								(!sqlDataReader.IsDBNull(3)
									 ? sqlDataReader.GetString(3).Equals(
										 SqlServerConstraintType.ForeignKey.ToString(),
										 StringComparison.CurrentCultureIgnoreCase)
									 : false);
							bool isIdentity = sqlDataReader["IsIdentity"].ToString() == "1" ? true : false;

							columns.Add(new Column
							{
								TableName = table.Name,
								Name = columnName,
								DataType = dataType,
								IsNullable = isNullable,
								IsPrimaryKey = isPrimaryKey,
								//IsPrimaryKey(selectedTableName.Name, columnName)
								IsForeignKey = isForeignKey,
								// IsFK()
								MappedDataType =
									DataTypeMapper.MapFromDBType(dataType).ToString(),
								DataLength = characterMaxLenth,
								IsIdentity = isIdentity,
								Remark = string.IsNullOrEmpty(remark) ? "" : remark
							});

							table.Columns = columns;
						}
						table.PrimaryKey = DeterminePrimaryKeys(table);
						//comment by ben 2011-09-04
						//table.ForeignKeys = DetermineForeignKeyReferences(table);
						//table.HasManyRelationships = DetermineHasManyRelationships(table);
					}
				}
			}
			return columns;
		}


		public IList<Table> RetriveTableDetails()
		{
			List<Table> tables = GetTables("dbo");
			List<Table> tableList = new List<Table>();
			var columns = new List<Column>();
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				using (SqlCommand tableDetailsCommand = conn.CreateCommand())
				{
					tableDetailsCommand.CommandText = string.Format(
						@"
                        SELECT c.column_name, c .data_type, c.is_nullable, tc.constraint_type, c.numeric_precision, c.numeric_scale, c.character_maximum_length,
							COLUMNPROPERTY(object_id(c.table_name), c.column_name, 'IsIdentity') as 'IsIdentity',c.table_name,t1.value as 'remark'
                        from information_schema.columns c
							join syscolumns on (
									id = object_id(c.table_name)
									and syscolumns.name = c.column_name
								)
                            left outer join (
                                information_schema.constraint_column_usage ccu
                                join information_schema.table_constraints tc on (
                                    tc.table_schema = ccu.table_schema
                                    and tc.constraint_name = ccu.constraint_name
                                    and tc.constraint_type <> 'CHECK'
                                )
                            ) on (
                                c.table_schema = ccu.table_schema and ccu.table_schema = 'dbo'
                                and c.table_name = ccu.table_name
                                and c.column_name = ccu.column_name
                            )
                          left join  sys.extended_properties t1 
								on (
										t1.major_id = object_id(c.table_name) 
										and syscolumns.colid = t1.minor_id
									)
							order by c.table_name, c.ordinal_position");
					using (SqlDataReader sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
					{
						while (sqlDataReader.Read())
						{
							string tableName = sqlDataReader["table_name"].ToString();
							string columnName = sqlDataReader.GetString(0);
							string dataType = sqlDataReader.GetString(1);
							bool isNullable = sqlDataReader.GetString(2).Equals("YES",
																				StringComparison.
																					CurrentCultureIgnoreCase);
							var characterMaxLenth = sqlDataReader["character_maximum_length"] as int?;
							var numericPrecision = sqlDataReader["numeric_precision"] as int?;
							var numericScale = sqlDataReader["numeric_scale"] as int?;
							var remark = sqlDataReader["remark"] as string;

							bool isPrimaryKey =
								(!sqlDataReader.IsDBNull(3)
									 ? sqlDataReader.GetString(3).Equals(
										 SqlServerConstraintType.PrimaryKey.ToString(),
										 StringComparison.CurrentCultureIgnoreCase)
									 : false);
							bool isForeignKey =
								(!sqlDataReader.IsDBNull(3)
									 ? sqlDataReader.GetString(3).Equals(
										 SqlServerConstraintType.ForeignKey.ToString(),
										 StringComparison.CurrentCultureIgnoreCase)
									 : false);
							bool isIdentity = sqlDataReader["IsIdentity"].ToString() == "1" ? true : false;

							columns.Add(new Column
							{
								TableName = tableName,
								Name = columnName,
								DataType = dataType,
								IsNullable = isNullable,
								IsPrimaryKey = isPrimaryKey,
								//IsPrimaryKey(selectedTableName.Name, columnName)
								IsForeignKey = isForeignKey,
								// IsFK()
								MappedDataType =
									DataTypeMapper.MapFromDBType(dataType).ToString(),
								DataLength = characterMaxLenth,
								IsIdentity = isIdentity,
								Remark = string.IsNullOrEmpty(remark) ? "" : remark
							});
						}

						for (int i = 0; i < columns.Count; i++)
						{
							Table temp = tableList.FirstOrDefault(x => x.Name == columns[i].TableName);
							if (temp == null)
							{
								temp = new Table() { Name = columns[i].TableName, Remark = tables.FirstOrDefault(x => x.Name == columns[i].TableName).Remark, TableType = GetTableType(columns[i].TableName, tables) };
								temp.Columns.Add(columns[i]);
								tableList.Add(temp);
								continue;
							}
							temp.Columns.Add(columns[i]);
						}
						foreach (var item in tableList)
						{
							item.PrimaryKey = DeterminePrimaryKeys(item);
						}

						//comment by ben 2011-09-04
						//table.ForeignKeys = DetermineForeignKeyReferences(table);
						//table.HasManyRelationships = DetermineHasManyRelationships(table);
					}
				}
			}
			return tableList;
		}

		public EnumTableType GetTableType(string tableName, List<Table> tables)
		{
			return tables.First(x => x.Name == tableName).TableType;
		}

		public IList<string> GetOwners()
		{
			var owners = new List<string>();
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				var tableCommand = conn.CreateCommand();
				tableCommand.CommandText = "SELECT SCHEMA_NAME from INFORMATION_SCHEMA.SCHEMATA";
				var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (sqlDataReader.Read())
				{
					var ownerName = sqlDataReader.GetString(0);
					owners.Add(ownerName);
				}
			}

			return owners;
		}

		public List<Table> GetTables(string owner)
		{
			var tables = new List<Table>();
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				var tableCommand = conn.CreateCommand();
				tableCommand.CommandText = String.Format(@"
															select distinct TABLE_NAME,TABLE_TYPE,t.value as Remark  from information_schema.tables
																	 outer apply(
																		 select top 1 [value] from sys.extended_properties  where object_id(TABLE_NAME)=major_id and minor_id = 0
																) t
																where (table_type like 'BASE TABLE' or table_type like 'VIEW') and TABLE_SCHEMA = '{0}'", owner);
				var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (sqlDataReader.Read())
				{
					var tableName = sqlDataReader.GetString(0);
					var tableType = sqlDataReader.GetString(1);
					var tableRemark = sqlDataReader.GetValue(2);
					tables.Add(new Table { Name = tableName, TableType = tableType == "VIEW" ? EnumTableType.View : EnumTableType.Table, Remark = tableRemark == null ? "" : tableRemark.ToString() });
				}
			}
			tables.Sort((x, y) => x.Name.CompareTo(y.Name));
			return tables;
		}

		public List<string> GetSequences()
		{
			return new List<string>();
		}

		#endregion

		private static PrimaryKey DeterminePrimaryKeys(Table table)
		{
			IEnumerable<Column> primaryKeys = table.Columns.Where(x => x.IsPrimaryKey.Equals(true));

			if (primaryKeys.Count() == 1)
			{
				Column c = primaryKeys.First();
				var key = new PrimaryKey
				{
					Type = PrimaryKeyType.PrimaryKey,
					Columns =
                                      {
                                          new Column
                                              {
                                                  DataType = c.DataType,
                                                  Name = c.Name,
												   IsIdentity=c.IsIdentity
                                              }
                                      }
				};
				return key;
			}
			else
			{
				var key = new PrimaryKey
				{
					Type = PrimaryKeyType.CompositeKey
				};
				foreach (var primaryKey in primaryKeys)
				{
					key.Columns.Add(new Column
					{
						DataType = primaryKey.DataType,
						Name = primaryKey.Name,
						IsIdentity = primaryKey.IsIdentity
					});
				}
				return key;
			}
		}

		private IList<ForeignKey> DetermineForeignKeyReferences(Table table)
		{
			var foreignKeys = table.Columns.Where(x => x.IsForeignKey.Equals(true));
			var tempForeignKeys = new List<ForeignKey>();

			foreach (var foreignKey in foreignKeys)
			{
				tempForeignKeys.Add(new ForeignKey
				{
					Name = foreignKey.Name,
					References = GetForeignKeyReferenceTableName(table.Name, foreignKey.Name)
				});
			}

			return tempForeignKeys;
		}

		private string GetForeignKeyReferenceTableName(string selectedTableName, string columnName)
		{
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				SqlCommand tableCommand = conn.CreateCommand();
				tableCommand.CommandText = String.Format(
					@"
                        select pk_table = pk.table_name
                        from information_schema.referential_constraints c
                        inner join information_schema.table_constraints fk on c.constraint_name = fk.constraint_name
                        inner join information_schema.table_constraints pk on c.unique_constraint_name = pk.constraint_name
                        inner join information_schema.key_column_usage cu on c.constraint_name = cu.constraint_name
                        inner join (
                        select i1.table_name, i2.column_name
                        from information_schema.table_constraints i1
                        inner join information_schema.key_column_usage i2 on i1.constraint_name = i2.constraint_name
                        where i1.constraint_type = 'PRIMARY KEY'
                        ) pt on pt.table_name = pk.table_name
                        where fk.table_name = '{0}' and cu.column_name = '{1}'",
					selectedTableName, columnName);
				object referencedTableName = tableCommand.ExecuteScalar();

				return (string)referencedTableName;
			}
		}

		// http://blog.sqlauthority.com/2006/11/01/sql-server-query-to-display-foreign-key-relationships-and-name-of-the-constraint-for-each-table-in-database/
		private IList<HasMany> DetermineHasManyRelationships(Table table)
		{
			var hasManyRelationships = new List<HasMany>();
			var conn = new SqlConnection(connectionStr);
			conn.Open();
			using (conn)
			{
				using (var command = new SqlCommand())
				{
					command.Connection = conn;
					command.CommandText =
						String.Format(
							@"
                        SELECT DISTINCT 
                            PK_TABLE = b.TABLE_NAME,
                            FK_TABLE = c.TABLE_NAME,
                            FK_COLUMN_NAME = d.COLUMN_NAME
                        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS a 
                          JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS b ON a.CONSTRAINT_SCHEMA = b.CONSTRAINT_SCHEMA AND a.UNIQUE_CONSTRAINT_NAME = b.CONSTRAINT_NAME 
                          JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS c ON a.CONSTRAINT_SCHEMA = c.CONSTRAINT_SCHEMA AND a.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                          JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE d on a.CONSTRAINT_NAME = d.CONSTRAINT_NAME
                        WHERE b.TABLE_NAME = '{0}'
                        ORDER BY 1,2",
							table.Name);
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						hasManyRelationships.Add(new HasMany
						{
							Reference = reader.GetString(1),
							ReferenceColumn = reader["FK_COLUMN_NAME"].ToString()
						});
					}

					return hasManyRelationships;
				}
			}
		}
	}
}
