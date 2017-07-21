/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Ajax_gettogoinof
 * Function : 
 * Created by jijunjian at 2010-11-4 16:06:10.
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

public partial class Ajax_gettogoinof :AdminPageBase
{
    Points daltogo = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // type = 1 表示获取商家简介
            int id = Convert.ToInt32(Request["togoid"]);
            string rs = "<span>商家信息</span><span style=\"float: right;\"><a href=\"#\" onclick=\"Hide(this);return false;\">关闭</a></span><hr>";
            Response.Clear();

            PointsInfo model = daltogo.GetModel(id);
            rs += "<span>联系人</span><span style=\"float: right;\">" + model.Name + "</span><br>";
            rs += "<span>联系电话</span><span style=\"float: right;\">" + model.Comm + "</span>";
            //rs += "<span>商家地址</span><span style=\"float: right;\">" + model.Address + "</span>";
            Response.Write(rs);
        }
        catch
        {
            Response.Write("-1");
        }
        Response.End();
    }
}
