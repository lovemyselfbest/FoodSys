using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Web.Mvc;
using Project.Common;
using System.Text.RegularExpressions;

namespace Project.Web.Base.Utility
{

	public enum EnumPaginatePageSizeOption
	{
		样式一 = 0,
		样式二 = 1,
		样式三 = 2
	}

	public class PaginateHelper
	{
		public int PageSize { get; private set; }
		public int PageIndex { get;set; }
		public int TotalCount { get; private set; }
		public int TotalPages { get; private set; }
		public string Url { get; set; }
		public string EmptyTip { get; set; }
		public EnumPaginatePageSizeOption PaginatePageSizeOption { get; set; }
		/// <summary>
		/// 搜索对象名称
		/// </summary>
		public string SearchClassObjName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="totalCount"></param>
		/// <param name="pageIndex"></param>
		/// <param name="url"></param>
		public PaginateHelper(int pageSize, int totalCount, int pageIndex, string url)
			: this(pageSize, totalCount, pageIndex, 5, url) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="totalCount"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pixCount"></param>
		/// <param name="url"></param>
		private PaginateHelper(int pageSize, int totalCount, int pageIndex, int pixCount, string url)
		{
			Url = url;
			PageIndex = pageIndex;
			PageSize = pageSize;
			TotalCount = totalCount;
			TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
			TotalCount = totalCount;
		}

