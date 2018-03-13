using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dark.Common.Serializer
{
    public class JsonTools
    {
        #region Json
        /// <summary>
        /// JsonConvert.SerializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }


        /// <summary>
        /// JsonConvert.DeserializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// 将JSON 字符串集合转换为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJSON"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(string strJSON) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(strJSON);
        }

        #endregion Json


    }
}
