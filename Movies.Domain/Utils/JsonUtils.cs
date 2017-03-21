using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Utils
{
    public class JsonUtils
    {
        private static readonly ILog log = LogManager.GetLogger("JsonUtils");

        /// <summary>
        /// Convert object to JSON String
        /// </summary>
        /// <param name="o">Converted Object</param>
        /// <returns>JSON String</returns>
        public static string ToJson(object o)
        {
            if(o == null)
            {
                log.Error("Object is Null");
                return "";
            }

            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return "";
            }
        }

        /// <summary>
        /// convert json to object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="json"></param>
        /// <returns>object</returns>
        public static T FromJson<T>(string json)
        {
            if(String.IsNullOrEmpty(json))
            {
                log.Error("json string is null or empty");
                return default(T);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return default(T);
            }
        }
    }
}
