using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;

namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
    public class SysUserTypeManageController : BaseController
    {
        private BizSysUserType bizSysUserType;
        /// <summary>
        /// 作者：尤啸
        /// 时间：2012-06-28
        /// </summary>
        /// <param name="model"></param>
        /// <param name="selectTabsId"></param>
        /// <returns></returns>
        public ActionResult Index(ModelSysUserTypeManageIndex model, int? selectTabsId)
        {
            model.SelectTabsId = selectTabsId;
            model.RetriveData();
            return View(model);
        }

        /// <summary>
        /// 移动用户类型菜单
        /// </summary>
        /// <param name="menuGuid"></param>
        /// <param name="operateType"></param>
        /// <param name="sysRoleID"></param>
        /// <returns></returns>
        public ActionResult MoveUserTypeMenu(IList<Guid> menuGuid, EnumDirection operateType, Guid? sysUserTypeID)
        {
            ModelSysUserTypeManageMoveUserTypeMenu model = new ModelSysUserTypeManageMoveUserTypeMenu();
            model.SysUserTypeID = sysUserTypeID;
            model.MenuGuid = menuGuid;
            model.OperateType = operateType;
            Error = model.MoveMenu();

            return RedirectToAction("Index", "SysUserTypeManage", new { SysUserTypeID = sysUserTypeID});
        }
        /// <summary>
        /// 新增或修改角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(ModelSysUserTypeManageCreate model)
        {
            model.RetriveData();
            return View(model);
        }

        /// <summary>
        /// 新增或修改角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ModelSysUserTypeManageCreate model, FormCollection collection)
        {
            try
            {
                model.Save();
                var option = DialogOption.GetDefaultInstance();
                option.RefreshOpenerAsynchronous = false;
                return Content(WebTools.ScriptCloseDialog(option));
            }
            catch
            {
                Error = Resources.Properties.Resources.M00002E;
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
                bizSysUserType.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
                jsresult.Data = new { result = string.Empty };
            }
            catch
            {
                //删除失败，请检查此角色是否已经被使用！
                jsresult.Data = new { result = Resources.Properties.Resources.M10010E };
            }
            return jsresult;
        }

        /// <summary>
        /// 验证用户类型名称是否已经存在
        /// </summary>
        public ActionResult RemoteCheckUserTypeName(ModelSysUserTypeManageCreate model)
        {
            if (model.SysUserType.ID != null)
                return bizSysUserType.CountBy(x => x.ID != model.sysUserTypeID && x.Name == model.SysUserType.Name) > 0
                    ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
            else
                return true ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
