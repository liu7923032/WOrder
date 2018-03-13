using System;
using System.Collections.Generic;
using System.Text;
using Dark.Common.Serializer;

namespace Dark.Common.Extension
{
    public static class ObjectExtension
    {
        public static string ToJson<T>(this T entity) where T : class
        {
            return JsonTools.ToJSON<T>(entity);
        }


    }
}
