using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchRenterNeedRental : BaseSearch
    {
        public string _StartDate { get; set; }

        public string _EndDate { get; set; }
    }
}
