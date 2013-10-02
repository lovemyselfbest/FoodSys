using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public  class SearchUVCompanyNotifyForRent:BaseSearch
    {
        /// <summary>
        /// 申请审核编号
        /// </summary>
        public String _SerialNo { get; set; }

        public String _Name { get; set; }

        public string _ContractNo { get; set; }
        /// <summary>
        /// 小区ID
        /// </summary>
        public Guid? _CommunityID { get; set; }


        public string _CompanyName { get; set; }


        public string _UnitsName { get; set; }

        public string _CertNO { get; set; }

        public string _OrganizationCode { get; set; }


    }
}
