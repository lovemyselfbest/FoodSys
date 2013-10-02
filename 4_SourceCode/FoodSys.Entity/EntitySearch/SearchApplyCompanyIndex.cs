using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchApplyCompanyIndex : BaseSearch
	{
		/// <summary>
		/// 单位全称
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		/// 组织机构代码
		/// </summary>
		public string _OrganizationCode { get; set; }

		/// <summary>
		/// 申请小区
		/// </summary>
		public Guid? _CommunityID { get; set; }

        public string _UnitsName { get; set; }

        public string _No { get; set; }
	}
}
