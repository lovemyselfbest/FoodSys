using System;
using System.Linq;
using System.Web.Mvc;
using Project.Common;
using System.Collections.Generic;
using Project.Web.Base;
using FoodSys.Biz.BizAccess;
using FoodSys.Web.Models;
using Project.Web.Base.Utility;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace FoodSys.Web.Controllers
{
	/// <summary>
	/// 供应商管理
	/// </summary>
	public class SampleController : BaseController
	{
		private BizUTSample bizUTSupplier;
		/// <summary>
		/// 供应商信息列表显示
		/// </summary>
		/// <param name="modelUTSupplierManage"></param>
		/// <returns></returns>
		public ActionResult Index(ModelSampleIndex model,ExportHelper export)
		{

			//byte[] content = System.Text.Encoding.Default.GetBytes("54bdc6d884fef21b7793bec6cef18230debc8b61ef8f78bddb847e7ac5450273e98198b109e13e949ce058140714c4d61c957053fa69d2c5f5306fcf62feae39128344ef18a1a5a9bbf51c04ec35b5e8447c051b2aa7f566d113e9130628198e18d2b5f79d4ef57a1de5cba58d995948b927c9c01c6f4c69eaa37fff9bce85da");
			//string key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCwahnopYCK7GVJaY0F5IADRykEIgmV8xHYu3HD5HuzE7/LxecemeVGnBKtoMaWOU1TMu2E59T5nTgPn2IN2ic4lyVYIoAcMZTQ4KvbdjdywR8LI6oslymKB2ZpWiWLHxfXIAwmt0SdUozIk7XXXrJZfk7ogPY5seMO0fJKKGWS+QIDAQAB";
			////string a = RSADecrypt(key, content);
			////string b =RSAEncrypt(key,content);
			//byte[] c = encrypt(content);
			//string d = c.ToString();

			a();
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
				ModelSampleCreate model = new ModelSampleCreate();
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
		public ActionResult Create(ModelSampleCreate model)
		{
			try
			{
				model.Save();
				return Content(WebTools.ScriptCloseDialog(DialogOption.GetDefaultInstance(new DialogOption() { HighlightData=model.UTSample.ID })));
			}
			catch
			{
				Error = "操作失败";
				ViewBag.PageState = PageState;
				return View(model);
			}
		}

		/// <summary>
		/// 删除供应商信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Delete(Guid id)
		{
			JsonResult jsresult = new JsonResult();
			jsresult.ContentType = Consts.CONTENT_TYPE;
			try
			{
				bizUTSupplier.DeleteByID(id, NHibernate.NHibernateUtil.Guid);
				jsresult.Data = new { result = string.Empty };
			}
			catch
			{
				jsresult.Data = new { result = "操作失败" };
			}
			return jsresult;
		}


		public static string RSADecrypt(string privatekey, string content) 
		{ 
			//privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>"; 
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); 
			byte[] cipherbytes; 
			rsa.FromXmlString(privatekey); 
			cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false); 
			return Encoding.UTF8.GetString(cipherbytes); 
		}

		public static string RSAEncrypt(string publickey, string content) 
		{
			publickey = @"<RSAKeyValue><Modulus>MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCwahnopYCK7GVJaY0F5IADRykEIgmV8xHYu3HD5HuzE7/LxecemeVGnBKtoMaWOU1TMu2E59T5nTgPn2IN2ic4lyVYIoAcMZTQ4KvbdjdywR8LI6oslymKB2ZpWiWLHxfXIAwmt0SdUozIk7XXXrJZfk7ogPY5seMO0fJKKGWS+QIDAQAB</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"; 
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(); 
			byte[] cipherbytes; 
			
			rsa.FromXmlString(publickey); 
			cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false); 
			return Convert.ToBase64String(cipherbytes); 
		}


		public static byte[] encrypt(byte[] Cryptograph)
		{
		//从证书文件载入证书，如果含有私钥的，需要提供保存证书时设置的密码

		X509Certificate2 myX509Certificate2 = new X509Certificate2(@"C:\Samples\PartnerAEncryptMsg\MyTestCert.pfx", "password");

		//从证书中获得含私钥的RSACryptoServiceProvider

		RSACryptoServiceProvider myRSACryptoServiceProvider = (RSACryptoServiceProvider)myX509Certificate2.PrivateKey;

		//使用RSACryptoServiceProvider把密文字节流解密为明文字节流

		byte[] plaintextByte = myRSACryptoServiceProvider.Decrypt(Cryptograph, false);

		return plaintextByte;
		}

		public void a()
		{

			try
			{
				// Create a UnicodeEncoder to convert between byte array and string.
				ASCIIEncoding ByteConverter = new ASCIIEncoding();

				string dataString = "account password";

				// Create byte arrays to hold original, encrypted, and decrypted data.
				byte[] originalData = ByteConverter.GetBytes(dataString);
				byte[] signedData;

				// Create a new instance of the RSACryptoServiceProvider class 
				// and automatically create a new key-pair.
				RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

				// Export the key information to an RSAParameters object.
				// You must pass true to export the private key for signing.
				// However, you do not need to export the private key
				// for verification.
				RSAParameters Key = RSAalg.ExportParameters(true);

				// Hash and sign the data.
				signedData = HashAndSignBytes(originalData, Key);

				// Verify the data and display the result to the 
				// console.
				if (VerifySignedHash(originalData, signedData, Key))
				{
					Console.WriteLine("The data was verified.");
				}
				else
				{
					Console.WriteLine("The data does not match the signature.");
				}

			}
			catch (ArgumentNullException)
			{
				Console.WriteLine("The data was not signed or verified");

			}
		}

		public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
		{
			try
			{
				// Create a new instance of RSACryptoServiceProvider using the 
				// key from RSAParameters.  
				RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

				RSAalg.ImportParameters(Key);

				// Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider
				// to specify the use of SHA1 for hashing.
				return RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);

				return null;
			}
		}


		public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
		{
			try
			{
				// Create a new instance of RSACryptoServiceProvider using the 
				// key from RSAParameters.
				RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

				RSAalg.ImportParameters(Key);

				// Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider
				// to specify the use of SHA1 for hashing.
				return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);

				return false;
			}
		}


	}
}