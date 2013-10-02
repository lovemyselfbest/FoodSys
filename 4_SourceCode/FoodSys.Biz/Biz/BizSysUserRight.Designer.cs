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
	/// 用户权限表（三类用户：内部用户、租户、商户）
	/// </summary>
	public partial class  BizSysUserRight :BaseBiz<SysUserRight,DalSysUserRight>
	{
		private BizSysUserRight()
			: base()
		{
			Initialize();
		}
	}
}
		