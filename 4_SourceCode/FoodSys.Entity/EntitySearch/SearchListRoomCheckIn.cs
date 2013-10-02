using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 入住台账搜索
    /// 作者：尤啸
    /// 时间：2012-07-20
    /// </summary>
    public class SearchListRoomCheckIn : BaseSearch
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string _CompanyName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }
        /// <summary>
        /// 租赁位置
        /// </summary>
        public string _RoomAddress { get; set; }
        /// <summary>
        /// 小区
        /// </summary>
        public string _CommunityName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? _BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? _EndDate { get; set; }

    }
}
