using System;
using System.Linq;
using System.Web.Mvc;
using Telegenic.Entities.Models;
using Telegenic.Entities.ViewModels;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Web.Areas.Admin.Controllers
{
    public class MovieController : Controller
    {
        MovieRepository _movieRepository;
        GenreRepository _genreRepository;

        public MovieController(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _movieRepository = (MovieRepository)movieRepository;
            _genreRepository = (GenreRepository)genreRepository;
        }

        // GET: Admin/Movie
        public ActionResult Index()
        {
            var vm = new vmSearch("Search Movies");
            return View(vm);
        }

        public ActionResult Find()
        {
            var results = _movieRepository.GetAll();

            return PartialView("_gridResultsPanel", results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(vmSearch vm)
        {
            var results = !string.IsNullOrWhiteSpace(vm.SearchTerm) ? _movieRepository.GetByTitle(vm.SearchTerm) : _movieRepository.GetAll();

            return PartialView("_gridResultsPanel", results);
        }

        // GET: Admin/Series/Create
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity(_genreRepository.GetAll().OrderBy(x => x.Title));
            vm.Movie = id != 0 ? _movieRepository.GetById(id.GetValueOrDefault()) : new Movie();
            vm.PageHeading = id != null ? string.Format("Edit Movie: {0}", vm.Movie.Title) : string.Format("Add New Movie");

            return PartialView("_savePanel", vm);
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
                    _movieRepository.Save(vm.Movie);
                    return RedirectToAction("Detail", new { id = vm.Movie.Id });
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
            vm.Movie = id > 0 ? _movieRepository.GetById(id) : new Movie();
            vm.PageHeading = string.Format("Movie: {0}", vm.Movie.Title);

            return PartialView("_detailPanel", vm);
        }

        public ActionResult Delete(int id)
        {
            var movie = _movieRepository.GetById(id);
            _movieRepository.Delete(movie);
            return RedirectToAction("Find");
        }

    }
}
