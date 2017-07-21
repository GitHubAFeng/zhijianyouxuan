<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuildingDetail.aspx.cs" Inherits="qy_54tss_AreaAdmin_BuildingDetail" EnableEventValidation="false" %>

<%@ Register Src="~/AreaAdmin/Banner.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="~/AreaAdmin/Foot.ascx" TagName="Foot" TagPrefix="uc2" %>
<%@ Register Src="~/AreaAdmin/Adleft.ascx" TagName="left" TagPrefix="uc3" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>地标管理-<%= WebUtility.GetMyName() %></title>
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/menu.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxes.css" rel="stylesheet" type="text/css" />
    <link href="../css/Validator.css" rel="stylesheet" type="text/css" />
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/Common.css" rel="stylesheet" type="text/css" />
    <link href="../css/building.css" rel="stylesheet" type="text/css" />
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

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="../javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>

    <script src="../javascript/city_section.js" type="text/javascript"></script>

    <script src="../javascript/QueryChina.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">

        $(window).load(function () { $("#loading-mask").hide(); });
        function reset() {
            var list = document.getElementsByName("invalue");
            for (var i = 0; i < list.length; i++) {
                list.item(i).value = "";
            }
        }
        $(document).ready(function () {
            WhitchActive(6);
            initmap();
            var id = Request("id");
            if (id != null && id != "") {
                sectionchange(2);
            }
        });

        //给一个输入框赋值
        function SetValue(objectName, objectValue) {
            $("#" + objectName + "").val(objectValue);
        }

        function changecity() {
            var cityid = $("#DDLArea").val() + "";
            var cityname = $("#DDLArea option:selected").text() + "";
            map.centerAndZoom(cityname, gzoom);

        }
    </script>

    <script language="javascript" type="text/javascript">

        function GotoBuildingDetail() {
            window.location.href = "BuildingDetail.aspx";
        }
        function GotoBuildingList() {
            window.location.href = "Building.aspx";
        }


        function getfirst() {
            var str = document.getElementById("tbName").value.trim();
            if (str == "") return;
            var arrRslt = makePy(str);
            var dd = arrRslt[0] + "";
            $("#tbFirstL").val(dd.substr(0, 1));

        }
    </script>


    <style type="text/css">
        .form-list td.label {
            width: 100px;
        }

        #loading-mask-1 {
            position: absolute;
            color: #d85909;
            font-size: 1.1em;
            font-weight: bold;
            text-align: center;
            opacity: 0.80;
            z-index: 500;
        }

            #loading-mask-1 .loader {
                position: fixed;
                top: 45%;
                left: 50%;
                width: 120px;
                margin-left: -60px;
                padding: 15px 60px;
                background: #fff4e9;
                border: 2px solid #f1af73;
                color: #d85909;
                font-weight: bold;
                text-align: center;
                z-index: 1000;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hflatlngs" />
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
                                            <button type="button" class="scalable back" onclick='GotoBuildingList()' style="">
                                                <span>返回列表</span></button>
                                            <asp:Button ID="btSave" runat="server" CssClass="button_1" OnClick="btSave_Click"
                                                Text="保存"></asp:Button>
                                        </p>
                                    </div>

                                </div>
                                <!--start-->
                                <div class="entry-edit">
                                    <div id="customer_info_tabs_account_content" style="">
                                        <div class="entry-edit">
                                            <div class="entry-edit-head">
                                                <h4 class="icon-head head-billing-address">热门地标</h4>
                                            </div>
                                            <fieldset class="np">
                                                <div class="order-address" id="order-billing_address_fields">
                                                    <div class="content">
                                                        <div class="hor-scroll">
                                                            <table class="form-list" cellspacing="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 40px;">名称<span class="required">*</span>
                                                                            <asp:HiddenField runat="server" ID="hidTogoId" />
                                                                            <asp:HiddenField runat="server" ID="hidDataId" />
                                                                        </td>
                                                                        <td style="width: 60px;">
                                                                            <epc:TextBox runat="server" ID="tbName" name="invalue" CanBeNull="必填" Width="200px"
                                                                                class=" required-entry required-entry input-text" onblur="getfirst()"></epc:TextBox>

                                                                            <asp:DropDownList ID="ddlIsShow" runat="server" >
                                                                                <asp:ListItem Value="-1">是否热门</asp:ListItem>
                                                                                <asp:ListItem Value="0">否</asp:ListItem>
                                                                                <asp:ListItem Value="1">是</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                        <td style="width: 40px;">首字母 <span class="required">*</span>
                                                                        </td>
                                                                        <td style="width: 60px;">
                                                                            <epc:TextBox runat="server" name="invalue" ID="tbFirstL" CanBeNull="必填" Width="100px"
                                                                                class=" required-entry required-entry input-text"></epc:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 40px;">城市 <span class="required">*</span>
                                                                        </td>
                                                                        <td style="width: 60px;">

                                                                            <asp:DropDownList ID="DDLArea" runat="server" Width="70" class=" required-entry required-entry input-text" onchange="changecity();" AppendDataBoundItems="true">
                                                                                <asp:ListItem Value="-1">选择城市</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 40px;">排序 <span class="required">*</span>
                                                                        </td>
                                                                        <td style="width: 60px;">
                                                                            <epc:TextBox runat="server" name="invalue" ID="tbtype" Text="1" CanBeNull="必填" RequiredFieldType="数据校验"
                                                                                Width="60px" class=" required-entry required-entry input-text"></epc:TextBox>数字大，排在前。
                                                                        </td>

                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <div style="margin-top: 10px;">
                                                                搜索区域/路/村/镇<asp:TextBox runat="server" ID="keyaddress" CssClass="j_text"></asp:TextBox>
                                                                <input type="button" value="搜索" onclick="return setPlace();">
                                                                <span style="color: Red;">提示：拖动地标，确定区域位置</span>
                                                                <div id="map_canvas" style="width: 98%; height: 500px; margin-top: 10px;">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
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
    var gzoom = 15;
    var marker = null;
    var map = null;

    function initmap() {
        map = new BMap.Map("map_canvas"); // 创建地图实例
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

        //map.addEventListener("click", mapclick);
        //EventWrapper.addListener(map, "click", mapclick);

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
    }
    var opts = {
        width: 250,     // 信息窗口宽度  
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


    function setPlace() {
        var cityname = $("#DDLArea option:selected").text() + "";
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
</script>

