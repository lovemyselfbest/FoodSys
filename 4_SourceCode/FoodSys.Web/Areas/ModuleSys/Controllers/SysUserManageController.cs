using System;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
	/// <summary>
	/// 用户管理
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public class SysUserManageController : BaseController
	{
		private BizSysUser bizSysUser;
		private BizUTOrgDepartment bizUTOrgDepartment;

        public ActionResult Index(ModelSysUserManageIndex model, Guid? departmentId, Guid? organizationId)
		{
			model.DepartmentId = departmentId;
            model.OrganizationId = organizationId;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 新增修改查看
		/// </summary>
		/// <param name="sysUserId">用户ID</param>
		/// <param name="pageState">窗口状态</param>
		/// <param name="departmentId">部门ID</param>
		public ActionResult Create(ModelSysUserManageCreate model, Guid? departmentId)
		{
			model.DepartmentId = departmentId;
			ViewBag.PageState = model.PageState;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建或修改用户
		/// </summary>
		[HttpPost]
        public ActionResult Create(ModelSysUserManageCreate model)
		{
			try
			{
				model.Save();
                var option = DialogOption.GetDefaultInstance();
                option.RefreshOpenerAsynchronous = false;
                return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
			}
			catch
			{
                Error = FoodSys.Resources.Properties.Resources.M00002E;
				model.RetriveData();
				return View(model);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			JsonResult jsresult = new JsonResult();
            jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };
			try
			{
				bizSysUser.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
                jsresult.Data = new { result = string.Empty };
			}
			catch 
			{
                //删除操作失败，请去确认该数据是否正在被使用！
                jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M00006E };
			}
			return jsresult;
		}

		/// <summary>
		/// 批量删除
		/// </summary>
		[HttpPost]
		public ActionResult DeleteBatch(string[] checkboxItem)
		{
			try
			{
				for (int i = 0; i < checkboxItem.Length; i++)
				{
					if (checkboxItem[i].ToLower() == EnumTrueOrFalse.False.ToString().ToLower())
						continue;
					bizSysUser.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
				}
                Message = Resources.Properties.Resources.M00001I;
				return RedirectToAction("Index");
			}
			catch
			{
                Error = FoodSys.Resources.Properties.Resources.M00002E;
				return Redirect(RetUrl);
			}
		}

		/// <summary>
		/// 锁定或激活用户
		/// </summary>
		[HttpPost]
		public ActionResult SysUserIsLock(Guid? id, int status)
		{
			JsonResult jsresult = new JsonResult();
            jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };

			try
			{
				SysUser sysUser = bizSysUser.GetFirst(x => x.ID == id);
				sysUser.Status = status;
				bizSysUser.Update(sysUser);
			}
			catch
			{
                jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M00007E };

			}

			return jsresult;
		}

		/// <summary>
		/// 修改密码
		/// </summary>
		public ActionResult EditPassword(Guid? id)
		{
			return View();
		}

		/// <summary>
		/// 修改密码
		/// </summary>
		[HttpPost]
		public ActionResult EditPassword(Guid? id, string password)
		{
			try
			{
				SysUser sysUser = bizSysUser.GetFirst(x => x.ID == id);
				sysUser.Password = StringUtility.Encrypt(password);
				bizSysUser.Update(sysUser);
                return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
			}
			catch
			{
				return View();
			}
		}

		//批量锁定
		[HttpPost]
		public ActionResult LockSysUserBatch(string[] checkboxItem)
		{
			if (checkboxItem == null)
				return View("Index");
			for (int i = 0; i < checkboxItem.Length; i++)
			{
                if (checkboxItem[i].ToLower() == EnumTrueOrFalse.False.ToString().ToLower())
					continue;
				SysUser sysUser = bizSysUser.GetFirst(x => x.ID == new Guid(checkboxItem[i]));
				sysUser.Status = (int)EnumUserStatus.已锁定;
				bizSysUser.Update(sysUser);
			}
            Message = FoodSys.Resources.Properties.Resources.M00001I;
			return Redirect(RetUrl);
		}

		/// <summary>
		/// TODO: 验证用户名是否已经存在
		/// </summary>
        public ActionResult RemoteCheckUserName(ModelSysUserManageCreate model)
        {
            if (model.SysUserEntity.ID != null)
                return bizSysUser.CountBy(x => x.ID != model.SysUserEntity.ID && x.UserAccount == model.SysUserEntity.UserAccount) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
            else
                return bizSysUser.CountBy(x => x.UserAccount == model.SysUserEntity.UserAccount) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}
