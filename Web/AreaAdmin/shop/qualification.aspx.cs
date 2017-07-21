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


public partial class AreaAdmin_shop_qualification : System.Web.UI.Page
{
    Points dal_pints = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTogoData();
        }
    }

    protected void BindTogoData()
    {
        PointsInfo info = dal_pints.GetModel(HjNetHelper.GetQueryInt("tid", 0));
        ppic1.Src = WebUtility.ShowPic(info.licensePic);
        pic1.Value = info.licensePic;
        ddllicense.SelectedValue = info.isLicense.ToString();

        ppic2.Src = WebUtility.ShowPic(info.cateringPic);
        pic2.Value = info.cateringPic;
        ddlcatering.SelectedValue = info.isCatering.ToString();

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        int TogoId = HjNetHelper.GetQueryInt("tid", 0);
        PointsInfo info = dal_pints.GetModel(TogoId);

        info.licensePic = pic1.Value;
        info.isLicense = Convert.ToInt32(ddllicense.SelectedValue);
        info.cateringPic = pic2.Value;
        info.isCatering = Convert.ToInt32(ddlcatering.SelectedValue);

        if (dal_pints.Update(info) > 0)
        {
            AlertScript.RegScript(this, UpdatePanel1, "alert('操作成功');gourl('qualification.aspx?tid=" + TogoId + "');");
        }
        else
        {
            AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
        }
    }
}