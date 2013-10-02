using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 用户权限表（三类用户：内部用户、租户、商户）
	/// </summary>
	public partial class  DalSysUserRight :Repository<SysUserRight>
	{
		private DalSysUserRight()
			: base()
		{
			Initialize();
		}
	}
}
		