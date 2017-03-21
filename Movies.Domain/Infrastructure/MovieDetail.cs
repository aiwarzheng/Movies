using log4net;
using Movies.Domain.Abstract;
using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Domain.Infrastructure
{
    /// <summary>
    /// class of getting movie detail
    /// </summary>
    public class MovieDetail
    {
        private ILog log = LogManager.GetLogger("MovieDetail");

        public MovieDetail(Movie m, IMovieProvider provider, ManualResetEvent e)
        {
            m_pProvider = provider;
            m_pMovie = m;
            m_pEvent = e;
        }

        private IMovieProvider m_pProvider = null;
        private Movie m_pMovie = null;
        private ManualResetEvent m_pEvent = null;

        public void ThreadPoolCallBack(object stateInfo)
        {
            m_pProvider.GetMovieDetail(m_pMovie);
            if(m_pEvent != null)
            {
                m_pEvent.Set();
            }

            log.Debug(string.Format("get data:{0}", m_pMovie.ID));
        }
    }
}
