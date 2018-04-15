using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Utils
{
    public class DateTool
    {
        /// <summary>
        /// 获取中文周
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static int GetWeekZN(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    return 0;
            }
        }
    }
}
