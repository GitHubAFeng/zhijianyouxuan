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

public partial class qy_55tuan_Admin_DeliverDetail : System.Web.UI.Page
{
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


    Deliver dal = new Deliver();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            int cityid = HjNetHelper.GetQueryInt("cityid", 0);
            if (cityid == 0)
            {
                return;
            }
            WebUtility.BindList("id", "classname", SectionProxyData.GetEdelivergroupList().Where(a=>a.Status == cityid).ToList(), ddlDeliverGroup);
            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加配送员";
            }
            else
            {
                pageType.Text = "编辑配送员";
                //控件绑定内容
                DeliverInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                if (info != null)
                {
                    this.tbName.Text = info.Name;
                    this.tbPhone.Text = info.Phone;
                    this.ddlStatus.SelectedValue = info.Status.ToString();
                    this.ddlApprovedState.SelectedValue = info.IsApproved.ToString();//审核状态
                    hidPassword.Value = info.Password;
                    tbUserName.Text = info.UserName;
                    WebUtility.SelectValue(ddlDeliverGroup, info.GpsIMEI);
                    this.ddlIsWorking.SelectedValue = info.IsWorking.ToString();//是否接单，1表示正常接单，0表示暂停接单

                    this.pic1.Value=info.pic1  ;//身份证照片 存入隐藏域
                    this.ppic1.Src = WebUtility.ShowPic(info.pic1);//身份证照片

                    tbCodeId.Text = info.CodeId;
                    tbSection.Text = info.Section;

                }
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        DeliverInfo info = new DeliverInfo();
        info.DataId = HjNetHelper.GetQueryInt("id", 0);
        if (info.DataId > 0)
        {
            dal.GetModel(info.DataId);
        }
        info.CodeId = WebUtility.InputText(this.tbCodeId.Text);
        info.Section = WebUtility.InputText(this.tbSection.Text);
        info.Name = WebUtility.InputText(this.tbName.Text);
        info.Phone = WebUtility.InputText(this.tbPhone.Text);
        info.Status = Convert.ToInt32(this.ddlStatus.SelectedValue);
        info.IsApproved = Convert.ToInt32(this.ddlApprovedState.SelectedValue);//审核状态
        info.GpsIMEI = WebUtility.InputText(this.ddlDeliverGroup.Text);
        info.OrderNum = 0;
        info.IsWorking = Convert.ToInt32(this.ddlIsWorking.SelectedValue);
        info.UserName = WebUtility.InputText(tbUserName.Text);
        info.Password = !string.IsNullOrEmpty(this.tbpassword.Text) ? WebUtility.GetMd5(this.tbpassword.Text) : hidPassword.Value;
        info.Inve2 = "";
        int cityid = HjNetHelper.GetQueryInt("cityid", 0);
        info.Inve1 = cityid;
        info.AddDate = DateTime.Now;

        info.pic1 = this.pic1.Value;//身份证照片
        this.ppic1.Src = WebUtility.ShowPic(info.pic1);//身份证照片

        if (info.DataId == 0)
        {
            info.havemoney = 0;
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }

            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);init();");
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
            if (dal.Update(info) > 0)
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
