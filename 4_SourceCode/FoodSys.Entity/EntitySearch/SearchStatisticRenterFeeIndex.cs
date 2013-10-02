using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchStatisticRenterFeeIndex : BaseSearch
	{


		/// <summary>
		/// 合同编号
		/// </summary>
		public string _ContractNo { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		/// 单位名称
		/// </summary>
		public string _CompanyName { get; set; }
	}
}
