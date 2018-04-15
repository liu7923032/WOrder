using System;
using System.Collections.Generic;
using System.Text;
using Dark.Common.Serializer;

namespace Dark.Common.Extension
{
    public static class StringExtension
    {
        public static T ToObject<T>(this string input) where T : class
        {
            return JsonTools.ToObject<T>(input);
        }


        public static List<int> ToListBySplit(this string input, char split = ',')
        {
            List<int> ids = new List<int>();
            if (string.IsNullOrEmpty(input))
            {
                return ids;
            }

            string[] strList = input.Split(split);
            foreach (var str in strList)
            {
                ids.Add(Convert.ToInt32(str));
            }
            return ids;
        }
    }
}
