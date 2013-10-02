using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchListRentContractRoom : BaseSearch
    {
        /// <summary>
        /// 房间用途
        /// </summary>
        public string _RoomUsing { get; set; }
        /// <summary>
        /// 租赁房间名
        /// </summary>
        public string _RoomName { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public string _RoomUnitNo { get; set; }
        /// <summary>
        /// 户型名
        /// </summary>
        public string _UnitsName { get;set; }
    }
}
