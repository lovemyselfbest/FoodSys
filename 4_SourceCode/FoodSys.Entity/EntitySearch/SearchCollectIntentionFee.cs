using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
   public class SearchCollectIntentionFee:BaseSearch
    {
       /// <summary>
       /// 名称
       /// </summary>
       public string _Name { get; set; }
       /// <summary>
       /// 证件编号
       /// </summary>
       public string _CertNO { get; set; }
       /// <summary>
       /// 意向金合同编号
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
       /// 汇总凭证编号
       /// </summary>
       public string _SumSerialNO { get; set; }

    }
}
