using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public  class SearchSelectPerson : BaseSearch
    {
        public string _Name { get; set; }

        public string _CertNO { get; set; }

        public Guid? _CommunityID { get; set; }

        public string _RoomAddress { get; set; }
    }
}
