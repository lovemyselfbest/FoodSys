using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchCollectApplyIndex : BaseSearch
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string _Name { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }

        /// <summary>
        ///收储小区
        /// </summary>
        public Guid? _Community { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string _ContractNo { get; set; }
    }
}
