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
using Hangjing.Common;
using Hangjing.Model;
using System.Collections.Generic;

public partial class Admin_NewsDetail :AdminPageBase
{
    News Newsbll = new News();
    protected void Page_Load(object sender, EventArgs e)
    {

        ValidatorSet validator = new ValidatorSet("admin");
        validator.SetValidator();
        this.pageType.Text = "添加公告";
        if (!Page.IsPostBack)
        {
            this.pageType.Text = "编辑公告";
            GetData();
        }
    }

    public void GetData()
    {
        if (HjNetHelper.GetQueryString("id") != "")
        {
            NewsInfo info = new NewsInfo();
            info = Newsbll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
            tbTitle.Text = info.Title;
            FCKContent.Value = info.EContent;
            tbreve1.Text = info.reve1 + "";
            tbistop.Text = info.SortNum + "";
        }
       
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        NewsInfo info = new NewsInfo();
        info.DataID = HjNetHelper.GetQueryInt("id", 0);
        info.Title = WebUtility.InputText(tbTitle.Text);
        info.Posttime = DateTime.Now;
        info.SortNum = Convert.ToInt32(tbistop.Text);
        info.EContent = FCKContent.Value;
        info.reve1 = Convert.ToInt32(tbreve1.Text);
        info.Reve2 = "";
        if (Request["id"] == null)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (Newsbll.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:新增成功!','250','150','true','2000','true','text');");
                ClearControl(Page);
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:新增失败!','250','150','true','2000','true','text');");
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
            if (Newsbll.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','2000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','2000','true','text');");
            }
        }
        SectionProxyData.ClearIndexNewsList();
    }
    //清空控件
    public void ClearControl(System.Web.UI.Control page)
    {
        int count = page.Controls.Count;
        for (int i = 0; i < count; i++)
        {
            foreach (System.Web.UI.Control con in page.Controls[i].Controls)
            {
                if (con.HasControls())
                {
                    ClearControl(con);
                }
                else
                {
                    if (con is TextBox)
                    {
                        (con as TextBox).Text = string.Empty;
                    }
                }
            }

        }
    }
}
