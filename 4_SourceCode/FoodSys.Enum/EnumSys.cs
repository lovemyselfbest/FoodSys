using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;

namespace FoodSys.Enum
{
	/// <summary>
	/// 企业类型
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public enum EnumCompanyType
	{
		普通单位 = 0,
		机关单位 = 1
	}

	/// <summary>
	/// 日志类型
	/// 作者：姚东
	/// 时间：20111020
	/// </summary>
	public enum EnumLogType
	{
		登录日志 = 0,
		操作日志 = 1,
		错误日志 = 2
	}

	/// <summary>
	/// 0、不可维护（资产） 1、可维护
	/// 作者：黄剑锋
	/// 时间：2011-10-20
	/// </summary>
	public enum EnumCodeTypeClass
	{
		不可维护 = 0,
		可维护 = 1,
	}


	/// <summary>
	/// 打印类型
	/// 作者：姚东
	/// 时间：20120118
	/// </summary>
	public enum EnumPrintType
	{
		公寓续退租征询单 = 0,
		公寓欠租催缴通知书 = 1,
		公寓解除合同通知书 = 2,
		商铺续退租征询单 = 3,
		商铺欠租催缴通知书 = 4,
		商铺解除合同通知书 = 5,
	}

	/// <summary>
	/// Flash统计图类型
	/// </summary>
	public enum FlashChartType
	{
		/// <summary>
		/// 折线图
		/// </summary>
		Line2D = 0,
		/// <summary>
		/// 2D柱图
		/// </summary>
		Column2D = 1,
		/// <summary>
		/// 3D柱图
		/// </summary>
		Column3D = 2,
		/// <summary>
		/// 2D饼图
		/// </summary>
		Pie2D = 3,
		/// <summary>
		/// 3D饼图
		/// </summary>
		Pie3D = 4,
		/// <summary>
		/// 面积图
		/// </summary>
		Area2D = 5,
		/// <summary>
		/// 3D柱线图
		/// </summary>
		ColumnLine3D = 6,
		/// <summary>
		/// 雷达图
		/// </summary>
		Radar = 7,
		/// <summary>
		/// 多Y轴折线图
		/// </summary>
		MultiAxisLine = 8,
		/// <summary>
		/// 3D堆积图
		/// </summary>
		StackedColumn3D = 9,
	}
    public enum EnumClanderType
    {
        current = 0,
        next = 1,
        prev = 2
    }

	/// <summary>
	/// 用户类型
	/// </summary>
	public enum EnumUserType
	{
		[AttachData(EnumAttachDataKey.String, "f14b4b7e-99a3-4fb4-ac04-a07f010cd98d")]
		保障房中心用户 = 0,

		[AttachData(EnumAttachDataKey.String, "e44c49db-0aa7-4268-9bd0-a07f010d0862")]
		住房保障工作领导小组办公室 = 1,

		[AttachData(EnumAttachDataKey.String, "9d6a61e5-48e5-4c6e-ab18-a07f010d2795")]
		公租房公司 = 2,

		[AttachData(EnumAttachDataKey.String, "364a06fb-3f95-4b6f-952e-a07f010d8e7a")]
		信息中心 = 3,

		[AttachData(EnumAttachDataKey.String, "2d44d271-9a24-47e8-8fd7-a07f010f190c")]
		党工委 = 4,

		[AttachData(EnumAttachDataKey.String, "f055b9c3-91a2-4ae7-afa4-a0c900e6e81a")]
		企业用户 = 5,

		[AttachData(EnumAttachDataKey.String, "f26aceeb-c92a-4bd0-8512-a0c900e6da32")]
		个人租户用户 = 6
	}

	public static class EnumUserTypeExtensions
	{
		public static string GetText(this EnumUserType userType)
		{
			return userType.GetAttachedData<string>(EnumAttachDataKey.String);
		}
	}
}
