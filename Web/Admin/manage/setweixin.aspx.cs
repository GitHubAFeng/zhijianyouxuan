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

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System.Collections.Generic;

/// <summary>
/// 公众平台信息
/// </summary>
public partial class Admin_Shop_setweixin : System.Web.UI.Page
{
    WeiXinAccount dal = new WeiXinAccount();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            BindFoodData();
            pageType.Text = "编辑微信公众平台帐号信息";
        }
    }

    /// <summary>
    /// 获取信息
    /// </summary>
    protected void BindFoodData()
    {
        WeiXinAccountInfo info = dal.GetModel(1);
        if (info != null)
        {
            tbwxusername.Text = info.wxusername;
            tbwxpwd.Text = info.wxpwd;
            tbAppId.Text = info.AppId;
            tbAppSecret.Text = info.AppSecret;
            tbrevevar1.Text = info.revevar1;
            tbrevevar2.Text = info.revevar2;
            tbpartnerid.Text = info.partnerid;
            tbapikey.Text = info.apikey;
        }
        else
        {
            tbdel.Visible = false;
            tbbuildmenu.Visible = false;
        }
    }

    /// <summary>
    /// 添加，编辑数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        WeiXinAccountInfo info = new WeiXinAccountInfo();
        info.dataId = 0;
        info.shopid = HjNetHelper.GetQueryInt("tid", 0);
        info.wxusername = WebUtility.InputText(tbwxusername.Text);
        info.wxpwd = WebUtility.InputText(tbwxpwd.Text);
        info.AppId = WebUtility.InputText(tbAppId.Text);
        info.AppSecret = WebUtility.InputText(tbAppSecret.Text);
        info.reveint1 = 0;
        info.reveint2 = 0;
        info.revevar1 = tbrevevar1.Text;
        info.revevar2 = WebUtility.InputText(this.tbrevevar2.Text);
        info.partnerid = WebUtility.InputText(tbpartnerid.Text);
        info.apikey = WebUtility.InputText(tbapikey.Text);
        info.revevar3 = "" ;
        info.revevar4 = "";

        if (dal.Add(info) > 0)
        {
            SectionProxyData.ClearWeiXinAccount();
            AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('setweixin.aspx');");

        }
        else
        {
            AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
        }

    }

    /// <summary>
    /// 删除数据数据(同时删除菜单)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void del_Click(object sender, EventArgs e)
    {
        new Hangjing.Weixin.UserDefinedMenu(Context).deletemenu();
        AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('setweixin.aspx');");
    }

    /// <summary>
    /// 生成菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void buildmenu_Click(object sender, EventArgs e)
    {
        string msg = new Hangjing.Weixin.UserDefinedMenu(Context).create();
        AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功,返回内容："+msg+"');gourl('setweixin.aspx');");

    }



}
