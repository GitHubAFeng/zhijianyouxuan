#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品分类详细
// Created by tuhui at 2010-6-24 16:28:34.
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

public partial class Admin_GiftsClass_GiftsClassDetail :AdminPageBase
{
    GiftsClass dal = new GiftsClass();

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
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            if ( Request["id"] != null)
            {
                GiftsClassInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryInt("id" , 0)));
                this.tbName.Text = info.ClassName;
                tborder.Text = info.ClassOrder.ToString();
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        GiftsClassInfo info = new GiftsClassInfo();

        info.ParentId = "0";
        info.ClassName = WebUtility.InputText(tbName.Text);
        info.ClassOrder = Convert.ToInt32(tborder.Text);

        if (Request["id"] == null)
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
            info.ClassId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));

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
