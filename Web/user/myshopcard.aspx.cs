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
using System.IO;
using System.Text;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Collections.Generic;

/// <summary>
/// 优惠券
/// </summary>
public partial class UserHome_myshopcard : PageBase
{
    ShopCard dal = new ShopCard();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            SqlWhere = "userid =" + UserHelp.GetUser().DataID;
            BindDate();
        }
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        if (Session["CheckCode"] == null || this.tbmycode.Text.ToLower() == "" || Session["CheckCode"].ToString().ToLower() != this.tbmycode.Text.ToLower())
        {
            WebUtility.RegScript(Page, UpdatePanel1, "alert('验证码错误，请重新输入!');hideload_super();");
            return;
        }
        string mykey = WebUtility.InputText(tbpwd1.Text.Trim()) + "-" + WebUtility.InputText(tbpwd2.Text.Trim()) + "-" + WebUtility.InputText(tbpwd3.Text.Trim());

        string sql = "  ckey = '" + mykey + "'";
        IList<ShopCardInfo> cardlist = dal.GetList(1, 1, sql, "cid", 1);
        if (cardlist.Count == 0)
        {
            WebUtility.RegScript(Page, UpdatePanel1, "alert('券号错误，请重新输入');hideload_super();");
        }
        else
        {
            if (cardlist[0].Inve2 == "0")
            {
                WebUtility.RegScript(Page, UpdatePanel1, "alert('这张券未激活，不能绑定');hideload_super();");
            }
            else
            {
                if (cardlist[0].State == 1)
                {
                    WebUtility.RegScript(Page, UpdatePanel1, "alert('这张券已经被绑定了');hideload_super();");
                }
                else
                {
                    ECustomerInfo user = UserHelp.GetUser();
                    sql = "update ShopCard set usergettime = '" + DateTime.Now.ToString() + "' , state =1 , userid = " + user.DataID + ",username = '" + user.Name + "' where CID in (" + cardlist[0].CID + ")";
                    if (WebUtility.excutesql(sql) > 0)
                    {
                        string js = "alert('操作成功');hideload_super();";

                        if (Request.QueryString["returnurl"] != null)
                        {
                            js += "gourl('" + Server.UrlDecode(Request["returnurl"]) + "');";
                        }

                        AlertScript.RegScript(this.Page, this.UpdatePanel1, js);
                        tbpwd1.Text = "";
                        tbpwd2.Text = "";
                        tbpwd3.Text = "";
                        tbmycode.Text = "";
                        BindDate();

                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('操作失败');hideload_super();");

                    }
                }
            }
        }

    }

    void BindDate()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);

        IList<ShopCardInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "isused asc, usergettime", 1);
        foreach (var item in list)
        {


        }
        this.rptPointCount.DataSource = list;
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
}
