using Movies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.WebUI.Infrastructure
{
    public class MovieFilterBinder: IModelBinder
    {
        const string SessionKey = "MovieFilter";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            MovieFilter filter = null;
            if (controllerContext.HttpContext.Session != null)
            {
                filter = controllerContext.HttpContext.Session[SessionKey] as MovieFilter;
            }

            if (filter == null)
            {
                filter = new MovieFilter();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[SessionKey] = filter;
                }
            }
            return filter;
        }
    }
}