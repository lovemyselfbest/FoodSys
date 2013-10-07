using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace Project.Web.Base.Utility
{
	public class UploadHelper
	{
		public const string PHYSIC_ROOT = "\\Files\\Upload\\";
		public const string HTTP_ROOT = "/Files/Upload/";

		public const string PHYSIC_ROOT_ORIGINAL_IMG = "\\Files\\UploadOriginalImg\\";
		public const string HTTP_ROOT_ORIGINALL_IMG = "/Files/UploadOriginalImg/";



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

		public static string SaveOriginalImgFile(HttpPostedFileBase file)
		{
			if (file == null)
				return string.Empty;
			string fileName = GenerateFileName(file.FileName);
			string directory = HttpContext.Current.Server.MapPath(PHYSIC_ROOT_ORIGINAL_IMG + GetFileDirectoryPath(fileName));
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

		/// <summary>
		/// 根据比例裁剪原图
		/// </summary>
		/// <param name="file">文件对象</param>
		/// <param name="filename">文件名：生成好的文件名</param>
		/// <param name="maxWidth">宽</param>
		/// <param name="maxHeight">高</param>
		/// <param name="quality">质量（范围0-100）</param>
		public static void CutImg(HttpPostedFileBase file, string filename, int maxWidth, int maxHeight, int quality = 100)
		{
			//从文件获取原始图片，并使用流中嵌入的颜色管理信息
			Image initImage = System.Drawing.Image.FromStream(file.InputStream, true);

			string directory = HttpContext.Current.Server.MapPath(PHYSIC_ROOT + GetFileDirectoryPath(filename));
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			string physicPath = directory + "\\" + filename;

			//原图宽高均小于模版，不作处理，直接保存
			if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
			{
				initImage.Save(physicPath, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			else
			{
				//模版的宽高比例
				double templateRate = (double)maxWidth / maxHeight;
				//原图片的宽高比例
				double initRate = (double)initImage.Width / initImage.Height;

				//原图与模版比例相等，直接缩放
				if (templateRate == initRate)
				{
					//按模版大小生成最终图片
					System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
					System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
					templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
					templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					templateG.Clear(Color.White);
					templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
					templateImage.Save(physicPath, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				//原图与模版比例不等，裁剪后缩放
				else
				{
					//裁剪对象
					System.Drawing.Image pickedImage = null;
					System.Drawing.Graphics pickedG = null;

					//定位
					Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
					Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

					//宽为标准进行裁剪
					if (templateRate > initRate)
					{
						//裁剪对象实例化
						pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)System.Math.Floor(initImage.Width / templateRate));
						pickedG = System.Drawing.Graphics.FromImage(pickedImage);

						//裁剪源定位
						fromR.X = 0;
						fromR.Y = (int)System.Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
						fromR.Width = initImage.Width;
						fromR.Height = (int)System.Math.Floor(initImage.Width / templateRate);

						//裁剪目标定位
						toR.X = 0;
						toR.Y = 0;
						toR.Width = initImage.Width;
						toR.Height = (int)System.Math.Floor(initImage.Width / templateRate);
					}
					//高为标准进行裁剪
					else
					{
						pickedImage = new System.Drawing.Bitmap((int)System.Math.Floor(initImage.Height * templateRate), initImage.Height);
						pickedG = System.Drawing.Graphics.FromImage(pickedImage);

						fromR.X = (int)System.Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
						fromR.Y = 0;
						fromR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
						fromR.Height = initImage.Height;

						toR.X = 0;
						toR.Y = 0;
						toR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
						toR.Height = initImage.Height;
					}

					//设置质量
					pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

					//裁剪
					pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

					//按模版大小生成最终图片
					System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
					System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
					templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
					templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					templateG.Clear(Color.White);
					templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

					//关键质量控制
					//获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
					ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
					ImageCodecInfo ici = null;
					foreach (ImageCodecInfo i in icis)
					{
						if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
						{
							ici = i;
						}
					}
					EncoderParameters ep = new EncoderParameters(1);
					ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

					//保存缩略图
					templateImage.Save(physicPath, ici, ep);


					//释放资源
					templateG.Dispose();
					templateImage.Dispose();

					pickedG.Dispose();
					pickedImage.Dispose();
				}
			}

			//释放资源
			initImage.Dispose();
		}
	}
}