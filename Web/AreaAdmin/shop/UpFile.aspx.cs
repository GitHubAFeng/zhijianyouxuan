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
using System.Xml.Linq;
using System.IO;

public partial class member_UpFile :AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sFile = Path.GetFileName(fileUp.FileName);
            sFile = sFile.Substring(sFile.LastIndexOf("."));
            string datatime = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");  //为了防止重名，获得日期为文件名  年月日时分秒毫秒                  
            sFile = datatime + sFile;  //上传后文件的新名

            //根据年月创建文件夹
            string strDay = System.DateTime.Now.ToString("yyyyMM");
            string DirUrl = HttpContext.Current.Server.MapPath("~/upload/" + strDay);
            string reUrl = "";

            if (!System.IO.Directory.Exists(DirUrl))//检测文件夹是否存在，不存在则创建
            {
                System.IO.Directory.CreateDirectory(DirUrl);
            }

            if (fileUp.FileContent.Length > 0 || !string.IsNullOrEmpty(fileUp.FileName))
            {
                //保存文件
                fileUp.SaveAs(DirUrl + "/" + sFile);
                //reUrl = _Url;
                reUrl = "~/upload/" + strDay + "/" + sFile;
            }
            Response.Write("<script Language='javascript'>parent.document.getElementById('" + Request.QueryString["id"] + "').value='" + reUrl + "';</script><script Language='javascript'>parent.document.getElementById('p" + Request.QueryString["id"] + "').src='../../"+"upload/" + strDay + "/" + sFile+"';</script>");
        }
        catch (Exception ex)
        {
            
        }
    }
}
