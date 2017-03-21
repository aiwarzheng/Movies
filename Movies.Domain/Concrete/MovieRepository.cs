using Movies.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Domain.Entities;

namespace Movies.Domain.Concrete
{
    /// <summary>
    /// Implement of IMovieRepository
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly IMovieProvider[] m_pProviders = null;
        private IEnumerable<Movie> m_pMovies = null;

        public MovieRepository(IMovieProvider[] provs)
        {
            m_pProviders = provs;
        }

        public IEnumerable<Movie> Movies
        {
            get
            {
                if(m_pMovies == null)
                {
                    m_pMovies = new List<Movie>();
                    foreach (IMovieProvider p in m_pProviders)
                    {
                        m_pMovies = m_pMovies.Concat(p.GetMovies());
                    }
                }

                return m_pMovies;
            }
        }
    }
}
