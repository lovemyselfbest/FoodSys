using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 收款凭证汇总 查看 收款汇总明细
	/// 作者：黄剑锋
	/// 时间：2011-12-2
	/// </summary>
	public class SearchPaymentSummaryPaymentSumDetail : BaseSearch
	{
		/// <summary>
		/// 费用类型
		/// </summary>
		public int? _ChargeItem { get; set; }

		/// <summary>
		/// 收取人
		/// </summary>
		public Guid? _PayedPerson { get; set; }

		/// <summary>
		/// 支付人
		/// </summary>
		public string _TargetUserName { get; set; }

		/// <summary>
		/// 支付方式
		/// </summary>
		public int? _PayedType { get; set; }
	}
}
