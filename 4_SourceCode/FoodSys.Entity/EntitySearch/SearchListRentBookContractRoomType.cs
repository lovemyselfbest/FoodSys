using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchListRentBookContractRoomType : BaseSearch
    {
        /// <summary>
        /// 小区名称
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 户型名称
        /// </summary>
        public string _UnitsName { get; set; }
        /// <summary>
        /// 申请数量
        /// </summary>
        public string _Quantity { get; set; }
    }
}
