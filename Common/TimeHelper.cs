/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-08-27 9:43:39.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangjing.Common
{
    /// <summary>
    /// 时间处理类
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// 重与tostring方法，返回指定格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToFormString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 如果时区不同，返回当前的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime Now(this DateTime dt)
        {
            return dt.AddHours(8);
        }

        /// <summary>
        /// 重与tostring方法，返回指定格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 本周第一天(周一的日期)
        /// </summary>
        /// <returns></returns>
        public static DateTime WeekFirst()
        {
            int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
            //星期日 获取weeknow为0  
            weeknow = weeknow == 0 ? 7 : weeknow;
            int daydiff = (-1) * weeknow + 1;
            int dayadd = 7 - weeknow;

            //本周第一天(周1)
            string datebegin = System.DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");

            //本周最后一天  
            //string dateend = System.DateTime.Now.AddDays(dayadd).ToString("yyyy-MM-dd");

            return Convert.ToDateTime(datebegin);
        }

        /// <summary>
        /// 返回今天在一周中的排序，周1为1，周2为2....周日为7
        /// </summary>
        /// <returns></returns>
        public static int dayOfWeek()
        {
            int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
            //星期日 获取weeknow为0  
            weeknow = weeknow == 0 ? 7 : weeknow;
            return weeknow;
        }

        /// <summary>
        /// 本月第一天(一号的日期)
        /// </summary>
        /// <returns></returns>
        public static DateTime MounthFirst()
        {
            DateTime dt = DateTime.Now;
            //本月第一天时间   
            DateTime dt_First = dt.AddDays(-(dt.Day) + 1);

            return dt_First;
        }
    }
}
