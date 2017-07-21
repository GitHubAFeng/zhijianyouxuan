/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : OrderDetail.aspx.cs
 * Function : 点餐提交订单
 * Created by jijunjian at 2010-7-30 17:05:41.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class myorder : System.Web.UI.Page
{
    Custorder dal = new Custorder();
    Foodlist dalof = new Foodlist();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string userid = WebUtility.FixgetCookie("uc");
            if (!string.IsNullOrEmpty(userid))
            {
                rptorder.DataSource = dal.GetList(10, 1, "tempcode = '" + userid + "'", "unid", 1);
                rptorder.DataBind();
            }

            if (rptorder.Items.Count > 0)
            {
                divno.Style["display"] = "none";
            }
        }
    }

    protected IList<FoodlistInfo> Getfood(object oid)
    {
        return dalof.GetAllByOrderID(oid.ToString());
    }


}
