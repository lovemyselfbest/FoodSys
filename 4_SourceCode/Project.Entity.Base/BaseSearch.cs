using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Common;

namespace Project.Entity.Base
{
	public class BaseSearch
	{
		private int? pageSize;

		/// <summary>
		/// 分页记录条数
		/// </summary>
		public int? _PageSize
		{
			get
			{
				return  pageSize ?? Consts.PAGE_SIZE;
			}
			set
			{
				pageSize = value ?? Consts.PAGE_SIZE;
			}
		}

		private int? pageIndex;

		/// <summary>
		/// 分页索引
		/// </summary>
		[PaginateNotCompose]
		public int? _PageIndex
		{
			get { return pageIndex ?? 0; }
			set { pageIndex = value; }
		}

		/// <summary>
		/// 排序列名称
		/// </summary>
		public string _OrderName { get; set; }

		/// <summary>
		/// 排序方向
		/// </summary>
		private string orderDirection;
		public string _OrderDirection
		{
			get
			{
				return orderDirection == null ? ((int)EnumOrder.ASC).ToString() : orderDirection;
			}
			set
			{
				orderDirection = value ?? ((int)EnumOrder.ASC).ToString();
			}
		}

		/// <summary>
		/// 获取排序方向
		/// </summary>
		[PaginateNotCompose]
		public EnumOrder EnumOrderDirection
		{
			get
			{
				return (EnumOrder)Convert.ToInt32(_OrderDirection);
			}
		}

		private string commonSearchCondition;
		/// <summary>
		/// 页面右上角通用的查询条件
		/// </summary>
		public string _CommonSearchCondition
		{
			get
			{
				return "请输入查询条件".Equals(commonSearchCondition) ? null : commonSearchCondition;
			}
			set
			{
				commonSearchCondition = value;
			}
		}
	}
}