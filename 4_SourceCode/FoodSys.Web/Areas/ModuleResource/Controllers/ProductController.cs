using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;
using FoodSys.Biz.BizAccess;
using Project.Web.Base.Utility;
using Project.Common;
using FoodSys.Web.Areas.ModuleResource.Models;

namespace FoodSys.Web.Areas.ModuleResource.Controllers
{
	public class ProductController : BaseController
	{
		private BizUTProduct bizUTProduct;
	 
		public ActionResult Index(ModelProductIndex model, ExportHelper export)
		{
			model.ExportObject = export;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看  
		/// </summary>
		public ActionResult Create(ModelProductCreate model)
		{
			ViewBag.PageState = model.PageState;
			model.RetriveData();
			return View(model);
		}

		/// <summary>
		/// 创建 修改 查看 
		/// </summary>
		[HttpPost]
		public ActionResult Create(ModelProductCreate model, FormCollection collection)
		{
			try
			{
				model.Save();
				return Content(WebTools.ScriptCloseDialog(DialogOption.GetDefaultInstance()));
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

				bizUTProduct.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
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
					bizUTProduct.DeleteByID(checkboxItem[i], NHibernate.NHibernateUtil.String);
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

	}
}
