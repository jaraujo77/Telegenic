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
            var entities = _genreRepository.GetAll();
            var vm = new vmEntityList<Genre>(entities, "Genre Admin");
            return View(vm);
        }

        // GET: Admin/Genre/Details/5
        public ActionResult Details(int id)
        {
            var vm = new vmEntity();
            vm.Genre = id > 0 ? _genreRepository.GetById(id) : new Genre();
            vm.PageHeading = string.Format("Genre: {0}", vm.Genre.Name);

            return View(vm);
        }

        // GET: Admin/Genre/Save        
        public ActionResult Save(int? id)
        {
            var vm = new vmEntity();
            vm.Genre = id != null ? _genreRepository.GetById(id.GetValueOrDefault()) : new Genre();
            vm.PageHeading = id != null ? string.Format("Edit Genre: {0}", vm.Genre.Name) : string.Format("Add New Genre");

            return View(vm);
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
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(vm);
                }
                
            }

            return View(vm);
        }

        // GET: Admin/Genre/Delete/5
        public ActionResult Delete(int id)
        {
            var genre = _genreRepository.GetById(id);
            _genreRepository.Delete(genre);
            return RedirectToAction("Index");
        }
    }
}
