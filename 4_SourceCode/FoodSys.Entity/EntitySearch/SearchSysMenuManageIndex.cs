using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
   public class SearchSysMenuManageIndex :BaseSearch
    {
        public string _Name { get; set; }

        public int? _OrderIndex { get; set; }
    
        public string _TargetURL { get; set; }
        
        public string _Icon { get; set; }
    }
}
