using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Web.Base;
using XAGZFSys.Web.Models;
using Project.Web.Base.Utility;
using Project.Common;
using XAGZFSys.Biz.BizAccess;

namespace XAGZFSys.Web.Controllers
{
    public class DefaultController : BaseController
    {
        private BizUTSample bizUTSupplier;
        //
        // GET: /Default/

        public ActionResult Index(ModelDefaultIndex model,ExportHelper export)
        {
            model.ExportObject = export;
            model.RetriveData();
            return View(model);
        }

        public ActionResult Create(Guid? id, EnumPageState? pageState)
        {
            ViewBag.PageState = GetPageState(id, pageState);
            if (id != null)
            {
                //修改供应商信息
                ModelDefaultCreate model = new ModelDefaultCreate();
                model.UTSample = bizUTSupplier.GetByID<Guid>(id.Value);
                return View(model);
            }
            return View();
        }

        /// <summary>
        /// 新增供应商信息
        /// </summary>
        /// <param name="modelUTSupplierManage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ModelDefaultCreate model)
        {
            try
            {
                model.Save();
                return Content(WebTools.ScriptCloseDialog(DialogOption.GetDefaultInstance(new DialogOption() { HighlightData = model.UTSample.ID })));
            }
            catch
            {
                Error = "操作失败";
                ViewBag.PageState = PageState;
                return View(model);
            }
        }

    }
}
