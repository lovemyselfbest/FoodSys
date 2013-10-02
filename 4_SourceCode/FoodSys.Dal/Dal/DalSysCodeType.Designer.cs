using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 代码组类型表
	/// </summary>
	public partial class  DalSysCodeType :Repository<SysCodeType>
	{
		private DalSysCodeType()
			: base()
		{
			Initialize();
		}
	}
}
		