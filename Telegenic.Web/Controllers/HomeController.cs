using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;
using Telegenic.Web.Models;

namespace Telegenic.Controllers
{
    public class HomeController : Controller
    {
        SeriesRepository _seriesRepository;
        MovieRepository _movieRepository;

        public HomeController(ISeriesRepository seriesRepository, IMovieRepository movieRepository)
        {
            _movieRepository = (MovieRepository)movieRepository;
            _seriesRepository = (SeriesRepository)seriesRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new vmShow(_seriesRepository.GetFeaturedItems(), _movieRepository.GetFeaturedItems());
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}