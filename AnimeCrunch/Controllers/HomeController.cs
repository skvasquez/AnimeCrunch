using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AnimeCrunch.Controllers {
    using AnimeCrunch.Models.DataModels;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Library");
        }
        // LANGUAGE COOKIE
        public ActionResult ChangeLanguage(string lang = "fr")
        {
            HttpCookie cookie = this.Response.Cookies["LanguageCookie"];
            if (cookie == null)
            {
                cookie = new HttpCookie("LanguageCookie");
            }
            else
            {
                Response.Cookies.Remove("LanguageCookie");
            }
            cookie.Values["CurrentLanguage"] = lang;
            cookie.Expires = DateTime.UtcNow.AddYears(1);
            Response.AppendCookie(cookie);
            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}