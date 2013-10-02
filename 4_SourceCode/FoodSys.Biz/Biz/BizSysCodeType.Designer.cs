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
	/// 代码组类型表
	/// </summary>
	public partial class  BizSysCodeType :BaseBiz<SysCodeType,DalSysCodeType>
	{
		private BizSysCodeType()
			: base()
		{
			Initialize();
		}
	}
}
		