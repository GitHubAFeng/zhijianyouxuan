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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
public partial class shop_TogoPolygon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!Page.IsPostBack)
        {
            //获取商家
            ETogoLocalInfo info = new ETogoLocalInfo();

            ETogoLocal bll = new ETogoLocal();
            int id = UserHelp.GetUser_Togo().Unid;
            info = bll.GetInfoById(id + "");

            //如无坐标点则默认

            string js = "function buildMap()";
            js += "{";
            js += "var container = document.getElementById(\"map\"); ";
            js += "map = new GMap2(container, {draggableCursor:\"auto\", draggingCursor:\"move\"});";
            js += "tooltip = document.createElement(\"div\");";
            js += "tooltip.className = \"tooltip\";";
            js += "map.getPane(G_MAP_MARKER_PANE).appendChild(tooltip);";
            if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
            {
                js += "center = new GLatLng(" + info.Lat + "," + info.Lng + ");";
            }
            else
            {
                js += "center = new GLatLng(" + Map.DefautLocal.getlocalInfo().Lat + "," + Map.DefautLocal.getlocalInfo().Lng + ");";
            }
            js += "map.setCenter(center, 13);";
            js += " marker = new GMarker(center, {draggable: true});";
            js += "map.addOverlay(marker);";
            js += "map.addControl(new GLargeMapControl3D()); ";
            js += "map.addMapType(G_PHYSICAL_MAP);";
            js += "var hierarchy = new GHierarchicalMapTypeControl();";
            js += " hierarchy.addRelationship(G_SATELLITE_MAP, G_HYBRID_MAP, \"Labels\", true);";
            js += " map.addControl(hierarchy);";
            js += "map.addControl(new GScaleControl()); ";
            js += "map.disableDoubleClickZoom();";
            js += "GEvent.addListener(map, \"click\", leftClick);";
            js += "}";

            js += "buildMap();";

            if (info.Polygon != null && info.Polygon != "")
            {
                string[] PolygonArray = info.Polygon.Split('|');

                for (int i = 0; i < PolygonArray.Length - 1; i++)
                {
                    //js += "latlngs.push(new GLatLng(" + PolygonArray[i] + ")); ";
                    js += " leftClickFix( new GLatLng(" + PolygonArray[i] + "));";
                }
            }

            AlertScript.RegScript(this.Page, js);
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        ETogoLocalInfo info = new ETogoLocalInfo();

        ETogoLocal bll = new ETogoLocal();
        int id = UserHelp.GetUser_Togo().Unid;
        info = bll.GetInfoById(id + "");

        info.Polygon = hidPolygon.Value;

        info.TogoId = id;
        info.Radius = info.Radius == null ? (decimal)0.0 : info.Radius;
        info.Lat = info.Lat == null ? "" : info.Lat;
        info.Lng = info.Lng == null ? "" : info.Lng;

        if (info.DataId == 0)
        {
            if (bll.Add(info) > 0)
            {
                Response.Redirect("TogoPolygon.aspx");
            }
            else
            {
                AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');");
            }
        }
        else
        {
            if (bll.Update(info) > 0)
            {
                //EasyEatCache.ClearTogoLocalInfo();
                Response.Redirect("TogoPolygon.aspx");
            }
            else
            {
                AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');");
            }
        }
    }
}
