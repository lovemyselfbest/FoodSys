using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using Project.Common;

namespace FoodSys.Web.Out.Controllers
{
	public class UploadController : Controller
	{
		//
		// GET: /Upload/

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Picture()
		{
			if (Request.Files.Count < 0)
				return new JsonResult() { ContentType = Consts.CONTENT_TYPE, Data = new { error = 0, url = "请选择文件" } };
			HttpPostedFileBase filedata = Request.Files[0];
			// 统一转换为byte数组处理
			byte[] file;
			string localname = "";
			string disposition = Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];
			if (disposition != null)
			{
				// HTML5上传
				file = Request.BinaryRead(Request.TotalBytes);
				localname = Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value;// 读取原始文件名
			}
			else
			{
				// 读取原始文件名
				localname = filedata.FileName;
				// 初始化byte长度.
				file = new Byte[filedata.ContentLength];

				// 转换为byte类型
				System.IO.Stream stream = filedata.InputStream;
				stream.Read(file, 0, filedata.ContentLength);
				stream.Close();
			}
			string extensionName = Path.GetExtension(localname);
			string fileName =string.Format("{0}{1}", Guid.NewGuid().ToString(), extensionName);
			string physicPath = Server.MapPath("/Files/Temp/") + fileName;
			string httpPath = string.Format("/Files/Temp/{0}", fileName);
			FileStream fs = new FileStream(physicPath, FileMode.OpenOrCreate);
			BinaryWriter writer = new BinaryWriter(fs);
			writer.Write(file);
			writer.Flush();
			fs.Flush();
			fs.Close();
			var result = new { error = 0, url = httpPath };

            return new JsonResult() { ContentType = Consts.CONTENT_TYPE, Data = result };
		}

		

	}
}
