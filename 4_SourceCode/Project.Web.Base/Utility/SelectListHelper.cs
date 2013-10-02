using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Project.Common;
using System.Runtime.InteropServices;

namespace Project.Web.Base.Utility
{
	public class SelectListHelper
	{

		/// <summary>
		/// 通过一个枚举类型组装成 SelectList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="isAliasAsText">是否用别名作为text,别名需添加属性Alias</param>
		/// <param name="isTextAsValue">是否以Text作为Value</param>
		/// <returns></returns>
		public static SelectList ComposeSelectListFromEnum<T>(bool isAliasAsText = false, bool isTextAsValue = false)
		{
			return ComposeSelectListFromEnum<T>(null, null, isAliasAsText, isTextAsValue);
		}

		/// <summary>
		/// 通过一个枚举类型组装成 SelectList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="defaultFirstOptionText">第一个默认选择的Text</param>
		/// <param name="defaultFirstOptionValue">第一个默认选项的Value</param>
		/// <returns></returns>
		public static SelectList ComposeSelectListFromEnum<T>(string defaultFirstOptionText, string defaultFirstOptionValue)
		{
			return ComposeSelectListFromEnum<T>(defaultFirstOptionText, defaultFirstOptionValue, false, false);
		}

		public static SelectList ComposeSelectListFromEnum<T>(string defaultFirstOptionText, string defaultFirstOptionValue, bool isAliasAsText, bool isTextAsValue)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			if (defaultFirstOptionText != null || defaultFirstOptionValue != null)
			{
				SelectListItem first = new SelectListItem();
				first.Text = defaultFirstOptionText;
				first.Value = defaultFirstOptionValue;
				list.Add(first);
			}

			T[] values = (T[])System.Enum.GetValues(typeof(T));
			for (int i = 0; i < values.Length; i++)
			{
				SelectListItem item = new SelectListItem();
				//item.Text = isAliasAsText ? AliasAttribute.GetAlias<T>(values[i].ToString()) : values[i].ToString();
				item.Text = isAliasAsText ? AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), EnumAttachDataKey.String) : values[i].ToString();
				item.Value = isTextAsValue ? values[i].ToString() : Convert.ToString((int)(Object)values[i]);
				list.Add(item);
			}
			return new SelectList(list, "Value", "Text");
		}

		/// <summary>
		/// 在SelectList 插入第一项
		/// </summary>
		/// <param name="selectList"></param>
		/// <param name="firstOptionName"></param>
		/// <param name="firstOptionValue"></param>
		/// <returns></returns>
		public static SelectList InsertFirstOption(SelectList selectList, string firstOptionName, string firstOptionValue)
		{
			SelectListItem item = new SelectListItem();
			item.Text = firstOptionName;
			item.Value = firstOptionValue;
			if (selectList == null)
			{
				List<SelectListItem> list = new List<SelectListItem>();
				list.Add(item);
				return new SelectList(list, "Value", "Text");
			}
			List<SelectListItem> items = selectList.ToList();
			items.Insert(0, item);
			return new SelectList(items, "Value", "Text");
		}

		/// <summary>
		/// 根据列表返回 selectList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="datasource"></param>
		/// <param name="value"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static SelectList ComposeSelectListFromCollect<T>(IEnumerable<T> datasource, Expression<Func<T, object>> value, Expression<Func<T, object>> text)
		{
			string textPropertName = ReflectionTools.GetPropertyNameFromExpression(text);
			string textValue = ReflectionTools.GetPropertyNameFromExpression(value);
			return new SelectList(datasource, textValue, textPropertName);
		}

		/// <summary>
		/// 根据列表和插入请选择 返回 selectList
		/// </summary>
		/// <typeparam name="T">对象</typeparam>
		/// <param name="datasource"></param>
		/// <param name="value">数据源value</param>
		/// <param name="text">数据源text</param>
		/// <param name="defaultFirstOptionText">第一个默认选择的Text</param>
		/// <param name="defaultFirstOptionValue">第一个默认选项的Value</param>
		/// <returns></returns>
		public static SelectList ComposeSelectListFromCollect<T>(IEnumerable<T> datasource, Expression<Func<T, object>> value, Expression<Func<T, object>> text, string defaultFirstOptionText, string defaultFirstOptionValue)
		{
			string textPropertName = ReflectionTools.GetPropertyNameFromExpression(text);
			string textValue = ReflectionTools.GetPropertyNameFromExpression(value);
			SelectList selectList = new SelectList(datasource, textValue, textPropertName);
			return InsertFirstOption(selectList, defaultFirstOptionText, defaultFirstOptionValue);
		}

		/// <summary>
		///  根据提供的枚举索引进行控制枚举显示
		/// </summary>
		public static SelectList ComposeSelectListFromEnumIndex<T>(string defaultFirstOptionText = null, string defaultFirstOptionValue = null, bool isAliasAsText = false, bool isTextAsValue = false, params int[] indexs)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			if (defaultFirstOptionText != null || defaultFirstOptionValue != null)
			{
				SelectListItem first = new SelectListItem();
				first.Text = defaultFirstOptionText;
				first.Value = defaultFirstOptionValue;
				list.Add(first);
			}

			T[] values = (T[])System.Enum.GetValues(typeof(T));
			for (int i = 0; i < values.Length; i++)
			{
				for (int j = 0; j < indexs.Length; j++)
				{
					if (indexs[j] == i)
					{
						SelectListItem item = new SelectListItem();
						//item.Text = isAliasAsText ? AliasAttribute.GetAlias<T>(values[i].ToString()) : values[i].ToString();
						item.Text = isAliasAsText ? AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), EnumAttachDataKey.String) : values[i].ToString();
						item.Value = isTextAsValue ? values[i].ToString() : Convert.ToString((int)(Object)values[i]);
						list.Add(item);
					}
				}
			}
			return new SelectList(list, "Value", "Text");
		}

		/// <summary>
		/// true or false selectList
		/// </summary>
		/// <returns></returns>
		public static SelectList GetBitSelectList()
		{
			return GetBitSelectList(null, null);
		}

		/// <summary>
		///  true or false selectList
		/// </summary>
		/// <param name="defaultFirstOptionText"></param>
		/// <param name="defaultFirstOptionValue"></param>
		/// <returns></returns>
		public static SelectList GetBitSelectList(string defaultFirstOptionText, string defaultFirstOptionValue)
		{
			IList<SelectListItem> items = new List<SelectListItem>();
			if (!string.IsNullOrEmpty(defaultFirstOptionText))
				items.Add(new SelectListItem() { Text = defaultFirstOptionText, Value = defaultFirstOptionValue });

			items.Add(new SelectListItem() { Text = "是", Value = "true" });
			items.Add(new SelectListItem() { Text = "否", Value = "false" });
			return ComposeSelectListFromCollect<SelectListItem>(items, x => x.Value, x => x.Text);
		}

		/// <summary>
		/// 获得空列表
		/// </summary>
		/// <returns></returns>
		public static SelectList GetEmptySelectList()
		{
			return GetEmptySelectList("--请选择--", "");
		}

		/// <summary>
		/// 获得空列表
		/// </summary>
		public static SelectList GetEmptySelectList(string defaultFirstOptionText, string defaultFirstOptionValue)
		{
			SelectList emptySelectList = null;
			List<SelectListItem> list = new List<SelectListItem>();
			emptySelectList = SelectListHelper.InsertFirstOption(new SelectList(list), defaultFirstOptionText, defaultFirstOptionValue);
			return emptySelectList;
		}


	}

}
