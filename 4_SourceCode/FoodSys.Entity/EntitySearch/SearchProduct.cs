using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Entity.Base;

namespace FoodSys.Entity.EntitySearch
{
	public class SearchProduct : BaseSearch
	{
		/// <summary>
		///  
		/// </summary>
		public string _Name { get; set; }

		/// <summary>
		///  
		/// </summary>
		public string _ProductType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string _Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string _PurchasePriceS { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public string _PurchasePriceE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string _SellPriceS { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public string _SellPriceE { get; set; }

	}
}
