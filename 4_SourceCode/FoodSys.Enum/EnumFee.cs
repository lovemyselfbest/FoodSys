using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSys.Enum
{
	/// <summary>
	/// 小区费用设置
	/// </summary>
	public enum EnumCommunityFeeSetting
	{
		租金单价 = 0,
		租金市场价 = 1,
		物业费单价 = 2,
		采暖基本费 = 3,
		电费单价 = 4,
		水费单价 = 5,
		采暖费单价 = 6,
		预定金 = 7
	}

	/// <summary>
	/// 缴纳状态
	/// </summary>
	public enum EnumFeePayStatus
	{
		正常缴纳 = 0,
		欠费缴纳 = 1
	}

	/// <summary>
	/// 费用收取方式
	/// </summary>
	public enum EnumPayedType
	{
		现金 = 0,
		POS机 = 1,
		银行转账 = 2,
		预订金结转 = 3,
		保证金结转 = 4,
		保证金结转赔偿金 = 5,
	}

	/// <summary>
	/// 缴费方式
	/// </summary>
	public enum EnumPayedRange
	{
		月付 = 1,
		季付 = 3,
		半年付 = 6,
		年付 = 12
	}

	/// <summary>
	/// 支付状态
	/// </summary>
	public enum EnumPayedStatus
	{
		未支付 = 0,
		已支付 = 1,
		未付满 = 2
	}

	/* EnumRefundPayedType 使用 EnumPayedType 
	 * EnumRefundFeeType 使用 EnumFeeType
	----------------------------------------------------------*/
	/*
    /// <summary>
    /// 费用退还统计--退还方式
    /// 作者：尤啸
    /// 时间：2012-09-10
    /// </summary>
    public enum EnumRefundPayedType
    {
        现金 = 0,
        转账 = 1,
        结转 = 2,

    }

	/// <summary>
	/// 退费表退费类型
	/// </summary>
	public enum EnumRefundFeeType
	{
		保证金 = 0,
		水费 = 1,
		电费 = 2,
		采暖费 = 3
	}*/

}
