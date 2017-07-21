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

public partial class shop_CommentDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            if (HjNetHelper.GetQueryString("id") != "")
            {
                GetData();
            }
        }
    }

  ETogoOpinion dal = new ETogoOpinion();
    ECustomer dal_customer = new ECustomer();

    void GetData()
    {
        ETogoOpinionInfo model = dal.GetModel(Convert.ToInt32(Request.QueryString["id"]));
        if (model != null)
        {
            this.lbUsername.Text = model.UserName;         
            //this.lbPoint.Text = model.Point.ToString();
            this.lbtime.Text = model.PostTime.ToLongDateString();
            this.LitContent.Text = model.Comment;
            tbrcontent.Text = model.Rcontent;
            model.Rtime = Convert.ToDateTime("1902-01-01");//浏览过
            dal.Update(model);
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        Hangjing.Model.ETogoOpinionInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id" , 0));
        model.Rtype = 0;
        model.Rtime = DateTime.Now;
        model.Rcontent = WebUtility.InputText(tbrcontent.Text);
        if (dal.Update(model) > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作成功','340','150','true','2000','true','text');");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作失败','320','150','true','2000','true','text');");
        }
    }
}
