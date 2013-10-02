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
	/// 用户表（内部）
	/// </summary>
	public partial class  BizSysUser :BaseBiz<SysUser,DalSysUser>
	{
		private BizSysUser()
			: base()
		{
			Initialize();
		}
	}
}
		