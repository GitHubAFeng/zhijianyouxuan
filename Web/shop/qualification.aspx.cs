using System;
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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using System.IO;


public partial class shop_qualification : System.Web.UI.Page
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
        PointsInfo info = dal_pints.GetModel(UserHelp.GetUser_Togo().Unid);
        ppic1.Src = WebUtility.ShowPic(info.licensePic);
        pic1.Value = info.licensePic;

        ppic2.Src = WebUtility.ShowPic(info.cateringPic);
        pic2.Value = info.cateringPic;

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        int TogoId = (UserHelp.GetUser_Togo().Unid);
        PointsInfo info = dal_pints.GetModel(TogoId);

        info.licensePic = pic1.Value;
        info.cateringPic = pic2.Value;

        if (dal_pints.Update(info) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','2000','true','text');");
            BindTogoData();
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','2000','true','text');");
        }
    }
}