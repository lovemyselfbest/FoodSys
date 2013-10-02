using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchRefundFeeListDeposit : BaseSearch
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string _ContractNo { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }
        /// <summary>
        /// 费用类型(1.水费2.电费)
        /// </summary>
        public int? _FeeType { get; set; }
    }
}
