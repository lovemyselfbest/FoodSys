using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodSys.Web.Base;
using FoodSys.Entity;

namespace FoodSys.Web.Areas.ModuleLogin.Models
{
	public class ModelLoginIndex : Model
	{
		public SysUser SysUser
		{
			get;
			set;
		}

		public bool RememberLoginStatus
		{
			get;
			set;
		}

	}
}