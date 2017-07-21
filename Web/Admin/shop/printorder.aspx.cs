/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_order_printorder
 * Function : 
 * Created by jijunjian at 2010-12-28 19:58:46.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_order_printorder : System.Web.UI.Page
{
    Foodlist dalpo = new Foodlist();
    Custorder dalorder = new Custorder();
    protected void Page_Load(object sender, EventArgs e)
    {
        //传ids过来
        if (!Page.IsPostBack)
        {
            string dy = WebUtility.FixgetCookie("dy");
            if (dy != "")
            {
                rptorder.DataSource = dalorder.GetListt(dy);
                rptorder.DataBind();
            }
        }
    }

    protected IList<FoodlistInfo> getproduct(object oid)
    {
        return dalpo.GetAllByOrderID(oid.ToString());
    }
}
