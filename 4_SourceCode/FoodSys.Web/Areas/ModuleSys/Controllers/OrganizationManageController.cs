using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;
using FoodSys.Web.Areas.ModuleSys.Models;
using Project.Common;
using FoodSys.Biz.BizAccess;
using FoodSys.Entity;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
    public class OrganizationManageController : BaseController
    {
        private BizUTOrganization bizUTOrganization;
        private BizUTOrgDepartment bizUTOrgDepartment;
       // private BizSysUser bizSysUser;
        //
        // GET: /ModuleSys/OrganizationManage/
        /// <summary>
        /// 组织机构管理
        /// 作者：尤啸
        /// 日期：2012-07-02
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index(ModelOrganizationManageIndex model)
        {
            model.RetriveData();
            return View(model);
        }
        /// <summary>
        /// 创建、查看、修改
        /// </summary>
        public ActionResult Create(Guid? id, EnumPageState? pageState)
        {
            ViewBag.PageState = pageState;
            if (id != null)
                return View(bizUTOrganization.GetByID<Guid>(id.Value));
            return View();
        }

        /// <summary>
        /// 创建、查看、修改
        /// </summary>
        [HttpPost]
        public ActionResult Create(UTOrganization departmentEntity)
        {
            try
            {
                ModelOrganizationManageIndex model = new ModelOrganizationManageIndex();
                model.Save(departmentEntity);
                var option = DialogOption.GetDefaultInstance();
                option.RefreshOpenerAsynchronous = false;
                return Content(WebTools.ScriptCloseDialog(option));
            }
            catch
            {
                Error = FoodSys.Resources.Properties.Resources.M00002E;
                ViewBag.PageState = PageState;
                return View(departmentEntity);
            }
        }
        /// <summary>
        /// 根据ID删除机构
        /// </summary>
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            JsonResult jsresult = new JsonResult();
            jsresult.ContentType = Consts.CONTENT_TYPE;
            jsresult.Data = new { result = string.Empty };
            try
            {
                if (bizUTOrgDepartment.CountBy(x => x.OrganizationID == id) > 0)
                {
                    jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10009E };
                }
                else
                {
                    bizUTOrganization.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
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
        /// 验证机构是否已经存在
        /// </summary>
        public ActionResult RemoteCheckDepartmentName(Guid? id, string departmentName)
        {
            if (id != null)
                return bizUTOrganization.CountBy(x => x.ID != id && x.Name == departmentName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
            else
                return bizUTOrganization.CountBy(x => x.Name == departmentName) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
