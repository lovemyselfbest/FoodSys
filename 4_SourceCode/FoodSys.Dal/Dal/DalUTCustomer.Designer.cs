using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 客户表
	/// </summary>
	public partial class  DalUTCustomer :Repository<UTCustomer>
	{
		private DalUTCustomer()
			: base()
		{
			Initialize();
		}
	}
}
		