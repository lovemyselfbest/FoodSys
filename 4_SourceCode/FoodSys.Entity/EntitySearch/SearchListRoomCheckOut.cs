using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 退租搜索条件
    /// 作者：尤啸
    /// 时间：2012-07-20
    /// </summary>
    public class SearchListRoomCheckOut : BaseSearch
    {
        /// <summary>
        /// 承租人
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string _ContractNo { get; set; }
        /// <summary>
        /// 退租类型
        /// </summary>
        public int? _Status { get; set; }

        public DateTime? _BeginDate { get; set; }
        public DateTime? _EndDate { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }

    }
}
