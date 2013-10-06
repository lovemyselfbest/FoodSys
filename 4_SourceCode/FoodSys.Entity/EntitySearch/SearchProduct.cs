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
		public Guid _ProductType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid _Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public decimal PurchasePriceS { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public decimal PurchasePriceE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public decimal SellPriceS { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public decimal SellPriceE { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public int Status { get; set; }
		
	}
}
