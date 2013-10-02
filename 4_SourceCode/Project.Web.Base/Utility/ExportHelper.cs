using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Aspose.Cells;
using System.Data;
using System.Net.Mime;
using Project.Common;
using Project.Entity.Base;

namespace Project.Web.Base.Utility
{
	public enum ExportType
	{
		导出本页 = 0,
		导出全部 = 1
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class AsposeExportFileAttribute : System.Attribute
	{

	}

	public class ExportReplace
	{
		public string PropertyName
		{
			get;
			set;
		}

		public Dictionary<string, object> Mappings
		{
			get;
			set;
		}
	}

	public class ExportFunction
	{
		public string PropertyName
		{
			get;
			set;
		}

		public object CallBack
		{
			get;
			set;
		}

		public void SetCallBack<P, PReturn>(Func<P, PReturn> callback)
		{
			CallBack = callback;
		}
	}

	public class ExportHelper
	{

		public static ExportHelper GetInstance()
		{
			return new ExportHelper();
		}
		/// <summary>
		/// 属性替换值对象
		/// </summary>
		private IList<ExportReplace> exportReplaceCollection;

		private IList<ExportFunction> exportFunctionCollection;

		/// <summary>
		/// 导出列
		/// </summary>
		public string ExportFields { get; set; }
		/// <summary>
		/// 导出类型
		/// </summary>
		public ExportType? ExportType { get; set; }

		public static IList<ExportAttribute> ListExportAttributes<T>()
		{
			return ListExportAttributes(typeof(T));
		}

		/// <summary>
		/// 列出所有标记ExportAttribute的属性
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IList<ExportAttribute> ListExportAttributes(Type t)
		{
			MetadataTypeAttribute[] metadataTypes = t.GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().ToArray();
			MetadataTypeAttribute metadata = metadataTypes.FirstOrDefault();
			PropertyInfo[] properties = metadata == null ? t.GetProperties() : metadata.MetadataClassType.GetProperties();
			Type exportAttributeType = typeof(ExportAttribute);
			List<ExportAttribute> exportAttributeList = new List<ExportAttribute>();
			for (int i = 0; i < properties.Length; i++)
			{
				object[] attributes = properties[i].GetCustomAttributes(exportAttributeType, false);
				if (attributes.Length == 0)
					continue;
				ExportAttribute temp = attributes[0] as ExportAttribute;
				temp.PropertyName = properties[i].Name;
				temp.PropertyType = properties[i].PropertyType;
				exportAttributeList.Add(temp);
			}
			return exportAttributeList;
		}

		/// <summary>
		/// 导出到Excel
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sources"></param>
		/// <param name="export"></param>
		/// <param name="fileName"></param>
		public void ExportExcel<T>(IList<T> sources, ExportHelper export, string fileName)
		{
			fileName = string.Format("{0}{1}", fileName, DateTime.Now.ToString("yyyyMMdd_HH_mm_ss"));
			Type t = typeof(T);
			List<Expression<Func<T, object>>> memberExpressions = new List<Expression<Func<T, object>>>();
			string[] array = export.ExportFields.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
			ParameterExpression paramter = Expression.Parameter(t);
			for (int i = 0; i < array.Length; i++)
			{
				MemberExpression memberExpression = Expression.Property(paramter, array[i]);
				UnaryExpression unaryExpression = Expression.Convert(memberExpression, typeof(object));
				Expression<Func<T, object>> tempExpression = Expression.Lambda<Func<T, object>>(unaryExpression, paramter);
				memberExpressions.Add(tempExpression);
			}
			ExportExcel(sources, fileName, memberExpressions.ToArray());
		}

		private void ExportExcel<T>(IList<T> sources, string fileName, params Expression<Func<T, object>>[] members)
		{
			Workbook workbook = new Workbook();
			Worksheet sheet = workbook.Worksheets[0];
			IList<ExportAttribute> willExport = new List<ExportAttribute>();
			IList<ExportAttribute> defaultExport = ListExportAttributes<T>();
			if (members == null || members.Length == 0)
				willExport = defaultExport;
			else
			{
				for (int i = 0; i < members.Length; i++)
				{
					MemberExpression memberExpr = null;
					if (members[i].Body.NodeType == ExpressionType.Convert)
						memberExpr = ((UnaryExpression)members[i].Body).Operand as MemberExpression;
					else if (members[i].Body.NodeType == ExpressionType.MemberAccess)
						memberExpr = members[i].Body as MemberExpression;
					var temp = defaultExport.FirstOrDefault(x => x.PropertyName == memberExpr.Member.Name);
					if (temp != null)
						willExport.Add(temp);
				}
			}

			//fields order.
			willExport = willExport.OrderBy(x => x.Order).ToList();

			DataTable table = new DataTable();
			for (int i = 0; i < willExport.Count; i++)
			{
				DataColumn datacolumn = new DataColumn(willExport[i].DisplayName);
				table.Columns.Add(datacolumn);
				if (willExport[i].Width > 0)
					sheet.Cells.Columns[i].Width = willExport[i].Width;
			}

			Type t = typeof(T);
			foreach (T data in sources)
			{
				DataRow row = table.NewRow();
				foreach (var p in willExport)
				{
					Object value = t.GetProperty(p.PropertyName).GetValue(data, null);
					//判断是否需要做替换
					var exportReplace = exportReplaceCollection == null ? null : exportReplaceCollection.FirstOrDefault(x => x.PropertyName == p.PropertyName);
					if (exportReplace != null)
					{
						object mappingValue = new object();
						if (value != null)
							exportReplace.Mappings.TryGetValue(value.ToString(), out mappingValue);
						else
							mappingValue = "";
						row[p.DisplayName] = mappingValue == null ? "" : mappingValue.ToString();
						continue;
					}
					//判断是否要回调函数
					ExportFunction exportFunction = exportFunctionCollection == null ? null : exportFunctionCollection.FirstOrDefault(x => x.PropertyName == p.PropertyName);
					if (exportFunction != null)
					{
						MethodInfo invokeMethod = exportFunction.CallBack.GetType().GetMethods().First(x => x.Name == "Invoke");
						value = invokeMethod.Invoke(exportFunction.CallBack, new object[] { data });
						row[p.DisplayName] = value;
						continue;
					}

					row[p.DisplayName] = value;
				}
				table.Rows.Add(row);
			}
			if (table.Rows.Count == 0)
				table.Rows.Add(table.NewRow());
			sheet.Cells.ImportDataTable(table, true, 0, 0, true);
			HttpContext.Current.Response.Clear();
			workbook.Save(HttpContext.Current.Response, HttpUtility.UrlEncode(fileName + ".xls", System.Text.Encoding.UTF8), Aspose.Cells.ContentDisposition.Attachment, new OoxmlSaveOptions(SaveFormat.Excel97To2003));
		}

