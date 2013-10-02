using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSys.Enum
{
	public enum EnumApplyType
	{
		企业员工合租 = 0,
		企业员工租赁 = 1,
		个人合租 = 2,
		个人租赁 = 3
	}

	/// <summary>
	/// 关系
	/// </summary>
	public enum EnumRelation
	{
		夫 = 0,
		妻 = 1,
		父 = 2,
		母 = 3,
		子 = 4,
		女 = 5,
		其他 = 6
	}

	public enum EnumAuditResult
	{
		待审核 = 0,
		通过 = 1,
		未通过 = 2,

	}


}
