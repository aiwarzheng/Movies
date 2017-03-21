using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.WebUI.Models
{
    public class MovieFilter
    {
        public MovieFilter()
        {
            Page = 1;
        }

        public int Genre { get; set; }
        public int Year { get; set; }
        public int Country { get; set; }
        public int Price { get; set; }
        public string Keywords { get; set; }
        public int Page { get; set; }

        public void Reset()
        {
            Genre = 0;
            Year = 0;
            Country = 0;
            Price = 0;
        }
    }
}