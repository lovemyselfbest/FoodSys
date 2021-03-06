﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchListRentBookContractAndRoomType : BaseSearch
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string _Name { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string _ContractNo { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string _CertNO { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public string _ContractStatus { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string _CompanyName { get; set; }
    }
}
