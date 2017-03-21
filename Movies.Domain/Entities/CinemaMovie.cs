using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    /// <summary>
    /// CinemaWorld's movie summary info
    /// </summary>
    public class CinemaMovie
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
