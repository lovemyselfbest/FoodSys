using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchNonAssetChangeList : BaseSearch
	{
		/// <summary>
		/// 
		/// </summary>
        public string _POriginalAssetNo { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
        public string _PReplaceAssetNo { get; set; }
        public Int32? _PChangeType { get; set; }
        public bool? _PIsEffect { get; set; } 
		public DateTime? _PInputDateFrom { get; set; }
		public DateTime? _PInputDateTo { get; set; }
	}
}
