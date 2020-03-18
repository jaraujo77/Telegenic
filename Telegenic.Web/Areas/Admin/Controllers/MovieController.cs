using System;
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
            var entities = _movieRepository.GetAll();
            var vm = new vmEntityList<Movie>(entities, "Movie Admin");
            return View(vm);
        }

        // GET: Admin/Movie/Details/5
        public ActionResult Details(int id)
        {
            var vm = new vmEntity(_genreRepository.GetAll());
            vm.Movie = id > 0 ? _movieRepository.GetById(id) : new Movie();
            vm.PageHeading = $"Movie: {vm.Movie.Title}";

            return View(vm);
        }

        // GET: Admin/Movie/Save
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity(_genreRepository.GetAll());
            vm.Movie = id != null ? _movieRepository.GetById(id.GetValueOrDefault()) : new Movie();
            vm.PageHeading = id != null ? $"Edit Movie {vm.Movie.Title}" : "Add New Movie";
            return View(vm);
        }

        // POST: Admin/Movie/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(vmEntity vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _movieRepository.Save(vm.Movie);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(vm);
                }
            }

            return View(vm);
        }
        
        // GET: Admin/Movie/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = _movieRepository.GetById(id);
            _movieRepository.Delete(movie);

            return RedirectToAction("Index");
        }

    }
}
