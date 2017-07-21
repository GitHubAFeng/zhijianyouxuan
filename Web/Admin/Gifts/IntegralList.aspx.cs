#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :积分兑换记录列表
// Created by tuhui at 2010-6-24 16:28:44.
// E-Mail: tuhui@ihangjing.com
#endregion
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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.DBUtility;

public partial class Admin_Integral_IntegralList : AdminPageBase
{
    Integral dal = new Integral();

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
        CheckRights("E");
        if (!Page.IsPostBack)
        {
            WebUtility.FixsetCookie("SearchGiftsSqlWhere", SqlWhere, 1);
            BindData();
        }
    }

    private void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rtpIntegral.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "IntegralId", 1);
        this.rtpIntegral.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1 = 1 ";
        string GiftName = WebUtility.InputText(tb_GiftName.Text);
        if (tbDate1.Text != "")
        {
            SqlWhere += " and  Cdate >= " + "'" + tbDate1.Text + "'";
        }
        if (tbDate2.Text != "")
        {
            SqlWhere += " and  Cdate <= " + "'" + tbDate2.Text + "'";
        }
        if (this.tb_UserName.Text.Trim() != "")
        {
            SqlWhere += " AND  UserName LIKE '%" + WebUtility.InputText(this.tb_UserName.Text.Trim()) + "%' ";
        }
        if (tb_GiftName.Text.Trim() != "")
        {
            SqlWhere += " AND  GiftName LIKE '%" + WebUtility.InputText(tb_GiftName.Text.Trim()) + "%'";
        }
        if (this.tb_UserID.Text.Trim() != "")
        {
            SqlWhere += " And CustID =" + tb_UserID.Text;
        }
        if (this.tb_GiftID.Text.Trim() != "")
        {
            SqlWhere += " And GiftsId =" + tb_GiftID.Text;
        }
        if (ddl_State.SelectedValue != "-1")
        {
            SqlWhere += " and state =" + ddl_State.SelectedValue;
        }
        BindData();
        WebUtility.FixsetCookie("SearchGiftsSqlWhere", SqlWhere, 1);
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hidDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rtpIntegral_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string[] ids = e.CommandArgument.ToString().Split('_');

        switch (e.CommandName)
        {
            case "Del":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(4);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    if (dal.DelIntegral(Convert.ToInt32(ids[0])) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                        BindData();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败!','error','true',5);init();");
                    }
                }
                break;
            case "pass":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(5);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    IntegralInfo m = dal.GetModel(Convert.ToInt32(ids[0]));
                    GiftsInfo gift = new Gifts().GetModel(m.GiftsId);
                    ECustomerInfo user = new ECustomer().GetModel(Convert.ToInt32(ids[1]));
                    if (m.State == "1")
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('已经审核过了，不用再审核!','error','true',5);init();");
                        return;
                    }
                    //用户加分
                    if (user == null)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('此用户已经不存在了!','error','true',5);init();");
                        return;
                    }
                    if (gift == null)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('此礼品已经不存在了!','error','true',5);init();");
                        return;
                    }
                    if (gift.Stocks < 1)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('此礼品库存不足，不能审核通过!','error','true',5);init();");
                        return;
                    }
                    if (user.Point < Convert.ToInt32(gift.NeedIntegral))
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('此用户积分不足!','error','true',5);init();");
                        return;
                    }
                    string sql = "update Integral set state = '1' where IntegralId in (" + ids[0] + ");";
                    sql += "update Gifts set stocks = stocks-1 where GiftsId in (" + gift.GiftsId + ");";
                    sql += "update ECustomer set point = point -" + gift.NeedIntegral + " where dataid = " + ids[1] + ";";
                    sql += "insert into EPointRecord(userid , point , event , time)values(" + ids[1] + " , " +"-"+gift.NeedIntegral + " ,'兑换礼品，用去" + gift.NeedIntegral + "个积分' , '" + DateTime.Now + "')";
                    WebUtility.excutesql(sql);
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('操作成功','success','true',5);init();");
                    BindData();

                }
                break;

        }
    }

    protected string GetState(object o)
    {
        int state = Convert.ToInt32(o);
        string value = "";
        switch (state)
        {
            case 0: value = "<span style=\"color:Gray;\">未审核</span>"; break;
            case 1: value = "<span style=\"color:Green;\">审核通过</span>"; break;
            case 2: value = "<span style=\"color:Red;\">审核未通过</span>"; break;
        }

        return value;
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void reset_pwd(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(6);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hidDels.Value;
        string sql = "update Integral set state = '2' where IntegralId in (" + IdList + ")";
        if (SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','1000','true','text');init();");
        }
    }
}
