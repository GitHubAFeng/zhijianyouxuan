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

public partial class Admin_manage_distributeRatioset : System.Web.UI.Page
{
    distributeRatio dal = new distributeRatio();
    distributeRatioInfo info
    {
        set
        {
            tbtitle.Text = value.title;
            tbonegraderatio.Text = value.onegraderatio.ToString();
            tbtwograderatio.Text = value.twograderatio.ToString();
            tbthreegraderatio.Text = value.threegraderatio.ToString();

            Page.Title = value.title + " - " + WebUtility.GetMyName();

        }
        get
        {
            distributeRatioInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id" ,0));
            model.onegraderatio = WebUtility.InputText(tbonegraderatio.Text,true);
            model.twograderatio = WebUtility.InputText(tbtwograderatio.Text, true);
            model.threegraderatio = WebUtility.InputText(tbthreegraderatio.Text, true);



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
        distributeRatioInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 1));
        info = model ;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        distributeRatioInfo model = new distributeRatioInfo();
        model = info;
        if (dal.Update(model) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('编辑成功','text:编辑成功!','250','150','true','1000','true','text');");
            CacheHelper.ClearDistributeRatConfigs();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('编辑失败','text:编辑失败!','250','150','true','1000','true','text');");
        }
    }
}
