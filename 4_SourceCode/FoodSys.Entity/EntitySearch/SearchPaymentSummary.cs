using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchPaymentSummary : BaseSearch
	{
		/// <summary>
		/// 费用类型
		/// </summary>
		public int? _CostType { get; set; }

		/// <summary>
		/// 收取人
		/// </summary>
		public string _PayedPersonName { get; set; }

		/// <summary>
		/// 支付方式
		/// </summary>
		public int? _PayedType { get; set; }

		/// <summary>
		/// 收取日期 开始
		/// </summary>
		public DateTime? _FromPayedDate { get; set; }

		/// <summary>
		/// 收取日期 结束
		/// </summary>
		public DateTime? _ToPayedDate { get; set; }
	}
}
