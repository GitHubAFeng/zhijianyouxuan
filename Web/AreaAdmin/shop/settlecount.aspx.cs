using System;
using System.Collections;
using System.Collections.Generic;
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
using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 商家结算帐号信息
/// </summary>
public partial class AreaAdmin_Shop_settlecount : AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            //编辑
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);

            if (TogoId > 0)
            {
                BindTogoData(TogoId);
            }
        }
    }

    shopsettleaccount dal = new shopsettleaccount();

    /// <summary>
    /// 获取商家的信息
    /// </summary>
    /// <param name="TogoId"></param>
    protected void BindTogoData(int TogoId)
    {
        IList<shopsettleaccountInfo> list = dal.GetList(1, 1, " shopid =" + TogoId, "id", 1);
        if (list.Count > 0)
        {
            shopsettleaccountInfo info = list[0];
            tbbankname.Text = info.bankname;
            tbbankusername.Text = info.bankusername;
            tbaliaccount.Text = info.aliaccount;
            tbaliname.Text = info.aliname;
            tbremark.Text = info.remark;
            tbrevevar1.Text = info.revevar1;
        }
    }

    /// <summary>
    /// 保存商家的信息 TODO:保存的标志建筑物那块功能是否可以变得更方便强大（删除建筑物）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click1(object sender, EventArgs e)
    {
        int TogoId = HjNetHelper.GetQueryInt("tid", 0);
        shopsettleaccountInfo info = new shopsettleaccountInfo();
        IList<shopsettleaccountInfo> list = dal.GetList(1, 1, " shopid =" + TogoId, "id", 1);
        if (list.Count > 0)
        {
            info = list[0];
        }
        else
        {
            info.reveint1 = 0;
            info.reveint2 = 0;
            info.reveint3 = 0;
            info.reveint4 = 0;
            info.reveint5 = 0;
            info.revefloat1 = 0;
            info.revefloat2 = 0;
            info.revefloat3 = 0;
            info.revevar2 = "";
            info.revevar3 = "";
            info.revevar4 = "";
            info.revevar5 = "";
            info.revetext = "";

        }
        info.shopid = TogoId;
        info.bankname = WebUtility.InputText(tbbankname.Text);
        info.bankusername = WebUtility.InputText(tbbankusername.Text);
        info.aliaccount = WebUtility.InputText(tbaliaccount.Text);
        info.aliname = WebUtility.InputText(tbaliname.Text);
        info.remark = WebUtility.InputText(tbremark.Text);
        info.opuser = UserHelp.GetAdmin().AdminName;
        info.optime = DateTime.Now;
        info.revevar1 = WebUtility.InputText(tbrevevar1.Text);

        if (dal.Add(info) > 0)
        {
            AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('settlecount.aspx?tid="+info.shopid+"');");

        }
        else
        {
            AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
        }


    }

}
