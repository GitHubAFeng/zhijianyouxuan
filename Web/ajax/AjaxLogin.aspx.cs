/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Ajax_AjaxLogin.aspx.cs
 * Function : 异步登录，返回用户名和餐品个数
 * Created by jijunjian at 2010-7-26 16:53:41.
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
using System.Text;
using System.Net;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Collections.Generic;

public partial class Ajax_AjaxLogin : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        int unid = -1;
        string flag = Request["flag"];
        string email = Request["name"];
        string password = Request["password"];
        ECustomerInfo model = dal.GetModelByNameAPassword(email, WebUtility.GetMd5(password));
        if (model != null)
        {
            Response.Write(model.Point + "@" + model.Name+"@"+ WebUtility.ShowPic( model.Picture));
            UserHelp.SetLogin(model);
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                unid = UserHelp.GetUser().DataID;
                EAddress addressdal = new EAddress();
                string sql = "userid = " + user.DataID;
                IList<EAddressInfo> list = addressdal.GetList(10, 1, sql, "pri", 1);
                if (list.Count > 0)
                {                  
                    WebUtility.FixsetCookie("mylat", list[0].Lat, 30);
                    WebUtility.FixsetCookie("mylng", list[0].Lng, 30);
                }
            }
        }
        else
        {
            Response.Write(0);
        }
        Response.End();
    }
}
