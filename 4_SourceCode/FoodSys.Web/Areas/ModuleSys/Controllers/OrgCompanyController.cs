using System;
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
	/// <summary>
	/// 企业管理
	/// 作者：尤啸
	/// 时间：2012-06-27
	/// </summary>
	public class OrgCompanyController : BaseController
	{
		private BizUTOrgCompany bizUTOrgCompany;

		public ActionResult Index(MdoelOrgCompanyIndex model, ExportHelper export)
		{
			model.ExportObject = export;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看 企业
		/// </summary>
		public ActionResult Create(MdoelOrgCompanyCreate model)
		{
			ViewBag.PageState = model.PageState;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看 企业
		/// </summary>
		[HttpPost]
		public ActionResult Create(MdoelOrgCompanyCreate model, FormCollection collection)
		{
			try
			{
				model.Save();
                return Content(WebTools.ScriptCloseEmbeddedFrameDialog(DialogOption.GetDefaultInstance()));
			}
			catch
			{
                //操作失败！
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
				bizUTOrgCompany.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
                jsresult.Data = new { result = string.Empty };
			}
			catch
			{
                //此企业下已有个人用户信息，无法做删除操作；\r请确保此企业下无个人用户信息后，再做删除操作！
                jsresult.Data = new { result = FoodSys.Resources.Properties.Resources.M10007E };
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
					bizUTOrgCompany.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
				}
                Message = FoodSys.Resources.Properties.Resources.M00001I;
				return RedirectToAction("Index");
			}
			catch
			{
                //您所要删除的企业中至少有一个企业已有个人用户信息；\\r无法做删除操作，请删除该企业下的所有个人用户信息！
                Error = FoodSys.Resources.Properties.Resources.M10008E;
				return Redirect(RetUrl);
			}
		}

		/// <summary>
		/// 验证企业名称是否已经存在
		/// </summary>
		public ActionResult RemoteCheckOrgCompanyName(MdoelOrgCompanyCreate model)
		{
			if (!string.IsNullOrEmpty(Request.Params["UTOrgCompanyEntity.IsNeedCompanyNameValidate"]) && Request.Params["UTOrgCompanyEntity.IsNeedCompanyNameValidate"] == "false")
				return Json(true, JsonRequestBehavior.AllowGet);

			if (model.UTOrgCompanyEntity.ID != Guid.Empty)
				return bizUTOrgCompany.CountBy(x => x.ID != model.UTOrgCompanyEntity.ID && x.Name == model.UTOrgCompanyEntity.Name) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizUTOrgCompany.CountBy(x => x.Name == model.UTOrgCompanyEntity.Name) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 验证组织机构代码是否已经存在
		/// </summary>
		public ActionResult RemoteCheckCompanyCode(MdoelOrgCompanyCreate model)
		{
			if (!string.IsNullOrEmpty(Request.Params["UTOrgCompanyEntity.IsNeedCompanyCodeValidate"]) && Request.Params["UTOrgCompanyEntity.IsNeedCompanyCodeValidate"] == "false")
				return Json(true, JsonRequestBehavior.AllowGet);

            if (model.UTOrgCompanyEntity.ID != Guid.Empty)
				return bizUTOrgCompany.CountBy(x => x.ID != model.UTOrgCompanyEntity.ID && x.OrganizationCode == model.UTOrgCompanyEntity.OrganizationCode) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizUTOrgCompany.CountBy(x => x.OrganizationCode == model.UTOrgCompanyEntity.OrganizationCode) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 验证税务登记证号是否已经存在
		/// </summary>
		public ActionResult RemoteCheckTaxRegisterNo(MdoelOrgCompanyCreate model)
		{
			if (!string.IsNullOrEmpty(Request.Params["UTOrgCompanyEntity.IsNeedTaxRegisterNoValidate"]) && Request.Params["UTOrgCompanyEntity.IsNeedTaxRegisterNoValidate"] == "false")
				return Json(true, JsonRequestBehavior.AllowGet);

			if (model.UTOrgCompanyEntity.ID != Guid.Empty)
				return bizUTOrgCompany.CountBy(x => x.ID != model.UTOrgCompanyEntity.ID && x.TaxRegNO == model.UTOrgCompanyEntity.TaxRegNO) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizUTOrgCompany.CountBy(x => x.TaxRegNO == model.UTOrgCompanyEntity.TaxRegNO) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 验证营业执照代码是否已经存在
		/// </summary>
		public ActionResult RemoteCheckLicenseNo(MdoelOrgCompanyCreate model)
		{
			if (!string.IsNullOrEmpty(Request.Params["UTOrgCompanyEntity.IsNeedLicenseNoValidate"]) && Request.Params["UTOrgCompanyEntity.IsNeedLicenseNoValidate"] == "false")
				return Json(true, JsonRequestBehavior.AllowGet);

			if (model.UTOrgCompanyEntity.ID != null)
				return bizUTOrgCompany.CountBy(x => x.ID != model.UTOrgCompanyEntity.ID && x.LicenseNo == model.UTOrgCompanyEntity.LicenseNo) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
			else
				return bizUTOrgCompany.CountBy(x => x.LicenseNo == model.UTOrgCompanyEntity.LicenseNo) > 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 选择企业
		/// </summary>
		/// <returns></returns>
		public ActionResult Select(ModelOrgCompanySelect model)
		{
			model.RetriveData();
			return View(model);
		}

	}
}
