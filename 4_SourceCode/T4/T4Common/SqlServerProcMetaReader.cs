using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using T4Common.Properties;
using System.Runtime.InteropServices;

namespace T4Common
{
	public class SqlServerProcMetaReader
	{
		private static DataTable emptyDataTable = new DataTable();
		private string connectionString;
		private SqlConnection connection;
		private Dictionary<string, Dictionary<string,bool>> dictionaryDefault;
		private Dictionary<string, List<SpColumn>> spResultMeta;
		private Dictionary<string, List<SpColumn>> spParaMeta;
		private List<StoreProcedure> storeProcedures;
		private Dictionary<string, string> spFailed;
		public Dictionary<string, string> SpFailed
		{
			get { return spFailed; }
			set { spFailed = value; }
		}


		public Dictionary<string, List<SpColumn>> RetriveStoreProcedureResultMeta(Dictionary<string, string> storeProcedureFailed, string[] storeProcedureNames, [Optional, DefaultParameterValue(null)] Dictionary<string, SqlParameter[]> storeProcedureParameters)
		{
			var storeProcedureResultMeta = new Dictionary<string, List<SpColumn>>();
			SqlCommand innerCommand = new SqlCommand();
			SqlDataReader innerReader = null;
			innerCommand.CommandType = CommandType.StoredProcedure;
			innerCommand.Connection = connection;
			for (int i = 0; i < storeProcedureNames.Length; i++)
			{
				try
				{
					List<SpColumn> columns = new List<SpColumn>();
					
					innerCommand.CommandText = storeProcedureNames[i];
					innerCommand.Parameters.Clear();
					if (storeProcedureParameters == null)
					{
						SqlParameterCollection collection = GetParameterCollectionByProcedure(storeProcedureNames[i]);
						for (int j = 0; j < collection.Count; j++)
						{
							if (collection[j].IsNullable)
								continue;
							SqlParameter newParameter = new SqlParameter(collection[j].ParameterName, collection[j].Value);
							innerCommand.Parameters.Add(newParameter);
						}
					}
					else
					{
						innerCommand.Parameters.AddRange(storeProcedureParameters[storeProcedureNames[i]]);
					}
					if (connection.State == System.Data.ConnectionState.Closed)
						connection.Open();
					innerReader = innerCommand.ExecuteReader(CommandBehavior.CloseConnection);
					DataTable table = innerReader.GetSchemaTable();
					if (table == null)
					{
						table = emptyDataTable;
						innerReader.Close();
					}

					int emptyColumnNameSerial = 0;
					for (int k = 0; k < table.Rows.Count; k++)
					{
						SpColumn spColumn = new SpColumn();
						spColumn.CName = table.Rows[k]["ColumnName"].ToString().Trim() == "" ? "Column" + (++emptyColumnNameSerial) : table.Rows[k]["ColumnName"].ToString().Trim();
						spColumn.IsNullable = true;
						spColumn.CType = DataTypeMapper.MapFromDBType(table.Rows[k]["DataTypeName"].ToString());
						columns.Add(spColumn);
					}
					storeProcedureResultMeta.Add(storeProcedureNames[i], columns);
					innerReader.Close();
				}
				catch (Exception e)
				{
					storeProcedureFailed.Add(storeProcedureNames[i], e.Message);
				}
				finally
				{
					if (innerReader!=null && !innerReader.IsClosed)
						innerReader.Close();
				}
			}
			return storeProcedureResultMeta;
		}

		public List<SpColumn> RetriveStoreProcedureParameterMeta(string storeProcedureName)
		{
			var result = RetriveStoreProcedureParameterMeta(new String[] { storeProcedureName });
			return result.FirstOrDefault().Value;
		}

		public Dictionary<string, List<SpColumn>> RetriveStoreProcedureParameterMeta(string[] storeProcedureNames)
		{
			var stroePorcedureParameterMeta = new Dictionary<string, List<SpColumn>>();
			for (int i = 0; i < storeProcedureNames.Length; i++)
			{
				List<SpColumn> columns = new List<SpColumn>();
				SqlParameterCollection collection = GetParameterCollectionByProcedure(storeProcedureNames[i]);
				for (int j = 0; j < collection.Count; j++)
				{
					SpColumn spColumn = new SpColumn();
					spColumn.CName = collection[j].ParameterName;
					spColumn.IsNullable = collection[j].IsNullable;
					spColumn.CType = DataTypeMapper.MapFromDBType(collection[j].SqlDbType.ToString());
					spColumn.Length = collection[j].Size;
					columns.Add(spColumn);
				}
				stroePorcedureParameterMeta.Add(storeProcedureNames[i], columns);
			}
			return stroePorcedureParameterMeta;
		}

