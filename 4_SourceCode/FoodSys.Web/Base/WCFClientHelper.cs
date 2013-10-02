using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using HYGZFSys.WCF;
using Project.Common;

namespace XAGZFSys.Web.Base
{
	public class WCFClientHelper
	{
		private static object synObject = new object();
		private static ChannelFactory<IPubilcyInfo> PubilcyInfoProxy
		{
			get;
			set;
		}

		private static IPubilcyInfo pubilcyInfo;
		public static IPubilcyInfo PubilcyInfo
		{
			get
			{
				if (PubilcyInfoProxy.State != CommunicationState.Opened)
				{
					PubilcyInfoProxy.Close();
					lock (synObject)
					{
						Configure();
					}
				}
				pubilcyInfo = PubilcyInfoProxy.CreateChannel();
				return pubilcyInfo;
			}
		}

		public static void Configure()
		{
			try
			{
				PubilcyInfoProxy = new ChannelFactory<IPubilcyInfo>("BindingIPubilcyInfoService");
				PubilcyInfoProxy.Open();
			}
			catch (Exception ex)
			{
				Log4netHelper.Logger.Fatal(ex);
			}
		}

		public static void Close()
		{
			if (PubilcyInfoProxy != null)
				PubilcyInfoProxy.Close();
		}

	}
}