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
using System.IO;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

using org.in2bits.MyXls;
using Hangjing.AppLog;

/// <summary>
/// 添加，编辑促销
/// </summary>
public partial class Admin_shop_addPromotion : AdminPageBase
{
     webPromotionConfig dal = new webPromotionConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        CheckRights("A");
        if (!IsPostBack)
        {

            BindFoodData();

        }
    }


    protected void BindFoodData()
    {
        WebUtility.BindList("state", "title", new promotionType().GetAllByType(1), tbptype);


        int id = HjNetHelper.GetQueryInt("id", 0);
        webPromotionConfigInfo info = dal.GetModel(id);
        if (info != null)
        {
            tbstartdate.Text = info.startdate.ToString("yyyy-MM-dd");
            tbenddate.Text = info.enddate.ToString("yyyy-MM-dd"); ;
            tbstarttime.Text = info.starttime.ToShortTimeString();
            tbendtime.Text = info.endtime.ToShortTimeString();
            WebUtility.SelectValue(tbptype, info.ptype.ToString());
            WebUtility.SelectValue(tbisopen, info.isopen.ToString());
            tbrevevar1.Text = info.revevar1;

            switch (info.ptype)
            {
                case 1:
                    tbminusmoney1.Text = info.minusmoney.ToString();
                    break;
                case 20:
                    tbovermoney20.Text = info.overmoney.ToString();
                    break;
                case 30:
                    tbminusmoney30.Text = info.minusmoney.ToString();
                    tbovermoney30.Text = info.overmoney.ToString();
                    break;
                case 40:
                    tbminusmoney40.Text = info.minusmoney.ToString();
                    tbovermoney40.Text = info.overmoney.ToString();
                    break;
                default:
                    break;
            }

        }
        else
        {
            tbisopen.Items[0].Selected = true;
            tbptype.Items[0].Selected = true;
        }

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        webPromotionConfigInfo info = new webPromotionConfigInfo();
        info.pId = HjNetHelper.GetQueryInt("id", 0);
        info.shopid = HjNetHelper.GetQueryInt("tid", 0);
        info.startdate = Convert.ToDateTime(tbstartdate.Text);
        info.enddate = Convert.ToDateTime(tbenddate.Text);
        info.starttime = Convert.ToDateTime(tbstarttime.Text);
        info.endtime = Convert.ToDateTime(tbendtime.Text);
        info.ptype = WebUtility.InputText(tbptype.SelectedValue,true);
        info.isopen = WebUtility.InputText(tbisopen.SelectedValue,true);
        info.freeSendFee = 0;
        info.overmoney = 0;
        info.minusmoney = 0;


        switch (info.ptype)
        {
            case 1:
                info.minusmoney = WebUtility.InputText(tbminusmoney1.Text,true);
                break;
            case 20:
                info.overmoney = WebUtility.InputText(tbovermoney20.Text, true);
                break;
            case 30:
                info.overmoney = WebUtility.InputText(tbovermoney30.Text, true);
                info.minusmoney = WebUtility.InputText(tbminusmoney30.Text, true);
                break;
            case 40:
                info.overmoney = WebUtility.InputText(tbovermoney40.Text, true);
                info.minusmoney = WebUtility.InputText(tbminusmoney40.Text, true);
                break;
            default:
                break;
        }

        info.reveint1 = 0;
        info.reveint2 = 0;
        info.revevar1 = WebUtility.InputText(tbrevevar1.Text);
        info.revevar2 = "";
        info.revefloat1 = 0;
        info.revefloat2 = 0;

        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);");
            return;
        }


        string url = "shopPromotion.aspx?tid=" + info.shopid+"";
        if (info.pId > 0)
        {
            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, "alert('操作成功','success','true',5);gourl('" + url + "');");
            }
            else
            {
                AlertScript.RegScript(this, "showMessage('添加失败','error','true','8');");

            }
        }

        else
        {

            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, "alert('操作成功','success','true',5);gourl('" + url + "');");

            }
            else
            {
                AlertScript.RegScript(this, "showMessage('添加失败','error','true','8');");

            }
        }




    }
}

