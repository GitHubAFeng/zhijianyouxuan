using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

/// <summary>
/// CreatJs 的摘要说明
/// </summary>
public class CreatJs
{
    public CreatJs() { }
    public void CreatJsFile(Hangjing.Model.AdTableInfo adi)
    {
        string path = HttpContext.Current.Server.MapPath("~/js/Advertisement/" + adi.TID.ToString() + ".js");
        Encoding codi = Encoding.GetEncoding("GB2312");
        StringBuilder strb = new StringBuilder();
        if (File.Exists(path))
        {
            File.SetAttributes(path, FileAttributes.Normal);
        }
        string ext = System.IO.Path.GetExtension(adi.AdImageAdrees).ToLower();
        if (ext.IndexOf("swf") >= 0)
        {
            commonflash(adi, strb);
        }
        else
        {
            commonimg(adi, strb);
        }

        StreamWriter wri = null;
        try
        {
            Stream output = new FileStream(path, FileMode.Create);
            wri = new StreamWriter(output);
            wri.Write(strb);
            wri.Flush();
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message);
            HttpContext.Current.Response.End();

        }
        finally
        {
            wri.Close();
        }
    }

     //<summary>
     //普通显示 图片
     //</summary>
     //<returns></returns>
    private StringBuilder commonimg(AdTableInfo adi, StringBuilder strb)
    {
        string imgurl = "";
        if (adi.AdImageAdrees.IndexOf('~') == 0)
        {
            imgurl = adi.AdImageAdrees.Substring(1);
        }
        else
        {
            imgurl = "/" + adi.AdImageAdrees;
        }
        string sys_url = ConfigurationManager.AppSettings["siteurl"].ToString();
        strb.Append("document.write(\"");
        strb.Append("<a href=\'" + adi.AdAdrees + "\' target=\'_blank\'>");
        strb.Append("<img src=\'" + sys_url + adi.AdImageAdrees.Substring(1) + "\' border=\'0\' width=\'" + adi.AdWidth + "' height=\'" + adi.AdHeight + "\'  target=\'_blank\'></a>\");");
        return strb;
    }

     //<summary>
     //普通显示 flash
     //</summary>
     //<returns></returns>
    private StringBuilder commonflash(AdTableInfo adi, StringBuilder strb)
    {
        string imgurl = "";
        if (adi.AdImageAdrees.IndexOf('~') == 0)
        {
            imgurl = adi.AdImageAdrees.Substring(1);
        }
        else
        {
            imgurl = adi.AdImageAdrees;
        }
        string sys_url = ConfigurationManager.AppSettings["siteurl"].ToString();
        strb.Append("document.write(\"");
        strb.Append("<a href=\'" + adi.AdAdrees + "\'>");
        strb.Append("<object height='" + adi.AdHeight + "' width='" + adi.AdWidth + "' ");
        strb.Append("codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'>");
        strb.Append("<param value='" + sys_url + imgurl + "' name='movie'>");
        strb.Append("<param value='high' name='quality'>");
        strb.Append("<param value='transparent' name='wmode'>");
        strb.Append("<embed height='" + adi.AdHeight + "' width='" + adi.AdWidth + "' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' quality='high' ");
        strb.Append("src='" + sys_url + imgurl + "'>");
        strb.Append("</object>");
        strb.Append("</a>\");");
        return strb;
    }
}


 
                                                               
                                                              
                                                             
                                                               
                                                           