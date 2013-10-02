using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Project.Web.Base.Utility;
using Project.Biz.Base;

namespace Project.Web.Base
{
	public class BaseControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
				return null;
			IController controllerInstance = base.GetControllerInstance(requestContext, controllerType);
			InjectBizObject.Inject(controllerInstance);
			return controllerInstance;
		}

		
	}
}