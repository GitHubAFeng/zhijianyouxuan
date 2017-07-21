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
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class Admin_packet_userpacketDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidatorSet validator = new ValidatorSet("Admin");
            validator.SetValidator();
            //编辑
            int DataId = HjNetHelper.GetQueryInt("id", 2);
            if (DataId > 0)
            {
                hidDataId.Value = DataId.ToString();
                BindData();
                pageType.Text = "更新红包配置信息";
            }
        }
    }

    packetconfig bll = new packetconfig();

    protected void BindData()
    {
        packetconfigInfo info = bll.GetModel(HjNetHelper.GetQueryInt("id", 2));
        WebUtility.SelectValue(tbisopen, info.isopen.ToString());

        hidDataId.Value = info.dataid.ToString();
        WebUtility.SelectValue(ddlShopSet, info.autotype.ToString());
        tbNum.Text = info.reveint1.ToString();
        WebUtility.SelectValue(tbstyle, info.reveint2.ToString());
        tbsum.Text = info.distance.ToString();
        tbKey.Text = info.revevar1;
        tbValidity.Text = info.revevar2;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        packetconfigInfo info = bll.GetModel(HjNetHelper.GetQueryInt("id", 2));
        info.autotype = Convert.ToInt32(ddlShopSet.SelectedValue);
        info.reveint1 = Convert.ToInt32(WebUtility.InputText(tbNum.Text));
        info.reveint2 = Convert.ToInt32(tbstyle.SelectedValue);
        info.distance = Convert.ToInt32(WebUtility.InputText(tbsum.Text));
        info.revevar1 = WebUtility.InputText(tbKey.Text);
        info.isopen = Convert.ToInt32(tbisopen.SelectedValue);
        info.revedate = DateTime.Now;
        info.revevar2 = WebUtility.InputText(tbValidity.Text);
        info.dataid = HjNetHelper.GetQueryInt("id", 0);

        if (bll.Update(info) > 0)
        {

            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新成功','id:divShowContent','640','150','true','','true','text')");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新失败','text:更新失败','250','150','true','','true','text')");
        }

    }
}
