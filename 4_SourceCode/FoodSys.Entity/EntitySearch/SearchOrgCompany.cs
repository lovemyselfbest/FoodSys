using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 企业信息模糊查询
	/// </summary>
	public class SearchOrgCompany : BaseSearch
	{
		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string _CompanyCode { get; set; }

		/// <summary>
		/// 企业全称
		/// </summary>
		public string _CompanyName { get; set; }

	}
}
