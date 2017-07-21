<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopPolygon.aspx.cs" Inherits="AreaAdmin_TogoPolygonFix" %>

<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="adleft" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置配送范围-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../css/iestyles.css" media="all" />
    <![endif]-->
    <!--[if lt IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/below_ie7.css" media="all" />
    <![endif]-->
    <!--[if IE 7]>
    <link rel="stylesheet" type="text/css" href="../css/ie7.css" media="all" />
    <![endif]-->

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>
    <script src="../javascript/GeoUtils_min.js?"></script>

    <%-- <script type="text/javascript" src="http://api.map.baidu.com/library/GeoUtils/1.2/src/GeoUtils_min.js"></script>--%>



    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); });

        $(document).ready(function () { WhitchActive(2); showContent(1); });
    </script>

    <style type="text/css">
        v\:* {
            /* Only for IE */
            behavior: url(#default#VML);
        }

        body {
            height: 600px;
        }

        h3 {
            margin-left: 10px;
        }

        #descr {
            position: absolute;
            top: 40px;
            left: 580px;
            width: 250px;
        }

        .button {
            display: block;
            width: 180px;
            border: 1px Solid #565;
            background-color: #F5F5F5;
            padding: 3px;
            text-decoration: none;
            font-size: smaller;
        }

            .button:hover {
                background-color: white;
            }

        .tooltip {
            text-align: center;
            opacity: .70;
            -moz-opacity: .70;
            filter: Alpha(opacity=70);
            white-space: nowrap;
            margin: 0;
            padding: 2px 0.5ex;
            border: 1px solid #000;
            font-weight: bold;
            font-size: 9pt;
            font-family: Verdana;
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />


        <asp:HiddenField ID="hidPolygon" runat="server" />
        <!--加载中显示的div-->
        <div id="loading-mask">
            <p class="loader" id="loading_mask_loader">
                <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <!--banner start-->
            <uc1:TogoBanner runat="server" ID="Banner" />
            <!--banner end-->
            <!--center start-->
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                        <div class="side-col" id="page:left">
                            <uc3:adleft runat="server" ID="adleft" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="messages">
                                </div>
                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer" runat="server" id="h3content">商家配送范围设置</h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">
                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                Text="保存配送范围信息" OnClientClick="return GetPolygon();"></asp:Button>
                                        </p>
                                    </div>
                                </div>
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-edit-form fieldset-legend">商家配送范围设置</h4>
                                                <div class="form-buttons">
                                                </div>
                                            </div>
                                            <div class="fieldset " id="_accountbase_fieldset">
                                                <div class="hor-scroll">
                                                    <table class="form-list" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="notice">
                                                                        提示：点击地图绘制商家配送范围(闭合区域)
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>lat:<input type="text" id="clat" class="j_text" /></td>
                                                                <td>lng:<input type="text" id="clng" class="j_text" />


                                                                    <input type="button" value="测试" onclick="ptInPolygon()" />

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <div id="map" style="width: 98%; height: 500px">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--foot start-->
            <uc2:Foot runat="server" ID="FootUC" />
            <!--foot end-->
        </div>

    </form>
</body>
</html>

<script type="text/javascript">
    var clat = $("#clat");
    var clng = $("#clng");
    var gzoom = 17;
    var points = new Array();
    var markers = new Array();
    var map = new BMap.Map("map"); // 创建地图实例
    map.enableScrollWheelZoom();
    var myPolygon = null;
    var count = 0;
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    var initpoint = new BMap.Point(_lng, _lat);  // 创建点坐标

    map.centerAndZoom(initpoint, gzoom); // 初始化地图，设置中心点坐标和地图级别
    map.addControl(new BMap.NavigationControl());  //缩放工具

    var myIcon = new BMap.Icon("/Admin/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
    marker = new BMap.Marker(initpoint, { icon: myIcon });
    map.addOverlay(marker);

    //map.addEventListener("click", mapclick);
    EventWrapper.addListener(map, "click", mapclick);

    //绘制多边形
    function drawPolygon() {
        if (myPolygon) {
            map.removeOverlay(myPolygon);
        }
        points.length = 0;
        //从marksers填充pints数组
        for (i = 0; i < markers.length; i++) {
            points.push(markers[i].getPosition());
        }
        //  points.push(markers[0].getPosition());

        myPolygon = new BMap.Polygon(points, { strokeColor: "red", strokeWeight: 1, strokeOpacity: 1 });
        map.addOverlay(myPolygon);
    }

    function mapclick(target) {
        //flag == 0表示点击的是标注
        //flag > 0表示点击的是地图    
        var flag = target.pixel.x;

        clat.val(target.point.lat);
        clng.val(target.point.lng);

        if (target && flag > 0) {
            count++
            var myIcon = new BMap.Icon("/Admin/images/mm_20_red.png", new BMap.Size(14, 22), { anchor: new BMap.Size(7, 22) });
            var _marker = new BMap.Marker(target.point, { icon: myIcon });
            map.addOverlay(_marker);
            markers.push(_marker);
            _marker.enableDragging();
            _marker.setTitle("point" + count + " lat:" + target.point.lat);

            _marker.addEventListener("dragging", function (e) {
                drawOverlay();
            });
            _marker.addEventListener("dragend", function (e) {
                drawOverlay();
            });
            _marker.addEventListener("dragstart", function (e) {
                drawOverlay();
            });

            // Click listener to remove a marker
            _marker.addEventListener("click", function () {
                var n = 0;
                for (n = 0; n < markers.length; n++) {
                    if (markers[n] == _marker) {
                        map.removeOverlay(markers[n]);
                        break;
                    }
                }
                // 指定从数组中移除元素的开始位置，这个位置是从 0 开始计算的。 
                // 删除从标n开始的，一个元素
                markers.splice(n, 1);
                if (markers.length == 0) {
                    count = 0;
                }
                else {
                    count = markers.length;
                    drawOverlay();
                }
            });
            drawPolygon();
        }
    }

    function drawOverlay() {

        drawPolygon();
    }

    //获取每个点的坐标,以: lat1,lng1|lat2,lng2..保存
    function GetPolygon() {
        var html = "";
        for (var i = 0; i < points.length; i++) {
            html += points[i].lat + "," + points[i].lng + "|";
        }
        var temp = html.replace(/\|$/, "");
        $("#hidPolygon").val(temp);
        if (html == "") {
            alert("请设定配置范围");
            return false;
        }
        return true;
    }

    //初始化多边形
    function initPolygon() {
        var hidPolygon = $("#hidPolygon").val();
        if (hidPolygon == "") {
            return;
        }
        var oldPolygon = hidPolygon.split('|');
        for (var i = 0; i < oldPolygon.length; i++) {
            count++;
            var latlng = oldPolygon[i].split(',');
            var _mypoint = new BMap.Point(latlng[1], latlng[0]);
            var myIcon = new BMap.Icon("/Admin/images/mm_20_red.png", new BMap.Size(14, 22), { anchor: new BMap.Size(7, 22) });
            var _marker = new BMap.Marker(_mypoint, { icon: myIcon });
            map.addOverlay(_marker);
            markers.push(_marker);
            _marker.enableDragging();
            _marker.setTitle("point" + count + " lat:" + _mypoint.lat);

            count++;
            addinitmarker(_marker);

        }
        drawPolygon();
    }

    function addinitmarker(_marker_init) {
        _marker_init.addEventListener("dragging", function (e) {
            drawOverlay();
        });
        _marker_init.addEventListener("dragend", function (e) {
            drawOverlay();
        });
        _marker_init.addEventListener("dragstart", function (e) {
            drawOverlay();
        });
        // Click listener to remove a marker
        EventWrapper.addListener(_marker_init, "click", function () {
            var n = 0;
            for (n = 0; n < markers.length; n++) {
                if (markers[n] == _marker_init) {
                    map.removeOverlay(markers[n]);
                    break;
                }
            }
            // 指定从数组中移除元素的开始位置，这个位置是从 0 开始计算的。 
            // 删除从标n开始的，一个元素
            markers.splice(n, 1);
            if (markers.length == 0) {
                count = 0;
            }
            else {
                count = markers.length;
                drawOverlay();
            }
        });
    }


    initPolygon();

    //点在多边形内
    function ptInPolygon() {
        var pts = [];
        var hidPolygon = $("#hidPolygon").val();
        var oldPolygon = hidPolygon.split('|');
        for (var i = 0; i < oldPolygon.length; i++) {
            var latlng = oldPolygon[i].split(',');
            var _mypoint = new BMap.Point(latlng[1], latlng[0]);

            pts.push(_mypoint);
        }

        var ply = new BMap.Polygon(pts);

        var mlat = parseFloat(clat.val());
        var mlng = parseFloat(clng.val());

        var pt = new BMap.Point(mlng, mlat);  // 创建点坐标

        var result = BMapLib.GeoUtils.isPointInPolygon(pt, ply);
        if (result == true) {
            console.log("点在多边形内");
        } else {
            console.log("点在多边形外")
        }

        //演示：将面添加到地图上    
        map.clearOverlays();
        var mkr = new BMap.Marker(pt);
        map.addOverlay(mkr);
        map.addOverlay(ply);

        //return;

        jQuery.ajax(
        {
            type: "post",
            cache:false,
            url: "/ajaxHandler.ashx",
            data: "method=test_isPointInPolygon&lat=" + mlat + "&lng=" + mlng + "&time=" + new Date().getTime() + "",
            success: function (msg) {
                console.log(msg);
            }
        })
    }

</script>

