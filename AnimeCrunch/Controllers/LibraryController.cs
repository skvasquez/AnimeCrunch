using AnimeCrunch.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimeCrunch.Controllers
{
    public class LibraryController : Controller
    {
        TVShowDb databaseTvShow = new TVShowDb();
        public ActionResult Index(string FiltreTitle = "", int? FiltreGenre = null, int? FiltreStudio = null)
        {
            var ls = databaseTvShow.TvShows.ToList();
            if (FiltreTitle != "")
            {
                ls = ls.Where(t => t.Title.ToLower().Contains(FiltreTitle.ToLower())).ToList();
            }
            if (FiltreGenre != null)
            {
                Genre g = databaseTvShow.Genres.Find(FiltreGenre);
                ls = ls.Where(t => t.Genres.Contains(g)).ToList();
            }
            if (FiltreStudio != null)
            {
                ls = ls.Where(t => t.StudioId == FiltreStudio).ToList();
            }
            return View(ls.OrderBy(show => show.Title));
        }

        // ONE TV SHOW
        public ActionResult TvShow(int TvShowId)
        {
            try
            {
                TvShow tv = databaseTvShow.TvShows.Find(TvShowId);
                return this.View(tv);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erreur TvShow");
            }
            return RedirectToAction("Index", "Library");
        }

        // La pagination, max de 12 par pages
        private const int NbEpisodeParPage = 12;
        public ActionResult Season(int SeasonId, int Page = 1)
        {
            Season season = databaseTvShow.Seasons.Find(SeasonId);
            ViewBag.EpisodeSaison = season.Episodes.Count();
            
            ViewBag.SeasonId = SeasonId;

            ViewBag.TotalPages = (int)Math.Ceiling(((double)season.Episodes.Count() / NbEpisodeParPage));

            //S'assurer que Page est >= 1
            ViewBag.Page = Page = Math.Max(Page, 1);
            //S'assurer que Page est <= TotalPages
            ViewBag.Page = Page = Math.Min(Page, (int)ViewBag.TotalPages);

            return this.View(season.Episodes.OrderBy(p=>p.Number).Skip(NbEpisodeParPage * (Page - 1)).Take(NbEpisodeParPage).ToList()
                );
        }
        // ONE EPISODE
        public ActionResult Episode(int EpisodeId)
        {
            try
            {
                Episode episode = databaseTvShow.Episodes.Find(EpisodeId);
                return this.View(episode);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erreur Episode");
            }
            return RedirectToAction("Index", "Library");
        }

        public ActionResult ViewEpisode(int EpisodeId)
        {
            try
            {
                Episode episode = databaseTvShow.Episodes.Find(EpisodeId);
                //History
                //Ajouter l'episode dans le User courant
                User user = this.databaseTvShow.Users.Find(int.Parse(this.User.Identity.Name));
                user.History.Add(episode);
                this.databaseTvShow.SaveChanges();
                return this.View(episode);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Erreur Chargement Episode");
            }
            return RedirectToAction("Index", "Library");
        }
    }
}