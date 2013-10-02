using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchAssetScrapIndex : BaseSearch
	{
		/// <summary>
		/// 动资产编码
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
