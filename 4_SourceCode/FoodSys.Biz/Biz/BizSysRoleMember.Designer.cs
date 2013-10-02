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
	/// 角色成员
	/// </summary>
	public partial class  BizSysRoleMember :BaseBiz<SysRoleMember,DalSysRoleMember>
	{
		private BizSysRoleMember()
			: base()
		{
			Initialize();
		}
	}
}
		