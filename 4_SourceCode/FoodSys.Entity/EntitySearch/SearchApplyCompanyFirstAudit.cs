using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchApplyCompanyFirstAudit : BaseSearch
	{
		/// <summary>
		/// 单位全称
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		/// 证件号码
		/// </summary>
		public string _CertNo { get; set; }

		/// <summary>
		/// 申请房型
		/// </summary>
		public Guid? _RoomTypeID { get; set; }
	}
}
