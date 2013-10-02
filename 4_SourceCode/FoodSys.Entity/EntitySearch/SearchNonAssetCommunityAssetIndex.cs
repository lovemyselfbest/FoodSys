using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 动资产管理 --> 所有资产
	/// 作者：黄剑锋
	/// 时间：2011-10-25
	/// </summary>
	public class SearchNonAssetCommunityAssetIndex : BaseSearch
	{
		/// <summary>
		/// 资产编码
		/// </summary>
		public string _AssetNO { get; set; }

		/// <summary>
		/// 系统及设备类别
		/// </summary>
		public string _EquipmentNO { get; set; }

		/// <summary>
		/// 规格/型号
		/// </summary>
		public string _Specific { get; set; }

		/// <summary>
		/// 品牌
		/// </summary>
		public string _Brand { get; set; }

	}
}
