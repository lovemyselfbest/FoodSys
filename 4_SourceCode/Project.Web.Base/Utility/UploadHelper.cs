using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Project.Web.Base.Utility
{
	public class UploadHelper
	{
		public const string PHYSIC_ROOT = "\\Files\\Upload\\";
		public const string HTTP_ROOT = "/Files/Upload/";
		public static string SaveFile(HttpPostedFileBase file)
		{
			if (file == null)
				return string.Empty;
			string fileName = GenerateFileName(file.FileName);
			string directory = HttpContext.Current.Server.MapPath(PHYSIC_ROOT + GetFileDirectoryPath(fileName));
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			string physicPath = directory + "\\" + fileName;
			file.SaveAs(physicPath);
			return fileName;
		}

		/// <summary>
		/// 生成文件名称
		/// </summary>
		/// <param name="originalFileName"></param>
		/// <returns></returns>
		public static string GenerateFileName(string originalFileName)
		{
			Guid fileGuid = Guid.NewGuid();
			return DateTime.Now.ToString("yyyy_MM_dd") + (DateTime.Now.Hour <= 12 ? "_am" : "_pm") + "_" + fileGuid.ToString() + Path.GetExtension(originalFileName);
		}

		/// <summary>
		/// 根据文件名称获得文件夹路径，不包括文件名部分
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string GetFileDirectoryPath(string fileName)
		{
			return fileName.Substring(0, 13).Replace("_", "\\");
		}

		/// <summary>
		/// 获得文件http访问路径
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string GetFileHttpUrl(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return string.Empty;
			return HTTP_ROOT + fileName.Substring(0, 14).Replace("_", "/") + fileName;
		}

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}
	}
}