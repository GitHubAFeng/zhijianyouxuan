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

public partial class admin_UpFile : AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        string fileType = ".gif.jpg.bmp.png.swf";
        string sFile = Path.GetFileName(fileUp.FileName);
        sFile = sFile.Substring(sFile.LastIndexOf("."));
        if (fileType.ToLower().IndexOf(sFile.ToLower()) == -1)
        {
            Response.Write("<script Language='javascript'>alert('上传图片格式不对,格式只能为：" + fileType + "');</script>");
            return;
        }

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
            string Thumbnailfilename = DirUrl + "/" + sFile;
            System.Drawing.Image img = System.Drawing.Image.FromStream(fileUp.FileContent);
            if (!Hangjing.Common.Thumbnail.IsAllowedUpload(img, Thumbnailfilename))
            {
                Response.Write("<script Language='javascript'>alert('上传图片格式不对');</script>");
                return;
            }

            img = System.Drawing.Image.FromFile(Thumbnailfilename);

            string watermsg = SectionProxyData.GetSetValue(33).Trim();

            //保存文件
            if (Request.QueryString["WaterType"] == "1"&&!string.IsNullOrEmpty(SectionProxyData.GetSetValue(32).Trim()))//加水印
            {
                string filepath_water = DirUrl + "/w" + sFile;
                //所有位置加文字的水印

                WebUtility.AddImageSignText(img, filepath_water, SectionProxyData.GetSetValue(32), -1, 100, "Tahoma", 30);
                File.Delete(DirUrl + "/" + sFile);
                reUrl = "~/upload/" + strDay + "/w" + sFile;
            }
            else
            {
                img.Dispose();
                fileUp.SaveAs(DirUrl + "/" + sFile);
                reUrl = "~/upload/" + strDay + "/" + sFile;
            }


        }
        string picurl = reUrl.Replace("~", "");
        Response.Write("<script Language='javascript'>parent.document.getElementById('" + Request.QueryString["id"] + "').value='" + reUrl + "';</script><script Language='javascript'>parent.document.getElementById('p" + Request.QueryString["id"] + "').src='" + picurl + "';</script>");//../../"+"upload/" + strDay + "/" + sFile+"

    }
}
