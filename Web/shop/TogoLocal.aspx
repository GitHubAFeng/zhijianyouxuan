<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TogoLocal.aspx.cs" Inherits="shop_TogoLocal" %>

<%@ Register Src="~/shop/left.ascx" TagName="Leftbar" TagPrefix="uc2" %>
<%@ Register Src="~/shop/rightbar.ascx" TagName="Rightbar" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>商家定位-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/user/css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>

</head>

<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hfcity" />
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />

        <uc2:Leftbar runat="server" ID="Left" />
        <uc3:Rightbar runat="server" ID="right" />
        <div class="container">
            <div class="main">
                <div class="Precision_filter">

                    <div class="shop_main">
                        <div class=" main-content ">
                            <%-- <h1 class="topbg">
                        <asp:Label ID="lbTitle" runat="server" Text="商家定位"></asp:Label></h1>--%>
                            <div class="shop_menu">
                                <ul>
                                    <li><a href="myshop.aspx">修改资料</a></li>
                                    <li class="cur"><a href="TogoLocal.aspx">商家定位</a></li>
                                    <li><a href="SetStatus.aspx">状态管理</a></li>
                                    <li><a href="myPromotion.aspx">促销活动</a></li>
                                    <li><a href="qualification.aspx">商家资质管理</a></li>
                                </ul>
                            </div>
                            <div class="usermima">
                                <div class="togolocal_title">
                                    搜素写字楼/小区：<asp:TextBox runat="server" ID="keyaddress" CssClass="text"></asp:TextBox>
                                    <input type="button" value="搜索" onclick="setPlace();" class="subBtn" />
                                    <span>(提示：拖动地标修改位置)</span>
                                </div>
                                <div id="map_canvas" style="width: 800px; height: 400px; margin-bottom: 15px;">
                                </div>
                                <p style="text-align: center;">
                                    <asp:Button runat="server" ID="btSave" OnClick="btSave_Click" Text="保存信息" OnClientClick="GetLatLng();"
                                        class="subBtn" />

                                </p>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    var gzoom = 15;
    var marker = null;
    var map = new BMap.Map("map_canvas"); // 创建地图实例
    map.enableScrollWheelZoom();
    var myGeo = new BMap.Geocoder();
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    var initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
    //图标
    var myIcon = new BMap.Icon("../images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
    marker = new BMap.Marker(initpoint, { icon: myIcon });
    map.addOverlay(marker);
    marker.enableDragging();
    marker.setTitle("拖动修改位置");
    map.centerAndZoom(initpoint, gzoom); // 初始化地图，设置中心点坐标和地图级别
    map.addControl(new BMap.NavigationControl());  //缩放工具

    EventWrapper.addListener(map, "click", function (e) {
        map.clearOverlays();
        initpoint = e.point;
        map.removeOverlay(marker);
        marker = new BMap.Marker(initpoint, { icon: myIcon });
        map.addOverlay(marker);
        marker.enableDragging();
        marker.openInfoWindow(infoWindow);
        setLatLng(initpoint);
        marker.addEventListener("dragend", function (e) {
            initpoint = e.point;
            marker = new BMap.Marker(initpoint);
            this.openInfoWindow(infoWindow);
            setLatLng(initpoint);
        });
        marker.addEventListener("dragstart", function (e) {
            map.closeInfoWindow()
        });
    });

    marker.addEventListener("dragend", function (e) {
        initpoint = e.point;
        marker = new BMap.Marker(initpoint);
        this.openInfoWindow(infoWindow);
        setLatLng(initpoint);
    });

    marker.addEventListener("dragstart", function (e) {
        map.closeInfoWindow()
    });

    var opts = {
        width: 300,     // 信息窗口宽度  
        height: 50    // 信息窗口高度
    }

    var winhtml = " <div><p>确定地图位置后，点击按钮“确认位置”进行保存</p>";
    winhtml += "<p style=\" float:right; padding-right:10px;\"><input type=\"button\" value=\"确认位置\" onclick='map.closeInfoWindow()' /></p></div>";


    var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象
    // 打开信息窗口

    function setLatLng(point) {
        document.getElementById("hidLat").value = point.lat;
        document.getElementById("hidLng").value = point.lng;
        return true;
    }

    //地图搜索
    function setPlace() {
        var hfcity = $("#hfcity").val();
        var address = document.getElementById("keyaddress").value.trim();


        var local = new BMap.LocalSearch(hfcity, {
            renderOptions: {
                map: map,
                autoViewport: true,
                selectFirstResult: false
            }
        });
        local.search(address);
    }

    function GetLatLng() {
        showload_super("", "", "dd");
    }
</script>

