using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Movies.Domain.Utils;
using Movies.Domain.Entities;
using System.Text;
using Movies.Domain.Abstract;
using Movies.Domain.Concrete;
using Ninject;
using System.Linq;
using System.Reflection;
using System.IO;
using Moq;
using Movies.WebUI.Controllers;
using Movies.WebUI.Models;
using System.Web.Mvc;

namespace Movies.UnitTests
{
    [TestClass]
    public class MoviesSourcesUnitTest
    {
        [TestMethod]
        public void Movie_Get_From_Provider_Test()
        {
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "x-access-token", "sjd1HfkjU83ksdsm3802k" }
            };

            string provider = "cinemaworld";

            string url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movies", provider));
            string strCinema = HttpUtils.Get(url, headers);

            provider = "filmworld";

            url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movies", provider));
            string strFilm = HttpUtils.Get(url, headers);

            Assert.IsNotNull(strCinema);
            Assert.IsNotNull(strFilm);
        }

        [TestMethod]
        public void Movie_Detail_Test()
        {
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "x-access-token", "sjd1HfkjU83ksdsm3802k" }
            };

            string provider = "cinemaworld";

            string url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movie/{1}", provider, "cw0076759"));
            string strCinema = HttpUtils.Get(url, headers);

            provider = "filmworld";

            url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movie/{1}", provider, "fw0121765"));
            string strFilm = HttpUtils.Get(url, headers);

            Assert.IsNotNull(strCinema);
            Assert.IsNotNull(strFilm);
        }

        [TestMethod]
        public void Movie_From_Json_Test()
        {
            string json = string.Format("{0}\"Title\":\"{1}\", \"Year\":\"{2}\", \"ID\":\"{3}\", \"Type\":\"{4}\", \"Poster\":\"{5}\"{6}", "{",
                "Star Wars: Episode IV - A New Hope", "1977", "cw0076759", "movie", "http://ia.media-imdb.com/images/M/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg", "}");

            CinemaMovie movie = JsonUtils.FromJson<CinemaMovie>(json);

            Assert.IsNotNull(movie);
            Assert.AreEqual("1977", movie.Year);
        }

        [TestMethod]
        public void Movies_From_Json_Test()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"Movies\":[");
            string json = string.Format("{0}\"Title\":\"{1}\", \"Year\":\"{2}\", \"ID\":\"{3}\", \"Type\":\"{4}\", \"Poster\":\"{5}\"{6}", "{",
                "Star Wars: Episode IV - A New Hope", "1977", "cw0076759", "movie", "http://ia.media-imdb.com/images/M/MV5BOTIyMDY2NGQtOGJjNi00OTk4LWFhMDgtYmE3M2NiYzM0YTVmXkEyXkFqcGdeQXVyNTU1NTcwOTk@._V1_SX300.jpg", "}");

            sb.Append(json);
            sb.Append(",");
            sb.Append(json);

            sb.Append("]}");

            CinemaMovies movies = JsonUtils.FromJson<CinemaMovies>(sb.ToString());

            Assert.IsNotNull(movies);
            Assert.AreEqual(2, movies.Movies.Count);
        }

        [TestMethod]
        public void Cinema_Provider_Test()
        {
            IMovieProvider provider = new CinemaWorldProvider();
            IEnumerable<Movie> movies = provider.GetMovies();

            Assert.IsNotNull(movies);
            Assert.AreEqual(7, movies.Count());
        }

        [TestMethod]
        public void Film_Provider_Test()
        {
            IMovieProvider provider = new FilmWorldProvider();
            IEnumerable<Movie> movies = provider.GetMovies();

            Assert.IsNotNull(movies);
            Assert.AreEqual(6, movies.Count());
        }
    }

    [TestClass]
    public class ApiControllerTest
    {
        [TestMethod]
        public void Api_Method_Movies_Test()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="1",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=10,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1983
                },
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="2",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=90,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1992
                }
            };

            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(movies);

            ApiController api = new ApiController(mock.Object);
            MovieFilter filter = new MovieFilter();

            JsonResult json = api.Movies(filter);
            MovieList ml = json.Data as MovieList;

            Assert.AreEqual(2, ml.Movies.Count());

            filter.Year = 3; //90's
            json = api.Movies(filter);
            ml = json.Data as MovieList;

            Assert.AreEqual(1, ml.Movies.Count());
        }

        [TestMethod]
        public void Api_Method_FilterMovie_Test()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="1",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=10,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1983
                },
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="2",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=90,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1992
                }
            };

            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(movies);

            ApiController api = new ApiController(mock.Object);
            MovieFilter filter = new MovieFilter();

            int category = 2; //year
            int type = 3;     //90's

            JsonResult json = api.FilterMovie(filter, category, type);
            var ml = json.Data as MovieList;

            Assert.AreEqual(1, ml.Movies.Count());
        }

        [TestMethod]
        public void Api_Method_Query_Test()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="1",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=10,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1983
                },
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="2",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=90,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Terminator",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1992
                }
            };

            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(movies);

            ApiController api = new ApiController(mock.Object);
            MovieFilter filter = new MovieFilter();

            string keywords = "termin";
            var json = api.Query(filter, keywords);
            var ml = json.Data as MovieList;

            Assert.AreEqual(1, ml.Movies.Count());
        }

        [TestMethod]
        public void Api_Method_UpdateSign_Test()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="1",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=10,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1983
                },
                new Movie
                {
                     HasDetail=false,
                     ID="2",
                     Poster="",
                     Title="Terminator",
                     Type="movie",
                     Year=1992
                }
            };

            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(movies);

            ApiController api = new ApiController(mock.Object);

            var json = api.UpdateSign();
            var obj = json.Data as IDictionary<string, int>;

            int count = obj["count"];

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void Api_Method_Paginate_Test()
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="1",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=10,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Start War",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1983
                },
                new Movie
                {
                     Actors="Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing",
                     Country="USA",
                     Awards="Won 6 Oscars. Another 48 wins & 28 nominations.",
                     Director="George Lucas",
                     Genre="Action, Adventure, Fantasy",
                     HasDetail=true,
                     ID="2",
                     Language="English",
                     Metascore=90,
                     Plot="Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a wookiee and two droids to save the galaxy from the Empire's world-destroying battle-station, while also attempting to rescue Princess Leia from the evil Darth Vader.",
                     Price=90,
                     Poster="",
                     Provider="cw",
                     Rated="3",
                     Rating=45.9,
                     Released="1980",
                     Runtime="120 min",
                     Title="Terminator",
                     Type="movie",
                     Votes=1000,
                     Writer="Lucas",
                     Year=1992
                }
            };

            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();
            mock.Setup(m => m.Movies).Returns(movies);

            ApiController api = new ApiController(mock.Object);
            MovieFilter filter = new MovieFilter();

            var json = api.Paginate(filter, 2);
            var ml = json.Data as MovieList;

            /*item per page is 6, so page 2 has no items*/
            Assert.AreEqual(0, ml.Movies.Count());
        }
    }
}
