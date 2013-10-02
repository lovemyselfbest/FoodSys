using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 个人用户表
	/// </summary>
	public partial class  DalSysRenterUser :Repository<SysRenterUser>
	{
		private DalSysRenterUser()
			: base()
		{
			Initialize();
		}
	}
}
		