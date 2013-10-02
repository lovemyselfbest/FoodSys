using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FoodSys.Entity; 
using Project.Dal.Base;
namespace FoodSys.Dal.DataAccess {
	/// <summary>
	/// 示例表
	/// </summary>
	public partial class  DalUTSample :Repository<UTSample>
	{
		private DalUTSample()
			: base()
		{
			Initialize();
		}
	}
}
		