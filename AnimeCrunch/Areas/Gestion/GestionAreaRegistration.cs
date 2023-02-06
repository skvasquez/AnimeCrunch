using System.Web.Mvc;

namespace AnimeCrunch.Areas.Gestion
{
    public class GestionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Gestion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Gestion_default",
                url: "Gestion/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AnimeCrunch.Areas.Gestion.Controllers" }
            );
        }
    }
}