using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 用户表（内部）
	/// </summary>
	public partial class  DalSysUser :Repository<SysUser>
	{
		private DalSysUser()
			: base()
		{
			Initialize();
		}
	}
}
		