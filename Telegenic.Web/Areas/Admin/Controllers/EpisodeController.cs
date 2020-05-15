using System;
using System.Linq;
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
            var vm = new vmSearch("Search Episodes");
            
            return View("Index",vm);
        }

        public ActionResult Find()
        {
            var results = _episodeRepository.GetAll();

            return PartialView("_gridResultsPanel", results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(vmSearch vm)
        {
            var results = !string.IsNullOrEmpty(vm.SearchTerm) ? _episodeRepository.GetByTitle(vm.SearchTerm) : _episodeRepository.GetAll();
            return PartialView("_gridResultsPanel", results);
        }

        //TODO change to _episodeId
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity(_genreRepository.GetAll().OrderBy(x => x.Title));
            vm.Episode = id != 0 ? _episodeRepository.GetById(id.GetValueOrDefault()) : new Episode();
            vm.PageHeading = id != null ? string.Format("Edit Episode: {0}", vm.Episode.Title) : string.Format("Add New Episode");

            return PartialView("_savePanel", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO Fix this to use a season assignment
                    vm.Episode.Season_Id = 15;
                    _episodeRepository.Save(vm.Episode);
                    return RedirectToAction("Detail", new { id = vm.Episode.Id });
                }
                catch (Exception ex)
                {

                    return PartialView("_savePanel", vm);
                }
            }

            return PartialView("_savePanel", vm);
        }

        public ActionResult Detail(int id)
        {
            var vm = new vmEntity(_genreRepository.GetAll().OrderBy(x => x.Title));
            vm.Episode = id > 0 ? _episodeRepository.GetById(id) : new Episode();
            vm.PageHeading = string.Format("Episode: {0}", vm.Episode.Title);

            return PartialView("_detailPanel", vm);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var episode = _episodeRepository.GetById(id);
            _episodeRepository.Delete(episode);

            return RedirectToAction("Find");
        }
    }
}
