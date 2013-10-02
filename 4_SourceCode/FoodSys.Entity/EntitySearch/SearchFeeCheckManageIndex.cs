using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 核封帐管理 --> 核帐管理  &&  收款凭证汇总 --> 已汇总凭证
	/// 作者：lifl
	/// 时间：2012-07-23
	/// </summary>
	public class SearchFeeCheckManageIndex : BaseSearch
	{
		/// <summary>
		/// 汇总编号
		/// </summary>
		public string _SumSerialNO { get; set; }

		/// <summary>
		/// 汇总日期开始
		/// </summary>
		public DateTime? _FormSumDate { get; set; }

		/// <summary>
		/// 汇总日期结束
		/// </summary>
		public DateTime? _ToSumDate { get; set; }

		/// <summary>
		/// 汇总部门
		/// </summary>
		public Guid? _SumitDepartment { get; set; }

		/// <summary>
		/// 汇总人
		/// </summary>
		public Guid? _SumitUser { get; set; }

		/// <summary>
		/// 汇总类型
		/// </summary>
		public int? _SumitType { get; set; }
	}
}
