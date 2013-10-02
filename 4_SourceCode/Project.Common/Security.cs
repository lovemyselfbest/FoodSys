using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Project.Common
{
	public class Security
	{
		/// <summary>
		/// ����KEY, �ͻ����Ǳ߲���ʹ�ô�ֵ�� ֻ�������ڷ���ˡ�
		/// </summary>
		public static readonly string Key = "kXwL7X2+fgM=";

		/// <summary>
		///  ����EncryptKey
		/// </summary>
		public static readonly string EncryptKey = "FwGQWRRgKCI=";
		//Ĭ����Կ����
		private static readonly byte[] Keys = { 0x12, 0x24, 0x52, 0x48, 0x92, 0x27, 0x72, 0x69 };

		/// <summary>
		/// DES�����ַ���
		/// </summary>
		/// <param name="encryptString">�����ܵ��ַ���</param>
		/// <returns>���ܳɹ����ؼ��ܺ���ַ���, ���쳣���� NULL </returns>
		public static string Encrypt(string encryptString)
		{
			try
			{
				byte[] rgbKey = Convert.FromBase64String(EncryptKey);
				byte[] rgbIv = Convert.FromBase64String(Key);
				byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString.Trim());
				using (var dCsp = new DESCryptoServiceProvider())
				{
					using (var mStream = new MemoryStream())
					{
						using (var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write))
						{
							cStream.Write(inputByteArray, 0, inputByteArray.Length);
							cStream.FlushFinalBlock();
						}
						return Convert.ToBase64String(mStream.ToArray());
					}
				}
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// DES�����ַ���
		/// </summary>
		/// <param name="decryptString">�����ܵ��ַ���</param>
		/// <returns>���ܳɹ����ؽ��ܺ���ַ���</returns>
		/// <param name="keys"></param>
		public static string Decrypt(string decryptString, string keys)
		{
			try
			{
				byte[] rgbKey = Convert.FromBase64String(EncryptKey);
				byte[] rgbIv = Convert.FromBase64String(keys);
				byte[] inputByteArray = Convert.FromBase64String(decryptString.Trim());
				using (var dcsp = new DESCryptoServiceProvider())
				{
					using (var mStream = new MemoryStream())
					{
						using (var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write))
						{
							cStream.Write(inputByteArray, 0, inputByteArray.Length);
							cStream.FlushFinalBlock();
						}
						return Encoding.UTF8.GetString(mStream.ToArray());
					}
				}
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// DES�����ַ���
		/// </summary>
		/// <param name="encryptString">�����ܵ��ַ���</param>
		/// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
		/// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
		public static string EncryptDES(string encryptString, string encryptKey)
		{
			try
			{
				byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
				byte[] rgbIv = Keys;
				byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
				var dCsp = new DESCryptoServiceProvider { IV = rgbIv, Key = rgbKey };
				using (var mStream = new MemoryStream())
				{
					var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(),
												   CryptoStreamMode.Write);
					cStream.Write(inputByteArray, 0, inputByteArray.Length);
					cStream.FlushFinalBlock();
					//return Encoding.UTF8.GetString(mStream.ToArray());
					string s = Convert.ToBase64String(mStream.ToArray());
					mStream.Close();
					dCsp.Clear();
					return s;
				}
			}
			catch
			{
				return encryptString;
			}
		}

		/// <summary>
		/// DES�����ַ���
		/// </summary>
		/// <param name="decryptString">�����ܵ��ַ���</param>
		/// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
		/// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
		public static string DecryptDES(string decryptString, string decryptKey)
		{
			try
			{

				byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
				byte[] rgbIv = Keys;
				byte[] inputByteArray = Convert.FromBase64String(decryptString);
				var dcsp = new DESCryptoServiceProvider { IV = rgbIv, Key = rgbKey };
				using (var mStream = new MemoryStream())
				{
					var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(),
												   CryptoStreamMode.Write);
					cStream.Write(inputByteArray, 0, inputByteArray.Length);
					cStream.FlushFinalBlock();
					string s = Encoding.UTF8.GetString(mStream.ToArray());
					mStream.Close();
					dcsp.Clear();
					return s;
				}
			}
			catch
			{
				return decryptString;
			}
		}
	}

	/// <summary> 
	/// ��̳�����㷨
	/// </summary> 
	public class BBSSecurity
	{
		/// <summary>
		/// �����㷨
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string Encode(string input)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			string password = s.ToString();
			return password;
		}
	}

}
