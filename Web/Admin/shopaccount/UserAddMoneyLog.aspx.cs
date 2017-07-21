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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_Account_UserAddMoneyLog : AdminPageBase
{
    UserAddMoneyLog bll = new UserAddMoneyLog();
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
        CheckRights("D");
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    //充值状态进行判断
    protected string State(object s)
    {
        int states = Convert.ToInt32(s);
        switch (states)
        {
            case -1:
                return "充值失败";
                break;
            case 0:
                return "新增充值";
                break;
            case 1:
                return "充值成功";
                break;
            default:
                return "失败";
                break;
        }
    }

    protected string PayState(object ps)
    {
        int PayState = Convert.ToInt32(ps);
        switch (PayState)
        {
            case -1:
                return "充值失败";
                break;
            case 1:
                return "充值成功";
                break;
            default:
                return "失败";
                break;
        }
    } 

    protected string PayType(object pType)
    {
        int PayTypes = Convert.ToInt32(pType);
        switch (PayTypes)
        {
            case 1:
                return "支付宝";
                break;
            case 2:
                return "银行卡";
                break;
            case 3:
                return "抽奖获奖金额";
                break;
            case 4:
                return "其它";
                break;

            default:
                return "失败";
                break;
        }
        
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = bll.GetCount(SqlWhere);
        this.rtpUserAddMoneyLog.DataSource = bll.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "UserId", 1);
        this.rtpUserAddMoneyLog.DataBind();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
      //  SqlWhere = " 1=1 ";
        if (this.tb_UserId.Text != "")
        {
            SqlWhere = " UserId in (select DataID from ecustomer where binary Name LIKE '%" + WebUtility.InputText(tb_UserId.Text) + "%')";
        }
        BindData();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rtpUserAddMoneyLog_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            if (bll.DelUserAddMoneyLog(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            }
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;

        if (bll.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            BindData();
        }
    }
}
