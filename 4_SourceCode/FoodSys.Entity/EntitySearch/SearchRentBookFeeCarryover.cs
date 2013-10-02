using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
  public  class SearchRentBookFeeCarryover : BaseSearch
    {
      /// <summary>
      /// 租赁合同编号
      /// </summary>
      public string _ContractNo { get; set; }
      /// <summary>
      /// 租赁预定合同编号
      /// </summary>
      public string _RentBookContractNo { get; set; }
      /// <summary>
      /// 名称
      /// </summary>
      public string _Name { get; set; }
      /// <summary>
      /// 证件号码
      /// </summary>
      public string _CertNO { get; set; }
      /// <summary>
      /// 合同类型
      /// </summary>
      public int? _ContractType { get; set; }
      /// <summary>
      /// 结转状态
      /// </summary>
      public int? _Status { get; set; }


    }
}
