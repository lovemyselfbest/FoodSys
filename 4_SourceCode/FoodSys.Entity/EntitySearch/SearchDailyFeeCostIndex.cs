using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
   public  class SearchDailyFeeCostIndex:BaseSearch
    {
       /// <summary>
		/// 收取日期开始
		/// </summary>
		public DateTime? _FromPayedDate { get; set; }

		/// <summary>
		/// 收取日期结束
		/// </summary>
		public DateTime? _ToPayedDate { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string _PayedContent { get; set; }


		/// <summary>
		/// 支付方式
		/// </summary>
		public string _PayedType { get; set; }

        /// <summary>
        /// 小区
        /// </summary>
        public Guid? _CommunityID { get; set; }

        /// <summary>
        /// 费用类型
        /// </summary>
        public int? _FeeType { get; set; }
    }
}
