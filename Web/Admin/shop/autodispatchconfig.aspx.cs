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
/// 自动调度配置
/// </summary>
public partial class Admin_Shop_autodispatchconfig : System.Web.UI.Page
{
    autodispatchconfig dal = new autodispatchconfig();
    int cityid = HjNetHelper.GetQueryInt("id", 0);

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            autodispatchconfigInfo info = dal.GetModelByCity(cityid);
            if (info != null)
            {
                WebUtility.SelectValue(tbisopen, info.isopen.ToString());
                if (info.isopen == 0)
                {
                    trlaw.Style["display"] = "none";
                }

                switch (info.autotype)
                {
                    case 1:
                        law1.Checked = true;
                        break;
                    case 2:
                        law2.Checked = true;
                        break;
                    case 3:
                        law3.Checked = true;
                        break;
                    default:
                        law1.Checked = true;
                        break;
                }
                tbdistance.Text = info.distance.ToString();
                tbreveint1.Text = info.reveint1.ToString();
            }
            
            pageType.Text = "自动调度配置";
        }
    }

    

    /// <summary>
    /// 添加，编辑数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        int cityid = HjNetHelper.GetQueryInt("id", 0);
        autodispatchconfigInfo info = dal.GetModelByCity(cityid);

        if (info == null)
        {
            info = new autodispatchconfigInfo();
            info.revevar1 = "";
            info.revevar2 = "";
            info.revedate = DateTime.Now;
        }

        info.isopen = Convert.ToInt32(tbisopen.SelectedValue);
        int autotype = 0;
        info.distance = 0;
        if (info.isopen == 1)
        {
            if (law1.Checked == true)
            {
                autotype = 1;
            }
            if (law2.Checked == true)
            {
                info.distance = Convert.ToDecimal(WebUtility.InputText(tbdistance.Text));
                info.reveint1 = Convert.ToInt32(tbreveint1.Text);
                autotype = 2;
            }
            if (law3.Checked == true)
            {
                autotype = 3;
            }
        }
        info.autotype = autotype;
        info.reveint2 = cityid;

        if (dal.GetModelByCity(cityid) == null)
        {
            if (dal.Add(info) > 0)
            {
                SectionProxyData.ClearAutodispatchconfig();
                AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('autodispatchconfig.aspx?id=" + cityid + "');");

            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
            }
        }
        else
        {
            if (dal.Update(info) > 0)
            {
                SectionProxyData.ClearAutodispatchconfig();
                AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('autodispatchconfig.aspx?id=" + cityid + "');");

            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "showMessage('操作失败','error','true','8');");
            }
        }
    }

}





