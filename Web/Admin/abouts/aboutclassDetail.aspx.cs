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
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class Admin_abouts_aboutclassDetail :AdminPageBase
{
    aboutClass dal = new aboutClass();

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
            if (HjNetHelper.GetQueryString("id") != "")
            {
                aboutClassInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                this.tbName.Text = info.Name;
                tborder.Text = info.FullId + "";

                pageType.Text = "编辑分类信息";
            }
            else
            {
                pageType.Text = "新增分类信息";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        aboutClassInfo info = new aboutClassInfo();
        info.ParentId = 0;
        info.Name = WebUtility.InputText(tbName.Text);
        info.FullId = Convert.ToInt32(tborder.Text);

        if (pageType.Text == "新增分类信息")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增成功','success','true',5)");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增失败','error','true',5)");
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
            info.Id = Convert.ToInt32(HjNetHelper.GetQueryString("id"));

            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑成功','success','true',5)");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑失败','error','true',5)");
            }
        }
    }
}

