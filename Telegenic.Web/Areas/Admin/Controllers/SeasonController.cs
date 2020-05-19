using System;
using System.Linq;
using System.Web.Mvc;
using Telegenic.Entities.Models;
using Telegenic.Entities.ViewModels;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Web.Areas.Admin.Controllers
{
    public class SeasonController : Controller
    {
        SeasonRepository _seasonRepository;
        EpisodeRepository _episodeRepository;
        SeriesRepository _seriesRepository;

        public SeasonController(ISeasonRepository seasonRepository, IEpisodeRepository episodeRepository, ISeriesRepository seriesRepository)
        {
            _seasonRepository = (SeasonRepository)seasonRepository;
            _episodeRepository = (EpisodeRepository)episodeRepository;
            _seriesRepository = (SeriesRepository)seriesRepository;
        }

        // GET: Admin/Season
        public ActionResult Index(int _seriesId)
        {            
            var seasons = _seasonRepository.GetSeasonsBySeriesId(_seriesId);
            var vm = new vmEntityList<Season>(seasons.Cast<Season>());
            vm.SearchEntity = _seriesRepository.GetById(_seriesId);
            vm.PageHeading = string.Format("{0} Seasons", vm.SearchEntity.Title);
            return PartialView("_seasonTableAlternating", vm);
        }

        // GET: Admin/Season/Details/5
        public ActionResult Details(int _seriesId)
        {
            var vm = new vmEntity();
            vm.Season = _seriesId > 0 ? _seasonRepository.GetById(_seriesId) : new Season();
            vm.PageHeading = $"Season: {vm.Season.Title}";

            return View(vm);
        }

        // GET: Admin/Season/Save
        public ActionResult Save(int _seasonId, int _seriesId)
        {
            var vm = new vmEntity();
            vm.Series = _seriesRepository.GetById(_seriesId);
            vm.Season = _seasonId > 0 ? _seasonRepository.GetById(_seasonId) : new Season();
            vm.Season.Series_Id = _seriesId;
            vm.PageHeading = _seasonId > 0 ? $"Edit {vm.Series.Title} Season {vm.Season.Season_Number}" : "Add New Season";

            return PartialView("_seasonSave", vm);
        }

        // POST: Admin/Season/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _seasonRepository.Save(vm.Season);
                    return RedirectToAction("Save", "Series", new { _seriesId = vm.Season.Series_Id });
                }
                catch (Exception ex)
                {
                    return View(vm);
                }

            }

            return View(vm);
        }

        // GET: Admin/Season/Delete/5
        public ActionResult Delete(int _seriesId)
        {
            var season = _seasonRepository.GetById(_seriesId);
            _seasonRepository.Delete(season);
            return RedirectToAction("Index", new { _seriesId = _seriesId });
        }

        // Get: 
        public ActionResult SeasonEpisodes(int _seasonId)
        {
            var episodes = _episodeRepository.GetEpisodesBySeasonId(_seasonId).OrderBy(x => x.Title).Cast<Episode>();

            return PartialView("_seasonEpisodes", episodes);
        }

    }
}
