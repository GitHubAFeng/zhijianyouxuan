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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Collections.Generic;

/// <summary>
/// 根据打印机num判断此打印机是否在使用
/// </summary>
public partial class qy_54tss_AreaAdmin_ajax_GetPrinterInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = Request["num"];
        if (content != null)
        {
            PrinterSecret ps = new PrinterSecret();
            PrinterSecretInfo info = ps.GetModel(content);
            if (info != null)
            {
                IList<TogoPrinterInfo> tps = new TogoPrinter().GetList(1, 1, " PrinterSn = '" + content + "'", "dataid", 1);
                if (tps.Count > 0)
                {
                    Response.Write("0");//此打印机已经在使用中不能作为商家的打印机
                }
                else
                {
                    Response.Write(info.PrinterSn);
                }
            }
            else
            {
                Response.Write("-1");//不存在此打印机
            }

            Response.End();

        }
    }
}
