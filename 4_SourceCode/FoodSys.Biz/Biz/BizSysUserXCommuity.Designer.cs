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
	/// 用户与小区关联表
	/// </summary>
	public partial class  BizSysUserXCommuity :BaseBiz<SysUserXCommuity,DalSysUserXCommuity>
	{
		private BizSysUserXCommuity()
			: base()
		{
			Initialize();
		}
	}
}
		