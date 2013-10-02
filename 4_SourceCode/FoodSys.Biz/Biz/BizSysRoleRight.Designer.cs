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
	/// 角色权限表
	/// </summary>
	public partial class  BizSysRoleRight :BaseBiz<SysRoleRight,DalSysRoleRight>
	{
		private BizSysRoleRight()
			: base()
		{
			Initialize();
		}
	}
}
		