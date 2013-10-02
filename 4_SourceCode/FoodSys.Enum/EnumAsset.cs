using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Common;

namespace FoodSys.Enum
{
    /* “资产管理”用到的枚举
    ----------------------------------------------------------*/

    /// <summary>
    /// 幢用途
    /// </summary>
    public enum EnumBuidingUsing
    {
        住宅 = 0,
        //商用 = 1,
        其他 = 2,
    }

    /// <summary>
    /// 建筑类型
    /// </summary>
    public enum EnumBuildingType
    {
        多层 = 0,
        框架 = 1
    }

	/// <summary>
	/// 布局类型
	/// </summary>
	public enum EnumLayoutType
	{
		U型 = 0,
		线性 = 1
	}

    /// <summary>
    /// 房源用途
    /// </summary>
    public enum EnumRoomKindsUsing
    {
        公租房 = 0,
        公租收储房 = 1,
        其他 = 2
    }

    /// <summary>
    /// 室用途
    /// </summary>
    public enum EnumRoomUsing
    {
        出租 = 0,
        其它 = 1,
    }

    /// <summary>
    /// 部位用途
    /// </summary>
    public enum EnumRoomTypePartUsing
    {
        出租 = 0,
        保留 = 1,
        其它 = 2
    }

    /// <summary>
    /// 允许性别
    /// </summary>
    public enum EnumAllowSex
    {
        女 = 0,
        男 = 1,
        不限 = 2
    }



    /* 动资产用到的参数
    ----------------------------------------------------------*/

    /// <summary>
    /// 设施系统编码
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumFacilityNo
    {
        /// <summary>
        /// 租赁房家电家具设施
        /// </summary>
        [AttachData(EnumAttachDataKey.String, "租赁房家电家具设施")]
        Z = 0,

        /// <summary>
        /// 公共配套设置
        /// </summary>
        [AttachData(EnumAttachDataKey.String, "公共配套设置")]
        G = 1,

        /// <summary>
        /// 办公设备
        /// </summary>
        [AttachData(EnumAttachDataKey.String, "办公设备")]
        B = 2
    }

    public static class EnumFacilityNoExtensions
    {
        public static string GetText(this EnumFacilityNo facilityNo)
        {
            return facilityNo.GetAttachedData<string>(EnumAttachDataKey.String);
        }
    }

    /// <summary>
    ///  资产配置变更生效状态
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumEffect
    {
        未生效 = 0,
        已生效 = 1
    }

    /// <summary>
    /// 动资产状态
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumAssetStatus
    {
        闲置 = 0,
        已分配 = 1,
        报修 = 2,
        报废 = 3,
        分配中 = 4
    }

    /// <summary>
    /// 动资产分配位置类型
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumAssignAddressType
    {
        项目 = 0,
        幢 = 1,
        部位 = 2,
        仓库 = 3
    }

    /// <summary>
    /// 不动产配置变更类型
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumAssetChangeType
    {
        添加 = 0,
        移除 = 1,
        替换 = 2
    }

    /// <summary>
    /// 采购单状态
    /// 作者：尤啸
    /// 时间2012-06-26
    /// </summary>
    public enum EnumPOStatus
    {
        未确认 = 0,
        确认 = 1
    }

    /// <summary>
    /// 幢朝向
    /// </summary>
    public enum EnumOrientation
    {
        南 = 0,
        北 = 1,
    }

    /// <summary>
    /// 小区类型
    /// </summary>
    public enum EnumCommunityType
    {
        自建小区 = 0,
        收储房源 = 1
    }

    /// <summary>
    /// 停车位状态
    /// </summary>
    public enum EnumParkingSpaceStatus
    {
        闲置 = 0,
        已分配 = 1,
        保留 = 2
    }

    /// <summary>
    /// 装修状态
    /// </summary>
    public enum EnumDecorateStatus {
        未装修 = 0,
        已装修 = 1    
    }

    /// <summary>
    /// 停车位位置
    /// </summary>
    public enum EnumParkingArea
    {
       
        [AttachData(EnumAttachDataKey.String, "地面")]
        G = 0,

        [AttachData(EnumAttachDataKey.String, "地下")]
        B = 1
    }
    public static class EnumParkingAreaExtensions
    {
        public static string GetText(this EnumParkingArea parkingArea)
        {
            return parkingArea.GetAttachedData<string>(EnumAttachDataKey.String);
        }
    }
}
