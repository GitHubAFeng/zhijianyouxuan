using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hangjing.DBUtility
{
    public static class HJConvert
    {
        public static string ToString(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(obj);
            }
        }

        public static Int32 ToInt32(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static Int32? ToNullInt32(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static Int64 ToInt64(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static Decimal ToDecimal(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public static Decimal? ToNullDecimal(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public static Double ToDouble(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        public static Double? ToNullDouble(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        public static DateTime ToDateTime(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return DateTime.Now;
            }
            else
            {
                string timestr = obj.ToString();
                if (timestr == "0000-00-00 00:00:00")
                {
                    return DateTime.Now;
                }
                return Convert.ToDateTime(obj);
            }
        }

        public static DateTime? ToNullDateTime(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        public static DateTime? ToNullDate(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        public static bool ToBoolean(object obj)
        {
            if (Convert.IsDBNull(obj) || obj == null)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(obj);
            }
        }

        public static Int32 ToInt32(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(str);
            }
        }

        public static Int64 ToInt64(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(str);
            }
        }

        public static Decimal ToDecimal(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(str);
            }
        }

        public static Double ToDouble(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(str);
            }
        }

        public static DateTime ToDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now;
            }
            else
            {
                return Convert.ToDateTime(str);
            }
        }

        public static DateTime? ToNullDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(str);
            }
        }

        public static bool ToBoolean(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(str);
            }
        }

        public static object ToDbNullDate(DateTime? date)
        {
            if (Convert.IsDBNull(date) || date == null)
            {
                return System.DBNull.Value;
            }
            else
            {
                return date;
            }
        }

        /// <summary>
        /// 检查字符串类型的变量是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CheckNull(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return System.DBNull.Value;
            }
            else
            {
                return (object)obj;
            }
        }

        /// <summary>
        /// 检查时间类型的变量是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CheckNull(DateTime obj)
        {
            if (DateTime.MinValue == obj)
            {
                return System.DBNull.Value;
            }
            else
            {
                return (object)obj;
            }
        }

        /// <summary>
        /// 检查可为空的时间类型的变量是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CheckNull(DateTime? obj)
        {
            if (obj == null)
            {
                return System.DBNull.Value;
            }
            else
            {
                return (object)obj;
            }
        }

        /// <summary>
        /// 检查Bool类型的变量是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CheckNull(bool obj)
        {
            if (!obj)
            {
                return System.DBNull.Value;
            }
            else
            {
                return (object)obj;
            }
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;

        }


        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
            {
                return IsNumeric(expression.ToString());
            }
            return false;

        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public static string GetRandom(int len)
        {
            char[] s = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            string str = String.Empty;
            Random random = new Random(GetRandomSeed());
            for (int i = 0; i < len; i++)
            {
                str += s[random.Next(0, s.Length)].ToString();
            }
            return str;
        }

        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 去最后的符号
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public static string dellast(string old)
        {
            return System.Text.RegularExpressions.Regex.Replace(old, @",$", "");
        }
    }
}
