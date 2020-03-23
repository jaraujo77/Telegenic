using System;
using System.Web.Mvc;
using Telegenic.Entities.Models;
using Telegenic.Entities.ViewModels;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Web.Areas.Admin.Controllers
{
    public class GenreController : Controller
    {
        GenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = (GenreRepository)genreRepository;
        }

        // GET: Admin/Genre
        public ActionResult Index()
        {
            var vm = new vmSearch("Search Genre");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(vmSearch vm)
        {
            var results = !string.IsNullOrWhiteSpace(vm.SearchTerm) ? _genreRepository.GetByTitle(vm.SearchTerm) : _genreRepository.GetAll();

            return PartialView("_gridResultsPanel", results);
        }

        // GET: Admin/Genre/Save
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity();
            vm.Genre = id != 0 ? _genreRepository.GetById(id.GetValueOrDefault()) : new Genre();
            vm.PageHeading = id != null ? string.Format("Edit Genre: {0}", vm.Genre.Title) : string.Format("Add New Genre");

            return PartialView("_savePanel", vm);
        }

        // POST: Admin/Genre/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _genreRepository.Save(vm.Genre);
                    return RedirectToAction("Detail", new { id = vm.Genre.Id });
                }
                catch (Exception ex)
                {
                    return View(vm);
                }

            }

            return View(vm);
        }

        public ActionResult Detail(int id)
        {
            var vm = new vmEntity();
            vm.Genre = id > 0 ? _genreRepository.GetById(id) : new Genre();
            vm.PageHeading = string.Format("Genre: {0}", vm.Genre.Title);

            return PartialView("_detailPanel", vm);
        }

        public ActionResult Delete(int id)
        {
            var series = _genreRepository.GetById(id);
            _genreRepository.Delete(series);
            return RedirectToAction("Find");
        }
    }
}
