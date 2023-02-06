using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AnimeCrunch.Models.DataModels;
using AnimeCrunch.Models.ViewModels.User;

namespace AnimeCrunch.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly TVShowDb db = new TVShowDb();

        public ActionResult Index() {
            if (this.User.Identity.IsAuthenticated) {
                return this.RedirectToAction("Edit");
            }
            return this.RedirectToAction("SignUp");
        }

        // INSCRIPTION
        [HttpGet, AllowAnonymous]
        public ActionResult SignUp() {
            return this.View(new SignUp());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult SignUp(SignUp s) {
            if (this.ModelState.IsValid) {
                try {
                    User u = new User();
                    u.Email = s.Email;
                    u.Password = s.Password;
                    u.UserName = s.UserName;
                    this.db.Users.Add(u);
                    this.db.SaveChanges();
                    return this.RedirectToAction("Index", "Home");
                }
                catch (Exception e) {
                    while(e.InnerException != null) { e=e.InnerException; }
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(s);
        }

        // CONNEXION
        [HttpGet, AllowAnonymous]
        public ActionResult Login() {
            return this.View(new Login());
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(Login login) {
            if (this.ModelState.IsValid) {
                //Envoyer le cookie d'authentification lorsque Login valide
                //Dans cet exemple on envoi le UserId
                User user = this.db.Users.Single(u => u.UserName == login.UserName);
                FormsAuthentication.SetAuthCookie(user.UserId.ToString(), login.RememberMe);
                return this.RedirectToAction("Index", "Home");
            }
            else {
                return this.View(login);
            }
        }

        // DECONNEXION
        [HttpGet, Authorize]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        //MODIFIER
        [HttpGet, Authorize]
        public ActionResult Edit() {
            //Obtenir les données de l'User courant
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            //L'User possède un cookie d'authentification mais l'User n'existe plus, faire un logout.
            if (user == null) { return this.RedirectToAction("Logout", "User"); }

            //Placer les données de l'User courant dans les données de la vue
            Profile p = new Profile();
            p.Email = user.Email;

            //Envoyer la vue à l'User
            return this.View(p);
        }

        [HttpPost, Authorize]
        public ActionResult Edit(Profile p) {
            //Si la vue contient des données valide, procéder aux modifications demandées par l'User
            if (this.ModelState.IsValid) {
                try {
                    //Obtenir les données de l'User courant
                    User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));

                    //Le Email a peut-être été modifié, et est obligatoire
                    user.Email = p.Email;

                    //Si l'User a spécifié un nouveau mot de passe, remplacer l'ancien
                    if (!string.IsNullOrEmpty(p.NewPassword)) {
                        user.Password = p.NewPassword;
                    }

                    //Lancer UPDATE ds la BD 
                    this.db.Entry(user).State = EntityState.Modified;
                    this.db.SaveChanges();
                    return this.RedirectToAction("Index", "Home");
                }
                catch (Exception e) {
                    while (e.InnerException != null) { e = e.InnerException; }
                    this.ModelState.AddModelError("", e.Message);
                }
            }
            return this.View(p);
        }

        //SUPPRIMER
        [HttpGet, Authorize]
        public ActionResult Delete() {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            //L'User possède un cookie d'authentification mais l'User n'existe plus, faire un logout.
            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            return this.View(user);
        }

        [HttpPost, Authorize]
        public ActionResult Delete(User ignore) {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));

            if (user.UserName == "admin") {
                return this.Content(@"<h1 class=""text-danger"">Le compte admin ne peut pas être supprimé.</h1>");
            }
            try {
                this.db.Users.Remove(user);
                this.db.SaveChanges();
                FormsAuthentication.SignOut();
                return this.RedirectToAction("Index", "Library");
            }
            catch (Exception e) {
                while (e.InnerException != null) { e = e.InnerException; }
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(user);
        }

        // HISTORIQUE
        [Authorize]
        public ActionResult History(int EpisodeId)
        {
            try
            {
                return this.View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erreur TvShow");
            }
            return RedirectToAction("Index", "Library");
        }

        [HttpGet, Authorize]
        public ActionResult History()
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));

            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            return this.View(user.History);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        // FAVORIS

        [HttpGet, Authorize]
        public ActionResult Favorites()
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            if (user == null) { return this.RedirectToAction("Logout", "User"); }
            return this.View(user.Favorites);
        }
        // AJOUTER FAVORIS
        [HttpGet, Authorize]
        public ActionResult AddFavorite(int Id)
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            TvShow tv = this.db.TvShows.Find(Id);
            try
            {
                if (user == null) { return this.RedirectToAction("Logout", "User"); }
                user.Favorites.Add(tv);
                db.SaveChanges();
                return this.RedirectToAction(actionName: "TvShow", controllerName: "Library", routeValues: new { TvShowId = Id });
            }
           catch(Exception e)
            {
                while (e.InnerException != null) { e = e.InnerException; }
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(tv);
        }

        // SUPPRIMER FAVORIS
        [HttpGet, Authorize]
        public ActionResult DeleteFavorite(int Id)
        {
            User user = this.db.Users.Find(int.Parse(this.User.Identity.Name));
            TvShow tv = this.db.TvShows.Find(Id);
            try
            {
                if (user == null) { return this.RedirectToAction("Logout", "User"); }
                user.Favorites.Remove(tv);
                db.SaveChanges();
                return this.RedirectToAction(actionName:"TvShow",controllerName:"Library", routeValues: new { TvShowId=Id });
            }
            catch (Exception e)
            {
                while (e.InnerException != null) { e = e.InnerException; }
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(tv);
        }
     

    }
}
