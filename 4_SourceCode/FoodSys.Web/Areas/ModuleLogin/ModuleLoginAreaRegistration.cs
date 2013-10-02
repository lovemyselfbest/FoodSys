using System.Web.Mvc;

namespace FoodSys.Web.Areas.ModuleLogin
{
	public class ModuleLoginAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "ModuleLogin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"ModuleLogin_default",
				"ModuleLogin/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
