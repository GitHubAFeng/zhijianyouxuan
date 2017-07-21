// Timing.aspx.cs :时间校验页面
// CopyRight (c) 2010 HangJing Teconology. All Rights Reserved.
// wlf@ihangjing.com
// 2009-03-25
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Text;
using System.IO;

public partial class Timing : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {



        StringBuilder results = new StringBuilder("<sysTime>");
        results.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        results.Append("</sysTime>");

        Response.Write(results.ToString());
        Response.End();

    }
    /// <summary>
    /// 返回延时时间:秒数
    /// </summary>
    /// <returns></returns>
    /// 
    protected string lastTime(DateTime dt)
    {

        if (Request.QueryString["id"] == "2010001")
        {
            StreamReader sr = File.OpenText(Server.MapPath("timing.txt"));
            int result = Convert.ToInt32(sr.ReadLine());
            string firsttime = sr.ReadLine();
            int lasttime = (Convert.ToInt32(sr.ReadLine()) - 1) / 2; ;
            sr.Close();
            if (result >= 0 && result <= 3)
            {
                //第一次访问;
                if (result == 0)
                {
                    result += 1;
                    StreamWriter sw = new StreamWriter(Server.MapPath("timing.txt"));
                    sw.WriteLine(result);
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine("0");
                    sw.Close();

                }
                else
                {
                    //第二次访问;   
                    if (result == 1)
                    {
                        StreamWriter sw = new StreamWriter(Server.MapPath("timing.txt"));
                        result += 1;
                        TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(firsttime));
                        sw.WriteLine(result);
                        sw.WriteLine(DateTime.Now);
                        sw.WriteLine(ts.Seconds);
                        sw.Close();

                    }
                    else
                    {
                        //第三次访问;
                        if (result == 2)
                        {
                            StreamWriter sw = new StreamWriter(Server.MapPath("timing.txt"));
                            sw.WriteLine(0);
                            sw.WriteLine(0);
                            sw.WriteLine(0);
                            sw.Close();

                            StringBuilder results = new StringBuilder("<sysTime>");
                            results.Append(firsttime);
                            results.Append("</sysTime>");
                            results.Append("<timing>");
                            results.Append(lasttime);
                            results.Append("</timing>");

                            Response.Write(results.ToString());
                            Response.End();

                        }
                    }
                }

            }
            else
            {
                //error;

            }
        }
        else
        {
            //nothing id错误;
        }

        return null;
    }
}
