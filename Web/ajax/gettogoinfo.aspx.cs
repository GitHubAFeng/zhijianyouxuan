#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-7-27 21:41:40.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;


/// <summary>
/// 返回商家提示信息，列表页面弹出层
/// </summary>
public partial class Ajax_gettogoinfo : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string[] strid = Request["id"].Split('_');
        if (strid.Length < 2)
        {
            return;
        }
        int id = Convert.ToInt32(strid[0]);
        //PointsInfo model = dal.GetModel(id);
        PointsInfo model = null;
        IList<PointsInfo> list = dal.GetList(1, 1, " Unid=" + id, "Unid", 1);
        if (list.Count>0)
        {
            model = list[0];
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<div id=\"mylayer_" + model.Unid+"_"+strid[1] + "\" class=\"shoplayer\" >");
      //  sb.Append("<div class=\"layer_left\"></div>");
        sb.Append("<div class=\"layer_right\">");
        sb.Append("<div class=\"layer_left\"></div>");
        sb.Append("<div class=\"layer_right_01 clearfix\">");
        sb.Append("<div class=\"layer_title_01\"><a href=\"ShowTogo.aspx?id=" + model.Unid + "\"><img src=\"" + WebUtility.ShowPic(model.Picture) + "\" width=\"82\" height=\"82\"/></a></div>");
        sb.Append("<div class=\"layer_title_02\">");
        sb.Append("<h2 style=' font-size: 14px;'><a >" + WebUtility.Left(model.Name, 7) + "</a><span style='" + ((model.Status == 1 && model.isbisness == 1) ? "background: none repeat scroll 0 0 #2E8E98;" : "background: none repeat scroll 0 0 #999999;") + "color: #FFFFFF;margin: 0 5px;padding: 2px 5px;'>" + ((model.Status == 1 && model.isbisness == 1) ? "营业中 OPEN" : "休息中 CLOSE") + "</span></h2>");
        //int point = Convert.ToInt32(model.Point * 2) - 1;
        //if (model.Banner1 == "2")
        //{
        //    point = Convert.ToInt32(Convert.ToDecimal(model.Banner2) * 2) - 1;
        //}
        //if (point < 0)
        //{
        //    point = 0;
        //}
        sb.Append("<p>起送价:" + model.SendLimit +"元起送</p>");
        sb.Append("<p style='width:221px'>地址:" + model.Address + "</p></div></div>");
       // sb.Append("<p class=\"layer_star"+point+"\"></p></div></div>");
        sb.Append("<div class=\"layer_right_02\">");
        sb.Append("<p style='color:#925842;width:318px'>" + model.special + "</p>");
        //sb.Append("<p>平均送餐速度：<span class=\"red\">"+model.SentTime+"分钟</span></p>");
        sb.Append("<p style='width:318px'>简介：" + WebUtility.NoHTML(model.Introduce) + "</p>");
        //string b = "";
        //string[] ids = model.QQ.Split(',');
        //IList<ShopDataInfo> slist = SectionProxyData.GetSortList();
        //foreach (string item in ids)
        //{
        //    foreach (ShopDataInfo sd in slist)
        //    {
        //        if (item == sd.ID.ToString())
        //        {
        //            b += sd.classname + ",";
        //            break;
        //        }
        //    }
        //}
       // b = WebUtility.dellast(b);
        sb.Append("</div></div></div>");

        Response.Write(sb.ToString());
        Response.End();
    }
}
