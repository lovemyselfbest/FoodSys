using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    /// <summary>
    /// 作者：尤啸
    /// 时间：2012-08-01
    /// 保证金统计
    /// </summary>
    public class SearchStatisticalRenterDepositIndex : BaseSearch
    {
        public string _StartDate { get; set; }

        public string _EndDate { get; set; }
    }
}
