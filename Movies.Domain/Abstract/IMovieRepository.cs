using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Abstract
{
    /// <summary>
    /// Movie Repository
    /// Provider movie information
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Total provided movies
        /// </summary>
         IEnumerable<Movie> Movies
        {
            get;
        }
    }
}
