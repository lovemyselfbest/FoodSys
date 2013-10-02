using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchLogs : BaseSearch
	{
		/// <summary>
		/// 
		/// </summary>
        public string _PCompanyName { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
        public string _POperatorName { get; set; }
        public Int32? _PLogTypeID { get; set; }
        
        public DateTime? _POperationTimeFrom { get; set; }
        public DateTime? _POperationTimeTo { get; set; }

        public string _PUserAccount { get; set; }

        public string _PLogContent { get; set; }
	}
}
