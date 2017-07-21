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

public partial class UserHome_MyexpressOrder : PageBase
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

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            string cityid = WebUtility.get_userCityid();
            if (cityid == "0")
            {
                string url = "citys.aspx";
                Response.Redirect(url);
            }
            hfcityname.Value = WebUtility.get_userCityName();
            hfcityid.Value = cityid;
            hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
            hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

            ECustomerInfo user = UserHelp.GetUser();
            SqlWhere = "UserId=" + user.DataID;

            BindDate();
            this.tbuserids.Value = UserHelp.GetUser().DataID.ToString();
        }
    }

    ExpressOrder dal = new ExpressOrder();

    void BindDate()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptPointCount.DataSource = dal.GetListForDelive(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "ordertime", 1);
        this.rptPointCount.DataBind();
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

    protected void btSearch_Click(object sender, EventArgs s)
    {
        SqlWhere = "1=1 and UserId=" + UserHelp.GetUser().DataID;
        if (this.tbKeyword.Value.Trim() != "")
        {
            SqlWhere += "and orderid like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Value.Trim())) + "%'";
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
