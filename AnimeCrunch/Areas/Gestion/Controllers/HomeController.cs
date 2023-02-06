using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimeCrunch.Areas.Gestion.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}