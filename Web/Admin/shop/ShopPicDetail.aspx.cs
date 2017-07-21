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

public partial class Admin_ShopPicDetail : System.Web.UI.Page
{
    ShopSurroundings dal = new ShopSurroundings();

    protected ShopSurroundingsInfo info
    {
        set
        {            
            this.tbName.Text = value.Title;           
            this.ImgUrl1.Value = value.Picture;
            this.ImgUrl.Src = WebUtility.ShowPic(value.Picture);
            tbsort.Text = value.Sort.ToString();           
        }
        get
        {
            ShopSurroundingsInfo model = new ShopSurroundingsInfo();
            model.ID = HjNetHelper.GetQueryInt("id", 0);
            model.Shopid = HjNetHelper.GetQueryInt("tid", 0);
            model.Picture = ImgUrl1.Value;
            model.Title = WebUtility.InputText(this.tbName.Text);
            model.Sort = Convert.ToInt32(tbsort.Text);
            model.ReveInt1 = 0;
            model.ReveInt2 = 0;
            model.ReveVar1 = "";
            model.ReveVar2 = "";
            return model;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            int Shopid = HjNetHelper.GetQueryInt("tid", 0);
            this.hidTogoId.Value = Shopid.ToString();

            if (Request["id"] != null)
            {
                getAdminData();
            }
        }
    }

    protected void getAdminData()
    {
        ShopSurroundingsInfo model = null;
        int id = HjNetHelper.GetQueryInt("id", 0);
        model = dal.GetModel(id);
        if (model != null)
        {
            info = model;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        ShopSurroundingsInfo model = info;
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
                //SectionProxyData.ClearPPTList();
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
