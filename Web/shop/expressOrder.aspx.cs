/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 订单列表
 * Created by jijunjian at 2009-7-24 15:49:47.
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
using System.Collections.Generic;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.WebCommon;

public partial class shop_expressOrder : PageBase
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    public string ImagePath = WebUtility.GetMasterPicturePath();


    ExpressOrder dal = new ExpressOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!this.Page.IsPostBack)
        {
            hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
            hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

            PointsInfo user = UserHelp.GetUser_Togo();
            SqlWhere = " TogoId=" + user.Unid;

            BindDate();

            this.tbuserids.Value = user.Unid.ToString();
        }
    }


    void BindDate()
    {
        int count=dal.GetCount(SqlWhere);
        this.AspNetPager1.RecordCount = count;
        this.rptPointCount.DataSource = dal.GetListForDelive(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "ordertime", 1);
        this.rptPointCount.DataBind();

        if (count <= 0)//没有就隐藏掉地图
        {
            this.map.Style["display"] = "none";
        }

        NoRecord();
    }

    private void NoRecord()
    {
        if (rptPointCount.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDate();
    }



    //查询
    protected void btSearch_Click(object sender, EventArgs s)
    {
        SqlWhere = " 1=1 and togoId=" + UserHelp.GetUser_Togo().Unid;
        if (this.tbKeyword.Value.Trim() != "")
        {
            SqlWhere += "and ExpressOrder.orderid like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Value.Trim())) + "%'";
        }
        if (this.starttime.Value.Trim() != "")
        {
            SqlWhere += "and ordertime >= '" + this.starttime.Value + "' ";
        }
        if (this.enttime.Value.Trim() != "")
        {
            SqlWhere += "and ordertime <= '" + this.enttime.Value + " 23:59:59' ";
        }
        BindDate();
    }


}
