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

public partial class Admin_WebMessageDetail :AdminPageBase
{
    WebMessageInfo info = new WebMessageInfo();
    WebMessage bll = new WebMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                GetwebMessage();
            }
            else
            {
                tbAddDate.Text = DateTime.Now.ToShortDateString();
            }
        }
    }

    protected void GetwebMessage()
    {
        if (HjNetHelper.GetQueryString("id") == " ")
        {
            this.pageType.Text = "添加站内信内容";
        }
        else
        {
            this.pageType.Text = "编辑站内信内容";
            int id = HjNetHelper.GetQueryInt("id",0);
            info   = bll.GetModel(id);
            this.hidDataId.Value = HjNetHelper.GetQueryInt("id", 0).ToString();
            this.tbTitle.Text = info.Title;
            this.tbAddDate.Text = info.AddDate.ToShortDateString();
            this.fcContent.Value = info.Message;

         }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        WebMessageInfo info = new WebMessageInfo();
        string title = WebUtility.InputText(this.tbTitle.Text.Trim());
        string datetime = this.tbAddDate.Text;
        string message = this.fcContent.Value;

        info.Title = title;
        info.AddDate = Convert.ToDateTime(datetime);
        info.Message = message;


        if (pageType.Text == "添加站内信内容")
        {
            if (bll.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);init();");
            }
        }
        else
        {
            info.DataId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            if (bll.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);init();");
            }
        }
    }
}
