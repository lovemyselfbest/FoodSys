using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchComanyContractManageSelectPersonal : BaseSearch
	{
		/// <summary>
		/// 姓名
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		/// 证件号码
		/// </summary>
		public string _CertNo { get; set; }

	}
}
