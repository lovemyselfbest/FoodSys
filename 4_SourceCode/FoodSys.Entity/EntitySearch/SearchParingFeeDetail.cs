using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchParingFeeDetail :BaseSearch
    {
        public string _Name { get; set; }
        public string _CertNo { get; set; }
        public string _CommunityName { get; set; }
        public string _ParkingSpaceNo { get; set; }
        public string _RoomAddress { get; set; }
        public bool _IsShowAll { get; set; }
    }
}
