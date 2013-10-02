using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchApplyCompanyCoapplicantIndex : BaseSearch
	{
		/// <summary>
		/// 姓名
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

		/// <summary>
		/// 申请类型
		/// </summary>
		public int? _ApplyType { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public int? _Sex { get; set; }

	}
}
