using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.WebUI.Models
{
    public class MovieList
    {
        public IEnumerable<Movie> Movies { get; set; }
        public PageInfo Pages { get; set; }
    }
}