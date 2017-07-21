/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : $codebesideclassname$
 * Function : 
 * Created by jijunjian at 2010-8-15 2:17:54.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///ChineseConvert 的摘要说明
/// </summary>
public class ChineseConvert
{
    /// <summary>
    /// 返回字符串的首写字母字符串
    /// </summary>
    /// <param name="IndexTxt">需得到首写字母的字符串</param>
    /// <returns></returns>
    public static String UtilIndexCode(String IndexTxt)
    {
        string _Temp = null;
        for (int i = 0; i < IndexTxt.Length; i++)
            _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
        return _Temp;
    }

    //得到单个字符的首字母
    private static String GetOneIndex(String OneIndexTxt)
    {
        if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
            return OneIndexTxt;
        else
            return GetGbkX(OneIndexTxt);
    }

    //根据汉字拼音排序得到首字母
    private static string GetGbkX(string str)
    {
        if (str.CompareTo("吖") < 0)
        {
            return str;
        }
        if (str.CompareTo("八") < 0)
        {
            return "A";
        }

        if (str.CompareTo("嚓") < 0)
        {
            return "B";
        }

        if (str.CompareTo("咑") < 0)
        {
            return "C";
        }
        if (str.CompareTo("妸") < 0)
        {
            return "D";
        }
        if (str.CompareTo("发") < 0)
        {
            return "E";
        }
        if (str.CompareTo("旮") < 0)
        {
            return "F";
        }
        if (str.CompareTo("铪") < 0)
        {
            return "G";
        }
        if (str.CompareTo("讥") < 0)
        {
            return "H";
        }
        if (str.CompareTo("咔") < 0)
        {
            return "J";
        }
        if (str.CompareTo("垃") < 0)
        {
            return "K";
        }
        if (str.CompareTo("呒") < 0)
        {
            return "L";
        }
        if (str.CompareTo("拏") < 0)
        {
            return "M";
        }
        if (str.CompareTo("噢") < 0)
        {
            return "N";
        }
        if (str.CompareTo("妑") < 0)
        {
            return "O";
        }
        if (str.CompareTo("七") < 0)
        {
            return "P";
        }
        if (str.CompareTo("亽") < 0)
        {
            return "Q";
        }
        if (str.CompareTo("仨") < 0)
        {
            return "R";
        }
        if (str.CompareTo("他") < 0)
        {
            return "S";
        }
        if (str.CompareTo("哇") < 0)
        {
            return "T";
        }
        if (str.CompareTo("夕") < 0)
        {
            return "W";
        }
        if (str.CompareTo("丫") < 0)
        {
            return "X";
        }
        if (str.CompareTo("帀") < 0)
        {
            return "Y";
        }
        if (str.CompareTo("咗") < 0)
        {
            return "Z";
        }
        return str;
    }

}

