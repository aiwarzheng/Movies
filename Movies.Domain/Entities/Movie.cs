using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    /// <summary>
    /// Movie model of the system
    /// </summary>
    public class Movie
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public int Metascore { get; set; }
        public double Rating { get; set; }
        public int Votes { get; set; }
        public decimal Price { get; set; }
        public string Provider { get; set; }
        public bool HasDetail { get; set; }
    }
}
