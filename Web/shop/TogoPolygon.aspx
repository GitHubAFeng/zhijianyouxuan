<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TogoPolygon.aspx.cs" Inherits="shop_TogoPolygon" %>

<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>配送范围-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="http://maps.google.com/maps?file=api&amp;v=2.173&amp;sensor=false&amp;key=ABQIAAAAGFtDHTuhIgOEKsGezSD4bxTgxtR5kgGCkqJhf2lfyyWNySqUnxSITnNC1go39q3wvQt9wshW55NXCQ"
        type="text/javascript" charset="utf-8">
    </script>
    <script type="text/javascript">
        //<![CDATA[

        // Global variables
        var map = null;
        var poly;
        var count = 0;
        var points = new Array();
        var markers = new Array();
        var icon_url = "http://labs.google.com/ridefinder/images/";
        var tooltip;
        var report = document.getElementById("status");

        var marker = null;
        var center = null;

        function addIcon(icon) { // Add icon properties

            icon.shadow = icon_url + "mm_20_shadow.png";
            icon.iconSize = new GSize(12, 20);
            icon.shadowSize = new GSize(22, 20);
            icon.iconAnchor = new GPoint(6, 20);
            icon.infoWindowAnchor = new GPoint(5, 1);
        }


        function showTooltip(marker) { // Display tooltips

            tooltip.innerHTML = marker.tooltip;
            tooltip.style.display = "block";

            // Tooltip transparency specially for IE
            if (typeof (tooltip.style.filter) == "string") {
                tooltip.style.filter = "alpha(opacity:70)";
            }

            var currtype = map.getCurrentMapType().getProjection();
            var point = currtype.fromLatLngToPixel(map.fromDivPixelToLatLng(new GPoint(0, 0), true), map.getZoom());
            var offset = currtype.fromLatLngToPixel(marker.getLatLng(), map.getZoom());
            var anchor = marker.getIcon().iconAnchor;
            var width = marker.getIcon().iconSize.width + 6;
            // var height = tooltip.clientHeight +18;
            var height = 10;
            var pos = new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(offset.x - point.x - anchor.x + width, offset.y - point.y - anchor.y - height));
            pos.apply(tooltip);
        }


        //function buildMap() {

        // var container = document.getElementById("map");
        // map = new GMap2(container, {draggableCursor:"auto", draggingCursor:"move"});

        // // Add a div element for toolips
        // tooltip = document.createElement("div");
        // tooltip.className = "tooltip";
        // map.getPane(G_MAP_MARKER_PANE).appendChild(tooltip);

        // // Load initial map and a bunch of controls
        // map.setCenter(new GLatLng(30.28271397934901,120.13532638549805), 14);
        // map.addControl(new GLargeMapControl3D()); // Zoom control
        // map.addMapType(G_PHYSICAL_MAP);
        // // Create a hierarchical map type control
        // var hierarchy = new GHierarchicalMapTypeControl();
        // // make Hybrid the Satellite default
        // hierarchy.addRelationship(G_SATELLITE_MAP, G_HYBRID_MAP, "Labels", true);
        // // add the control to the map
        // map.addControl(hierarchy);

        // map.addControl(new GScaleControl()); // Scale bar
        // map.disableDoubleClickZoom();

        // // Add listener for the click event
        // GEvent.addListener(map, "click", leftClick);
        //}


        function leftClick(overlay, point) {

            if (point) {
                count++;

                // Red marker icon
                var icon = new GIcon();
                icon.image = icon_url + "mm_20_red.png";
                addIcon(icon);

                // Make markers draggable
                var marker = new GMarker(point, { icon: icon, draggable: true, bouncy: false, dragCrossMove: true });
                map.addOverlay(marker);
                marker.content = count;
                markers.push(marker);
                marker.tooltip = "Point " + count;

                GEvent.addListener(marker, "mouseover", function () {
                    showTooltip(marker);
                });

                GEvent.addListener(marker, "mouseout", function () {
                    tooltip.style.display = "none";
                });

                // Drag listener
                GEvent.addListener(marker, "drag", function () {
                    tooltip.style.display = "none";
                    drawOverlay();
                });

                // Click listener to remove a marker
                GEvent.addListener(marker, "click", function () {
                    tooltip.style.display = "none";

                    // Find out which marker to remove
                    for (var n = 0; n < markers.length; n++) {
                        if (markers[n] == marker) {
                            map.removeOverlay(markers[n]);
                            break;
                        }
                    }

                    // Shorten array of markers and adjust counter
                    markers.splice(n, 1);
                    if (markers.length == 0) {
                        count = 0;
                    }
                    else {
                        count = markers[markers.length - 1].content;
                        drawOverlay();
                    }
                });
                drawOverlay();
            }
        }


        function leftClickFix(point) {

            if (point) {
                count++;

                // Red marker icon
                var icon = new GIcon();
                icon.image = icon_url + "mm_20_red.png";
                addIcon(icon);

                // Make markers draggable
                var marker = new GMarker(point, { icon: icon, draggable: true, bouncy: false, dragCrossMove: true });
                map.addOverlay(marker);
                marker.content = count;
                markers.push(marker);
                marker.tooltip = "Point " + count;

                GEvent.addListener(marker, "mouseover", function () {
                    showTooltip(marker);
                });

                GEvent.addListener(marker, "mouseout", function () {
                    tooltip.style.display = "none";
                });

                // Drag listener
                GEvent.addListener(marker, "drag", function () {
                    tooltip.style.display = "none";
                    drawOverlay();
                });

                // Click listener to remove a marker
                GEvent.addListener(marker, "click", function () {
                    tooltip.style.display = "none";

                    // Find out which marker to remove
                    for (var n = 0; n < markers.length; n++) {
                        if (markers[n] == marker) {
                            map.removeOverlay(markers[n]);
                            break;
                        }
                    }

                    // Shorten array of markers and adjust counter
                    markers.splice(n, 1);
                    if (markers.length == 0) {
                        count = 0;
                    }
                    else {
                        count = markers[markers.length - 1].content;
                        drawOverlay();
                    }
                });
                drawOverlay();
            }
        }

        function toggleMode() {

            if (markers.length > 1) drawOverlay();
        }


        function drawOverlay() {

            if (poly) { map.removeOverlay(poly); }
            points.length = 0;

            for (i = 0; i < markers.length; i++) {
                points.push(markers[i].getLatLng());
            }
            // if (lineMode) {
            //   // Polyline mode
            //   poly = new GPolyline(points, "#ff0000", 2, .9);
            //   var length = poly.getLength()/1000;
            //   var unit = " km";
            //   report.innerHTML = "Total line length:<br> " + length.toFixed(3) + unit;
            //  }
            //  else {
            // Polygon mode
            points.push(markers[0].getLatLng());
            poly = new GPolygon(points, "#ff0000", 2, .9, "#ff0000", .2);
            //var area = poly.getArea()/(1000*1000);
            //var unit = " km&sup2;";
            //report.innerHTML = "Area of polygon:<br> " + area.toFixed(3) + unit;
            //}
            map.addOverlay(poly);
        }


        function clearMap() {

            // Clear current map and reset globals
            map.clearOverlays();
            points.length = 0;
            markers.length = 0;
            count = 0;
            report.innerHTML = "&nbsp;";
        }

        //获取范围的顶点坐标信息
        function GetPolygon() {
            var polygon_list = "";
            for (var i = 0; i < poly.getVertexCount() - 1; i++)//6  0 1 2 3 4
            {
                polygon_list += poly.getVertex(i).lat();
                polygon_list += ",";
                polygon_list += poly.getVertex(i).lng();
                polygon_list += "|";
            }
            document.getElementById("hidPolygon").value = polygon_list;

            return true;
        }
        //]]>
    </script>

    <style type="text/css">
        #cblsearch {
            padding-left: 20px;
        }
    </style>
</head>
<body onunload="GUnload()">
    <form id="form2" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField ID="hidPolygon" runat="server" />
        <div class="warp">
            <div class="warp_con">
                <uc2:LeftBanner runat="server" ID="LeftBanner1" />
                <div class="rightmenu_cont">
                    <h1 class="topbg">
                        <asp:Label ID="lbTitle" runat="server" Text="区域标注"></asp:Label></h1>
                    <div class="usermima">
                        <div id="map" style="width: 700px; height: 400px">
                        </div>
                        <div style="padding-top: 20px; padding-left: 200px;">
                            <asp:Button runat="server" ID="btSave" OnClick="btSave_Click" Text="保存信息" OnClientClick="return GetPolygon();"
                                class="subBtn" />
                            (提示：点击地图绘制派送区域)
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
