using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSys.Enum
{

	/// <summary>
	/// Sys_Code_Type 表中的类型,需要和表进行同步。
	/// 作者：尤啸
	/// 时间2012-06-26
	/// </summary>
	public enum EnumSysCodeInfoType
	{
		/// <summary>
		/// 退租原因
		/// </summary>
		CheckOutReason = 0,
		/// <summary>
		/// 学位
		/// </summary>
		Degree = 1,
		/// <summary>
		/// 民族
		/// </summary>
		Nation = 2,
		/// <summary>
		/// 政治面貌
		/// </summary>
		PoliticalStatus = 3,
		/// <summary>
		/// 部位名称类型
		/// </summary>
		PartNameType = 4,
		/// <summary>
		/// 规格型号
		/// </summary>
		Specific = 5,
		/// <summary>
		/// 银行类型
		/// </summary>
		BankType = 6,
		/// <summary>
		/// 品牌
		/// </summary>
		Brand = 7,
		/// <summary>
		/// 租赁期限
		/// </summary>
		RentYears = 8,
		/// <summary>
		/// 学历
		/// </summary>
		Education = 9,
		/// <summary>
		/// 系统及设备类别
		/// </summary>
		EquipmentNO = 10,
		/// <summary>
		/// 商铺类型
		/// </summary>
		ShopType = 11,
		/// <summary>
		/// 证件类型
		/// </summary>
		CertType = 12,
		/// <summary>
		/// 公寓审核不通过原因
		/// </summary>
		ApplyNotApproveReason = 13,
		/// <summary>
		/// 商铺审核不通过原因
		/// </summary>
		BusinessmanApplyNotApproveReason = 14,
		/// <summary>
		/// 投诉类别
		/// </summary>
		ComplaintType = 15,
		/// <summary>
		/// 添加者：尤啸
		/// 时间:2012-06-05
		/// 邮件/短信模板类型
		/// </summary>
		ModleType = 16,

		/// <summary>
		/// 婚姻状况
		/// </summary>
		MarriageStatus = 17,

		/// <summary>
		/// 住房情况
		/// </summary>
		HousingStatus = 18,

		/// <summary>
		/// 关系
		/// </summary>
		RelationOfApplyPerson = 19,

		/// <summary>
		/// 验房结果
		/// </summary>
		CheckRoomResult = 20,

		/// <summary>
		/// 日常费用类型
		/// </summary>
		DailyFee = 21,

		/// <summary>
		/// 银行卡类型
		/// </summary>
		BankCardType = 22,
		/// <summary>
		/// 审核不通过
		/// </summary>
		Disqualification = 23,

	}

	/// <summary>
	/// 合同类型
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumContractType
	{
		单位租赁合同 = 0,
		个人租赁合同 = 1,
		个人合租合同 = 2,
		企业收储租赁合同 = 3,
		个人收储租赁合同 = 4,
	}
	/// <summary>
	/// 房间类型
	/// 作者：尤啸
	/// 时间：2012-07-27
	/// </summary>
	public enum EnumRoomType
	{
		家庭型 = 0,
		部位型 = 1,
	}

	/// <summary>
	/// 合同类型
	/// 作者：姚东
	/// 时间：2012-07-28
	/// </summary>
	public enum EnumBookContractType
	{
		企业预定合同 = 0,
		个人预定合同 = 1,
		收储合同 = 2,
	}
	/// <summary>
	/// 租金类型(租金统计页面JS传参用到的)
	/// </summary>
	public enum EnumRentType
	{
		租金明细 = 0,
		欠租补缴金额 = 1,
		物业费已收明细 = 2,
		电梯费已收明细 = 3
	}
	/// <summary>
	/// 打印说明
	/// </summary>
	public enum PringExplain
	{
		保证金 = 0,
		押金 = 1,
		合计 = 2,
		制表 = 3,
		时间 = 4,
        月份=5,
        收款项目=6,
        出票张数=7,
        应收金额=8,
        实收金额=9,
        欠费金额=10,
        备注=11

	}

	public enum EnumCheckoutType
	{
		正常退租 = 0,
		提前退租 = 1
	}

	public enum EnumFeeType
	{
		保证金 = 0,
		水费 = 1,
		电费 = 2,
		采暖费基本费 = 3,
		租金 = 4,
		物业管理费 = 5,
		电梯费 = 6,
		退租赔偿费 = 7,
		采暖流量费 = 8
	}

	public enum EnumRefundType
	{
		退租退费 = 0,
		租金调价 = 1
	}

	public enum BatchApplyType
	{
		企业公示 = 0,
		个人公示 = 1
	}

	public enum BatchPubliclyType
	{
		初审公示 = 0,
		复审公示 = 1,
		续租公示 = 2
	}

	public enum EnumSysRoleType
	{
		企业 = 0,
		企业共同申请人 = 1,
		企业共同申请人家庭成员 = 2,
		个人申请人 = 3,
		个人申请人家庭成员 = 4
	}

	/// <summary>
	/// 租赁合同资格复审表 Type
	/// </summary>
	public enum EnumRenewAuditType
	{
		续租复审 = 0,
		日常复审 = 1
	}

	/// <summary>
	/// 个人退租登记表 Type
	/// </summary>
	public enum EnumIndividualCheckoutType
	{
		企业共同申请人退租 = 0,
		个人共同申请人退租 = 1,
	}

	/// <summary>
	/// 西安政务网账号信息管理 证件类型
	/// </summary>
	public enum EnumCertTypeXdz
	{
		居民身份证 = 0,
		学生证 = 1,
		军官证 = 2
	}

	/// <summary>
	///  证件类型
	/// </summary>
	public enum EnumCertType
	{
		身份证 = 01,
		军官证 = 02,
		护照 = 03
	}

	/// <summary>
	/// 内部用户类型
	/// </summary>
	public enum EnumInternalUserType
	{
		普通账号 = 0,
		多方登录账号 = 1
	}

	/// <summary>
	/// 附件上传类型
	/// </summary>
	public enum EnumApplyAttachment
	{
		身份证 = 0,
		结婚证 = 1,
		住房证明 = 2,
		社会保险缴纳 = 3,
		公积金缴纳 = 4
	}

	/// <summary>
	/// 人员类型
	/// </summary>
	public enum EnumPersonalType
	{
		企业共同申请人 = 0,
		个人主申请人 = 1,
		个人共同申请人 = 2,
	}
}
