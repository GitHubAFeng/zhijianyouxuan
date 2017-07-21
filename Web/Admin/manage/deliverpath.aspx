<%@ Page Language="C#" AutoEventWireup="true" CodeFile="deliverpath.aspx.cs" Inherits="EasyEatHome_MTogo_deliverpath" %>

<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="../Adleft.ascx" TagName="adleft" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配送轨迹--<%= WebUtility.GetMyName() %></title>
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

    <script type="text/javascript" src="../javascript/DatePicker/WdatePicker.js"></script>
    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>
    <script src="../javascript/spin.min.js"></script>

    <script language="javascript" type="text/javascript">

        var gzoom = 13;
        var marker = null;
        var map = null;
        $(window).load(function () { $("#loading-mask").hide(); initmap(); });

        $(document).ready(function () { initmap(); });
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
                请等待...
            </p>
        </div>
        <epc:Hint ID="Hint1" runat="server" HintImageUrl="../images/Control" />
        <div class="wrapper">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="middle" id="anchor-content">
                <div id="page:main-container">
                    <div class="columns ">
                       
                        <div class="main-col" id="content" style="margin-left:0; padding-left:0;">
                            <div class="main-col-inner">
                                <div id="messages">
                                </div>

                                <ul id="diagram_tab" class="tabs-horiz" style="border-bottom: none" runat="server">
                                    <li><a href="javascript:" id="diagram_tab_orders" class="tab-item-link " runat="server">
                                        <span><span class="changed"></span><span class="error"></span>基本信息</span> </a>
                                    </li>

                                    <li><a class="tab-item-link active" href="javascript:"><span>
                                        <span class="changed"></span><span class="error"></span>配送轨迹</span> </a></li>

                                </ul>




                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-edit-form fieldset-legend">配送轨迹</h4>
                                                <div class="form-buttons">
                                                </div>
                                            </div>
                                            <div class="fieldset " id="_accountbase_fieldset">
                                                <div class="hor-scroll">
                                                    <table class="form-list" cellspacing="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>日期：<epc:TextBox runat="server" ID="tbdate" onfocus="WdatePicker({readOnly:true})" Width="100"></epc:TextBox>
                                                                    <input type="button" id="btsearch" value="搜索" onclick="searchdata();">


                                                                     <div class="mynotice">提示：选择日期，点击搜索，可查看某日数据</div>

                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <div id="map_canvas" style="width: 98%; height: 500px">
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

        map.centerAndZoom(initpoint, gzoom); // 初始化地图，设置中心点坐标和地图级别
        map.addControl(new BMap.NavigationControl());  //缩放工具

        var hfcity = $("#hfcity").val();
        map.centerAndZoom(hfcity, gzoom); // 初始化地图，设置中心点坐标和地图级别


    }
    function setLatLng(point) {
        document.getElementById("hidLat").value = point.lat;
        document.getElementById("hidLng").value = point.lng;
        return true;
    }


    var car;   //汽车图标
    var label; //信息标签
    var centerPoint;

    var timer;     //定时器
    var index = 0; //记录播放到第几个point

    ///查询数据
    function searchdata() {

        Loader.show("#btsearch");
        var tid = request("tid");
        var tbdate = $("#tbdate").val();

        jQuery.ajax(
        {
            type: "post",
            url: "/ajaxHandler.ashx",
            data: 'method=getdeliverpath&tid=' + tid + '&tbdate=' + tbdate,
            success: function (msg) {
                var paths = JSON.parse(msg);
                Loader.hide();
                var myIcon = new BMap.Icon("/Admin/images/mm_20_red.png", new BMap.Size(14, 22), { anchor: new BMap.Size(7, 0) });

                for (var i = 0; i < paths.length; i++) {
                    var marker = new BMap.Marker(new BMap.Point(paths[i].lng, paths[i].lat), { icon: myIcon });
                    map.addOverlay(marker);
                    marker.setTitle(paths[i].bear);

                 

                }

            }
        });

    }




</script>

