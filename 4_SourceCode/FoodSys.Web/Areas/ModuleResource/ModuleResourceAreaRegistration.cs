using System.Web.Mvc;

namespace FoodSys.Web.Areas.ModuleResource
{
	public class ModuleResourceAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "ModuleResource";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"ModuleResource_default",
				"ModuleResource/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
