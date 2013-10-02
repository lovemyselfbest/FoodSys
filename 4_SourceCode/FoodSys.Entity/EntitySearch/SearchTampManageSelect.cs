using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 短信/Email模板搜索条件类
    /// 作者:尤啸
    /// 时间：2012-06-07
    /// </summary>
    public class SearchTampManageSelect : BaseSearch
    {
        /// <summary>
        /// 短信模板编号
        /// </summary>
        public string _TampNumb { get; set; }
        /// <summary>
        /// 短信模板名称
        /// </summary>
        public string _TampName { get; set; }
        /// <summary>
        /// 短信模板类型
        /// </summary>
        public int? _TampTypeForIndividual { get; set; }


        /// <summary>
        /// Email模板编号
        /// </summary>
        public string _ETampNumb { get; set; }
        /// <summary>
        /// Email模板名称
        /// </summary>
        public string _ETampName { get; set; }
        /// <summary>
        /// Email模板类型
        /// </summary>
        public int? _ETampTypeForIndividual { get; set; }
      

       
        /// <summary>
        /// 小区ID
        /// </summary>
        public Guid? _CommunityID { get; set; }
        /// <summary>
        /// 户型ID
        /// </summary>
        public Guid? _RoomTypeID { get; set; }
    }
}
