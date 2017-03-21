using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Domain.Utils
{
    /// <summary>
    /// HTTP Protocal Utilities
    /// </summary>
    public class HttpUtils
    {
        private static readonly ILog log = LogManager.GetLogger("HttpUtils");

        public static string Get(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(url);
                }     
            }
            catch(Exception ex)
            {
                log.Error(ex);

                return "";
            }
        }

        public static string Get(string url, IDictionary<string,string> headers)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    if (headers != null)
                    {
                        foreach (var pv in headers)
                        {
                            client.Headers.Add(pv.Key, pv.Value);
                        }
                    }

                    return client.DownloadString(url);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return "";
            }
        }

        /// <summary>
        /// Get Information
        /// if fail, retry the specified number of times
        /// if retry times equal to -1, infinitely retry
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="tryCount">retry times, if equals to -1, infinitely retry</param>
        /// <returns></returns>
        public static string Get(string url, IDictionary<string, string> headers, int tryCount)
        {
            int count = 0;
            string content = Get(url, headers);
            while(String.IsNullOrEmpty(content) && (count < tryCount || tryCount == -1))
            {
                Thread.Sleep(100);

                content = Get(url, headers);

                count++;
            }

            return content;
        }
    }
}
