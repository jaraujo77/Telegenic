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
        public ActionResult Index(int seriesId)
        {            
            var seasons = _seasonRepository.GetSeasonsBySeriesId(seriesId);
            var vm = new vmEntityList<Season>(seasons.Cast<Season>());
            vm.SearchEntity = _seriesRepository.GetById(seriesId);
            vm.PageHeading = string.Format("{0} Seasons", vm.SearchEntity.Title);
            return PartialView("_seasonAdmin", vm);
        }

        // GET: Admin/Season/Details/5
        public ActionResult Details(int id)
        {
            var vm = new vmEntity();
            vm.Season = id > 0 ? _seasonRepository.GetById(id) : new Season();
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
                    return RedirectToAction("Index", new { seriesId = vm.Season.Series_Id });
                }
                catch (Exception ex)
                {
                    return View(vm);
                }

            }

            return View(vm);
        }

        // GET: Admin/Season/Delete/5
        public ActionResult Delete(int id)
        {
            var season = _seasonRepository.GetById(id);
            _seasonRepository.Delete(season);
            return RedirectToAction("Index");
        }

        // Get: 
        public ActionResult SeasonEpisodes(int _seasonId)
        {
            var episodes = _episodeRepository.GetEpisodesBySeasonId(_seasonId).OrderBy(x => x.Title).Cast<Episode>();

            return PartialView("_seasonEpisodes", episodes);
        }

    }
}
