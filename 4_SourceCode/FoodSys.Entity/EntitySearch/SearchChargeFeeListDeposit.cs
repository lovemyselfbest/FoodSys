using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchChargeFeeListDeposit : BaseSearch
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
        /// 收据编号
        /// </summary>
        public string _ReceiptNo { get; set; }
        /// <summary>
        /// 汇总凭证编号
        /// </summary>
        public string _SumSerialNO { get; set; }
        /// <summary>
        /// 押金类型
        /// </summary>
        public int? _FeeType { get; set; }
    }
}
