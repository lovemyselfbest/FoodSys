using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 收储保证金台账
    /// 作者：尤啸
    /// 时间：2012-09-13
    /// </summary>
   public class SearchCollectFeeDeposit : BaseSearch
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
       /// 收储合同编号
       /// </summary>
       public string _ContractNo { get; set; }
       /// <summary>
       /// 支付状态
       /// </summary>
       public int? _PayedStatus { get; set; }
       /// <summary>
       /// 收据编号
       /// </summary>
       public string _ReceiptNo { get; set; }
       /// <summary>
       /// 收据凭证编号
       /// </summary>
       public string _SumSerialNO { get; set; }



    }
}
