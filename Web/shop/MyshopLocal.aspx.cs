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


public partial class shop_MyshopLocal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!Page.IsPostBack)
        {
            //获取商家
            ETogoLocalInfo info = new ETogoLocalInfo();

            ETogoLocal bll = new ETogoLocal();

            info = bll.GetInfoById(UserHelp.GetUser_Togo().Unid.ToString());

            //如无坐标点则默认

            string js = "function initialize() ";
            js += "{";
            js += "if (GBrowserIsCompatible()) ";
            js += "{";
            js += "map = new GMap2(document.getElementById(\"map_canvas\"));";
            js += "map.enableScrollWheelZoom();";
            if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
            {
                js += "center = new GLatLng(" + info.Lat + "," + info.Lng + ");";
            }
            else
            {
                string lat = SectionProxyData.GetSetValue(4);
                string lng = SectionProxyData.GetSetValue(5);
                js += "center = new GLatLng(" + lat + "," + lng + ");";//
                hidLat.Value = "40.083198";
                hidLng.Value = "116.324966";
            }

            js += "map.setCenter(center, 13);";
            js += " marker = new GMarker(center, {draggable: true});";
            js += "geocoder = new GClientGeocoder();";
            js += "GEvent.addListener(marker, \"dragstart\", function() {";
            js += "map.closeInfoWindow();";
            js += "});";

            js += "GEvent.addListener(marker, \"dragend\", function() {";
            js += "GetLatLng();";
            js += "marker.openInfoWindowHtml(\"确定商家的地图位置后请点击“保存位置信息” 按钮进行保存\");";
            js += "});";
            js += "map.addOverlay(marker);";
            js += "map.addControl(new GLargeMapControl3D()); ";
            js += "map.addMapType(G_PHYSICAL_MAP);";
            js += "var hierarchy = new GHierarchicalMapTypeControl();";
            js += " hierarchy.addRelationship(G_SATELLITE_MAP, G_HYBRID_MAP, \"Labels\", true);";
            js += " map.addControl(hierarchy);";
            js += "map.addControl(new GScaleControl()); ";
            js += "map.disableDoubleClickZoom();";
            js += "}";
            js += "}";
            js += "initialize();";
            AlertScript.RegScript(this.Page, js);
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //查看商家是否已经存在位置信息记录

        //获取商家
        ETogoLocalInfo info = new ETogoLocalInfo();

        ETogoLocal bll = new ETogoLocal();

        info = bll.GetInfoById(UserHelp.GetUser_Togo().Unid.ToString());

        info.Lat = hidLat.Value;
        info.Lng = hidLng.Value;
        info.TogoId = UserHelp.GetUser_Togo().Unid;
        info.Polygon = info.Polygon == null ? "" : info.Polygon;
        info.Radius = info.Radius == null ? (decimal)0.0 : info.Radius;
        if (info.DataId == 0)
        {
            if (bll.Add(info) > 0)
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:保存成功!','250','150','true','1000','true','text');");
               
            }
            else
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');");
            }
        }
        else
        {
            if (bll.Update(info) > 0)
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:更新成功!','250','150','true','1000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');");
            }
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("MyshopLocal.aspx");
    }
}