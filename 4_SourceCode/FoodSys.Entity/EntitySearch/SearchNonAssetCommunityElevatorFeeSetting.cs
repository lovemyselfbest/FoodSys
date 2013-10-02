using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchNonAssetCommunityElevatorFeeSetting:BaseSearch
    {
        /// <summary>
        /// 小区ID
        /// Length : 
        /// </summary>
        public Guid? _CommunityID { get; set; }


        public int? _BeginFloor { get; set; }

        public int? _EndFloor { get; set; }

        /// <summary>
        /// 生效年月
        /// Length : 
        /// </summary>
        public DateTime? _ValidMonth { get; set; }
    }
}
