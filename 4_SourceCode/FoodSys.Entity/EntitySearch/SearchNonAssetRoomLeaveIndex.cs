using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 
	/// </summary>
	public class SearchNonAssetRoomLeaveIndex : BaseSearch
	{
		public Guid? _BuildingID
		{
			get;
			set;
		}

		public string _RoomNO
		{
			get;
			set;
		}

		public string _Remark
		{
			get;
			set;
		}
	}
}
