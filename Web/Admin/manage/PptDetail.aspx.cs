/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_PptDetail
 * Function : 
 * Created by jijunjian at 2010-8-21 14:45:36.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Admin_PptDetail : System.Web.UI.Page
{
    PPT dal = new PPT();
    protected string oldppt
    {
        set
        {
            ViewState["oldppt"] = value;
        }
        get 
        {
            return ViewState["oldppt"] == null ? "" : ViewState["oldppt"].ToString();
        }
    }

    protected PPTInfo info
    {
        set
        {
            this.tbName.Text = value.Title;
            this.tburl.Text = value.PUrl;
            oldppt = value.picture;
            this.ImgUrl1.Value = value.picture;
            WebUtility.SelectValue(ddltype, value.Reve2 + "");
            this.ImgUrl.Src = WebUtility.ShowPic(value.picture);
            tborder.Text = value.Reve1.ToString();
        }
        get
        {
            PPTInfo model = new PPTInfo();
            model.DataID = HjNetHelper.GetQueryInt("id", 0);
            model.picture = ImgUrl1.Value;
            model.PUrl = this.tburl.Text;
            model.Title = WebUtility.InputText(this.tbName.Text);
            model.Reve1 = Convert.ToInt32(tborder.Text);
            model.Reve2 = ddltype.SelectedValue;
            model.SecID = 0;
            return model;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            if (Request["id"] != null)
            {
                getAdminData();
            }
        }
    }

    protected void getAdminData()
    {
        PPTInfo model = null;
        int id = HjNetHelper.GetQueryInt("id", 0);
        model = dal.GetModel(id);
        if (model != null)
        {
            info = model;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        PPTInfo model = info;
        SectionProxyData.ClearWebSet();
        if (HjNetHelper.GetQueryInt("id", 0) > 0)
        {

            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();localchange();");
                return;
            }
            if (dal.Update(model) > 0)
            {
                SectionProxyData.ClearPPTList();
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存成功!','250','150','true','1000','true','text');localchange();");
                this.ImgUrl.Src = WebUtility.ShowPic(this.ImgUrl1.Value);

            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');localchange();");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();localchange();");
                return;
            }
            if (dal.Add(model) > 0)
            {
                SectionProxyData.ClearPPTList();
                
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:添加成功!','250','150','true','1000','true','text');localchange();");
                this.ImgUrl.Src = WebUtility.ShowPic(this.ImgUrl1.Value);
                //判断是否重新上传图片,是就删除先前的图片.
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:添加失败!','250','150','true','1000','true','text');localchange();");
            }
        }
    }

}
