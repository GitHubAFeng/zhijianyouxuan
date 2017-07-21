using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizone.library.core.CharSet
{
    public class Base64Helper
    {
        public static byte[] StrToBytes(String str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static byte[] StrToBytes(String str, String char_set)
        {
            return Encoding.GetEncoding(char_set).GetBytes(str);
        }

        public static String BytesToStr(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public static String BytesToStr(byte[] bytes, String char_set)
        {
            return Encoding.GetEncoding(char_set).GetString(bytes);
        }

        public static String StrToBase64(String str)
        {
            byte[] bytes = StrToBytes(str);
            return Convert.ToBase64String(bytes);
        }

        public static String StrToBase64(String str, String char_set)
        {
            byte[] bytes = StrToBytes(str, char_set);
            return Convert.ToBase64String(bytes);
        }

        public static String Base64ToStr(String str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return BytesToStr(bytes);
        }

        public static String Base64ToStr(String str, String char_set)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return BytesToStr(bytes, char_set);
        }

    }
}
