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
    private const int UploadFileLimit = 4;//�ϴ��ļ���������	
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
            _msg = "û������";
        }
        else
        {
            for (int i = 0; i < iTotal; i++)
            {
                try
                {
                    //ȡ���ϴ��ļ���Ϣ�����滻������
                    HttpPostedFile file = context.Request.Files[i];
                    if (!IsAllowedExtension(file))
                    {
                        context.Response.Write("<script>window.parent.Finish();</script>");
                        return;
                    }
                    sFile = Path.GetFileName(file.FileName);
                    sFile = sFile.Substring(sFile.LastIndexOf("."));
                    string datatime = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");  //Ϊ�˷�ֹ�������������Ϊ�ļ���  ������ʱ�������                  
                    sFile = datatime + sFile;  //�ϴ����ļ�������

                    //�������´����ļ���
                    string strDay = System.DateTime.Now.ToString("yyyyMM");
                    DirUrl = HttpContext.Current.Server.MapPath("~/upload/" + strDay);
                    if (!System.IO.Directory.Exists(DirUrl))//����ļ����Ƿ���ڣ��������򴴽�
                    {
                        System.IO.Directory.CreateDirectory(DirUrl);
                    }
                    //�����ļ���λ�÷���ҳ������Ҫ��ʾ��ͼƬ��ַ
                    string folderType = context.Request.Form["FolderType"];
                    if (folderType == "1")//������Ŀ¼
                    {
                        _Url = "../../upload/" + strDay + "/" + sFile;
                    }
                    else//һ��Ŀ¼
                    {
                        _Url = "../upload/" + strDay + "/" + sFile;
                    }
                    if (file.ContentLength > 0 || !string.IsNullOrEmpty(file.FileName))
                    {
                        //�����ļ�
                        if (context.Request.Form["WaterType"].ToUpper() != "1")//����ˮӡ
                        {
                            file.SaveAs(DirUrl + "/" + sFile);
                            //reUrl = _Url;
                            reUrl = "~/upload/" + strDay + "/" + sFile;
                        }
                        else//��ˮӡ
                        {
                            file.SaveAs(DirUrl + "/" + sFile);
                            if (folderType == "1")//������Ŀ¼
                            {
                                _Url = "../../upload/" + strDay + "/w" + sFile;
                            }
                            else//һ��Ŀ¼
                            {
                                _Url = "../upload/" + strDay + "/w" + sFile;
                            }
                            //��ͼƬ��ˮӡ
                            System.Drawing.Image img = System.Drawing.Image.FromFile(DirUrl + "/" + sFile);
                            string filepath_water = DirUrl + "/w" + sFile;
                            //�����ֵ�ˮӡ
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

    //�����ж��ļ����͵Ĺؼ�����
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
        if (fileclass == "255216" || fileclass == "7173" || fileclass == "6677" || fileclass == "13780")//˵��255216��jpg;7173��gif;6677��BMP,13780��PNG;7790��exe,8297��rar
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}