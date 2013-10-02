using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 角色权限表
	/// </summary>
	public partial class  DalSysRoleRight :Repository<SysRoleRight>
	{
		private DalSysRoleRight()
			: base()
		{
			Initialize();
		}
	}
}
		