		/// <summary>
		/// 添加属性替换值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyExpression"></param>
		/// <param name="mapping"></param>
		/// <returns></returns>
		public ExportHelper AddMapping<T>(Expression<Func<T, object>> propertyExpression, Dictionary<string, object> mapping)
		{
			if (exportReplaceCollection == null)
				exportReplaceCollection = new List<ExportReplace>();
			exportReplaceCollection.Add(new ExportReplace() { PropertyName = ReflectionTools.GetPropertyNameFromExpression<T>(propertyExpression), Mappings = mapping });
			return this;
		}

		/// <summary>
		/// 添加导出映射，比如某一个字段为枚举值，需要从枚举值进行替换。
		/// </summary>
		/// <typeparam name="T">需要数据源的实体类</typeparam>
		/// <typeparam name="U">枚举类</typeparam>
		/// <param name="propertyExpression">需要数据源的实体类属性</param>
		/// <param name="enumPart">导出枚举的哪个部分？</param>
		/// <returns></returns>
		public ExportHelper AddMapping<T, U>(Expression<Func<T, object>> propertyExpression, EnumPart enumPart)
		{
			if (exportReplaceCollection == null)
				exportReplaceCollection = new List<ExportReplace>();

			U[] values = (U[])System.Enum.GetValues(typeof(U));
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			for (int i = 0; i < values.Length; i++)
			{
				switch (enumPart)
				{
					case EnumPart.Text:
						dictionary.Add(Convert.ToString((int)(Object)values[i]), values[i].ToString());
						break;
					case EnumPart.AttachStringAndText:
						dictionary.Add(values[i].ToString(), AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), EnumAttachDataKey.String));
						break;
					case EnumPart.AttachStringAndValue:
						dictionary.Add(Convert.ToString((int)(Object)values[i]), AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), EnumAttachDataKey.String));
						break;
					default:
						break;
				}
			}
			exportReplaceCollection.Add(new ExportReplace() { PropertyName = ReflectionTools.GetPropertyNameFromExpression<T>(propertyExpression), Mappings = dictionary });
			return this;
		}

		/// <summary>
		/// 添加导出映射，比如某一个字段为枚举值，需要从枚举值进行替换。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="U"></typeparam>
		/// <param name="propertyExpression"></param>
		/// <returns></returns>
		public ExportHelper AddMapping<T, U>(Expression<Func<T, object>> propertyExpression)
		{
			return AddMapping<T, U>(propertyExpression, EnumPart.Text);
		}

		/// <summary>
		/// 布尔型的字段导出
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyExpression"></param>
		/// <returns></returns>
		public ExportHelper AddBitMapping<T>(Expression<Func<T, object>> propertyExpression)
		{
			if (exportReplaceCollection == null)
				exportReplaceCollection = new List<ExportReplace>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add(true.ToString(), "是");
			dictionary.Add(false.ToString(), "否");
			exportReplaceCollection.Add(new ExportReplace() { PropertyName = ReflectionTools.GetPropertyNameFromExpression<T>(propertyExpression), Mappings = dictionary });
			return this;
		}

		/// <summary>
		/// 对某个字段进行单独处理，为某个字段添加委托方法。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="P"></typeparam>
		/// <typeparam name="PReturn"></typeparam>
		/// <param name="propertyExpression"></param>
		/// <param name="callBackFun"></param>
		/// <returns></returns>
		public ExportHelper AddFunction<T, PReturnType>(Expression<Func<T, object>> propertyExpression, Func<T, PReturnType> callBackFun)
		{
			if (exportFunctionCollection == null)
				exportFunctionCollection = new List<ExportFunction>();
			var exportFunction = new ExportFunction { PropertyName = ReflectionTools.GetPropertyNameFromExpression<T>(propertyExpression) };
			exportFunction.SetCallBack<T, PReturnType>(callBackFun);
			exportFunctionCollection.Add(exportFunction);
			return this;
		}

	}
}
