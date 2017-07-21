#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-4-15 18:01:17.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class Admin_Shop_ShopReviewdetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
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
            lbtogoname.Text = model.TogoName;
            this.lbtime.Text = model.PostTime.ToLongDateString();
            this.LitContent.Text = model.Comment;
            tbrcontent.Text = model.Rcontent;
            if (model.Rtime < Convert.ToDateTime("1902-01-01"))
            {
                model.Rtime = Convert.ToDateTime("1902-01-01");//浏览过
                dal.Update(model);
            }
           // WebUtility.SelectValue(ddlpint, model.Point.ToString());
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        ETogoOpinionInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
        model.Rtype = 0;
        model.Rtime = DateTime.Now;
        model.Rcontent = WebUtility.InputText(tbrcontent.Text);
        model.Point =0;
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
