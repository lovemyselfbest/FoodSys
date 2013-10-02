
using Project.Common;
namespace FoodSys.Enum
{
	/// <summary>
	/// 合同状态(预订合同、公寓租赁合同、公寓续签合同、意向合同、商铺租赁合同、商铺续签合同)
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumContractStatus
	{
		待签章 = 0,
		未生效 = 1,
		有效 = 2,
		失效 = 3,
		解除 = 4,
		作废 = 5,
	}


	/// <summary>
	/// 预订金结转状态
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumBookCarryoverStatus
	{
		未结转 = 0,
		已结转 = 1,
		作废 = 2
	}

	/// <summary>
	/// 入住状态
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumCheckInStatus
	{
		未入住 = 0,

		已入住 = 1,
	}

	public enum EnumParkingSpaceApplyStatus
	{
		有效 = 0,
		终止 = 1,
	}
	/// <summary>
	/// 主申请状态（企业或个人）
	/// </summary>
	public enum EnumApplyStatus
	{
		待接单 = 0,
		待初审 = 1,
		初审待公示 = 2,
		初审公示中 = 3,
		待复审 = 4,
		复审待公示 = 5,
		复审公示中 = 6,
		待预定合同生成 = 7,
		预定合同待签章 = 8,
		待缴纳预定金 = 9,
		待生成配租通知书 = 10,
		待选房 = 11,
		待验房 = 12,
		待租赁合同生成 = 13,
		租赁合同待签章 = 14,
		租金费用待缴纳 = 15,
		待入住办理 = 16,
		已入住 = 17,
		已终止 = 18,
	}

	/// <summary>
	/// 人员状态
	/// </summary>
	public enum EnumPersonalApplyStatus
	{
		待接单 = 0,
		待初审 = 1,
		初审待公示 = 2,
		初审公示中 = 3,
		待复审 = 4,
		复审待公示 = 5,
		复审公示中 = 6,
		资格审核完成 = 7,
		已分配房间 = 8,
		待入住办理 = 9,
		已入住 = 10,
		已终止 = 11,
	}

	/// <summary>
	/// 
	/// 退房相关的状态
	/// </summary>
	public enum EnumCheckOutStatus
	{
		未提交 = 0,
		待审核 = 1,
		待验房 = 2,
		待退房 = 3,
		待退费 = 4,
		已终止 = 5,
		已退回 = 6
	}
	/// <summary>
	/// 预订合同房间使用状态
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumBookRoomStatus
	{
		未使用 = 0,
		已使用 = 1,
		已作废 = 2
	}

	/// <summary>
	/// 收款凭证汇总审核状态
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumSumCheckStatus
	{
		待提交 = 1,
		待审核 = 2,
		已核帐 = 3,
		退回 = 4,
		已封帐 = 5
	}

	/// <summary>
	/// 初始化封帐表
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumClosingStatus
	{
		未初始化 = 0,
		已初始化 = 1,
		已封帐 = 2
	}

	/// <summary>
	/// 登录用户的状态
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumUserStatus
	{
		已锁定 = 0,
		已激活 = 1,
		待激活 = 2
	}

	/// <summary>
	/// 投诉处理状态
	/// </summary>
	public enum EnumComplaintStatus
	{
		未处理 = 0,
		处理中 = 1,
		已处理 = 2,
		已回访 = 3
	}

	/// <summary>
	/// 报修处理状态
	/// </summary>
	public enum EnumRepairStatus
	{
		待受理 = 0,
		待派单 = 1,
		待处理 = 2,
		待完成 = 3,
		待回访 = 4,
		结束 = 5
	}

	/// <summary>
	/// 维修类型
	/// </summary>
	public enum EnumRepairType
	{
		配电 = 0,
		弱电 = 1,
		给排水 = 2,
		电梯 = 3,
		空调 = 4,
		其他 = 5
	}


	/// <summary>
	/// 合同房源表
	/// </summary>
	public enum EnumContractRoomStatus
	{
		在租 = 0,
		退租中 = 1,
		已退租 = 2
	}

	/// <summary>
	/// 合同房源表具体部位状态
	/// </summary>
	public enum EnumContractRoomDetailStatus
	{
		在租 = 0,
		退租中 = 1,
		已退租 = 2
	}

	/// <summary>
	/// 退租登记房间
	/// </summary>
	public enum EnumCheckoutRoomStatus
	{
		待退租设置 = 0,
		待验房 = 1,
		待退房 = 2,
		已退房 = 3
	}
	/// <summary>
	/// 作者：尤啸
	/// 时间：2012-09-12
	/// 收储意向协议状态
	/// </summary>
	public enum EnumCollectIntentionContractStatus
	{
		无效 = 0,
		有效 = 1,
	}

	/// <summary>
	/// 作者：尤啸
	/// 时间：2012-09-12
	/// 打印状态
	/// </summary>
	public enum EnumPrintStatus
	{
		未打印 = 0,
		已打印 = 1,
	}


	/// <summary>
	/// 租赁合同资格复审表
	/// </summary>
	public enum EnumRenewStatus
	{
		待复审 = 0,
		待复审公示 = 1,
		公示中 = 2,
		待续签合同生成 = 3,
		续签合同待签章 = 4,
		已完成 = 5,

	}

	public enum EnumCollectStatus
	{
		待提交 = 0,
		待受理 = 1,
		待生成意向合同 = 2,
		意向合同待签章 = 3,
		意向合同待缴费 = 4,
		待生成收储合同 = 5,
		收储合同待签章 = 6,
		收储合同待缴费 = 7,
		终止 = 8,
	}

	/// <summary>
	/// 短信发送状态
	/// </summary>
	public enum EnumMessageStatus
	{
		未发送 = 0,
		已发送 = 1
	}

	/// <summary>
	/// Email发送状态
	/// </summary>
	public enum EnumEmailStatus
	{
		未发送 = 0,
		已发送 = 1
	}

}