		public bool HasPreviousPage
		{
			get { return PageIndex > 0; }
		}
		public bool HasNextPage
		{
			get { return PageIndex + 1 < TotalPages; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize">每一页记录总数</param>
		/// <param name="totalCount">总记录数</param>
		/// <param name="pageIndex">当前页</param>
		/// <param name="searchClassObj">查询实体</param>
		/// <param name="searchClassObjName">查询实体名称</param>
		/// <param name="url">前缀URL</param>
		/// <returns></returns>
		public static PaginateHelper ConstructPaginate(int pageSize, int totalCount, int pageIndex, object searchClassObj, string searchClassObjName, string url)
		{
			PaginateHelper pHelper = new PaginateHelper(pageSize, totalCount, pageIndex, url);
			pHelper.SearchClassObjName = searchClassObjName;
			url = url == null ? HttpContext.Current.Request.RawUrl : url;
			url = url.Contains("?") ? url + "&" : url + "?";
			url = RemoveUrlPageIndex(searchClassObjName, url);
			string contructPart = pHelper.ConstructSearchCondition(searchClassObj, ref url);
			pHelper.Url = url + contructPart;
			return pHelper;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize">每一页记录总数</param>
		/// <param name="totalCount">总记录数</param>
		/// <param name="pageIndex">当前页</param>
		/// <param name="searchClassObj">查询实体</param>
		/// <param name="searchClassObjName">查询实体名称</param>
		/// <returns></returns>
		public static PaginateHelper ConstructPaginate(int pageSize, int totalCount, int pageIndex, object searchClassObj, string searchClassObjName)
		{
			return ConstructPaginate(pageSize, totalCount, pageIndex, searchClassObj, searchClassObjName, null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize">每一页记录总数</param>
		/// <param name="totalCount">总记录数</param>
		/// <param name="pageIndex">当前页</param>
		/// <param name="url">前缀URL</param>
		/// <returns></returns>
		public static PaginateHelper ConstructPaginate(int pageSize, int totalCount, int pageIndex, string url)
		{
			return ConstructPaginate(pageSize, totalCount, pageIndex, null,string.Empty,string.Empty);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageSize">每一页记录总数</param>
		/// <param name="totalCount">总记录数</param>
		/// <param name="pageIndex">当前页</param>
		/// <returns></returns>
		public static PaginateHelper ConstructPaginate(int pageSize, int totalCount, int pageIndex)
		{
			return ConstructPaginate(pageSize, totalCount, pageIndex, string.Empty, string.Empty, string.Empty);
		}

		/// <summary>
		/// remove pageIndex in URL
		/// </summary>
		/// <param name="searchClassObjName"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		private static string RemoveUrlPageIndex(string searchClassObjName, string url)
		{
			//replace url pageIndex
			string pattern = string.IsNullOrEmpty(searchClassObjName) ? string.Format(@"{0}=[^&]*&", "PageIndex") : string.Format(@"{0}.{1}=[^&]*&", searchClassObjName, "_PageIndex");
			Regex regex = new Regex(pattern);
			url = regex.Replace(url, "");
			return url;
		}

		private string ConstructSearchCondition(object searchClassObj, ref string url)
		{
			if (searchClassObj == null)
				return "";
			SearchClassObjName = string.IsNullOrEmpty(SearchClassObjName) ? "" : SearchClassObjName;
			StringBuilder sb = new StringBuilder();
			Type searchClassType = searchClassObj.GetType();
			PropertyInfo[] propertyInfos = null;
			propertyInfos = searchClassType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
			foreach (var item in propertyInfos)
			{
				if (Attribute.GetCustomAttribute(item, typeof(PaginateNotComposeAttribute)) != null)
					continue;
				object propertyValue = item.GetValue(searchClassObj, null);
				//移除url 中之前拼的条件
				string patttern = string.Format(@"{0}.{1}=[^&]*&", SearchClassObjName, item.Name);
				Regex regex = new Regex(patttern);
				url = regex.Replace(url, "");

				if ( propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
					continue;
				if (propertyValue.GetType() == typeof(DateTime))
					propertyValue = ((DateTime)propertyValue).ToString("yyyy-MM-dd");
				string parameter = string.Format("{0}.{1}={2}", SearchClassObjName, item.Name, HttpUtility.UrlEncode(propertyValue.ToString()));
				sb.Append(parameter);
				sb.Append("&");
			}

			string temp = sb.ToString();
			if (string.IsNullOrEmpty(temp))
				return "";
			return temp;

		}

		/// <summary>
		/// 多少条每页
		/// </summary>
		/// <returns></returns>
		public SelectList PageSizeOptions
		{
			get
			{
				switch (PaginatePageSizeOption)
				{
					case EnumPaginatePageSizeOption.样式一:
						return PageSizeOptions1;
					case EnumPaginatePageSizeOption.样式二:
						return PageSizeOptions2;
					case EnumPaginatePageSizeOption.样式三:
						return PageSizeOptions3;
					default:
						return PageSizeOptions1;
				}
			}
		}

		/// <summary>
		/// 分页页数列表样式一
		/// </summary>
		private static SelectList pageSizeOptions1;
		private static SelectList PageSizeOptions1
		{
			get
			{
				if (pageSizeOptions1 != null)
					return pageSizeOptions1;
				List<SelectListItem> list = null;
				list = new List<SelectListItem>() { 
									 new SelectListItem(){ Text="10", Value="10"},
									 new SelectListItem(){ Text="20", Value="20", Selected=true},
									 new SelectListItem(){ Text="40", Value="40"},
									 new SelectListItem(){ Text="60", Value="60"},
									 new SelectListItem(){ Text="100", Value="100"},
									 new SelectListItem(){ Text="200", Value="200"}
								};
				pageSizeOptions1 = new SelectList(list, "Value", "Text");
				return pageSizeOptions1;

			}
		}

		/// <summary>
		/// 分页页数列表样式二
		/// </summary>
		private static SelectList pageSizeOptions2;
		private static SelectList PageSizeOptions2
		{
			get
			{
				if (pageSizeOptions2 != null)
					return pageSizeOptions2;
				List<SelectListItem> list = null;
				list = new List<SelectListItem>() { 
									 new SelectListItem(){ Text="10", Value="10"},
									 new SelectListItem(){ Text="20", Value="20", Selected=true},
									 new SelectListItem(){ Text="40", Value="40"},
									 new SelectListItem(){ Text="60", Value="60"},
									 new SelectListItem(){ Text="100", Value="100"},
									 new SelectListItem(){ Text="200", Value="200"},
									 new SelectListItem(){ Text="全部", Value="200000"}
								};
				pageSizeOptions2 = new SelectList(list, "Value", "Text");
				return pageSizeOptions2;

			}
		}

		/// <summary>
		/// 分页页数列表样式三
		/// </summary>
		private static SelectList pageSizeOptions3;
		private static SelectList PageSizeOptions3
		{
			get
			{
				if (pageSizeOptions3 != null)
					return pageSizeOptions3;
				List<SelectListItem> list = null;
				list = new List<SelectListItem>() { 
									 new SelectListItem(){ Text="10", Value="10"},
									 new SelectListItem(){ Text="20", Value="20", Selected=true},
									 new SelectListItem(){ Text="40", Value="40"},
									 new SelectListItem(){ Text="60", Value="60"},
									 new SelectListItem(){ Text="100", Value="100"},
									 new SelectListItem(){ Text="200", Value="200"}
								};
				pageSizeOptions3 = new SelectList(list, "Value", "Text");
				return pageSizeOptions3;

			}
		}

	}
}
