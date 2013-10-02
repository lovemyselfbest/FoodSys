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
	/// 用户类型
	/// </summary>
	public partial class  BizSysUserType :BaseBiz<SysUserType,DalSysUserType>
	{
		private BizSysUserType()
			: base()
		{
			Initialize();
		}
	}
}
		