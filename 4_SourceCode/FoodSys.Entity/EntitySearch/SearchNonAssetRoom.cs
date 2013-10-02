using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	/// <summary>
	/// 室查询类
	/// </summary>
	public class SearchNonAssetRoom : BaseSearch
	{
		public String _NO
		{
			get;
			set;
		}

		public String _Name
		{
			get;
			set;
		}

		public int? _Using
		{
			get;
			set;
		}

		public String _UnitNo
		{
			get;
			set;
		}

		public String _FloorNo
		{
			get;
			set;
		}

		public Guid? _RoomTypeID
		{
			get;
			set;
		}
	}
}
