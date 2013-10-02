using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
    public class SearchUTRoomKinds : BaseSearch
    {
        public Guid? _CommunityID { get; set; }

        public int? _Using { get; set; }

        public string _Name { get; set; }

    }
}