		public SqlParameter[] RetriveParametersWithDefaultValue(string procedureName)
		{
			
			SqlParameterCollection collection = GetParameterCollectionByProcedure(procedureName);
			List<SqlParameter> parameters = new List<SqlParameter>();
			for (int j = 0; j < collection.Count; j++)
			{
				if (collection[j].IsNullable)
					continue;
				SqlParameter newParameter = new SqlParameter(collection[j].ParameterName, collection[j].Value);
				parameters.Add(newParameter);
			}
			return parameters.ToArray();
		}

		public SqlParameterCollection GetParameterCollectionByProcedure(string procedureName)
		{
			SqlCommand innerCommand = new SqlCommand() { Connection = connection };
			innerCommand.CommandType = System.Data.CommandType.StoredProcedure;
			innerCommand.CommandText = procedureName;
			if (connection.State == ConnectionState.Closed)
				connection.Open();
			SqlCommandBuilder.DeriveParameters(innerCommand);
			innerCommand.Parameters.RemoveAt(0);
			for (int i = 0; i < innerCommand.Parameters.Count; i++)
			{
				bool result = IsParameterHasDefaultValue(procedureName, innerCommand.Parameters[i].ParameterName);
				innerCommand.Parameters[i].IsNullable = result;
				if (!result)
				{
					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(string))
						innerCommand.Parameters[i].Value = string.Empty;
					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(DateTime))
						innerCommand.Parameters[i].Value = DateTime.Now;
					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(long) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(int) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(double) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(float) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(Single) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(Int16) ||
						DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(decimal)
						)
						innerCommand.Parameters[i].Value = 0;
					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(Boolean))
						innerCommand.Parameters[i].Value = true;

					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(Guid))
						innerCommand.Parameters[i].Value = new Guid();

					if (DataTypeMapper.MapFromDBType(innerCommand.Parameters[i].SqlDbType.ToString()) == typeof(string))
						innerCommand.Parameters[i].Value = string.Empty;

				}
			}
			return innerCommand.Parameters;
		}


		public Dictionary<string, List<SpColumn>> SpResultMeta
		{
			get
			{
				if (spResultMeta != null)
					return spResultMeta;
				spFailed = new Dictionary<string, string>();
				spResultMeta = new Dictionary<string, List<SpColumn>>();
				spResultMeta = RetriveStoreProcedureResultMeta(spFailed, StoreProcedures.Select(x => x.Name).ToArray(),null);
				return spResultMeta;
			}
		}

		public Dictionary<string, List<SpColumn>> SpParaMeta
		{
			get
			{
				if (spParaMeta != null)
					return spParaMeta;
				spParaMeta = RetriveStoreProcedureParameterMeta(StoreProcedures.Select(x => x.Name).ToArray());
				return spParaMeta;
			}
		}

		public List<StoreProcedure> StoreProcedures
		{
			get
			{
				if (storeProcedures != null)
					return storeProcedures;
				SqlDataReader innerReader;
				SqlCommand innerCommand = new SqlCommand() { Connection = connection };
				storeProcedures = new List<StoreProcedure>();
				if (connection.State == System.Data.ConnectionState.Closed)
					connection.Open();
				innerCommand.CommandText = @"select * from sys.procedures where [name] like 'UP_%' order by [name]";
				innerReader = innerCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				while (innerReader.Read())
				{
					storeProcedures.Add(new StoreProcedure() { Name = innerReader.GetString(innerReader.GetOrdinal("name")) });
				}
				innerReader.Close();
				return storeProcedures;
			}
		}

		public List<StoreProcedure> AllStoreProcedures
		{
			get
			{
				if (storeProcedures != null)
					return storeProcedures;
				SqlDataReader innerReader;
				SqlCommand innerCommand = new SqlCommand() { Connection = connection };
				storeProcedures = new List<StoreProcedure>();
				if (connection.State == System.Data.ConnectionState.Closed)
					connection.Open();
				innerCommand.CommandText = @"select * from sys.procedures order by [name]";
				innerReader = innerCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				while (innerReader.Read())
				{
					storeProcedures.Add(new StoreProcedure() { Name = innerReader.GetString(innerReader.GetOrdinal("name")) });
				}
				innerReader.Close();
				return storeProcedures;
			}
		}

		public void FillParameter(StoreProcedure sp)
		{
			List<SpColumn> columns = new List<SpColumn>();
			SqlParameterCollection collection = GetParameterCollectionByProcedure(sp.Name);
			for (int j = 0; j < collection.Count; j++)
			{
				SpColumn spColumn = new SpColumn();
				spColumn.CName = collection[j].ParameterName;
				spColumn.IsNullable = collection[j].IsNullable;
				spColumn.CType = DataTypeMapper.MapFromDBType(collection[j].SqlDbType.ToString());
				columns.Add(spColumn);
			}
			sp.Parameters = columns;
		}

		public SqlServerProcMetaReader(string conStr)
		{
			connectionString = conStr;
			connection = new SqlConnection();
			connection.ConnectionString = connectionString;
			dictionaryDefault = new Dictionary<string, Dictionary<string, bool>>();
			CreateNecessaryProcedure();
		}


		private bool IsParameterHasDefaultValue(string procedureName,string parameterName)
		{
			SqlDataReader innerReader;
			SqlCommand innerCommand = new SqlCommand() { Connection = connection };
			if (dictionaryDefault.ContainsKey(procedureName))
			{
				Dictionary<string,bool> values = dictionaryDefault[procedureName];
				return values[parameterName];
			}
			Dictionary<string, bool> subDictionary = new Dictionary<string, bool>();
			if (connection.State == System.Data.ConnectionState.Closed)
				connection.Open();
			innerCommand.CommandText = "sys_GetParameters";
			innerCommand.CommandType = System.Data.CommandType.StoredProcedure;
			innerCommand.Parameters.Add("@object_name", procedureName);
			innerReader = innerCommand.ExecuteReader(CommandBehavior.CloseConnection);
			while (innerReader.Read())
			{
				subDictionary.Add(innerReader.GetString(innerReader.GetOrdinal("param_name")), innerReader.GetBoolean(innerReader.GetOrdinal("has_default_value")));
			}
			innerReader.Close();
			return subDictionary[parameterName];
		}

		private void CreateNecessaryProcedure()
		{
			if (AllStoreProcedures.Where(x => x.Name == "sys_GetParameters").Count() > 0)
				return;
			SqlCommand innerCommand = new SqlCommand() { Connection = connection };
			innerCommand.CommandText = @"CREATE PROCEDURE [dbo].[sys_GetParameters]
											@object_name NVARCHAR(511)
										AS
										BEGIN
											SET NOCOUNT ON;

											DECLARE 
												@object_id INT,
												@paramID INT,
												@paramName SYSNAME,
												@definition NVARCHAR(MAX),
												@t NVARCHAR(MAX),
												@loc1 INT,
												@loc2 INT,
												@loc3 INT,
												@loc4 INT,
												@has_default_value BIT;

											SET @object_id = OBJECT_ID(@object_name);

											IF (@object_id IS NOT NULL)
											BEGIN
    
												SELECT @definition = OBJECT_DEFINITION(@object_id);

												CREATE TABLE #params
												(
													parameter_id        INT PRIMARY KEY,
													has_default_value    BIT NOT NULL DEFAULT (0)
												);

												DECLARE c CURSOR
												LOCAL FORWARD_ONLY STATIC READ_ONLY
												FOR 
													SELECT 
														parameter_id,
														[name]
													FROM
														sys.parameters
													WHERE
														[object_id] = @object_id;

												OPEN c;

												FETCH NEXT FROM c INTO @paramID, @paramName;

												WHILE (@@FETCH_STATUS = 0)
												BEGIN
    
													SELECT
														@t = SUBSTRING
														(
															@definition,
															CHARINDEX(@paramName, @definition),
															4000
														),
														@has_default_value = 0;
            
													SET @loc1 = COALESCE(NULLIF(CHARINDEX('''', @t), 0), 4000);
													SET @loc2 = COALESCE(NULLIF(CHARINDEX(',', @t), 0), 4000);
													SET @loc3 = NULLIF(CHARINDEX('OUTPUT', @t), 0);
													SET @loc4 = NULLIF(CHARINDEX('AS', @t), 0);
            
													SET @loc1 = CASE WHEN @loc2 < @loc1 THEN @loc2 ELSE @loc1 END;
													SET @loc1 = CASE WHEN @loc3 < @loc1 THEN @loc3 ELSE @loc1 END;
													SET @loc1 = CASE WHEN @loc4 < @loc1 THEN @loc4 ELSE @loc1 END;
            
													IF CHARINDEX('=', LTRIM(RTRIM(SUBSTRING(@t, 1, @loc1)))) > 0
														SET @has_default_value = 1;

													INSERT #params
													(
														parameter_id,
														has_default_value
													)
													SELECT
														@paramID,
														@has_default_value;
            
													FETCH NEXT FROM c INTO @paramID, @paramName;
												END
        
												SELECT 
													sp.[object_id],
													[object_name] = @object_name,
													param_name = sp.[name],
													sp.parameter_id,
													type_name = UPPER(st.[name]),
													sp.max_length,
													sp.[precision],
													sp.scale,
													sp.is_output,
													p.has_default_value
												FROM
													sys.parameters sp
												INNER JOIN
													#params p
												ON
													sp.parameter_id = p.parameter_id
												INNER JOIN
													sys.types st
												ON
													sp.user_type_id = st.user_type_id
												WHERE
													sp.[object_id] = @object_id;
        
												CLOSE c;
												DEALLOCATE c;
												DROP TABLE #params;
											END
										END
										";
			if (connection.State == System.Data.ConnectionState.Closed)
				connection.Open();
			innerCommand.ExecuteNonQuery();
		}

	}
}
