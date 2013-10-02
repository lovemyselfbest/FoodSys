using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using Project.Web.Base.Utility;
using Project.Web.Base;
using FoodSys.Entity;
using FoodSys.Enum;
using FoodSys.Biz.BizAccess;
using Project.Common;

namespace FoodSys.Web.Base
{
	/// <summary>
	/// Model333
	/// </summary>
	public abstract class Model : Project.Web.Base.BaseModel
	{
		private BizSysCodeInfo bizSysCodeInfo = Project.Biz.Base.BizFactory.Get<BizSysCodeInfo>();
		private BizSysUser bizSysUser = Project.Biz.Base.BizFactory.Get<BizSysUser>();

		public Model()
		{

		}

		public string Navigation
		{
			get;
			set;
		}

		/// <summary>
		/// 代码组信息集合
		/// </summary>
		private Dictionary<EnumSysCodeInfoType, IList<SysCodeInfo>> sysCodeInfoDictionary = new Dictionary<EnumSysCodeInfoType, IList<SysCodeInfo>>();
		public IList<SysCodeInfo> ListSysCodeInfo(EnumSysCodeInfoType enumSysCodeInfoType)
		{
			if (sysCodeInfoDictionary.Keys.Contains(enumSysCodeInfoType))
				return sysCodeInfoDictionary[enumSysCodeInfoType];
			sysCodeInfoDictionary.Add(enumSysCodeInfoType, bizSysCodeInfo.ListBy(x => x.Type == enumSysCodeInfoType.ToString(), x => x.Code, EnumOrder.ASC));
			return sysCodeInfoDictionary[enumSysCodeInfoType];
		}

		private IList<SysUser> sysUserCollection;
		/// <summary>
		/// 用户表（内部） 集合
		/// </summary>
		public IList<SysUser> SysUserCollection
		{
			get
			{
				if (sysUserCollection == null)
				{
					sysUserCollection = bizSysUser.List();
				}
				return sysUserCollection;
			}
		}

		private SelectList emptySelectList;
		/// <summary>
		/// 空列表
		/// </summary>
		public SelectList EmptySelectList
		{
			get
			{
				if (emptySelectList != null)
					return emptySelectList;
				List<SelectListItem> list = new List<SelectListItem>();
				emptySelectList = SelectListHelper.InsertFirstOption(new SelectList(list), "-- 请选择 --", "");
				return emptySelectList;
			}
		}
	}
}