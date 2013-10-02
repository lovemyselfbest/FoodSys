using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchRepairApplyInfo : BaseSearch
    {
        public string _Name { get; set; }

        public string _RoomAddress { get; set; }

        public string _Mobile { get; set; }

        public string _ApplyDateStart { get; set; }

        public string _ApplyDateEnd { get; set; }

        public int? _Status { get; set; }

        public Guid? _CommunityID { get; set; }

        public string _NO { get; set; }

    }
}
