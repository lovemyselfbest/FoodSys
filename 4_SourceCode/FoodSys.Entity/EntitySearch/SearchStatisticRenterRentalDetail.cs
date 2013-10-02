using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 租金统计---已收详细，查询
    /// </summary>
   public class SearchStatisticRenterRentalDetail:BaseSearch
    {
       /// <summary>
       ///租户姓名
       /// </summary>
       public string _Name { get; set; }
       /// <summary>
       /// 证件号码
       /// </summary>
       public string _CertNO { get; set; }
    }
}
