#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-7-15 16:25:35.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 获取商品配料
/// </summary>
public partial class Ajax_getattr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fid = Request["id"].ToString();
        Response.Clear();

        IList<FoodAttributesInfo> list = new FoodAttributes().GetList(100, 1, "foodtid = "+fid, "dataid", 1);
        Response.Write(WebUtility.ObjectToJson("myattr" , list));

        Response.End();
    }
}
