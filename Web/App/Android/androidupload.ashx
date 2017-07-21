<%@ WebHandler Language="C#" Class="androidupload" %>

using System;
using System.Web;

/// <summary>
/// 上传图片，注意参数
/// type=1 表示上传商家图片 id 表示商家编号
/// type=2 表示上传菜品图片 id 表示菜品编号
/// type=3 表示上传桌子图片 id 表示桌子编号
/// type=4 表示上传骑士身份证图片  id表示骑士编号
/// ext=jpg 表示后缀名

public class androidupload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //-1表示失败，1表示成功
        string state = "-1";
        string picurl = "";

        try
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            string strDay = System.DateTime.Now.ToString("yyyyMM");
            string uploadPath = HttpContext.Current.Server.MapPath("~/upload/" + strDay);
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            System.IO.Stream stream = context.Request.InputStream;//这是你获得的流
            //获取参数，注意：此处参数是放在http头信息里的。
            string type = context.Request.Headers["type"];
            string id = context.Request.Headers["id"];
            string ext = context.Request.Headers["ext"];

            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + ext;

            string fullpicurl = "~/upload/" + strDay + "/" + filename;

            //将图片的路径保存到数据库
            switch (type)
            {
                case "1"://店名图片
                    //updateshopnamepic(id, fullpicurl);
                    break;
                case "2"://店的菜单和价格图片
                    //updatefoodandpricepic(id, fullpicurl);
                    break;
                case "3"://上传评论图片
                    {
                        string sql = "update ETogoOpinion set  userpic='" + fullpicurl + "' where dataid=" + id;
                        WebUtility.excutesql(sql);
                    }
                    break;
                    
                case "4"://上传骑士身份证图片
                    {
                        string sql = "UPDATE dbo.Deliver SET pic1='" + fullpicurl + "' WHERE DataId=" + id;
                        WebUtility.excutesql(sql);
                    }
                    break;
                case "5"://上传用户头像
                    {
                        string sql = "UPDATE dbo.ECustomer SET Picture='" + fullpicurl + "' WHERE DataId=" + id;
                        WebUtility.excutesql(sql);
                    }
                    break;

                default:
                    break;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);     //将流的内容读到缓冲区 

            System.IO.FileStream fs = new System.IO.FileStream(uploadPath + "/" + filename, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();

            state = "1";
            picurl = WebUtility.GetConfigsite() + "/upload/" + strDay + "/" + filename;
            //picurl = "~/upload/" + strDay + "/" + filename;
        }
        catch (Exception ee)
        {
            Hangjing.Common.HJlog.toLog("上传图片失败："+ee);
        }
        finally
        {
            context.Response.Clear();
            context.Response.Write("{\"pic\":\"" + picurl + "\",\"state\":\"" + state + "\"}");
            context.Response.End();
        }


    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}