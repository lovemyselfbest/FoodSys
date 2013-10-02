using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchCompanyRentalFeeChargeCompanyRoomInfo : BaseSearch
	{
		public System.Nullable<Guid> _BuildingID { get; set; }
		public virtual System.String _No { get; set; }
	}
}
