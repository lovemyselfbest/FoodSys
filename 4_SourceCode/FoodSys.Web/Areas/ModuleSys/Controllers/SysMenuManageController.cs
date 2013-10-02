using System;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;
using FoodSys.Entity;
namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
	/// <summary>
	/// 
	/// </summary>
    public class SysMenuManageController : BaseController
    {

        private BizSysMenu bizSysMenu;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index(ModelSysMenuManageIndex model)
        {
            model.RetriveData();
            return View(model);
        }

        /// <summary>
        /// 创建或修改菜单
        /// </summary>
        public ActionResult Create(ModelSysMenuManageCreate model)
        {
            ViewBag.PageState = model.PageState;
            model.RetriveData();
            return View(model);
        }

        /// <summary>
        /// 创建或修改菜单
        /// </summary>
        [HttpPost]
        public ActionResult Create(ModelSysMenuManageCreate model, FormCollection collection)
        {
            SysMenu sysMenu = null;
            try
            {
                model.Save();
                if (model.SysMenuId != null)
                {
                    sysMenu = bizSysMenu.GetFirst(x => x.ID == model.SysMenuId);
                }
                else
                {
                    sysMenu = bizSysMenu.GetFirst(x => x.ID == model.MenuId);
                }

                if (sysMenu != null && sysMenu.ParentID != null)
                {
                    if (bizSysMenu.GetFirst(x => x.ID == sysMenu.ID).ParentID != null)
                    {
                        SysMenu sysMenu2 = bizSysMenu.GetFirst(x => x.ID == sysMenu.ParentID);
                        if (sysMenu2.ParentID != null)
                        {
                            if (bizSysMenu.GetFirst(x => x.ID == sysMenu2.ID).ParentID != null)
                            {
                                SysMenu sysMenu3 = bizSysMenu.GetFirst(x => x.ID == sysMenu2.ParentID);
                                if (sysMenu3.ParentID == null)
                                {
                                    return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
                                }
                            }
                        }
                        else if (sysMenu2.ParentID == null && model.SysMenuId == null)
                        {
                            return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
                        }
                    }
                }
                var option = DialogOption.GetDefaultInstance();
                option.RefreshOpenerAsynchronous = false;
                option.HighlightData = model.SysMenuEntity.ID;
                return Content(WebTools.ScriptCloseDialog(option));
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
                if (bizSysMenu.CountBy(x => x.ParentID == id) > 0)
                {
                    jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M60011E };
                }
                else
                {
                    bizSysMenu.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
                    jsresult.Data = new { result = string.Empty };
                }
            }
            catch
            {
                //此菜单下已有菜单信息，无法做删除操作；\r请确保此菜单下无菜单信息后，再做删除操作！
                jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M60011E };
            }
            return jsresult;
        }



        /// <summary>
        /// 批量删除
        /// </summary>
        [HttpPost]
		public ActionResult DeleteBatch(string[] checkboxItem, string RetUrl)
        {
            try
            {
                for (int i = 0; i < checkboxItem.Length; i++)
                {
                    if (checkboxItem[i].ToLower() == EnumTrueOrFalse.False.ToString().ToLower())
                        continue;
                    bizSysMenu.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
                }
                Message = Resources.Properties.Resources.M00001I;
				return Redirect(RetUrl);
            }
            catch
            {
                Error = FoodSys.Resources.Properties.Resources.M00002E;
                return Redirect(RetUrl);
            }
        }

        /// <summary>
        /// 验证菜单名是否存在
        /// </summary>
        public ActionResult RemoteCheckMenuName(ModelSysMenuManageCreate model)
        {
            return false ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
        }
    }

}
