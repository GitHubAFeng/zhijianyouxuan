#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-7-16 1:21:47.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
/// <summary>
/// 添加
/// </summary>
public partial class Ajax_addToCart : System.Web.UI.Page
{
    Hangjing.SQLServerDAL.ETogoShoppingCart bll = new Hangjing.SQLServerDAL.ETogoShoppingCart();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string jsonstr = Request["id"];
        ETogoShoppingCartInfo s = (ETogoShoppingCartInfo)JsonConvert.DeserializeObject(jsonstr, typeof(ETogoShoppingCartInfo));

        s.ReveInt1 = 0;
        s.ReveInt2 = 0;
        s.ReveVar1 = "";
        s.ReveVar2 = "";

        FoodinfoInfo food = new Foodinfo().GetModel(s.PId);
        s.owername = food.FullPrice;


        int id = new Hangjing.SQLServerDAL.ETogoShoppingCart().Add(s);
        Response.Write(id);
        Response.End();
    }
}
