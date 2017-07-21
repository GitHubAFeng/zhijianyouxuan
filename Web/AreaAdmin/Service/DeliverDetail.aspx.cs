#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品信息详细
// Created by tuhui at 2010-6-24 16:28:14.
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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.Cache;

public partial class qy_54tss_AreaAdmin_DeliverDetail : System.Web.UI.Page
{
    Deliver dal = new Deliver();

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
            int cid = HjNetHelper.GetQueryInt("cid", 0);
            if (cid == 0)
            {
                return;
            }

            WebUtility.BindList("ID", "classname", SectionProxyData.GetEdelivergroupList().Where(a => a.Status == cid).ToList(), ddlDeliverGroup);

            WebUtility.BindList("SectionName", "SectionName", SectionProxyData.GetSectionList().Where(a => a.cityid == cid).ToList(), ddlSection);

            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加配送员";
            }
            else
            {
                pageType.Text = "编辑配送员";
                //控件绑定内容
                DeliverInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                this.tbCoadId.Text = info.CodeId;
                this.tbName.Text = info.Name;
                this.tbPhone.Text = info.Phone;
                this.ddlSection.SelectedValue = info.Section.ToString();
                this.ddlStatus.SelectedValue = info.Status.ToString();
                this.ddlDeliverGroup.SelectedValue = info.GpsIMEI;
                this.tbOrderCount.Text = info.OrderNum.ToString();
                hidPassword.Value = info.Password;
                tbUserName.Text = info.UserName;
                tbInve2.Text = info.Inve2;

            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        DeliverInfo info = new DeliverInfo();
        //info.ClassId = WebUtility.InputText(ddlGclass.SelectedValue);
        info.CodeId = WebUtility.InputText(this.tbCoadId.Text);
        info.Name = WebUtility.InputText(this.tbName.Text);
        info.Phone = WebUtility.InputText(this.tbPhone.Text);
        info.Section = WebUtility.InputText(this.ddlSection.SelectedValue);
        info.Status = Convert.ToInt32(this.ddlStatus.SelectedValue);
        info.GpsIMEI = WebUtility.InputText(ddlDeliverGroup.SelectedValue);
        info.OrderNum = Convert.ToInt32(this.tbOrderCount.Text);
        info.UserName = WebUtility.InputText(tbUserName.Text);
        info.Password = !string.IsNullOrEmpty(this.tbpassword.Text) ? WebUtility.GetMd5(this.tbpassword.Text) : hidPassword.Value;
        info.Inve1 = HjNetHelper.GetQueryInt("cid", 0);
        info.Inve2 = WebUtility.InputText(tbInve2.Text);
        info.AddDate=DateTime.Now;

        if (pageType.Text == "添加配送员")
        {
            if (dal.Add(info) > 0)
            {
                EasyEatCache.GetCacheService().RemoveObject("/DeliverInfo");
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','id:divShowContent','320','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);init();");
            }
        }
        else
        {
            info.DataId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));

            if (dal.Update(info) > 0)
            {
                EasyEatCache.GetCacheService().RemoveObject("/DeliverInfo");
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);init();");
            }
        }
        
    }
}
