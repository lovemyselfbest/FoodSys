using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 用户与小区关联表
	/// </summary>
	public partial class  DalSysUserXCommuity :Repository<SysUserXCommuity>
	{
		private DalSysUserXCommuity()
			: base()
		{
			Initialize();
		}
	}
}
		