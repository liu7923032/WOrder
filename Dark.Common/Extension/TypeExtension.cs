using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Extension
{
    public static class TypeExtension
    {
        /// <summary>
        /// 检查是否是可控类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCanNull(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
