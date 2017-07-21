#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-7-14 15:55:40.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using System.IO;

public partial class Admin_shop_download : System.Web.UI.Page
{
    FileInfo fileInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       // WebUtility.getFile(Server.MapPath("~/shop/foodupload.rar"));
        DownloadFile(Server.MapPath("~/shop/foodupload.rar"), "foodupload.rar"); 
    }

    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="fullName">文件完整路径</param>
    /// <param name="sendFileName">发送到客户端显示的文件名</param>
    /// <returns>下载成功返回true,否则返回false</returns>
    public bool DownloadFile(string fullName, string sendFileName)
    {
        if (!IsFileExists(fullName))
            return false;

        try
        {
            fileInfo = new FileInfo(fullName);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(sendFileName, System.Text.Encoding.UTF8));
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(fileInfo.FullName);
            Response.End();
            Response.Flush();
            Response.Clear();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool IsFileExists(string fullName)
    {
        fileInfo = new FileInfo(fullName);
        if (fileInfo.Exists)
            return true;
        return false;
    }
}
