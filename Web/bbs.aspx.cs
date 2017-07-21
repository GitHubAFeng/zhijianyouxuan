
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 留言版
/// </summary>
public partial class bbs : System.Web.UI.Page
{
    EUserWord dal = new EUserWord();

    protected string SqlWhere
    {
        get
        {
            object o = ViewState["sqlwhere"];
            return (o == null) ? "" : ViewState["sqlwhere"].ToString();
        }
        set
        {
            ViewState["sqlwhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "state = 1";
            Binddata();
        }

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        Binddata();
    }

    protected void Binddata()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptMessagelsit.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "time", 1);
        this.rptMessagelsit.DataBind();
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        ECustomerInfo model = UserHelp.GetUser();
        if (model == null)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('登录后，才能留言。');");
            return;
        }
        string reviewcookie = WebUtility.FixgetCookie("review_rat");
        if (reviewcookie == null || reviewcookie == "")
        {
            //点评是否要审核。0表示不审核，1表示要审核
            int isreviewcheck = Convert.ToInt32(SectionProxyData.GetSetValue(42));
            EUserWordInfo word = new EUserWordInfo();

            string msg = WebUtility.InputText(textarea.Value);

            word.Word = msg;
            word.UserName = model.Name;
            word.UserID = model.DataID;
            if (isreviewcheck == 0)
            {
                word.State = 1;
            }
            else
            {
                word.State = 0;
            }
            word.Time = DateTime.Now;
            word.Rremark = "";
            word.RTime = DateTime.Now;
            word.adminID = "";
            word.MyType = 0;

            if (dal.Add(word) > 0)
            {
                if (isreviewcheck == 0)
                {
                    AlertScript.RegScript(Page, UpdatePanel1, "alert('留言成功。');");
                }
                else
                {
                    AlertScript.RegScript(Page, UpdatePanel1, "alert('留言成功，审核才会看到。');");
                }

                textarea.Value = "";
            }
            else
            {
                AlertScript.RegScript(Page, UpdatePanel1, "alert('服务器繁忙，请稍后再试。');");
            }
            Binddata();
            WebUtility.FixsetCookie_Minutes("review_rat", "1", 60);
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('亲，您留言得太频繁了，休息会吧^-^!');");
        }
    }

    protected string getrReview(object remsg, object time, object adminid)
    {
        string rs = "";
        string s = adminid.ToString().Trim();
        if (s != "")
        {
            rs = "<div class=\"message_reply\"><span class=\"blue margin_r10\">网站回复</span>&nbsp;&nbsp;<span class=\"gray f11\">";
            rs += time + "</span>";
            rs += "<p>" + remsg + "</p></div>";
        }
        return rs;
    }
}
