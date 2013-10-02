using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchUTSupplier : BaseSearch
    {
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string _SupplierName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string _ContactName { get; set; }
        /// <summary>
        /// 供货内容
        /// </summary>
        public string _SupplyContent { get; set; }

    }
}
