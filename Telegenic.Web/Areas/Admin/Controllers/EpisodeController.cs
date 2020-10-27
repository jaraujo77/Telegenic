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
        SeasonRepository _seasonRepository;

        public EpisodeController(IEpisodeRepository episodeRepository, IGenreRepository genreRepository, ISeasonRepository seasonRepository)
        {
            _episodeRepository = (EpisodeRepository)episodeRepository;
            _genreRepository = (GenreRepository)genreRepository;
            _seasonRepository = (SeasonRepository)seasonRepository;
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
        public ActionResult Save(int _seasonId, int? _episodeId)
        {
            var vm = new vmEntity(_genreRepository.GetAll().OrderBy(x => x.Title));
            vm.Season = _seasonRepository.GetById(_seasonId);
            vm.Episode = _episodeId != 0 ? _episodeRepository.GetById(_episodeId.GetValueOrDefault()) : GetNewEpisodeForEdit(_episodeId.GetValueOrDefault(), _seasonId);
            vm.PageHeading = _episodeId != null ? $"Edit Episode {vm.Episode.Title} Season {vm.Season.Season_Number}" : $"Add Episode to Season {vm.Season.Season_Number}";

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
                    _episodeRepository.Save(vm.Episode);
                    return RedirectToAction("Detail", new { _episodeId = vm.Episode.Id });
                }
                catch (Exception ex)
                {

                    return PartialView("_savePanel", vm);
                }
            }

            return PartialView("_savePanel", vm);
        }

        public ActionResult Detail(int _episodeId)
        {
            var vm = new vmEntity(_genreRepository.GetAll().OrderBy(x => x.Title));
            vm.Episode = _episodeId > 0 ? _episodeRepository.GetById(_episodeId) : new Episode();
            vm.PageHeading = string.Format("Episode: {0}", vm.Episode.Title);

            return PartialView("_detailPanel", vm);
        }

        [HttpPost]
        public ActionResult Delete(int _episodeId)
        {
            var episode = _episodeRepository.GetById(_episodeId);
            _episodeRepository.Delete(episode);

            return RedirectToAction("Find");
        }

        private Episode GetNewEpisodeForEdit(int _episodeId, int _seasonId)
        {
            var ep = new Episode();
            ep.Season_Id = _seasonId;
            return ep;

        }
    }
}
