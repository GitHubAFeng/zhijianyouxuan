// foodcommplit.cs:餐品名称自动完成.
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// jijunjian@ihangjing.com
// 2010-04-28

using System;
using System.Collections;
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
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Ajax_FoodCommplit :AdminPageBase
{
    ArrayList list = new ArrayList();
    AllFoods dal_food = new AllFoods();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = Request["names"];
        if (content != null)
        {
            FillList( Server.UrlDecode( Request["names"]));
        }
    }

    protected void FillList(string  s)
    {
        string sql = "FoodName like '%"+s+"%'";
        int count = dal_food.GetCount(sql);

        IList<AllFoodsInfo> temp = dal_food.GetList(count , 1 ,sql , "dataid" , 1);

        for (int x = 0; x < temp.Count; x++)
        {
            list.Add(temp[x].Foodname);
        }

        NameService nams = NameService.getInstance(list);
        ArrayList maches = nams.findNames(s, 0);
        maches.Sort();
        StringBuilder results = new StringBuilder("");

        if (maches.Count != 0)
        {
            int ncount = 0;
            foreach (string name in maches)
            {
                ncount++;
                results.Append("<a onmouseover='mouseOverAddr(this)' onmouseout='mouseOutDiv(this)' href='javascript:void(0)' onclick=\"selectItem('" + name + "')\">" + name + "</a>");
                results.Append("<input name='" + name + "' type='hidden'>");
                if (ncount >= 7)
                {
                    break;
                }
            }
            results.Append("<a class=\"off\" href='javascript:void(0)' onclick=\"SetDivDisplayType('address_drop','none')\" >关闭</a>");
        }
        else
        {
            //results.Append("<name>");
            //results.Append("没有此记录");
            //results.Append("</name>");
        }

        Response.AddHeader("Cache-Control", "no-cache");

        Response.Write(results.ToString());
        Response.End();
    }
}
