using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Abstract
{
    /// <summary>
    /// Movie Provider
    /// </summary>
    public interface IMovieProvider
    {
        /// <summary>
        /// Provider Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get all movies provided by provider
        /// </summary>
        /// <returns>movies</returns>
        IEnumerable<Movie> GetMovies();

        /// <summary>
        /// Get movie detail
        /// </summary>
        /// <param name="m">movie summary info</param>
        void GetMovieDetail(Movie m);
    }
}
