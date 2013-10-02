using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchNonAssetCommunityFeeSetting :BaseSearch
    {
        /// <summary>
        /// 小区ID
        /// Length : 
        /// </summary>
        public Guid? _CommunityID { get; set; }

        /// <summary>
        /// 费用类型
        /// Length : 
        /// </summary>
        public int? _FeeType { get; set; }

        /// <summary>
        /// 生效年月
        /// Length : 
        /// </summary>
        public DateTime? _ValidMonth { get; set; }

        /// <summary>
        /// 户型名称
        /// </summary>
        public Guid? _RoomTypeID { get; set; }
    }
}
