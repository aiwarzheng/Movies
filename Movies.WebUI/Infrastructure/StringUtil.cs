using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.WebUI.Infrastructure
{
    public static class StringUtil
    {
        public static bool ContainsIgnoreCase(this string src, string dst)
        {
            return src.ToLower().Contains(dst.ToLower());
        }
    }
}