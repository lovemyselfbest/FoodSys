using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchCompanyRentalFeeChargeIndex : BaseSearch
    {

        public string _ContractNo { get; set; }

        public string _CompanyName { get; set; }

        //public bool _IsShowAll { get; set; }
        private bool _isShowAll = false;

        public bool _IsShowAll
        {
            get { return _isShowAll; }
            set { _isShowAll = value; }
        }


        public Guid? _CommunityID { get; set; }

        public int? _PayedRange { get; set; }

        public string _OrganizationCode { get; set; }

        public string _Name { get; set; }

        public string _CertNo { get; set; }

        public string _RoomAddress { get; set; }

    }
}
