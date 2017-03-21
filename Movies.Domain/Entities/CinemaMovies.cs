using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    /// <summary>
    /// Movies provided by CinemaWorld
    /// </summary>
    public class CinemaMovies
    {
        public IList<CinemaMovie> Movies { get; set; }
    }
}
