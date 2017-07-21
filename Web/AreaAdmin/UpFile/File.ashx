<%@ WebHandler Language="c#" Class="File_WebHandler" Debug="true" %>
using System.IO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

public class File_WebHandler : IHttpHandler
{
    private const int UploadFileLimit = 4;//上传文件数量限制	
    string url = "";
    string _msg = "";
    private string sFile;
    private string dFile;
    private string syFile;

    private string Type = "";
    private string DirUrl = "";
    private string txtCompanyName;
    private string _isAddWater = "0";
    private string Oldimage;
    private string FilePic;
    private string weburl = "http://localhost:19832/web/";
    private string reUrl = "";
    
    public void ProcessRequest(HttpContext context)
    {
        int iTotal = context.Request.Files.Count;
        if (iTotal == 0)
        {
            _msg = "没有数据";
        }
        else
        {
            for (int i = 0; i < iTotal; i++)
            {
                try
                {
                    //取得上传文件信息，并替换其名称
                    HttpPostedFile file = context.Request.Files[i];

                    string strDay = System.DateTime.Now.ToString("yyyyMM");
                    string DirUrl = HttpContext.Current.Server.MapPath("~/upload/" + strDay + "/");
                    if (!System.IO.Directory.Exists(DirUrl))//检测文件夹是否存在，不存在则创建
                    {
                        System.IO.Directory.CreateDirectory(DirUrl);
                    }
                    string sFile = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + Path.GetExtension(file.FileName);
                    string Thumbnailfilename = DirUrl + "" + sFile;
                    if (!IsAllowedExtension(file, Thumbnailfilename))
                    {
                        context.Response.Write("<script>window.parent.Finish();</script>");
                        return;
                    }

                    //根据文件夹位置返回页面是需要显示的图片地址
                    string folderType = context.Request.Form["FolderType"];
                    string WaterType = context.Request.Form["WaterType"];
                    
                    if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                    {
                        string watermsg = SectionProxyData.GetSetValue(32).Trim();  
                       
                        //保存文件
                        if (context.Request.Form["WaterType"].ToUpper() != "1" || watermsg == "")//不加水印
                        {
                            reUrl = "~/upload/" + strDay + "/" + sFile;
                        }
                        else//加水印
                        {
                            System.Drawing.Image img = System.Drawing.Image.FromFile(DirUrl + "/" + sFile);
                            string filepath_water = DirUrl + "/w" + sFile;
                            //加文字的水印

                            WebUtility.AddImageSignText(img, filepath_water, SectionProxyData.GetSetValue(32), 9, 100, "Tahoma", 14);
                            File.Delete(DirUrl + "/" + sFile);
                            reUrl = "~/upload/" + strDay + "/w" + sFile;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write("<script>window.parent.Finish('" + ex.Message + "');</script>");
                }
            }
        }
        context.Response.Write("<script>window.parent.Finish('" + reUrl + "');</script>");

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    //真正判断文件类型的关键函数
    public static bool IsAllowedExtension(HttpPostedFile hifile, string Thumbnailfilename)
    {
        //检查mime
        string sMimeType = hifile.ContentType.ToLower();
        if (sMimeType.IndexOf("image/") < 0)
        {
            return false;
        }
        
        int fileLen = hifile.ContentLength;
        byte[] imgArray = new byte[fileLen];
        hifile.InputStream.Read(imgArray, 0, fileLen);

        //图片尺寸检查
        System.IO.Stream picstream = hifile.InputStream;
        System.Drawing.Image img = System.Drawing.Image.FromStream(picstream);
        if (img.Width <= 0 || img.Height <= 0)
        {
            return false;
        }
        //生成缩略图检测

        System.IO.MemoryStream fs = new System.IO.MemoryStream(imgArray);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = "";
        byte buffer;
        try
        {
            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();

        }
        catch
        {
            return false;
        }
        r.Close();
        fs.Close();
        if (fileclass == "255216" || fileclass == "7173" || fileclass == "6677" || fileclass == "13780")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
        {
            
        }
        else
        {
            return false;
        }

        //根据年月创建文件夹

        
        if (!Hangjing.Common.Thumbnail.MakeThumbnailImage(img, Thumbnailfilename))
        {
            return false;
        }

        return true;

    }
}