/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 获取用户信息
 * Created by jijunjian at 2010-7-31 15:49:47.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
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

using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class AreaAdmin_Ajax_CheckEmail : AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        ECustomer dal = new ECustomer();
        ECustomerInfo model = null;
        string type = Request["type"];
        string value = Request["value"];
        switch (type)
        {
            case "nike":
                {
                    model = dal.GetModel(value);
                    if (model != null)
                    {
                        Response.Write(model.Point);
                    }
                    else
                    {
                        Response.Write("n");
                    }
                }
                break;
            case "uid":
                {
                    model = dal.GetModel(Convert.ToInt32(value));
                    if (model != null)
                    {
                        Response.Write(model.Point);
                    }
                    else
                    {
                        Response.Write("n");
                    }
                }
                break;
        }
        Response.End();
    }
}
