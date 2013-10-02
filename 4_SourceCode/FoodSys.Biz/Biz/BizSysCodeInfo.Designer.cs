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
	/// 代码组信息表
	/// </summary>
	public partial class  BizSysCodeInfo :BaseBiz<SysCodeInfo,DalSysCodeInfo>
	{
		private BizSysCodeInfo()
			: base()
		{
			Initialize();
		}
	}
}
		