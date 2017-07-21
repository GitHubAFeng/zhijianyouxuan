<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopLocal.aspx.cs" Inherits="AreaAdmin_TogoLocal" %>

<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="../Adleft.ascx" TagName="adleft" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家位置信息管理--<%= WebUtility.GetMyName() %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
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

    <script language="javascript" type="text/javascript">

        var gzoom = 15;
        var marker = null;
        var map = null;
        $(window).load(function() { $("#loading-mask").hide(); initmap(); });

        $(document).ready(function() { WhitchActive(2); showContent(1); });      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hidLat" />
    <asp:HiddenField runat="server" ID="hidLng" />
    <asp:HiddenField runat="server" ID="hfcity" />
    <!--加载中显示的div-->
    <div id="loading-mask">
        <p class="loader" id="loading_mask_loader">
            <img src="../images/ajax-loader-tr.gif" alt="加载中..." /><br />
            请等待...</p>
    </div>
    <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
    <div class="wrapper">
        <uc1:TogoBanner runat="server" ID="Banner" />
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


                             <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none">
                                            <li><a href="ShopDetail.aspx?id=<%= Request["tid"] %>" class="tab-item-link ">
                                                <span><span class="changed"></span><span class="error"></span>商家信息</span> </a>
                                            </li>
                                            <li><a href="FoodSortList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单分类</span> </a></li>
                                            <li><a href="FoodList.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>菜单管理</span> </a></li>
                                            <li><a href="Distancepaylist.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>配送距离管理</span> </a></li>
                                            <li><a href="ShopLocal.aspx?tid=<%= Request["tid"] %>" class="tab-item-link active"><span>
                                                <span class="changed"></span><span class="error"></span>地图定位</span> </a></li>

                                             <li><a href="AddPrinter.aspx?tid=<%= Request["tid"] %>" class="tab-item-link "><span>
                                                <span class="changed"></span><span class="error"></span>打印机</span> </a></li>

                                        </ul>


                            <div style="visibility: visible;" class="content-header">
                                <h3 class="icon-head head-customer" runat="server" id="h3content">
                                    商家位置地图标注</h3>
                                <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                    <p style="" class="content-buttons form-buttons">
                                        <asp:Button ID="Button1" runat="server" CssClass="button_1" OnClick="return_Click"
                                            Text="返回"></asp:Button>
                                        <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                            Text="保存位置信息" OnClientClick="GetLatLng();"></asp:Button>
                                    </p>
                                </div>
                            </div>
                            <div class="entry-edit">
                                <div id="customer_info_tabs_account_content" style="">
                                    <div class="entry-edit">
                                        <div class="entry-edit-head">
                                            <h4 class="icon-head head-edit-form fieldset-legend">
                                                商家位置地图标注</h4>
                                            <div class="form-buttons">
                                            </div>
                                        </div>
                                        <div class="fieldset " id="_accountbase_fieldset">
                                            <div class="hor-scroll">
                                                <table class="form-list" cellspacing="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                搜索地址:<epc:TextBox runat="server" ID="keyaddress"></epc:TextBox>
                                                                <input type="button" value="搜索" onclick=" setPlace();"><div class="mynotice">
                                                                    提示：拖动标准修改商家的位置</div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div id="map_canvas" style="width: 98%; height: 400px">
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
    function initmap() {
        map = new BMap.Map("map_canvas"); // 创建地图实例
        map.enableScrollWheelZoom();
        var _lat = parseFloat($("#hidLat").val());
        var _lng = parseFloat($("#hidLng").val());
        var initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
        // 打开信息窗口
        var opts = {
            width: 250,     // 信息窗口宽度  
            height: 50    // 信息窗口高度
        }
        var gzoom = 15;
        var winhtml = " <div><p>确定地图位置后，点击按钮“确认位置”进行保存</p>";
        winhtml += "<p style=\" float:right; padding-right:10px;\"><input type=\"button\" value=\"确认位置\" onclick='map.closeInfoWindow()' /></p></div>";
        var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象
        //图标
        var myIcon = new BMap.Icon("../images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
        marker = new BMap.Marker(initpoint, { icon: myIcon });
        map.addOverlay(marker);
        marker.enableDragging();
        marker.setTitle("拖动修改位置");
        map.centerAndZoom(initpoint, gzoom); // 初始化地图，设置中心点坐标和地图级别
        map.addControl(new BMap.NavigationControl());  //缩放工具

        marker.addEventListener("dragend", function(e) {
            initpoint = e.point;
            marker = new BMap.Marker(initpoint);
            this.openInfoWindow(infoWindow);
            setLatLng(initpoint);
        });

        marker.addEventListener("dragstart", function(e) {
            map.closeInfoWindow()
        });

        EventWrapper.addListener(map, "click", function(e) {
            map.clearOverlays();
            initpoint = e.point;
            map.removeOverlay(marker);
            marker = new BMap.Marker(initpoint, { icon: myIcon });
            map.addOverlay(marker);
            marker.enableDragging();
            marker.openInfoWindow(infoWindow);
            setLatLng(initpoint);

            marker.addEventListener("dragend", function(e) {
                initpoint = e.point;
                marker = new BMap.Marker(initpoint);
                this.openInfoWindow(infoWindow);
                setLatLng(initpoint);
            });
            marker.addEventListener("dragstart", function(e) {
                map.closeInfoWindow()
            });
        });
        // 打开信息窗口
    }
    function setLatLng(point) {
        document.getElementById("hidLat").value = point.lat;
        document.getElementById("hidLng").value = point.lng;
        return true;
    }


    function setPlace() {
        debugger;
        var cityname = $("#hfcity").val() + "市";
        var address = document.getElementById("keyaddress").value;

        var local = new BMap.LocalSearch(cityname, {
            renderOptions: {
                map: map,
                autoViewport: true,
                selectFirstResult: true
            }
        });
        local.search(address);
    }

    function GetLatLng() {
        showload_super("", "", "dd");
    }
</script>

