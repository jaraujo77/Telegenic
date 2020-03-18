using System;
using System.Linq;
using System.Web.Mvc;
using Telegenic.Entities.Models;
using Telegenic.Entities.ViewModels;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Web.Areas.Admin.Controllers
{
    public class SeriesController : Controller
    {
        SeriesRepository _seriesRepository;
        GenreRepository _genreRepository;

        public SeriesController(ISeriesRepository seriesRepository, IGenreRepository genreRepository)
        {
            _genreRepository = (GenreRepository)genreRepository; 
            _seriesRepository = (SeriesRepository)seriesRepository;
        }

        // GET: Admin/Series
        public ActionResult Index()
        {
            var entities = _seriesRepository.GetAll();
            var vm = new vmEntityList<Series>(entities, "Series Admin");
            return View(vm);
        }

        // GET: Admin/Series/Details/5
        public ActionResult Details(int id)
        {
            var vm = new vmEntity(_genreRepository.GetAll());
            vm.Series = id > 0 ? _seriesRepository.GetById(id) : new Series();
            vm.PageHeading = string.Format("Series: {0}", vm.Series.Title);
            
            return View(vm);
        }

        // GET: Admin/Series/Create
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity(_genreRepository.GetAll());
            vm.Series = id != null ? _seriesRepository.GetById(id.GetValueOrDefault()) : new Series();
            vm.PageHeading = id != null ? string.Format("Edit Series: {0}", vm.Series.Title) : string.Format("Add New Series");

            return View(vm);
        }

        // POST: Admin/Series/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _seriesRepository.Save(vm.Series);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(vm);
                }

            }

            return View(vm);

        }

        public ActionResult Delete(int id)
        {
            var series = _seriesRepository.GetById(id);
            _seriesRepository.Delete(series);
            return RedirectToAction("Index");
        }
    }
}
