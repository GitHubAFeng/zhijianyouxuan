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

// 标志建筑物管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12

public partial class qy_54tss_Admin_BuildingDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            //编辑
            int DataId = HjNetHelper.GetQueryInt("id", 0);
            hidDataId.Value = DataId.ToString();
            WebUtility.SetDDLCity(DDLArea);

            if (DataId > 0)
            {
                BindData(DataId);
                pageType.Text = "更新地标";
            }
            else
            {
                pageType.Text = "新增地标";
                int cityid = Convert.ToInt32(DDLArea.SelectedValue);
                if (cityid == 0)
                {
                    cityid = 1;
                }
                hidLat.Value = SectionProxyData.GetSetValue(4);
                hidLng.Value = SectionProxyData.GetSetValue(5);
            }
            ////获取所有城市的经纬度
        }
    }

    EBuilding bll = new EBuilding();

    protected void BindData(int DataId)
    {
        BuildingInfo info = new BuildingInfo();
        info = bll.GetModel(DataId);

        hidDataId.Value = info.DataID.ToString();
        tbName.Text = info.Name;
        tbtype.Text = info.Type + "";
        tbFirstL.Text = info.FirstL;
        hidLat.Value = info.Lat;
        hidLng.Value = info.Lng;

        if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
        {
            hidLat.Value = info.Lat;
            this.hidLng.Value = info.Lng;
        }
        else
        {
            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);
        }

        DDLArea.ClearSelection();
        WebUtility.SelectValue(DDLArea, info.cityid.ToString());
        WebUtility.SelectValue(ddlIsShow, info.IsShow.ToString());
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        BuildingInfo info = new BuildingInfo();
        info.Picture = "";// WebUtility.InputText(ImgUrl1.Value);
        info.Name = WebUtility.InputText(tbName.Text);
        info.Address = "";
        info.Type = Convert.ToInt32(tbtype.Text);
        info.Remark = "";
        info.SectionId = 0;
        info.FirstL = WebUtility.InputText(tbFirstL.Text);
        info.XYUrl = "";
        info.Lat = hidLat.Value;
        info.Lng = hidLng.Value;

        info.IsShow = Convert.ToInt32(ddlIsShow.SelectedValue);
        info.cityid = Convert.ToInt32(DDLArea.SelectedValue);
        info.DataID = HjNetHelper.GetQueryInt("id", 0);

        if (info.DataID == 0)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page,  "alert('无操作权限','success','true',5);init();");
                return;
            }
            int dataid = bll.Add(info);
            if (dataid > 0)
            {
                AlertScript.RegScript(this, "alert('添加建筑物成功,可以继续添加建筑物','success','true','5');sectionchange(2);");
                this.tbName.Text = "";;
            }
            else
            {
                AlertScript.RegScript(this, "alert('添加失败','error','true','8');");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
                return;
            }

            if (bll.Update(info) == 1)
            {
                AlertScript.RegScript(this, "alert('修改成功','success','true','5');sectionchange(2);");
            }
            else
            {
                AlertScript.RegScript(this, "alert('修改失败','error','true','8');");
            }
        }
        CacheHelper.ClearBuildingListList();

    }

}
