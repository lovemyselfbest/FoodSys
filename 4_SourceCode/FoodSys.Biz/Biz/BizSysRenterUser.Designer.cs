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
	/// 个人用户表
	/// </summary>
	public partial class  BizSysRenterUser :BaseBiz<SysRenterUser,DalSysRenterUser>
	{
		private BizSysRenterUser()
			: base()
		{
			Initialize();
		}
	}
}
		