/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_manage_zhifubao
 * Function : 
 * Created by jijunjian at 2010-11-16 21:37:16.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class AreaAdmin_manage_zhifubao : System.Web.UI.Page
{
    acount dal = new acount();
    acountInfo info
    {
        set
        {
            tbAli_Seller_Mail.Text = value.Ali_Seller_Mail;
            tbAli_Key.Text = value.Ali_Key;
            tbAli_Partner.Text = value.Ali_Partner;
            tbReve3.Text = value.Reve3;
            WebUtility.SelectValue(ddlReve1, value.Reve1);
        }
        get
        {
            acountInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id" ,0));
            model.Ali_Seller_Mail = WebUtility.InputText(tbAli_Seller_Mail.Text);
            model.Ali_Key = WebUtility.InputText(tbAli_Key.Text);
            model.Ali_Partner = WebUtility.InputText(tbAli_Partner.Text);
            model.Sxy_Partner = "";
            model.Sxy_Key = "";
            model.ALI_NOTIFY_URL = "";
            model.ALI_RETURN_URL = "";
            model.Reve1 = ddlReve1.SelectedValue;
            model.Reve3 = tbReve3.Text;

            return model;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getinfo();
        }
    }

    protected void getinfo()
    {
        acountInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 1));
        info = model ;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        acountInfo model = new acountInfo();
        model = info;
        if (dal.Update(model) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','1000','true','text');");
            SectionProxyData.ClearOnlinePayTypeList();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','1000','true','text');");
        }
    }
}
