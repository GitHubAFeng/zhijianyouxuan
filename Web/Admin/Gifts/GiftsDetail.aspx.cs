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
using System.IO;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_Gifts_GiftsDetail :AdminPageBase
{
    Gifts dal = new Gifts();
    GiftsClass gdal = new GiftsClass();

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

    private string bigpic
    {
        get
        {
            object o = ViewState["bigpic"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["bigpic"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("E");
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            SqlWhere = "parentid = 0";
            this.ddlGclass.DataSource = gdal.GetList(100, 1, SqlWhere, "ClassId", 1);
            this.ddlGclass.DataTextField = "ClassName";
            this.ddlGclass.DataValueField = "ClassId";
            this.ddlGclass.DataBind();

            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加礼品信息";
            }
            else
            {
                pageType.Text = "编辑礼品信息";
                //控件绑定内容
                GiftsInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                this.tbName.Text = info.Gname;
                this.tbNeed.Text = info.NeedIntegral;
                this.fcContent.Value = info.Content;
                this.tbPrice.Text = Convert.ToString(info.GiftsPrice);

                this.ImgUrl1.Value = info.bigpicture;
                this.ImgUrl.Src = WebUtility.ShowPic(info.bigpicture);

                this.ddlGclass.SelectedValue = info.ClassId;
                hfpicture.Value = info.Picture;
                bigpic = info.bigpicture;
                this.tbsortnum.Text = info.sortnum + "";
                tbstocks.Text = info.Stocks.ToString();
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        GiftsInfo info = new GiftsInfo();
        info.ClassId = WebUtility.InputText(ddlGclass.SelectedValue);
        info.Content = fcContent.Value;
        info.GiftsPrice = Convert.ToDecimal(WebUtility.InputText(tbPrice.Text));
        info.Gname = WebUtility.InputText(tbName.Text);
        info.NeedIntegral = WebUtility.InputText(tbNeed.Text);
        info.bigpicture = this.ImgUrl1.Value;
        info.sortnum = Convert.ToInt32(tbsortnum.Text);
        info.Stocks = Convert.ToInt32(tbstocks.Text);
        info.Picture = info.bigpicture;

        if (pageType.Text == "添加礼品信息")
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
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);init();");
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
            info.GiftsId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));

            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);init();");
            }
        }
        this.ImgUrl.Src = WebUtility.ShowPic(info.Picture);
    }

}
