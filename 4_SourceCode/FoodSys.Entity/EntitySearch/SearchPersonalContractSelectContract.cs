using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchPersonalContractSelectContract:BaseSearch
	{
		public string _Name { get; set; }
		public string _CertNo { get; set; }
		public int? _ContractStatus { get; set; }
		public string _ContractNo { get; set; }
	}
}
