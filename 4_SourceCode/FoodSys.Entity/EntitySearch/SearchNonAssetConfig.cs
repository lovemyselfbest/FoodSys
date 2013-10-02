using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchNonAssetConfig:BaseSearch
	{
		public Guid? _BuildingID { get; set; }
		public Guid? _RoomTypeID { get; set; }
		public string _RoomsText { get; set; }
		public string _RoomsGuid { get; set; }
		public string _PartCode { get; set; }
		public string _EquipmentNO { get; set; }
		public string _Brand { get; set; }
		public string _AssetNO { get; set; }


		[PaginateNotComposeAttribute]
		public IList<Guid?> ArrayRoomsNullableGuid
		{
			get
			{
				IList<Guid?> list = new List<Guid?>();
				if (string.IsNullOrEmpty(_RoomsGuid))
					return list;
				string[] guids= _RoomsGuid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < guids.Length; i++)
				{
					list.Add(Guid.Parse(guids[i]));
				}
				return list;
			}
		}

		[PaginateNotComposeAttribute]
		public IList<Guid> ArrayRoomsGuid
		{
			get
			{
				IList<Guid> list = new List<Guid>();
				if (string.IsNullOrEmpty(_RoomsGuid))
					return list;
				string[] guids = _RoomsGuid.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < guids.Length; i++)
				{
					list.Add(Guid.Parse(guids[i]));
				}
				return list;
			}
		}

	}
}
