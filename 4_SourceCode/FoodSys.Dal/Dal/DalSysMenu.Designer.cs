using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 菜单表
	/// </summary>
	public partial class  DalSysMenu :Repository<SysMenu>
	{
		private DalSysMenu()
			: base()
		{
			Initialize();
		}
	}
}
		