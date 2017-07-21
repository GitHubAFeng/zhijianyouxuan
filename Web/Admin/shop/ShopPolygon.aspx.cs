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
using System.Reflection;
using System.Text;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class EasyEatHome_MTogo_TogoPolygonFix : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            //获取商家
            ETogoLocalInfo info = new ETogoLocalInfo();

            ETogoLocal bll = new ETogoLocal();

            info = bll.GetInfoById(HjNetHelper.GetQueryString("tid"));
            if (info != null && info.TogoId != 0)
            {
                hidLat.Value = info.Lat;
                hidLng.Value = info.Lng;
                hidPolygon.Value = info.Polygon;
            }
            //如无坐标点则默认

            if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
            {
                hidLat.Value = info.Lat;
                hidLng.Value = info.Lng;
            }
            else
            {
                hidLat.Value = SectionProxyData.GetSetValue(11);
                hidLng.Value = SectionProxyData.GetSetValue(12);
            }

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        ETogoLocalInfo info = new ETogoLocalInfo();

        ETogoLocal bll = new ETogoLocal();

        info = bll.GetInfoById(HjNetHelper.GetQueryString("tid"));
        info.Polygon = hidPolygon.Value;
        info.TogoId = Convert.ToInt32(HjNetHelper.GetQueryString("tid"));
        info.Radius = info.Radius == null ? (decimal)0.0 : info.Radius;
        info.Lat = info.Lat == null ? "" : info.Lat;
        info.Lng = info.Lng == null ? "" : info.Lng;

        string Polygon = "";
        Polygon = hidPolygon.Value;

        try
        {
            bll.Add(info);
            SectionProxyData.ShopLocallisClear();

            AlertScript.RegScript(this, "alert('保存成功');");
          
        }
        catch (Exception ex)
        {
            AlertScript.RegScript(this, "tipsWindown('提示信息','text:" + ex.Message + "保存失败!','250','150','true','1000','true','text');");
        }
    }

}
