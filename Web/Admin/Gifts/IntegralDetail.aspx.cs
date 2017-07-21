#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品兑换信息
// Created by jijunjian at 2010-6-24 16:29:12.
// E-Mail: jijunjian@ihangjing.com
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


public partial class Admin_Integral_IntegralDetail :AdminPageBase
{
    Integral dal = new Integral();
    Gifts gdal = new Gifts();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("E");
        if (!Page.IsPostBack)
        {
            if (HjNetHelper.GetQueryString("id") != "")
            {
                this.pageType.Text = "编辑礼品兑换信息";

                IntegralInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                this.tbCdate.Text = Convert.ToString(info.Cdate);
                this.tbCustid.Text = info.CustId;
                this.tbNeed.Text = info.PayIntegral;

                tbUserName.Text = info.UserName;
                tbGiftName.Text = info.GiftName;
                hidGiftId.Value = info.GiftsId.ToString();
                hidUserId.Value = info.CustId.ToString();
                tbAddress.Text = info.Address;
                tbDate.Text = info.Date;
                tbPerson.Text = info.Person;
                tbPhone.Text = info.Phone;
                WebUtility.SelectValue(ddlState, info.State + "");
                tbremark.Text = info.Remark;
            }
            else
            {
                this.pageType.Text = "添加礼品兑换信息";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        IntegralInfo info = new IntegralInfo();
        info.Cdate = Convert.ToDateTime(WebUtility.InputText(tbCdate.Text));
        info.CustId = WebUtility.InputText(tbCustid.Text);
        info.DetailId = 0;
        info.GiftsId = Convert.ToInt32(hidGiftId.Value);
        info.PayIntegral = WebUtility.InputText(tbNeed.Text);
        info.State = WebUtility.InputText(ddlState.SelectedValue);
        info.Phone = WebUtility.InputText(tbPhone.Text);
        info.Person = WebUtility.InputText(tbPerson.Text);
        info.Address = WebUtility.InputText(tbAddress.Text);
        info.Date = WebUtility.InputText(tbDate.Text);
        info.UserName = WebUtility.InputText(tbUserName.Text);
        info.GiftName = WebUtility.InputText(tbGiftName.Text);
        info.Remark = WebUtility.InputText(tbremark.Text);

        if (pageType.Text == "添加礼品兑换信息")
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
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增成功','success','true',5);");
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
            info.IntegralId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));

            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
    }
}
