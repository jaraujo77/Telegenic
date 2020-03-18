using System;
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

        public SeasonController(ISeasonRepository seasonRepository)
        {
            _seasonRepository = (SeasonRepository)seasonRepository;
        }

        // GET: Admin/Season
        public ActionResult Index()
        {
            var seasons = _seasonRepository.GetAll();
            var vm = new vmEntityList<Season>(seasons, "Season Admin");
            return View(vm);
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
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity();
            vm.Season = id != null ? _seasonRepository.GetById(id.GetValueOrDefault()) : new Season();
            vm.PageHeading = id != null ? $"Edit Season {vm.Season.Season_Number}" : "Add New Season";

            return View(vm);
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
                    return RedirectToAction("Index");
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
    }
}
