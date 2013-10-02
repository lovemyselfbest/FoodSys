using System;
using System.Web.Mvc;
using FoodSys.Biz.BizAccess;
using FoodSys.Dal;
using FoodSys.Enum;
using FoodSys.Web.Areas.ModuleSys.Models;
using FoodSys.Web.Base;
using Project.Common;
using Project.Web.Base;
using Project.Web.Base.Utility;
using Project.Dal.Base;
namespace FoodSys.Web.Areas.ModuleSys.Controllers
{
	/// <summary>
	/// 参数设置管理
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
	public class SysCodeInfoController : BaseController
	{
		private BizSysCodeInfo bizSysCodeInfo;
		public ActionResult Index(ModelSysCodeInfoIndex model)
		{
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看 参数
		/// </summary>
		public ActionResult Create(ModelSysCodeInfoCreate model, string sysCodeInfoType)
		{
			ViewBag.PageState = model.PageState;
			model.SysCodeInfoEntity.Type = sysCodeInfoType;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看 参数
		/// </summary>
		[HttpPost]
		public ActionResult Create(ModelSysCodeInfoCreate model, FormCollection collection)
		{
			try
			{
				model.Save();
				return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance(new DialogOption() { HighlightData=model.SysCodeInfoEntity.ID })));
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
				bizSysCodeInfo.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
				jsresult.Data = new { result = string.Empty };
			}
			catch
			{
				jsresult.Data = new { result = Resources.Properties.Resources.M00006E };
			}
			return jsresult;
		}

        /// <summary>
        /// 批量删除
        /// </summary>
        [HttpPost]
        public ActionResult DeleteBatch(string[] checkboxItem, string sysCodeInfoType)
        {
            try
            {
                for (int i = 0; i < checkboxItem.Length; i++)
                {
                    if (checkboxItem[i].ToLower() == EnumTrueOrFalse.False.ToString().ToLower())
                        continue;
                    bizSysCodeInfo.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
                }
                Message = Resources.Properties.Resources.M00001I;
                return RedirectToAction("Index", "SysCodeInfo", new { sysCodeInfoType = sysCodeInfoType });
            }
            catch
            {
                Error = Resources.Properties.Resources.M00002E;
                return Redirect(RetUrl);
            }
        }

		/// <summary>
		/// 验证参数名称是否已经存在
		/// </summary>
		public ActionResult RemoteCheckSysCodeInfoName(ModelSysCodeInfoCreate model)
		{
			string parentCode = Request["SysCodeInfoEntity.SysCodeInfoCode"];

			if (model.SysCodeInfoEntity.ID != Guid.Empty)
				return bizSysCodeInfo.CountBy(x => x.ID != model.SysCodeInfoEntity.ID && x.Name == model.SysCodeInfoEntity.Name && x.Code.Contains(parentCode)) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizSysCodeInfo.CountBy(x => x.Name == model.SysCodeInfoEntity.Name && x.Code.Contains(parentCode)) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult RetriveSpecificByEquipmentNO(string equipmentNO)
		{
			var specificCollection = bizSysCodeInfo.ListBy(x => x.Type == EnumSysCodeInfoType.Specific.ToString() && x.Code.Contains(equipmentNO));
			JsonResult result = new JsonResult();
			result.Data = specificCollection;
			result.ContentType = Consts.CONTENT_TYPE;
			return result;
		}

		/// <summary>
		///  验证代码是否已经存在
		/// </summary>
		public ActionResult RemoteCheckSysCodeInfoCode(ModelSysCodeInfoCreate model)
		{
			//TODO:该行代码的用法？？

			string SysCodeInfoCode = Request["SysCodeInfoEntity.SysCodeInfoCode"];
			if (SysCodeInfoCode != "undefined" && SysCodeInfoCode != null)
			{
				if (model.SysCodeInfoEntity.ID != Guid.Empty)
					return bizSysCodeInfo.CountBy(x => x.ID != model.SysCodeInfoEntity.ID && x.Code == SysCodeInfoCode + model.SysCodeInfoEntity.Code) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
				else
					return bizSysCodeInfo.CountBy(x => x.Code == SysCodeInfoCode + model.SysCodeInfoEntity.Code) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

			}
			else
			{
				if (model.SysCodeInfoEntity.ID != Guid.Empty)
					return bizSysCodeInfo.CountBy(x => x.ID != model.SysCodeInfoEntity.ID && x.Code == model.SysCodeInfoEntity.Code && x.Type == model.SysCodeInfoEntity.Type) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
				else
					return bizSysCodeInfo.CountBy(x => x.Code == model.SysCodeInfoEntity.Code && x.Type == model.SysCodeInfoEntity.Type) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

			}

		}
	}
}
