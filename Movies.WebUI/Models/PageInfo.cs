using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.WebUI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
            }
        }
    }
}