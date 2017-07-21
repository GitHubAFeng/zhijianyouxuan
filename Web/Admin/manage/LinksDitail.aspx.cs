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

public partial class Admin_LinksDitail :AdminPageBase
{
    Links linkBLL = new Links();
    LinksInfo info = new LinksInfo();

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
            if (HjNetHelper.GetQueryString("id") == "")
            {
                this.pageType.Text = "添加友情链接";
            }
            else
            {
                this.pageType.Text = "编辑友情链接";
                SelLinkInfo();
            }
        }
    }

    protected void SelLinkInfo()
    {
        int id = HjNetHelper.GetQueryInt("id",0);
        LinksInfo info = linkBLL.GetModel(id);
        this.ddltype.SelectedValue = 1+"";
        this.tbURL.Text = info.Url;
        ImgUrl1.Value = info.Picture;
        this.tbtitle.Text = info.title;
        ImgUrl.Src = WebUtility.ShowPic(info.Picture);
        this.tbIntroduce.Text = info.Introduce + "";
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        if (this.pageType.Text == "编辑友情链接")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            int id = HjNetHelper.GetQueryInt("id", 0);
            LinksInfo info = linkBLL.GetModel(id);
            info.title = WebUtility.InputText(this.tbtitle.Text);
            info.Type = Convert.ToInt32(ddltype.SelectedValue);
            info.Url = this.tbURL.Text;
            info.Introduce = Convert.ToInt32(this.tbIntroduce.Text);
            info.Picture = ImgUrl1.Value;
            linkBLL.Update(info);
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            LinksInfo info = new LinksInfo();
            info.title = WebUtility.InputText(this.tbtitle.Text);
            info.Type = Convert.ToInt32(ddltype.SelectedValue);
            info.Url = this.tbURL.Text;
            info.Introduce = Convert.ToInt32(this.tbIntroduce.Text);
            info.Picture = ImgUrl1.Value;
            linkBLL.Add(info);
        }
        SectionProxyData.ClearLinkList();
        Response.Redirect("LinksLIst.aspx");
    }
}