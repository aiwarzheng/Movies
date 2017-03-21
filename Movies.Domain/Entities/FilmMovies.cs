using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    /// <summary>
    /// Movies provided by FilmWorld
    /// </summary>
    public class FilmMovies
    {
        public IList<FilmMovie> Movies { get; set; }
    }
}
