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
    }
}
