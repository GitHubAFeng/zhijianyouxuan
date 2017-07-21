<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyshopLocal.aspx.cs" Inherits="shop_MyshopLocal" %>

<%@ Register Src="~/shop/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商家基本信息-<%= SectionProxyData.GetSetValue(3)%></title>
    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/time.css" rel="stylesheet" type="text/css" />
    <link href="Style/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />



    <script src="../Admin/javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../Admin/javascript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../Admin/javascript/time.js" type="text/javascript"></script>

    <script src="../javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="http://ditu.google.cn/maps?file=api&amp;v=2&amp;key=ABQIAAAAGFtDHTuhIgOEKsGezSD4bxTgxtR5kgGCkqJhf2lfyyWNySqUnxSITnNC1go39q3wvQt9wshW55NXCQ"
        type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript">
        var marker = null;
        var map = null;
        var center = null;
        var geocoder = null;
        var gzoom = 13;

        function showAddress(address) {
            var temp_address = "北京市" + address;
            //setMsg("");
            map.clearOverlays();
            if (geocoder) {
                geocoder.getLatLng(
	     temp_address,
	     function (point) {
	         if (!point) {
	             setMsg("没找到对应地址,请在地图上点击所在位置!");
	         }
	         else if (point.x == 116.324966 && point.y == 40.083198)//24.48116319730152,118.17992091178894
	         {
	             setMsg("没找到对应地址,请在地图上点击所在位置!");
	         }
	         else {
	             map.setCenter(point, gzoom);

	             marker.setLatLng(point);
	             map.addOverlay(marker);
	             GetLatLng();
	         }
	     }
	   );
            }
        }
        function searchshop() {
            showAddress(document.getElementById("keyaddress").value);
            return false;
        }

        var marker = null;
        var map = null;
        var center = null;


        //获取标注点的经纬度
        function GetLatLng() {
            document.getElementById("hidLat").value = marker.getLatLng().lat();
            document.getElementById("hidLng").value = marker.getLatLng().lng();
            return true;
        }

        function setMsg(msg) {
            alert(msg);
        }
    </script>

    <style type="text/css">
        .left_span {
            width: 100px;
        }

        #cblweekday {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hidBuilding" />
        <div class="warp">
            <div class="center_bg">
                <div id="page:main-container">
                    <div class="columns ">
                        <uc2:LeftBanner ID="Left" runat="server" />
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="rightmenu_cont">
                                <h1 class="topbg">商家位置地图标注</h1>
                                <div class="usermima">
                                    <div style="visibility: visible;" class="content-header">

                                        <div style="width: 100%; height: 17px; padding-top: -1px;">
                                            <p style="text-align: right" class="">

                                                <asp:Button ID="btSave" runat="server" class="subBtn" OnClick="btSave_Click"
                                                    Text="保存位置信息" OnClientClick="return GetLatLng();"></asp:Button>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="entry-edit">
                                        <div id="customer_info_tabs_account_content" style="">
                                            <div class="entry-edit">
                                                <div class="entry-edit-head">
                                                    <h4 class="icon-head head-edit-form fieldset-legend">商家位置地图标注</h4>
                                                    <div class="form-buttons">
                                                    </div>
                                                </div>
                                                <div class="fieldset " id="_accountbase_fieldset">
                                                    <div class="hor-scroll">
                                                        <table class="form-list" cellspacing="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>搜素写字楼/小区<asp:TextBox runat="server" ID="keyaddress"></asp:TextBox>
                                                                        <input type="submit" value="搜索" onclick="return searchshop();">
                                                                    </td>
                                                                </tr>

                                                            </tbody>
                                                        </table>
                                                        <div id="map_canvas" style="width: 90%; height: 400px">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
