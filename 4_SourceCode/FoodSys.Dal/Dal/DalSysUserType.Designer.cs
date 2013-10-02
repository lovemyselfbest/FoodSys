using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 用户类型
	/// </summary>
	public partial class  DalSysUserType :Repository<SysUserType>
	{
		private DalSysUserType()
			: base()
		{
			Initialize();
		}
	}
}
		