using Movies.Domain.Abstract;
using Movies.Domain.Entities;
using Movies.WebUI.Infrastructure;
using Movies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Movies.WebUI.Controllers
{
    public class ApiController : Controller
    {
        private IMovieRepository repository = null;

        const int ITEM_PER_PAGE = 6;

        public ApiController(IMovieRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Get movies, according to special filter
        /// Main use is loading data, when page is first loaded
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>json</returns>
        public JsonResult Movies(MovieFilter filter)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";
            json.Data = QueryMovies(filter);

            return json;
        }

        /// <summary>
        /// Get data, let page check if need to update movie data again
        /// </summary>
        /// <returns>json</returns>
        public JsonResult UpdateSign()
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";
            int count = repository.Movies.Count(m => m.HasDetail == false);
            IDictionary<string, int> obj = new Dictionary<string, int>
            {
                { "count", count}
            };

            json.Data = obj;

            return json;
        }

        /// <summary>
        /// Movie search by keywords
        /// </summary>
        /// <param name="filter">saved filter about genre, country,etc</param>
        /// <param name="keywords">keywords</param>
        /// <param name="page">currently load page</param>
        /// <returns>json</returns>
        [HttpPost]
        public JsonResult Query(MovieFilter filter, string keywords, int page=1)
        {
            if (page <= 0) page = 1;

            filter.Reset();

            filter.Keywords = keywords;
            filter.Page = page;

            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";

            json.Data = QueryMovies(filter);

            return json;
        }

        /// <summary>
        /// Movie search by filter of genre, country data
        /// </summary>
        /// <param name="filter">saved filter about genre, country,etc</param>
        /// <param name="category">category index, genre, country,year,price</param>
        /// <param name="type">category data type</param>
        /// <param name="page">current page</param>
        /// <returns>json</returns>
        [HttpPost]
        public JsonResult FilterMovie(MovieFilter filter, int category, int type, int page=1)
        {
            if (page <= 0) page = 1;

            filter.Page = page;

            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";

            switch(category)
            {
                case 0: //genre
                    filter.Genre = type;
                    break;

                case 1: //country
                    filter.Country = type;
                    break;

                case 2: //year
                    filter.Year = type;
                    break;

                case 3: //price
                    filter.Price = type;
                    break;
            }

            json.Data = QueryMovies(filter);

            return json;
        }

        /// <summary>
        /// get paged info
        /// </summary>
        /// <param name="filter">saved filter</param>
        /// <param name="page">current page</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Paginate(MovieFilter filter, int page)
        {
            if (page <= 0) page = 1;

            filter.Page = page;

            JsonResult json = new JsonResult();
            json.ContentEncoding = Encoding.UTF8;
            json.ContentType = "application/json";

            json.Data = QueryMovies(filter);

            return json;
        }

        /// <summary>
        /// get movies by filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        private MovieList QueryMovies(MovieFilter filter)
        {
            IEnumerable<Movie> movies = repository.Movies;
            if (!String.IsNullOrEmpty(filter.Keywords))
            {
                movies = movies.Where(m => m.Title.ContainsIgnoreCase(filter.Keywords) || (m.HasDetail && m.Actors.ContainsIgnoreCase(filter.Keywords)));
            }

            movies = FilterByGenre(movies, filter.Genre);
            movies = FilterByCountry(movies, filter.Country);
            movies = FilterByYear(movies, filter.Year);
            movies = FilterByPrice(movies, filter.Price);

            MovieList model = new MovieList
            {
                Movies = movies
                .OrderBy(m => m.Price)
                .Skip((filter.Page - 1) * ITEM_PER_PAGE)
                .Take(ITEM_PER_PAGE),
                Pages = new Models.PageInfo
                {
                    CurrentPage = filter.Page,
                    ItemPerPage = ITEM_PER_PAGE,
                    TotalItems = movies.Count()
                }
            };

            return model;
        }

        private IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, int type)
        {
            switch (type)
            {
                case 1: //action
                    return movies.Where(m => m.Genre.ContainsIgnoreCase("Action"));

                case 2://adventure
                    return movies.Where(m => m.Genre.ContainsIgnoreCase("Adventure"));

                case 3://fantasy
                    return movies.Where(m => m.Genre.ContainsIgnoreCase("Fantasy"));

                default:
                    return movies;
            }
        }

        private IEnumerable<Movie> FilterByCountry(IEnumerable<Movie> movies, int type)
        {
            switch (type)
            {
                case 1://USA
                    return movies.Where(m => m.Country.ToLower() == "usa");
                default:
                    return movies;
            }
        }

        private IEnumerable<Movie> FilterByYear(IEnumerable<Movie> movies, int type)
        {
            switch (type)
            {
                case 1://2017-2010
                    return movies.Where(m => m.Year <= 2017 && m.Year >= 2010);

                case 2://2009-2000
                    return movies.Where(m => m.Year < 2010 && m.Year >= 2000);

                case 3://90's
                    return movies.Where(m => m.Year < 2000 && m.Year >= 1990);

                case 4://80's
                    return movies.Where(m => m.Year < 1990 && m.Year >= 1980);
                case 5://others
                    return movies.Where(m => m.Year < 1980);

                default:
                    return movies;
            }
        }

        private IEnumerable<Movie> FilterByPrice(IEnumerable<Movie> movies, int type)
        {
            switch (type)
            {
                case 1://0-50
                    return movies.Where(m => m.Price <= 50);
                case 2://50-100

                    return movies.Where(m => m.Price > 50 && m.Price <= 100);

                case 3://100-500

                    return movies.Where(m => m.Price > 100 && m.Price <= 500);

                case 4://500-
                    return movies.Where(m => m.Price > 500);
                default:
                    return movies;
            }
        }
    }
}