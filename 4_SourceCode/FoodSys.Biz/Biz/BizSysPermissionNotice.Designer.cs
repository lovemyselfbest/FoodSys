using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodSys.Dal.DataAccess;
using FoodSys.Entity;
using NHibernate;
using Project.Biz.Base;
using Project.Dal.Base;
namespace FoodSys.Biz.BizAccess {
	/// <summary>
	/// 首页提醒权限控制
	/// </summary>
	public partial class  BizSysPermissionNotice :BaseBiz<SysPermissionNotice,DalSysPermissionNotice>
	{
		private BizSysPermissionNotice()
			: base()
		{
			Initialize();
		}
	}
}
		