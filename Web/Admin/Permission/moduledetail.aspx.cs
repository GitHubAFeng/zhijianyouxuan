using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_Permission_moduledetail : System.Web.UI.Page
{
    sys_Module dal = new sys_Module();
    sys_ModulePermition dalstyle = new sys_ModulePermition();
    private sys_ModuleInfo Info
    {
        get
        {
            sys_ModuleInfo info = new sys_ModuleInfo();
            info.ModuleID = HjNetHelper.GetQueryInt("id", 0);
            if (info.ModuleID > 0)
            {
                info = dal.GetModel(info.ModuleID);
            }
            else
            {
                info.M_IsSystem = 1;
                info.M_Close = 0;
            }
            info.M_ApplicationID = 0;
            info.M_ParentID = Convert.ToInt32(WebUtility.InputText(tbM_ParentID.SelectedValue));
            info.M_CName = WebUtility.InputText(tbM_CName.Text);
            info.M_Directory = WebUtility.InputText(tbM_Directory.Text);
            info.M_OrderLevel = Convert.ToInt32(WebUtility.InputText(tbM_OrderLevel.Text));

            return info;
        }
        set
        {
            WebUtility.SelectValue(tbM_ParentID, value.M_ParentID.ToString());
            tbM_CName.Text = value.M_CName;
            tbM_Directory.Text = value.M_Directory;
            tbM_OrderLevel.Text = value.M_OrderLevel.ToString();
        }
    }

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
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            string sql = "M_ParentID = 0";
            WebUtility.BindList("ModuleID", "M_CName", dal.GetList(100, 1, sql, "M_OrderLevel", 1) , tbM_ParentID);

            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加模块";
                if (Request["pid"] != null)//编辑
                {
                    WebUtility.SelectValue(tbM_ParentID, Request["pid"]);
                }
                else
                {

                }
                divitems.Visible = false;
            }
            else
            {
                pageType.Text = "编辑模块";
                sys_ModuleInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
                Info = model;
                if (HjNetHelper.GetQueryInt("pid" ,0) == 0)
                {
                    divitems.Visible = false;
                }
                BindData();
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        sys_ModuleInfo model = Info;
        if (HjNetHelper.GetQueryString("id") == "")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            //計算pagecode
            string _pagecode = "";
            if (tbM_ParentID.SelectedValue == "0")//一級
            {
                string sql = "M_ParentID=0";
                _pagecode = dal.getMaxPagecode(sql, 0, "");
            }
            else
            {
                string sql = "1=1 and M_ParentID =" + tbM_ParentID.SelectedValue;
                sys_ModuleInfo pmodel = dal.GetModel(Convert.ToInt32(tbM_ParentID.SelectedValue));
                if (pmodel != null)
                {
                    _pagecode = dal.getMaxPagecode(sql, 1, pmodel.M_PageCode);
                }
                else
                {
                    //出錯
                    AlertScript.RegScript(Page, UpdatePanel1, "alert('系統错误');");
                }
            }
            model.M_PageCode = _pagecode;

            int mid = dal.Add(model) ;

            if (mid > 0)
            {
                if (HjNetHelper.GetQueryInt("pid", 0) == 0)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增成功','success','true',5);");
                }
                else
                {
                    string url = "moduledetail.aspx?id=" + mid + "&pid=" + HjNetHelper.GetQueryInt("pid", 0);
                    //string js = "alert('添加成功，点击确定添加操作项目');gourl('" + url + "');";//本项目不用添加操作项目
                    string js = "alert('添加成功');gourl('" + url + "');";
                    AlertScript.RegScript(this.Page, UpdatePanel1, js);
                }
               
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增失败','error','true',5);");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Update(model) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑成功','success','true',5);");
                //更新缓存(所有角色的)
                Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/Permissions");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }

        }

    }

    /******************************** 操作项目管理 ****************************/

    /// <summary>
    /// 主菜
    /// </summary>
    protected void BindData()
    {
        SqlWhere = "ModuleID =" + HjNetHelper.GetQueryInt("id", 0);
        IList<sys_ModulePermitionInfo> flist = dalstyle.GetList(100, 1, SqlWhere, "ReveInt", 1);
        rptsubItem.DataSource = flist;
        rptsubItem.DataBind();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dalstyle.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除成功！');");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "jtip('删除失败，请重试！');");
            }
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        rptsubItem.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        rptsubItem.EditIndex = -1;
        BindData();
    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        sys_ModulePermitionInfo mm = new sys_ModulePermitionInfo();
        string classname = ((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_name")).Text.Trim();
        int pri = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_Inve1")).Text.Trim());
        int id = Convert.ToInt32(((Label)rptsubItem.Rows[e.RowIndex].FindControl("lb_id")).Text.Trim());
        int pvalue = Convert.ToInt32(((TextBox)rptsubItem.Rows[e.RowIndex].FindControl("Lbl_Price")).Text.Trim());

        mm.mid = id;
        mm.ModuleID = HjNetHelper.GetQueryInt("id", 0);
        mm.pername = classname;
        mm.pvalue = pvalue;
        mm.ReveInt =pri;
        mm.ReveVar = "";
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dalstyle.Update(mm) > 0)
        {
            BindData();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "jtip('操作失败，请重试！');");
        }
        rptsubItem.EditIndex = -1;
        BindData();
    }

    protected void master_Add(object sender, EventArgs e)
    {
        sys_ModulePermitionInfo info = new sys_ModulePermitionInfo();
        info.mid = 0;
        info.ModuleID = HjNetHelper.GetQueryInt("id", 0);
        info.pername = WebUtility.InputText(tbpername.Text);
        info.pvalue =  Convert.ToInt32(tbpvalue.Text);
        info.ReveInt = Convert.ToInt32(tbReveInt.Text);
        info.ReveVar = "";
        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dalstyle.Add(info) > 0)
        {
            BindData();
            tbpername.Text = "";
            tbpvalue.Text = "0";
            tbReveInt.Text = "1";
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('服务器繁忙,请稍后再试');");
        }
    }
}
