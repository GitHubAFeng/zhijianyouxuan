// StringHelper.cs
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-01-12
// 字符串帮助类
using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;

namespace Hangjing.Common
{
	/// <summary>
	/// 工具类
	/// </summary> 
    public class Utils
	{

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            return Validator.IsNumericArray(strNumber);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!Utils.StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (Utils.StrIsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        }


        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }


        public const string ASSEMBLY_VERSION = "2.0.1";
       
        private static FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

		/// <summary>
		/// 得到正则编译参数设置
		/// </summary>
        /// <returns>参数设置</returns>
		public static RegexOptions GetRegexCompiledOptions()
		{
            #if NET1
            return RegexOptions.Compiled;
            #else
            return RegexOptions.None;
            #endif
        }
	
		/// <summary>
		/// 获得当前绝对路径
		/// </summary>
		/// <param name="strPath">指定的路径</param>
		/// <returns>绝对路径</returns>
		public static string GetMapPath(string strPath)
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.Server.MapPath(strPath);
			}
			else //非web程序引用
			{
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
				return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
			}
		}

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

		/// <summary>
		/// 以指定的ContentType输出指定文件
		/// </summary>
		/// <param name="filepath">文件路径</param>
		/// <param name="filename">输出的文件名</param>
		/// <param name="filetype">将文件输出时设置的ContentType</param>
		public static void ResponseFile(string filepath, string  filename, string filetype)
		{
			Stream iStream = null;

			// 缓冲区为10k
			byte[] buffer = new Byte[10000];

			// 文件长度
			int length;

			// 需要读的数据长度
			long dataToRead;

			try
			{
				// 打开文件
				iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


				// 需要读的数据长度
				dataToRead = iStream.Length;

				HttpContext.Current.Response.ContentType = filetype;
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.UrlEncode(filename.Trim()).Replace("+", " "));

				while (dataToRead > 0)
				{
					// 检查客户端是否还处于连接状态
					if (HttpContext.Current.Response.IsClientConnected)
					{
						length = iStream.Read(buffer, 0, 10000);
						HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
						HttpContext.Current.Response.Flush();
						buffer = new Byte[10000];
						dataToRead = dataToRead - length;
					}
					else
					{
						// 如果不再连接则跳出死循环
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HttpContext.Current.Response.Write("Error : " + ex.Message);
			}
			finally
			{
				if (iStream != null)
				{
					// 关闭文件
					iStream.Close();
				}
			}
			HttpContext.Current.Response.End();
		}

		/// <summary>
		/// 判断文件名是否为浏览器可以直接显示的图片文件名
		/// </summary>
		/// <param name="filename">文件名</param>
		/// <returns>是否可以直接显示</returns>
		public static bool IsImgFilename(string filename)
		{
			filename = filename.Trim();
			if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
			{
				return false;
			}
			string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
			return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
		}			
		
		/// <summary>
		/// int型转换为string型
		/// </summary>
		/// <returns>转换后的string类型结果</returns>
		public static string IntToStr(int intValue)
		{
			return Convert.ToString(intValue);
		}

		/// <summary>
		/// MD5函数
		/// </summary>
		/// <param name="str">原始字符串</param>
		/// <returns>MD5结果</returns>
		public static string MD5(string str)
		{
			byte[] b = Encoding.Default.GetBytes(str);
			b = new MD5CryptoServiceProvider().ComputeHash(b);
			string ret = "";
			for(int i = 0; i < b.Length; i++)
				ret += b[i].ToString("x").PadLeft(2,'0');
			return ret;
		}

        /** 获取大写的MD5签名结果 */
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            inputBye = Encoding.UTF8.GetBytes(encypStr);

            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        ///  Sha1加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String getSha1(String str)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            //将mystr转换成byte[] 
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");
            return hash;
        }


		/// <summary>
		/// SHA256函数
		/// </summary>
		/// /// <param name="str">原始字符串</param>
		/// <returns>SHA256结果</returns>
		public static string SHA256(string str)
		{
			byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
			SHA256Managed Sha256 = new SHA256Managed();
			byte[] Result = Sha256.ComputeHash(SHA256Data);
			return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
		}

		/// <summary>
		/// 返回URL中结尾的文件名
		/// </summary>		
		public static string GetFilename(string url)
		{
			if (url == null)
			{
				return "";
			}
			string[] strs1 = url.Split(new char[]{'/'});
			return strs1[strs1.Length - 1].Split(new char[]{'?'})[0];
		}

		/// <summary>
		/// 返回指定目录下的非 UTF8 字符集文件
		/// </summary>
		/// <param name="Path">路径</param>
		/// <returns>文件名的字符串数组</returns>
		public static string[] FindNoUTF8File(string Path) 
		{ 
			StringBuilder filelist = new StringBuilder();   
			DirectoryInfo Folder = new DirectoryInfo(Path); 
			FileInfo[] subFiles = Folder.GetFiles(); 
			for (int j = 0; j < subFiles.Length; j++) 
			{ 
				if (subFiles[j].Extension.ToLower().Equals(".htm")) 
				{           
					FileStream fs = new FileStream(subFiles[j].FullName, FileMode.Open,FileAccess.Read); 
					bool bUtf8 = IsUTF8(fs);
					fs.Close();
					if (!bUtf8)
					{
						filelist.Append(subFiles[j].FullName);
						filelist.Append("\r\n");
					}       
				} 
			}
            return StringHelper.SplitString(filelist.ToString(), "\r\n");
     
		} 
   
		//0000 0000-0000 007F - 0xxxxxxx  (ascii converts to 1 octet!)
		//0000 0080-0000 07FF - 110xxxxx 10xxxxxx    ( 2 octet format)
		//0000 0800-0000 FFFF - 1110xxxx 10xxxxxx 10xxxxxx (3 octet format)

		/// <summary>
		/// 判断文件流是否为UTF8字符集
		/// </summary>
		/// <param name="sbInputStream">文件流</param>
		/// <returns>判断结果</returns>
		private static bool IsUTF8(FileStream sbInputStream) 
		{ 
			int   i; 
			byte cOctets;  // octets to go in this UTF-8 encoded character 
			byte chr; 
			bool  bAllAscii= true; 
			long iLen = sbInputStream.Length; 

			cOctets = 0; 
			for (i = 0; i < iLen; i++)  
			{ 
				chr = (byte)sbInputStream.ReadByte(); 

				if ( (chr & 0x80) != 0 ) bAllAscii= false; 

				if ( cOctets == 0 )   
				{ 
					if( chr >= 0x80 )  
					{   
						do  
						{ 
							chr <<= 1; 
							cOctets++; 
						} 
						while ((chr & 0x80) != 0); 

						cOctets--;                         
						if (cOctets == 0) return false;   
					} 
				} 
				else  
				{ 
					if ((chr & 0xC0) != 0x80)  
					{ 
						return false; 
					} 
					cOctets--;                        
				} 
			} 

			if (cOctets > 0)  
			{   
				return false; 
			} 

			if (bAllAscii)  
			{     
				return false; 
			} 

			return true; 

		}

		/// <summary>
		/// 格式化字节数字符串
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static string FormatBytesStr(int bytes)
		{
			if (bytes > 1073741824)
			{
				return ((double)(bytes / 1073741824)).ToString("0") + "G";
			}
			if (bytes > 1048576)
			{
				return ((double)(bytes / 1048576)).ToString("0") + "M";
			}
			if (bytes > 1024)
			{
				return ((double)(bytes / 1024)).ToString("0") + "K";
			}
			return bytes.ToString() + "Bytes";
		}

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            return TypeParse.IsNumeric(Expression);
        }

		/// <summary>
		/// 将long型数值转换为Int32类型
		/// </summary>
		/// <param name="objNum"></param>
		/// <returns></returns>
		public static int SafeInt32(object objNum)
		{
			if (objNum == null)
			{
				return 0;
			}
			string strNum = objNum.ToString();
			if (IsNumeric(strNum))
			{
				
				if (strNum.ToString().Length > 9)
				{
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
				}
				return Int32.Parse(strNum);
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// 返回相差的秒数
		/// </summary>
		/// <param name="Time"></param>
		/// <param name="Sec"></param>
		/// <returns></returns>
		public static int StrDateDiffSeconds(string Time, int Sec)
		{
			TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
			if (ts.TotalSeconds > int.MaxValue)
			{
				return int.MaxValue;
			}
			else if (ts.TotalSeconds < int.MinValue)
			{
				return int.MinValue;
			}
			return (int)ts.TotalSeconds;
		}

		/// <summary>
		/// 返回相差的分钟数
		/// </summary>
		/// <param name="time"></param>
		/// <param name="minutes"></param>
		/// <returns></returns>
		public static int StrDateDiffMinutes(string time, int minutes)
		{
			if (time == "" || time == null)
				return 1;
			TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
			if (ts.TotalMinutes > int.MaxValue)
			{
				return int.MaxValue;
			}
			else if (ts.TotalMinutes < int.MinValue)
			{
				return int.MinValue;
			}
			return (int)ts.TotalMinutes;
		}

		/// <summary>
		/// 返回相差的小时数
		/// </summary>
		/// <param name="time"></param>
		/// <param name="hours"></param>
		/// <returns></returns>
		public static int StrDateDiffHours(string time, int hours)
		{
			if (time == "" || time == null)
				return 1;
			TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
			if (ts.TotalHours > int.MaxValue)
			{
				return int.MaxValue;
			}
			else if (ts.TotalHours < int.MinValue)
			{
				return int.MinValue;
			}
			return (int)ts.TotalHours;
		}
		
		/// <summary>
		/// 为脚本替换特殊字符串
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ReplaceStrToScript(string str)
		{
			str = str.Replace("\\","\\\\");
			str = str.Replace("'","\\'");
			str = str.Replace("\"","\\\"");
			return str;
		}

		/// <summary>
		/// 是否为ip
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIP(string ip)
		{
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
                                     
		}

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }

		/// <summary>
		/// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="iparray"></param>
		/// <returns></returns>
		public static bool InIPArray(string ip, string[] iparray)
		{
			
			string[] userip = StringHelper.SplitString(ip, @".");
			for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
			{
                string[] tmpip = StringHelper.SplitString(iparray[ipIndex], @".");
				int r = 0;
				for (int i = 0; i < tmpip.Length; i++)
				{
					if (tmpip[i] == "*")
					{
						return true;
					}

					if (userip.Length > i)
					{
						if (tmpip[i] == userip[i])
						{
							r ++;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}

				}
				if (r == 4)
				{
					return true;
				}
				

			}
			return false;

		}

		/// <summary>
		/// 获得Assembly版本号
		/// </summary>
		/// <returns></returns>
		public static string GetAssemblyVersion()
		{
            return string.Format("{0}.{1}.{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);
		}

		/// <summary>
		/// 获得Assembly产品名称
		/// </summary>
		/// <returns></returns>
		public static string GetAssemblyProductName()
		{
            return AssemblyFileVersion.ProductName;
		}

		/// <summary>
		/// 获得Assembly产品版权
		/// </summary>
		/// <returns></returns>
		public static string GetAssemblyCopyright()
		{
            return AssemblyFileVersion.LegalCopyright;
		}

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
            {
                return HttpContext.Current.Request.Cookies[strName][key].ToString();
            }

            return "";
        }

		/// <summary>
		/// 得到真实路径
		/// </summary>
		/// <returns></returns>
		public static string GetTrueForumPath()
		{
			string forumPath = HttpContext.Current.Request.Path;
			if (forumPath.LastIndexOf("/") != forumPath.IndexOf("/"))
			{
				forumPath = forumPath.Substring(forumPath.IndexOf("/"), forumPath.LastIndexOf("/") + 1);
			}
			else
			{
				forumPath = "/";
			}
			return forumPath;

		}

        /// <summary>
        /// 转换长文件名为短文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="repstring"></param>
        /// <param name="leftnum"></param>
        /// <param name="rightnum"></param>
        /// <param name="charnum"></param>
        /// <returns></returns>
        public static string ConvertSimpleFileName(string fullname,string repstring,int leftnum,int rightnum,int charnum)
        {
            string simplefilename = "", leftstring = "", rightstring = "", filename = "";
           
            string extname = GetFileExtName(fullname);
            if (extname == "" || extname == null)
            {

                throw new Exception("字符串不含有扩展名信息");
            }
            int filelength = 0, dotindex = 0;
            
            dotindex = fullname.LastIndexOf('.');
            filename = fullname.Substring(0, dotindex);
            filelength = filename.Length;
            if (dotindex > charnum)
            {
                leftstring = filename.Substring(0, leftnum);
                rightstring = filename.Substring(filelength - rightnum, rightnum);
                if (repstring == "" || repstring == null)
                {
                    simplefilename = leftstring + rightstring + "." + extname;
                }
                else
                {
                    simplefilename = leftstring + repstring + rightstring + "." + extname;
                }
            }
            else
            {

                simplefilename = fullname;
            }
            return simplefilename;
        
        }

        public static string GetFileExtName(string filename)
        {
            string[] array = filename.Trim().Split('.');
            Array.Reverse(array);
            return array[0].ToString();
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
            {
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号
            }
            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号
            }

            if (dt_dispose)
            { 
                dt.Dispose(); 
            }
            return stringBuilder.Append("\r\n];");
        }




        /// <summary>
        /// 检查颜色值是否为3/6位的合法颜色
        /// </summary>
        /// <param name="color">待检查的颜色</param>
        /// <returns></returns>
        public static bool CheckColorValue(string color)
        {
            if (StringHelper.IsNullorEmpty(color))
            {
                return false;
            }

            color = color.Trim().Trim('#');

            if (color.Length != 3 && color.Length != 6)
            {
                return false;
            }
            //不包含0-9  a-f以外的字符
            if (!Regex.IsMatch(color, "[^0-9a-f]", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Timeout = 20000;//20秒超时
            WebResponse response = request.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

         /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        public static string RegEsc(string str)
        {
            string[] pattern = { @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                //Regex rgx = new Regex(s);
                //keyword = rgx.Replace(keyword, "\\" + s);
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;

                }

            }
            return str;
        }
		
		        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 清除UBB标签
        /// </summary>
        /// <param name="sDetail">帖子内容</param>
        /// <returns>帖子内容</returns>
        public static string ClearUBB(string sDetail)
        {
            return Regex.Replace(sDetail, @"\[[^\]]*?\]", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }
    }

}
