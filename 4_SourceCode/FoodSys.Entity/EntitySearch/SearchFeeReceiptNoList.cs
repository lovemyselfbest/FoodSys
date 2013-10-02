using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 收据补打
    /// 作者：尤啸
    /// 时间：2012-09-13
    /// </summary>
    public class SearchFeeReceiptNoList : BaseSearch
    {
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
        /// 合同编号
        /// </summary>
        public string _ContractNo { get; set; }
        /// <summary>
        /// 是否打印
        /// </summary>
        public int? _IsPrint { get; set; }




    }
}
