<%@ WebHandler Language="c#" Class="File_WebHandler1" Debug="true" %>
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

public class File_WebHandler1 : IHttpHandler
{
    private const int UploadFileLimit = 4;//上传文件数量限制	
    string url = "";
    string _msg = "";
    private string sFile;
    private string dFile;
    private string syFile;
    private string _Url;

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
                    if (!IsAllowedExtension(file))
                    {
                        context.Response.Write("<script>window.parent.Finish();</script>");
                        return;
                    }
                    sFile = Path.GetFileName(file.FileName);
                    sFile = sFile.Substring(sFile.LastIndexOf("."));
                    string datatime = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");  //为了防止重名，获得日期为文件名  年月日时分秒毫秒                  
                    sFile = datatime + sFile;  //上传后文件的新名

                    //根据年月创建文件夹
                    string strDay = System.DateTime.Now.ToString("yyyyMM");
                    DirUrl = HttpContext.Current.Server.MapPath("~/upload/" + strDay);
                    if (!System.IO.Directory.Exists(DirUrl))//检测文件夹是否存在，不存在则创建
                    {
                        System.IO.Directory.CreateDirectory(DirUrl);
                    }
                    //根据文件夹位置返回页面是需要显示的图片地址
                    string folderType = context.Request.Form["FolderType"];
                    if (folderType == "1")//有两级目录
                    {
                        _Url = "../../upload/" + strDay + "/" + sFile;
                    }
                    else//一级目录
                    {
                        _Url = "../upload/" + strDay + "/" + sFile;
                    }
                    if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                    {
                        //保存文件
                        if (context.Request.Form["WaterType"].ToUpper() != "1")//不加水印
                        {
                            file.SaveAs(DirUrl + "/" + sFile);
                            //reUrl = _Url;
                            reUrl = "~/upload/" + strDay + "/" + sFile;
                        }
                        else//加水印
                        {
                            file.SaveAs(DirUrl + "/" + sFile);
                            if (folderType == "1")//有两级目录
                            {
                                _Url = "../../upload/" + strDay + "/w" + sFile;
                            }
                            else//一级目录
                            {
                                _Url = "../upload/" + strDay + "/w" + sFile;
                            }
                            //加图片的水印
                            System.Drawing.Image img = System.Drawing.Image.FromFile(DirUrl + "/" + sFile);
                            string filepath_water = DirUrl + "/w" + sFile;
                            //加文字的水印
                            WebUtility.AddImageSignText(img, filepath_water, "Chilema.com", 9, 50, "Tahoma", 12);
                            File.Delete(DirUrl + "/" + sFile);
                            reUrl = "~/upload/" + strDay+"/w"+sFile;
                        }
                    }
                }
                catch (Exception ex)
                {
                    url = "error";
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
    public static bool IsAllowedExtension(HttpPostedFile hifile)
    {
        int fileLen = hifile.ContentLength;
        byte[] imgArray = new byte[fileLen];
        hifile.InputStream.Read(imgArray, 0, fileLen);
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

        }
        r.Close();
        fs.Close();
        if (fileclass == "255216" || fileclass == "7173" || fileclass == "6677" || fileclass == "13780")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}