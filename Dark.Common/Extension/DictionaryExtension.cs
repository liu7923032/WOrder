using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Extension
{
    public static class DictionaryExtension
    {
        public static List<T> GetKeys<T, K>(this Dictionary<T, K> dictionary) where T : class where K : class
        {
            List<T> list = new List<T>();
            if (dictionary.Count == 0)
            {
                return list;
            }

            var enumerator = dictionary.Keys.GetEnumerator();

            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current as T);
            }

            return list;
        }
    }
}
