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

public partial class Admin_User_UserList : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
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
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1";
            BindData();
        }
    }

    protected void BindData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptCustomerList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere , "dataid" ,1);
        this.rptCustomerList.DataBind();
        AlertScript.RegScript(this.Page, UpdatePanel1, "init();$('#loading-mask').hide();");
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1 ";

        if (this.tb_Name.Text.Trim() != "")
        {
            SqlWhere += " AND Name LIKE '%" + WebUtility.InputText(this.tb_Name.Text.Trim()) + "%' ";
        }
        if (this.tb_Point.Text.Trim() != "")
        {
            SqlWhere += " AND Point " + this.ddl_Operator.SelectedValue + " " + WebUtility.InputText(this.tb_Point.Text.Trim()) + " ";
        }
        if (this.tb_Email.Text.Trim() != "")
        {
            SqlWhere += " AND Email LIKE '%" + WebUtility.InputText(this.tb_Email.Text.Trim()) + "%' ";
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND RegTime >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND RegTime <= '" + this.tb_End.Text + "' ";
        }
        if (this.tb_Phone.Text.Trim() != "")
        {
            SqlWhere += " AND tell LIKE '%" + WebUtility.InputText(this.tb_Phone.Text) + "%' ";
        }
        if (this.tb_UserName.Text.Trim() != "")
        {
            SqlWhere += " AND TrueName LIKE '%" + WebUtility.InputText(this.tb_UserName.Text.Trim()) + "%' ";
        }
        if (this.tbDataID.Text.Trim() != "")
        {
            SqlWhere += " AND DataID =" + tbDataID.Text;
        }
        if (ddlsex.SelectedValue != "-1")
        {
            SqlWhere += " AND sex = '" + ddlsex.SelectedValue + "'";
        }
        if (this.tb_userMoney.Text.Trim() != "")
        {
            SqlWhere += " AND userMoney " + this.ddl_usermoney.SelectedValue + " " + WebUtility.InputText(this.tb_userMoney.Text.Trim()) + " ";
        }
        if (ddlstate.SelectedValue != "-1")
        {
            SqlWhere += " AND state = '" + ddlstate.SelectedValue + "' ";
        }
        if (ddlPayPWDAnswer.SelectedValue != "-1")
        {
            SqlWhere += " AND PayPWDAnswer = '" + ddlPayPWDAnswer.SelectedValue + "' ";
        }
        



        BindData();
        WebUtility.FixsetCookie("SearchUserSqlWhere", SqlWhere, 1);
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
        string IdList = this.hdDels.Value;

        if (dal.Delete(IdList)>0)
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

    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        //删除后要删除相应的东西吗?
        //EAdminInfo a = UserHelp.GetAdmin();
        //if (a.Permission == "0")
        //{
        //    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:您没有权限删除!','250','150','true','2000','true','text');init();");
        //    return;
        //}

        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(4);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    if (dal.Delete(id)>0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','2000','true','text');init();");
                        BindData();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','2000','true','text');init();");

                    }
                }
                break;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }


    protected void set_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        string sql = "update  ECustomer set Password = '" + WebUtility.GetMd5("123456") + "' where DataID in (" + IdList + ")";
        if (SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:密码已经初始化为123456','250','150','true','3000','true','text');init();");
        }
        else
        {
            BindData();
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('操作失败!','error','true',5);init();");
        }
    }

    protected static string Getage(object o)
    {
        string rs = "";
        DateTime birthday = DateTime.Now;
        if (!string.IsNullOrEmpty(o.ToString()))
        {
            birthday = Convert.ToDateTime(o.ToString());
        }
        if (birthday.ToString() != "" && birthday.ToString() != null)
        {
            int year = DateTime.Now.Year - birthday.Year;
            return year.ToString();
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 设置黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void set_hmd(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(5);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        string sql = "update  ECustomer set state = '1' where DataID in (" + IdList + ")";
        if (SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, " $.jBox.tip('设置成功', 'success');;init();");
            BindData();
        }
        else
        {
            BindData();
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('操作失败!','error','true',5);init();");
        }
    }

    /// <summary>
    /// 取消黑名单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void clear_hmd(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(6);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        string sql = "update  ECustomer set state = '0' where DataID in (" + IdList + ")";
        if (SQLHelper.ExecuteNonQuery(CommandType.Text, sql, null) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, " $.jBox.tip('设置成功', 'success');;init();");
            BindData();
          
        }
        else
        {
            //弹出框的提示效果AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('操作失败!','error','true',5);init();");
        }
    }
}

