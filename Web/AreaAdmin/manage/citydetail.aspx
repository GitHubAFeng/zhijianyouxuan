<%@ Page Language="C#" AutoEventWireup="true" CodeFile="citydetail.aspx.cs" Inherits="qy_54tss_AreaAdmin_manage_citydetail" %>

<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Src="../Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="../Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>城市分站管理-<%= WebUtility.GetMyName()%></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/QueryChina.js" type="text/javascript"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>


    <script language="javascript" type="text/javascript">
        $(window).load(function () { $("#loading-mask").hide(); initmap();});

        function queryCity() {
            var str = document.getElementById("tbcname").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            $("#tbReveVar").val(arrRslt[0].substr(0, 1));
        }

    </script>

    <style type="text/css">
        .style1 {
            width: 85px;
            height: 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcity" Value="全国" />


        <asp:HiddenField ID="hfcid" Value="0" runat="server" />
        <asp:HiddenField ID="hfadddate" runat="server" />
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
                            <uc3:left runat="server" ID="left" />
                        </div>
                        <div class="main-col" id="content">
                            <div class="main-col-inner">
                                <div id="divMessages">
                                </div>
                                <div style="visibility: visible;" class="content-header">
                                    <h3 class="icon-head head-customer">
                                        <asp:Label runat="server" ID="pageType"></asp:Label></h3>
                                    <div style="width: 0px; height: 17px;" class="content-buttons-placeholder">
                                        <p style="" class="content-buttons form-buttons">
                                            <div style="float: right; height: 25px; margin-top: -1px">
                                                <input type="button" value="返回" onclick="window.location = 'citylist.aspx'" class="form-button" />
                                                <asp:Button ID="btSave" runat="server" CssClass="form-button" OnClick="btSave_Click"
                                                    Text="提交"></asp:Button>
                                            </div>
                                        </p>
                                    </div>
                                </div>
                                <!--start-->
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">城市分站管理</h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address" id="order-billing_address_fields">
                                                    <div class="content">
                                                        <div class="hor-scroll" style="overflow: auto; height: auto">
                                                            <table class="form-list" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountwebsite_id">
                                                                                城市 <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <epc:TextBox runat="server" ID="tbcname" onblur="queryCity();" CanBeNull="必填" Width="160"></epc:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountwebsite_id">
                                                                                首字母 <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <epc:TextBox runat="server" ID="tbReveVar" CanBeNull="必填" Width="60"></epc:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="label">
                                                                            <label for="_accountwebsite_id">
                                                                                排序 <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value" style="width: 500px;">
                                                                            <epc:TextBox runat="server" ID="tbReveInt" CanBeNull="必填" Width="60"></epc:TextBox><div class="mynotice">提示：数字大，排序在前</div>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td class="label">
                                                                            <label for="_accountfirstname">
                                                                                logo <span class="required">*</span></label>
                                                                        </td>
                                                                        <td class="value">
                                                                            <asp:HiddenField ID="ImgUrl1" runat="server" Value="" />
                                                                            <asp:HiddenField ID="FolderType" runat="server" Value="1" />
                                                                            <asp:HiddenField ID="WaterType" runat="server" Value="0" />
                                                                            &nbsp; <a href="" id="txturldz" target="_blank">
                                                                                <img border="0" src="../images/System/wutu1.gif" id="ImgUrl" alt="" style="width: 78px; height: 78px"
                                                                                    runat="server" /></a><br />
                                                                            <input id="txtupload" type="button" value="上传" onclick="return document.getElementById('rowTest').style.display = 'block';" />请上传78*78的图片<br />
                                                                            <div id="rowTest" style="display: none">
                                                                                <iframe name="tag" src="../upfile/Upload.html?Links" style="width: 550px; height: 130px"
                                                                                    frameborder="0" scrolling="no" onload="this.height=document.body.scrollHeight"></iframe>
                                                                            </div>
                                                                            <div id="Upload">
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <small>&nbsp;</small>
                                                                        </td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>


                                 <div class="entry-edit">
                                <div id="customer_info_tabs_account_content1" style="">
                                    <div class="entry-edit">
                                        <div class="entry-edit-head">
                                            <h4 class="icon-head head-edit-form fieldset-legend">
                                                设置前台默认位置</h4>
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
                                                                    提示：拖动标准修改默认位置</div>
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

                                <!--end-->
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

        marker.addEventListener("dragend", function (e) {
            initpoint = e.point;
            marker = new BMap.Marker(initpoint);
            this.openInfoWindow(infoWindow);
            setLatLng(initpoint);
        });

        marker.addEventListener("dragstart", function (e) {
            map.closeInfoWindow()
        });

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

