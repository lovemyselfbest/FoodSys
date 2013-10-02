using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 角色成员
	/// </summary>
	public partial class  DalSysRoleMember :Repository<SysRoleMember>
	{
		private DalSysRoleMember()
			: base()
		{
			Initialize();
		}
	}
}
		