using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchCheckInIndex : BaseSearch
	{
		/// <summary>
		/// 证件号码
		/// </summary>
		public string _CertNo { get; set; }

		/// <summary>
		/// 合同编号
		/// </summary>
		public string _ContractNo { get; set; }

		/// <summary>
		/// 单位全称
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		/// 申请小区
		/// </summary>
		public Guid? _CommunityID { get; set; }

		public string _RoomAddress { get; set; }
	}
}
