using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;
namespace FoodSys.Entity.EntitySearch
{
	public class SearchListPersonalAuditIndex : BaseSearch
	{
		public System.String _MainSerialNO{get;set;}
   		public System.Nullable<System.Int32> _ApplyType{get;set;}
		public System.String _Name { get; set; }
   		public System.String _CompanyName{get;set;}
      }

}
   