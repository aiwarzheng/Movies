using log4net;
using Movies.Domain.Abstract;
using Movies.Domain.Entities;
using Movies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.WebUI.Controllers
{
    public class MoviesController : Controller
    {
        private ILog log = LogManager.GetLogger("MoviesController");
        private IMovieRepository repository = null;
        public MoviesController(IMovieRepository repo)
        {
            repository = repo;
        }

        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Paginate(int total, int itemPerPage, int page)
        {
            PageInfo pageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemPerPage = itemPerPage,
                TotalItems = total
            };

            return PartialView("Paginate", pageInfo);
        }

        public ActionResult Detail(string id)
        {
            Movie movie = repository.Movies.FirstOrDefault(m => m.ID == id);
            if(movie != null)
            {
                return View(movie);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}