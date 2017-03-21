using Movies.Domain.Abstract;
using Movies.Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;

            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IMovieProvider>().To<CinemaWorldProvider>();
            kernel.Bind<IMovieProvider>().To<FilmWorldProvider>();
            kernel.Bind<IMovieRepository>().To<MovieRepository>().InSingletonScope();
        }
    }
}