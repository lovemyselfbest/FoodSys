using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchParkingSpace : BaseSearch
    {
        /// <summary>
        /// 
        /// Length : 
        /// </summary>
        public Guid? _ID { get; set; }


        /// <summary>
        /// 
        /// Length : 20
        /// </summary>
        public String _Name { get; set; }


        /// <summary>
        /// 
        /// Length : 
        /// </summary>
        public Guid? _ParkingAreaID { get; set; }


        /// <summary>
        /// 
        /// Length : 50
        /// </summary>
        public String _AreaName { get; set; }


        /// <summary>
        /// 
        /// Length : 
        /// </summary>
        public Guid?  _ParkingSpaceID { get; set; }

        /// <summary>
        /// 编号
        /// Length : 10
        /// </summary>
        public String _No { get; set; }

        /// <summary>
        /// 状态（0: 闲置  1 已分配  2保留）
        /// Length : 
        /// </summary>
        public Int32? _Status { get; set; }



    }
}
