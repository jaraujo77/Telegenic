using System;
using System.Web.Mvc;
using Telegenic.Entities.Models;
using Telegenic.Entities.ViewModels;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Web.Areas.Admin.Controllers
{
    public class EpisodeController : Controller
    {
        EpisodeRepository _episodeRepository;
        GenreRepository _genreRepository;

        public EpisodeController(IEpisodeRepository episodeRepository, IGenreRepository genreRepository)
        {
            _episodeRepository = (EpisodeRepository)episodeRepository;
            _genreRepository = (GenreRepository)genreRepository;
        }

        // GET: Admin/Episode
        public ActionResult Index()
        {
            var episodes = _episodeRepository.GetAll();
            var vm = new vmEntityList<Episode>(episodes, "Episode Admin");
            return View(vm);
        }

        // GET: Admin/Episode/Details/5
        public ActionResult Details(int id)
        {
            var vm = new vmEntity();
            vm.Episode = _episodeRepository.GetById(id);
            vm.PageHeading = $"Episode: {vm.Episode.Title}";

            return View(vm);
        }

        // GET: Admin/Episode/Save
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity();
            vm.Episode = id != null ? _episodeRepository.GetById(id.GetValueOrDefault()) : new Episode();
            vm.PageHeading = id != null ? $"Edit Episode {vm.Episode.Title}" : "Add New Episode";

            return View(vm);
        }

        // POST: Admin/Episode/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _episodeRepository.Save(vm.Episode);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(vm);
                }
            }

            return View(vm);
        }

        // GET: Admin/Episode/Delete/5
        public ActionResult Delete(int id)
        {
            var episode = _episodeRepository.GetById(id);
            _episodeRepository.Delete(episode);

            return RedirectToAction("Index");
        }
    }
}
