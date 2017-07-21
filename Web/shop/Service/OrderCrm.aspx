<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderCrm.aspx.cs" Inherits="shopqy_54tss_Admin_GpsSet_OrderCrm" %>

<%@ Register Src="CrmTop.ascx" TagName="TogoBanner" TagPrefix="uc1" %>
<%@ Register Src="CRMLeft.ascx" TagName="left" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>客服系统-<%= WebUtility.GetMyName()%></title>
    <link type="text/css" rel="stylesheet" href="css/indis_style.css" />

    <script src="../../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../../javascript/jCommon.js" type="text/javascript"></script>

    <script src="javascript/togoshoppingcart.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>

    <script src="../../Admin/javascript/eventwrapper.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        /***** 地图数据 ********/
        var gzoom = 15;
        var isfirst = false;
        var marker = null;
        var ac = null;
        var map = null;
        var myGeo = null;
        var initpoint = null;
        var myIcon = null;
        var issearch = 0; //是否搜索地址，如果是否表示地址加载完成后，开始搜索地址
        var opts = {
            width: 250,     // 信息窗口宽度  
            height: 50    // 信息窗口高度
        }

        var winhtml = " <div><p>确定您的的地图位置后，再输入相关信息点击按钮进行保存</p>";
        winhtml += "<p style=\" float:right; padding-right:10px;\"><input type=\"button\" value=\"确认位置\" onclick='addressOK();' /></p></div>";

        var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象

        var islocal = 0; //1表示已经定位.

        $(document).ready(function () {

            mapinit();
            $(".submit_order_btn").hide();
        })

        function goto_myshop(url) {
            var hasaddress = handlecookie("used_addressid");
            if (hasaddress == null || hasaddress == "" || hasaddress == "0") {
                alert("请先添加地址");
            }
            else {
                window.location = url;
            }
            return false;
        }
    </script>

</head>
<body>
    <form runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hidUid" Value="-2" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="wrap">
            <uc1:TogoBanner runat="server" ID="Banner" />
            <div class="infaq_con">
                <uc1:left runat="server" ID="left" />
                <div class="indis_right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="indis_right_top" id="shopdiv">
                                <div class="infaq_list_title">
                                    <div class="infaq_list_left" style="height: 50px; _padding-top: 10px; display: none">
                                        <strong>选择分类：</strong>
                                        <asp:DropDownList ID="ddlsection" runat="server" Width="100px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="-1">选择分类</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="infaq_list_right">
                                            <input name="" type="text" runat="server" id="tbshopkey" value="请输入商家名称" onfocus="if(value=='请输入商家名称') {value=''}"
                                                onblur="if (value=='') {value='请输入商家名称'}" class="infaq_list_text" />
                                            <asp:Button runat="server" ID="tbsearch" OnClick="search_Click" class="infaq_list_btn"
                                                Text="搜索" />
                                        </div>
                                    </div>
                                </div>
                                <div class="infaq_list_con">
                                    <ul>
                                        <asp:Repeater runat="server" ID="rptJoinTogolist">
                                            <ItemTemplate>
                                                <li><a href="javascript:" title="<%# Eval("name") %>" onclick="goto_myshop('showMenu.aspx?id=<%# Eval("unid") %>&tel=<%= Request["tel"] %>');return false;">
                                                    <%# WebUtility.Left(Eval("name"), 12)%></a> </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    <div class="pages">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="27%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="30" SubmitButtonClass="flatbutton" SubmitButtonText="GO"
                                            TextAfterPageIndexBox=" 页 " Wrap="False">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%----%>
                    <div class="indis_right_top" runat="server" id="mapdiv" style="visibility: hidden;">
                        <div style="padding-left: 20px; margin: 10px;">
                            <a class="infaq_address_sure_btn" href="javascript:hide_map()" style="float: right; padding-right: 10px;">关闭地图</a>
                            <div class="mynotice">
                                提示：拖动标准修改用户的位置
                            </div>
                        </div>
                        <div style="margin: 10px;">
                            <div id="map_canvas" style="width: 100%; height: 400px">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script src="javascript/map.js?v=0824" type="text/javascript"></script>

