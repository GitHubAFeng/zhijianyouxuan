/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : TogoHome_paisong
 * Function : 
 * Created by jijunjian at 2010-12-29 11:31:04.
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
using Hangjing.DBUtility;

public partial class TogoHome_paisong : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!Page.IsPostBack)
        {
            PointsInfo info = dal.GetModel(UserHelp.GetUser_Togo().Unid);
            WebUtility.SetDDLCity(DDLArea);

            WebUtility.SelectValue(DDLArea, info.cityid.ToString());

            if (info.EBuilding != "")
            {
                string tempbid = WebUtility.OpBuildstr(info.EBuilding);
                hfids.Text = tempbid;
                this.tbBuilding.InnerHtml = new Hangjing.SQLServerDAL.EBuilding().GetNameList(tempbid);
            }

            rptSectinList.DataSource = SectionProxyData.GetSectionList().Where(p => p.cityid == info.cityid);
            rptSectinList.DataBind();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        int count = dal.UpdateValue("EBuilding", WebUtility.GetStrAdd(WebUtility.OpBuildstr(hfids.Text.Trim())), " where Unid = " + UserHelp.GetUser_Togo().Unid);
        if (count > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('操作成功','success','true',5);getbuild(1, -1, -1, -1, 1, -1, 1);");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('操作失败','error','true',5);getbuild(1, -1, -1, -1, 1, -1, 1);");
        }
    }

}
