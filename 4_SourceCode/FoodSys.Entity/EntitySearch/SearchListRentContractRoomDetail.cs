using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchListRentContractRoomDetail : BaseSearch
    {
        /// <summary>
        /// 部位
        /// </summary>
        public string _PartAddress { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }
        /// <summary>
        ///  所属单位
        /// </summary>
        public string _CompanyName { get; set; }
        /// <summary>
        /// 合同类别
        /// </summary>
        public int? _ContractType { get; set; }
    }
}
