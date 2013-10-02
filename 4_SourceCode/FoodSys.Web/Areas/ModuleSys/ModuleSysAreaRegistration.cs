using System.Web.Mvc;

namespace FoodSys.Web.Areas.ModuleSys
{
    public class ModuleSysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModuleSys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModuleSys_default",
                "ModuleSys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
