using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 首页提醒权限控制
	/// </summary>
	public partial class  DalSysPermissionNotice :Repository<SysPermissionNotice>
	{
		private DalSysPermissionNotice()
			: base()
		{
			Initialize();
		}
	}
}
		