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
	/// 部门管理
	/// 作者：尤啸
	/// 时间：2012-06-26
	/// </summary>
	public class SysOrgDepartmentManageController : BaseController
	{
		private BizUTOrgDepartment bizUTOrgDepartment;

        public ActionResult Index(ModelSysOrgDepartmentManageIndex model, Guid? organizationId)
        {
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
        public ActionResult Create(ModelSysOrgDepartmentManageCreate model, Guid? organizationId)
        {
            model.OrganizationId = organizationId;
            ViewBag.PageState = model.PageState;
            model.RetriveData();
            return View(model);
        }

        /// <summary>
        ///修改 查看 
        /// </summary>
        [HttpPost]
        public ActionResult Create(ModelSysOrgDepartmentManageCreate model, FormCollection collection)
        {
            try
            {
                model.Save();
                return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
            }
            catch
            {
                Error = Resources.Properties.Resources.M00002E;
                model.RetriveData();
                return View(model);
            }
        }

		/// <summary>
		/// 根据ID删除部门
		/// </summary>
		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			JsonResult jsresult = new JsonResult();
			jsresult.ContentType = Consts.CONTENT_TYPE;
			jsresult.Data = new { result = string.Empty };
			try
			{
                if (bizUTOrgDepartment.CountBy(x => x.ParentID == id) > 0)
				{
                    jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10009E };
				}
				else
				{
					bizUTOrgDepartment.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
					jsresult.Data = new { result = string.Empty };
				}
			}
			catch
			{
				//此部门下已有用户信息，无法做删除操作；\r请确保此部门下无用户信息后，再做删除操作！
                jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10009E };
			}
			return jsresult;
		}

		/// <summary>
		/// 批量ID删除部门
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

					bizUTOrgDepartment.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
				}
				return RedirectToAction("Index");
			}
			catch
			{
                Error = FoodSys.Resources.Properties.Resources.M00002E;
				return Redirect(RetUrl);
			}
		}

		/// <summary>
		/// 显示部门详细信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Details(Guid? id)
		{
			ModelSysOrgDepartmentManageDetails model = new ModelSysOrgDepartmentManageDetails();
			model.UTOrgDepartmentID = id;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 
		/// </summary>
		[HttpPost]
		public ActionResult Details(ModelSysOrgDepartmentManageDetails model)
		{
			model.Save();
			return RedirectToAction("Index");
		}

		/// <summary>
		/// 验证部门是否已经存在
		/// </summary>
		public ActionResult RemoteCheckDepartmentName(Guid? id, string departmentName)
		{
			if (id != null)
				return bizUTOrgDepartment.CountBy(x => x.ID != id && x.DepartmentName == departmentName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizUTOrgDepartment.CountBy(x => x.DepartmentName == departmentName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}
	}
}
