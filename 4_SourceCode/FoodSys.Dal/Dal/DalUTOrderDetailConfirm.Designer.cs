using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 
	/// </summary>
	public partial class  DalUTOrderDetailConfirm :Repository<UTOrderDetailConfirm>
	{
		private DalUTOrderDetailConfirm()
			: base()
		{
			Initialize();
		}
	}
}
		