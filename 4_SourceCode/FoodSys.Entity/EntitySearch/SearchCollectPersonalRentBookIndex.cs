using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 台账管理 --> 个人预定台账
	/// </summary>
	public class SearchCollectPersonalRentBookIndex : BaseSearch
	{
		/// <summary>
		/// 姓名
		/// </summary>
		public String _Name { get; set; }

		/// <summary>
		/// 证件号码
		/// </summary>
		public String _CertNo { get; set; }

		/// <summary>
		/// 小区ID
		/// </summary>
		public Guid? _CommunityID { get; set; }


		/// <summary>
		/// 户型ID
		/// </summary>
		public Guid? _RoomTypeID { get; set; }
	}
}
