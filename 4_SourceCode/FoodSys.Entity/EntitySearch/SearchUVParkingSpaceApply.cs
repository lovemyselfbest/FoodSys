using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchUVParkingSpaceApply : BaseSearch
    {
        /// <summary>
        /// 停车位
        /// </summary>
        public String _Name { get; set; }

        /// <summary>
        /// 
        /// Length : 18
        /// </summary>
        public String _CertNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String _CarNo { get; set; }

    

        public string _ParkingSpaceNo { get; set; }

        public string _CommunityName { get; set; }

        public string _RoomAddress { get; set; }

        public string _No { get; set; }

    }
}
