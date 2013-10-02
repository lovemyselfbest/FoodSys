using System;
using System.Collections.Generic;
using System.Text;
using Project.Entity.Base;
namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 个人申请资料表
	/// </summary>
	public class SearchUVPersonalBookContract : BaseSearch
	{
		public string _Name { get; set; }
		public string _CertNo { get; set; }
		public string _CompanyName { get; set; }
		public System.Nullable<System.Guid> _CommunityID { get; set; }
		public System.Nullable<System.Guid> _RoomTypeID { get; set; }
		public int? _ApplyType { get; set; }
		public string _ContractNo { get; set; }
		public string _SerialNo { get; set; }

	}
}
