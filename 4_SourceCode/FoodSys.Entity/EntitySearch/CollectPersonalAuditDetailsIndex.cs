using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class CollectPersonalAuditDetailsIndex : BaseSearch
	{
		public string _Name { get; set; }
		public string _CompanyName { get; set; }
		public string _CertNO { get; set; }
		public int? _PersonalType { get; set; }
	}
}
