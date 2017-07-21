#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/12/23 15:58:09.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZXing.QrCode;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web;

namespace Hangjing.Common
{
    /// <summary>
    /// 根据 ZXing 再进行相关业务封装
    /// </summary>
    public class HJQRCode
    {
        public string codemsg = "";

        /// <summary>
        /// 生成二维码图片.内容为json {"type":"user","data":"1"} 类型
        /// </summary>
        /// <param name="type">类型,目前用catering,market,也分别为文件夹<</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string BuildQRCode(string type, string data, string url)
        {
            return BuildQRCode(type, data, url, null);
        }

        /// <summary>
        /// 生成二维码，保存在虚拟目录的
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <param name="context"></param>
        /// <param name="Virtualpay"></param>
        /// <returns></returns>
        public static string BuildQRCode(string type, string data, string url, HttpContext context)
        {
            //string json = "{\"type\":\"" + type + "\",\"data\":\"" + data + "\"}";
            string json = "";
            json = url;

            type = DateTime.Now.ToString("yyyyMM");


            if (context == null)
            {
                context = HttpContext.Current;
            }

            BarcodeWriter writer = null;
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 160,
                Height = 160
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            string savepath = "";
            string Directory = "~/upload/" + type + "/";
            string filepath = Directory + data + ".jpg";

            if (!System.IO.Directory.Exists(context.Server.MapPath(Directory)))
            {
                System.IO.Directory.CreateDirectory(context.Server.MapPath(Directory));
            }
            savepath = context.Server.MapPath(filepath);


            Bitmap bitmap = writer.Write(json);

            try
            {
                bitmap.Save(savepath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                filepath = "";
                HJlog.toLog("生成二维码出错:" + ex);
            }

            return filepath;
        }


        /// <summary>
        /// 识别二维码
        /// </summary>
        /// <returns></returns>
        public void ReadCode(HttpContext context, string pic)
        {
            BarcodeReader reader = null;
            reader = new BarcodeReader();
            reader.ResultFound += new Action<Result>(reader_ResultFound);

            Bitmap bitmap = null;
            try
            {
                string fileName = context.Server.MapPath(pic);
                bitmap = (Bitmap)Bitmap.FromFile(fileName);
            }
            catch (System.Exception ex)
            {
                codemsg = "图片读取错误";
            }
            Result result = reader.Decode(bitmap);
            if (result == null)
            {
                codemsg = "图片读取错误";
            }
        }

        void reader_ResultFound(Result obj)
        {
            codemsg = obj.Text;
        }
    }



    /// <summary>
    /// 二维码可选择类型
    /// </summary>
    public enum QRCodeType
    {
        desk,
        deliverorder
    }
}
