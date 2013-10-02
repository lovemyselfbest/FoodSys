using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 户型查询类
	/// </summary>
	public class SearchRoomType : BaseSearch
	{
		public Guid? _CommunityID
		{
			get;
			set;
		}

		public Guid? _RoomKindsID
		{
			get;
			set;
		}

		public string _UnitsName
		{
			get;
			set;
		}

	}
}
