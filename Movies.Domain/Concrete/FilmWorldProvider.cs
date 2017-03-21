using Movies.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Domain.Entities;
using Movies.Domain.Utils;
using System.Threading;
using Movies.Domain.Infrastructure;

namespace Movies.Domain.Concrete
{
    /// <summary>
    /// FilmWorld Provider
    /// </summary>
    public class FilmWorldProvider : IMovieProvider
    {
        public string Name
        {
            get
            {
                return "filmworld";
            }
        }

        public void GetMovieDetail(Movie m)
        {
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "x-access-token", "sjd1HfkjU83ksdsm3802k" }
            };

            string url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movie/{1}", Name, m.ID));
            string strMovie = HttpUtils.Get(url, headers, -1);
            FilmMovieDetail movieDetail = JsonUtils.FromJson<FilmMovieDetail>(strMovie);
            if (movieDetail != null)
            {
                m.Rated = movieDetail.Rated;
                m.Released = movieDetail.Released;
                m.Runtime = movieDetail.Runtime;
                m.Genre = movieDetail.Genre;
                m.Director = movieDetail.Director;
                m.Writer = movieDetail.Writer;
                m.Actors = movieDetail.Actors;
                m.Plot = movieDetail.Plot;
                m.Language = movieDetail.Language;
                m.Country = movieDetail.Country;
                m.Awards = "";
                m.Metascore = Convert.ToInt32(movieDetail.Metascore);
                m.Rating = Convert.ToDouble(movieDetail.Rating);
                m.Votes = Convert.ToInt32(movieDetail.Votes.Replace(",", ""));
                m.Price = Convert.ToDecimal(movieDetail.Price);

                m.HasDetail = true;
            }
        }

        public IEnumerable<Movie> GetMovies()
        {
            IDictionary<string, string> headers = new Dictionary<string, string>
            {
                { "x-access-token", "sjd1HfkjU83ksdsm3802k" }
            };

            string url = string.Format("{0}{1}", "http://webjetapitest.azurewebsites.net/", string.Format("api/{0}/movies", Name));
            string strMovies = HttpUtils.Get(url, headers, -1);

            IList<Movie> pMovies = new List<Movie>();

            CinemaMovies movies = JsonUtils.FromJson<CinemaMovies>(strMovies);
            if (movies != null)
            {
                foreach (var movie in movies.Movies)
                {
                    Movie m = new Movie
                    {
                        ID = movie.ID,
                        Title = movie.Title,
                        Poster = movie.Poster,
                        Type = movie.Type,
                        Year = Convert.ToInt32(movie.Year),
                        Provider = Name,
                        HasDetail = false
                    };

                    pMovies.Add(m);
                }

                GetMovieDetails(pMovies);
            }

            return pMovies;
        }

        private void GetMovieDetails(IList<Movie> ms)
        {
            int size = ms.Count;
            for(int i = 0; i < size; i++)
            {
                MovieDetail detail = new MovieDetail(ms[i], this, null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(detail.ThreadPoolCallBack));
            }
        }
    }
}
