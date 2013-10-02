using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using log4net;
using System.IO;

namespace Project.Common
{
	public class Log4netHelper
	{
		private static ILog logger;
		public static void Configure()
		{
			log4net.Config.XmlConfigurator.Configure();
			logger = LogManager.GetLogger("Project");
		}

		public static ILog Logger
		{
			get
			{
				return logger;
			}
		}
	}
}
