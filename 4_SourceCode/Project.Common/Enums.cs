using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Common
{

	/// <summary>
	/// 排序
	/// </summary>
	public enum EnumOrder
	{
		ASC = 0,
		DESC = 1
	}

	/// <summary>
	/// 选择模式
	/// </summary>
	public enum EnumSelectModel
	{
		CheckBox = 0,
		Radio = 1
	}

	/// <summary>
	/// 页面状态
	/// </summary>
	public enum EnumPageState
	{
		新增 = 0,
		编辑 = 1,
		查看 = 2
	}

	/// <summary>
	/// 位置
	/// </summary>
	public enum EnumLoaction
	{
		First = 0,
		Last = 1
	}

	/// <summary>
	/// 方向
	/// </summary>
	public enum EnumDirection
	{
		Left = 0,
		Right = 1,
		Up = 2,
		Down = 3
	}

	/// <summary>
	/// 正确或则错误
	/// </summary>
	public enum EnumTrueOrFalse
	{
		False = 0,
		True = 1
	}

	/// <summary>
	/// 是否
	/// </summary>
	public enum EnumYesOrNo
	{
		否 = 0,
		是 = 1
	}

	/// <summary>
	/// 性别
	/// </summary>
	public enum EnumSex
	{
		女 = 0,
		男 = 1
	}

	/// <summary>
	/// 页面窗口实例
	/// </summary>
	public enum EnumDialog
	{
		Dialog1 = 1,
		Dialog2 = 2,
		Dialog3 = 3
	}

}
