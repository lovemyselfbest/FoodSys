﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchPersonalRenewIndex : BaseSearch
	{
		public System.String _ContractNo { get; set; }
		public System.String _Name { get; set; }
		public System.String _CertNo { get; set; }
		public System.String _CompanyName { get; set; }
		public System.String _RoomAddress { get; set; }
		public System.String _RenewContractNo { get; set; }
	}
}
