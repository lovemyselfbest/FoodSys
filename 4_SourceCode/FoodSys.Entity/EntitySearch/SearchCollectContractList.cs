using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
   public class SearchCollectContractList : BaseSearch
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
       /// 意向合同编号
       /// </summary>
       public string _ContractNo { get; set; }
       /// <summary>
       /// 收储合同编号
       /// </summary>
       public string _CollectContractNo { get; set; }
       /// <summary>
       /// 状态
       /// </summary>
       public int? _Status { get; set; }
    }
}
