using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 资产管理 >> 资产盘点
	/// </summary>
	public class SearchAssetCheckIndex : BaseSearch
	{
		/// <summary>
		/// 资产编号
		/// </summary>
		public string _AssetNO { get; set; }

		/// <summary>
		/// 分配位置
		/// </summary>
		public string _AssignAddress { get; set; }

		/// <summary>
		/// 是否已盘点
		/// </summary>
		public System.Nullable<Boolean> _IsCheck { get; set; }
	}
}
