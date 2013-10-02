using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
   public  class SearchCollectIntentionContractList : BaseSearch
    {
       /// <summary>
       ///名称
       /// </summary>
       public string _Name { get; set; }
       /// <summary>
       /// 证件编号
       /// </summary>
       public string _CertNO { get; set; }
       /// <summary>
       /// 合同编号
       /// </summary>
       public string _ContractNo { get; set; }
       /// <summary>
       /// 状态
       /// </summary>
       public int? _Status { get; set; }
    }
}
