<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDelive.aspx.cs" Inherits="qy_54tss_AreaAdmin_GpsSet_OrderDelive"
    EnableEventValidation="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单调度系统-<%= WebUtility.GetMyName() %></title>
    <link href="css/indis_style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/building.css" rel="stylesheet" type="text/css" />--%>
    <link href="../css/divDialog.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2"></script>

    <script src="../javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="../javascript/Common.js" type="text/javascript"></script>

    <script src="javascript/orderdeliver.js?v=1224" type="text/javascript"></script>

    <script src="../../javascript/jCommon.js" type="text/javascript"></script>

    <script src="../../javascript/jquery.floatDiv.js" type="text/javascript"></script>
    <script src="/AreaAdmin/javascript/soundmanager2.js" type="text/javascript"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            soundManager.debugMode = false;
            soundManager.debugFlash = false;
            soundManager.url = "soundmanager2.swf";
            $(".indis_left").css("height", "538px");
            pageinit();
            //每分钟提醒
            Tellmsg();
            setInterval("Tellmsg()", 60000);

            getsuminfo();
            setInterval("getsuminfo()", 60000);

            var id = $("#hfflag").val();
            
            
            delayURL();
        })

        function init() {
            $(".indis_order_list_table tr").mouseover(function () { $(this).addClass("on-mouse"); }).mouseout(function () { $(this).removeClass("on-mouse") });

            $(".indis_order_list_table tr").click(function () { $(".indis_order_list_table tr").removeClass("invalid"); $(this).addClass("invalid"); })

            $(".indis_order_list_table tr:even").addClass("even pointer");
            $(".indis_order_list_table tr").addClass(" pointer");
        }

        function pageinit() {
            init();
            hideload_super('dddd');
        }

        function serchorder() {
            var key = $("#tbkeyword").val() + "";
            if (key == "" || key == "可按照电话、姓名、订单号查询") {
                alert("可按照电话、姓名、订单号查询");
                return false;
            }
            return true;
        }

        function delayURL() {
            var timebox = $("#time");
            var delay = parseInt(timebox.html());
            if (delay > 0) {
                delay--;
                timebox.html(delay + "");
            }

            setTimeout(delayURL, 1000);
        }

        //通知事件
        function play(flag) {
            $("#time").html(30);
            if (flag == 0) {
                return;
            }
            soundManager.play('mySound1', 'notify.mp3');
        }

        function settable(name, cursel, n, css, defaut) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                if (i == cursel) {
                    menu.className = css;
                    $("#" + name + "_div" + i).show();
                }
                else {
                    if (typeof defaut != 'undefined') {
                        menu.className = defaut;
                    }
                    else {
                        menu.className = "";
                    }
                    $("#" + name + "_div" + i).hide();
                }
            }
        }
    </script>

    <style type="text/css">
     
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />
        <asp:HiddenField runat="server" ID="hfflag" Value="0" />
        <asp:HiddenField runat="server" ID="hf_curr_dids" />
        <asp:HiddenField runat="server" ID="hforderuserlatlng" />
        <asp:HiddenField runat="server" ID="hfexpressororder" Value="0" />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="wrap">
            <div class="indis_top">
                <a href="../../index.aspx" class="float_l padding_l5">
                    <img src="../../images/logo.png" style="height: 75px;" />
                </a>
                <span class="top_title">订单调度系统</span>
                <span class="fa margin_t3"> 操作员：<label runat="server" id="lbadminname"></label></span>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <span class="search_hujiao">
                            <input name="" type="text" class="search_hujiao_text" value="可按照电话、姓名、订单号查询" onfocus="if(value=='可按照电话、姓名、订单号查询') {value='';}"
                                onblur="if (value=='') {value='可按照电话、姓名、订单号查询'}" runat="server" id="tbkeyword"
                                style="width: 160px;" />
                            <asp:DropDownList ID="ddlmysecion" runat="server" Width="80px" AppendDataBoundItems="true"
                                CssClass="j_seclect" Style="float: left; margin-left: 5px; display: none;">
                                <asp:ListItem Value="0">所有区域</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlstate" runat="server" Width="80px" CssClass="j_seclect" AppendDataBoundItems="true"
                                Style="float: left; margin-left: 5px;">
                                <asp:ListItem Value="0">所有状态</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddldeliverfix" runat="server" Width="90px" CssClass="j_seclect"
                                Style="float: left; margin-left: 5px;" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">所有配送员</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button runat="server" ID="Button2" class="my_btn" Text="搜索" OnClick="search_click" />
                            <asp:Button runat="server" ID="Button3" class="my_btn" Text="所有订单" OnClientClick="gourl('OrderDelive.aspx')" />

                            <span style="padding-top: 0; margin-left: 10px;">
                                <label id="time" style="color: Red; font-size: 23px;">
                                    30</label>秒后自动刷新</span>

                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--<p>
                    <label runat="server" id="snDate">
                    </label>
                </p>--%>
                <p>
                    <a href="../basic.aspx">返回后台</a>
                </p>
            </div>
            <div class="indis_con">
                <div class="indis_left" style="overflow-y: scroll;">
                    <h4>配送骑士状态信息[<span><a href="javascript:void(0)" onclick="LoadDeliver(9)" style="font-size: 12px; color: red;">刷新</a></span>]</h4>
                    <div style="margin: 10px;">
                    </div>
                    <div id="divDeliver" style="min-height: 200px;">
                    </div>
                    <div id="loading-divDeliver" style="height: 200px; text-align: center;">
                        <p class="loader" id="loading_mask_loader">
                            <img src="../images/ajax-loader-tr.gif" alt="搜索中..." /><br />
                            数据加载中...
                        </p>
                    </div>

                </div>
                <div class="indis_right">
                    <div class="indis_right_top">
                        <div class="indis_right_map">
                            <div id="map_canvas" style="width: 100%; height: 538px">
                            </div>
                        </div>
                        <div class="indis_order_detail">
                            <div class="indis_order_detail_con">
                                <p>
                                    配送人员：<asp:DropDownList ID="ddldeliver" runat="server" Width="100px" CssClass="j_seclect"
                                        onchange="getsubsort();">
                                        <asp:ListItem Value="0">选择配送人员</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    群组：<asp:DropDownList ID="ddlgroup" runat="server" Width="100px" CssClass="j_seclect"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">选择群组</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                                <asp:HiddenField runat="server" ID="hfdid_hh" Value="0" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <p>
                                            <asp:HiddenField runat="server" ID="hfstate" Value="0" />
                                            <asp:HiddenField runat="server" ID="hfordersection" Value="0" />
                                            <asp:HiddenField runat="server" ID="hforderid" Value="" />
                                            <textarea id="tborderinfo" class="order_detail_textarea" runat="server" style="height: 200px; display:none"></textarea>
                                        </p>
                                        <p style="text-align: right;">
                                            <asp:Button runat="server" ID="Button4" class="my_btn" Text="发群" OnClick="sendgroup_click"
                                                OnClientClick="return checksendgroup()" />
                                            <asp:Button runat="server" ID="tbsearch" class="my_btn" Text="发人" OnClick="deliver_click"
                                                OnClientClick="return checkdeliver()" />
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="clear">
                                </div>
                                <div class="indis_state">
                                    <div id="ul_sum">
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
                </asp:Timer>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>

                        <div style="margin-bottom: 15px; height: 30px;">
                            <div class="hinfo_top_left_ds" id="shop_menu_1" onclick="settable('shop_menu_',1,2,'hinfo_top_left_ds','hinfo_top_left')">外卖订单</div>
                            <div  class="hinfo_top_left" id="shop_menu_2" onclick="settable('shop_menu_',2,2,'hinfo_top_left_ds','hinfo_top_left')">跑腿订单</div>
                        </div>

                        <div id="shop_menu__div1">
                            <div class="indis_order_list">
                                <table class="indis_order_list_table" id="indis_order_list_table" width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="2%">&nbsp;
                                        </th>
                                        <th width="6%">订单号
                                        </th>
                                        <th width="10%">状态-骑士-商家
                                        </th>
                                        <th width="6%">付款
                                        </th>
                                        <th width="6%">商家
                                        </th>
                                        <th width="5%">客户
                                        </th>
                                        <th width="7%">联系电话
                                        </th>
                                        <th>地址
                                        </th>
                                        <th width="5%">小计
                                        </th>
                                        <th width="5%">备注
                                        </th>
                                        <th width="5%">送达时间
                                        </th>
                                        <th width="5%">接单
                                        </th>
                                        <th width="4%">处理
                                        </th>
                                        <th width="5%">骑士
                                        </th>

                                        <th width="4%">调度
                                        </th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptOrderList">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# (Container.ItemIndex+1).ToString() %>
                                                </td>
                                                <td>
                                                    <label id="latlng_<%# Eval("OrderId") %>" style="display: none;">
                                                        <%# Eval("ReveVar2")%></label>
                                                    <a href="javascript:" onclick="showorderlocal(<%# Eval("OrderStatus") %>,0,<%# Eval("DeliveInfo.DeliverId") %>,'<%# Eval("orderid") %>','<%# Eval("OrderRcver") %>','<%# Eval("AddressText") %>','<%# Eval("togoname") %>','0')">
                                                        <%# Eval("OrderId") %></a>
                                                </td>
                                                <td>
                                                    <%#WebUtility.TurnOrderState(Eval("OrderStatus").ToString())%>-<%#WebUtility.TurnOrderSendState(Eval("sendstate").ToString(),Eval("IsShopSet").ToString())%>-<%# Hangjing.WebCommon.WebHelper.TurnOrderReceiveState(Eval("isshopset")) %>
                                                </td>
                                                <td>
                                                    <%# Eval("PayState").ToString()=="1"?"已付":"未付" %>[<%# WebUtility.TurnPayModel(Eval("PayMode").ToString())%>]
                                                </td>
                                                <td title="<%# Eval("TogoName")%>">
                                                    <%#WebUtility.Left(Eval("TogoName"), 4)%>
                                                </td>
                                                <td>
                                                    <%#  Eval("OrderRcver")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderComm")%>
                                                </td>
                                                <td title="<%# Eval("AddressText")%>">
                                                    <%#WebUtility.Left(Eval("AddressText"), 10)%>
                                                </td>
                                                <td>
                                                    <%# Eval("OrderSums")%>
                                                </td>
                                                <td title="<%# Eval("OrderAttach")%>">
                                                    <%#WebUtility.Left(Eval("OrderAttach"), 5)%>&nbsp;
                                                </td>
                                                <td>
                                                    <%# Convert.ToDateTime(Eval("sendtime")).ToString("MM-dd HH:mm")%>
                                                </td>
                                                <td>
                                                    <%# Convert.ToDateTime(Eval("DeliveInfo.DispatchTime").ToString()).ToString("HH:mm")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DeliveInfo.Dispatcher")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DeliveInfo.DeliverName")%>
                                                </td>

                                                <td>
                                                    <input type="button" value="调度" onclick="showorderlocal(<%# Eval("OrderStatus") %>,0,<%# Eval("DeliveInfo.DeliverId") %>,'<%# Eval("orderid") %>    ','<%# Eval("OrderRcver") %>    ','<%# Eval("AddressText") %>    ','<%# Eval("togoname") %>    ','0')" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                    PageSize="15" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
                                    Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>

                        <div id="shop_menu__div2" style="display: none">
                            <div class="indis_order_list">
                                <table class="indis_order_list_table" id="indis_order_list_table2" width="100%" border="0"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="2%">&nbsp;
                                        </th>
                                        <th width="6%">订单号
                                        </th>
                                        <th width="5%">状态
                                        </th>
                                        <th width="5%">商品
                                        </th>
                                        <th width="3%">发件联系人
                                        </th>
                                        <th width="6%">发件联系电话
                                        </th>
                                        <th width="5%">发件地址
                                        </th>
                                        <th width="7%">收件联系人
                                        </th>
                                        <th width="6%">收件联系电话
                                        </th>
                                        <th width="5%">收件联系地址
                                        </th>
                                        <th width="5%">取件时间
                                        </th>
                                        <th width="4%">备注
                                        </th>
                                        <th width="4%">服务费用
                                        </th>
                                        <th width="5%">接单
                                        </th>
                                        <th width="4%">处理
                                        </th>
                                        <th width="5%">骑士
                                        </th>
                                        <th width="5%">配送时间(分)
                                        </th>
                                        <th width="5%">来源
                                        </th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptExpressList">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# (Container.ItemIndex+1).ToString() %>
                                                </td>
                                                <td>
                                                    <label id="latlng_<%# Eval("OrderId") %>" style="display: none;">
                                                        <%#Eval("sitelat") %>
                                                    </label>
                                                    <a href="javascript:" onclick="showorderexpress(<%# Eval("State") %>,0,<%# Eval("DeliveInfo.DeliverId") %>,'<%# Eval("orderid") %>','<%# Eval("callmsg") %>','<%# Eval("Oorderid") %>','<%# Eval("UserName") %>','0')">
                                                        <%# Eval("OrderId") %></a>
                                                </td>
                                                <td>
                                                    <%# WebUtility.TurnExpressOrderState(Eval("state"))%>
                                                </td>
                                                <td>
                                                    <%#  Eval("Inve2")%>  
                                                </td>
                                                <td>
                                                    <%#  Eval("UserName")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("Tel")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("Address")%>
                                                </td>

                                                <td>
                                                    <%#  Eval("callmsg")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("ReveVar")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("Oorderid")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("SentTime")%>                                                   
                                                </td>

                                                <td>
                                                    <%#  Eval("Remark")%>
                                                </td>
                                                <td>
                                                    <%#  Eval("TotalPrice")%>
                                                </td>
                                                <td>
                                                    <%# Convert.ToDateTime(Eval("DeliveInfo.DispatchTime").ToString()).ToString("HH:mm")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DeliveInfo.Dispatcher")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DeliveInfo.DeliverName")%>
                                                </td>
                                                <td>
                                                    <%#getdiffdate(Eval("DeliveInfo.DeliveryTime"), Eval("DeliveInfo.DispatchTime"))%>
                                                </td>
                                                <td>
                                                    <%#Eval("ordersource")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div class="pages">
                                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager2" runat="server" Width="100%"
                                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                    HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                    TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                                    PageSize="2" SubmitButtonClass="flatbutton" SubmitButtonText="GO " TextAfterPageIndexBox=" 页 "
                                    Wrap="False" OnPageChanged="AspNetPager2_PageChanged">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <script type="text/javascript">
            function loading() {
                $("#loading-divDeliver").show();
            }

            function loadover() {
                $("#loading-divDeliver").hide();
            }


            function LoadDeliver(type) {
                var hfcityid = $("#hfcityid").val();
                var sname = $("#ddlsection option:selected").text();
                $.ajax({
                    type: "get",
                    url: "../ajax/GetDeliverList.aspx",
                    cache: false,
                    dataType: "html",
                    data: "type=" + type + "&sname=" + escape(sname) + "&cid=" + hfcityid,
                    beforeSend: function (XMLHttpRequest) {
                        loading();
                    },
                    success: function (data, textStatus) {
                        //处理数据
                        $("#divDeliver").html(data);

                        loadover();

                        //显示所有配送员
                        var deliverlist = eval("(" + $("#deliverlistjson").val() + ")");
                        map.clearOverlays();
                        for (var i = 0; i < deliverlist.length; i++) {
                            var d_point = new BMap.Point(deliverlist[i].Lng, deliverlist[i].Lat); // 创建点坐标
                            var d_marker = new BMap.Marker(d_point);
                            var deliverlabel = new BMap.Label(deliverlist[i].Name + "(" + deliverlist[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                            deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                            d_marker.setLabel(deliverlabel);
                            d_marker.setTitle(deliverlist[i].DataId);
                            map.addOverlay(d_marker);

                            d_marker.addEventListener("click", function (e) {

                                showDeliverBox(this.getTitle(), this);
                            });
                        }

                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        //$("#loading").html("加载完成");
                    },
                    error: function () {
                        //请求出错处理
                    }

                });

                return false;

            }


        </script>

        <script type="text/javascript">
            var gzoom = 17;
            var marker = null;
            var markerlist = new Array();
            var map = new BMap.Map("map_canvas"); // 创建地图实例
            map.enableScrollWheelZoom();
            var myGeo = new BMap.Geocoder();
            var _lat = parseFloat($("#hidLat").val());
            var _lng = parseFloat($("#hidLng").val());
            var initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
            //图标
            var myIcon = new BMap.Icon("http://www.ihangjing.com/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });

            var cityname = $("#hfcityname").val();
            map.centerAndZoom(cityname, gzoom);

            map.addControl(new BMap.NavigationControl());  //缩放工具

            function setLatLng(point) {
                document.getElementById("hidLat").value = point.lat;
                document.getElementById("hidLng").value = point.lng;
                return true;
            }

            function showorder(shopid, orderid) {
                var sheight = window.screen.height - 70;
                var swidth = window.screen.width - 10;

                var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
                var url = "updateorder.aspx?id=" + shopid + "&oid=" + orderid;
                window.open(url, "newwindow", winoption);
            }


            function j_ShowWindow(msg) {
                var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
                innerHTML += "<div style='float:left;font-size:12px;color:#fff'>温馨提醒</div>";
                innerHTML += "<div style='float:right;'>";
                innerHTML += "<a href='javascript:HiddenWindow();'><img src='/Images/window_close.gif' alt='关闭窗口' /> </a>";
                innerHTML += "</div></div>";
                innerHTML += "<div style='text-align:left;font-size:12px;width:95%;overflow:hidden;padding:5px; padding-left:10px;' id='divMassage'>";
                innerHTML += msg;
                innerHTML += "</div></div></div>";

                if (!document.getElementById("divMsg")) {
                    var div = document.createElement('div');
                    div.id = 'divMsg';
                    div.setAttribute('style', 'bottom:0px; right:20px; width:350px; height:200px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;display:none;');
                    div.setAttribute('innerHTML', innerHTML);
                    div.innerHTML = innerHTML;
                    document.body.appendChild(div);
                    with (document.getElementById("divMsg").style) {
                        bottom = "0px";
                        right = "0px";
                        width = "350px";
                        height = "200px";
                        position = "absolute";
                        background = "#FFFFFF";
                        border = "2px solid #447AA9";
                    }
                }
                else {
                    document.getElementById("divMsg").style.display = "block";
                    document.getElementById("divMsg").setAttribute('innerHTML', innerHTML);
                    document.getElementById("divMsg").innerHTML = innerHTML;
                }
                //setTimeout(HiddenWindow, 3000);
            }
            function HiddenWindow() {
                if (document.getElementById("divMsg")) {
                    document.getElementById("divMsg").style.display = "none";
                }
            }

            function select_mycity(name, id) {
                var url = "OrderDelive.aspx?cid=" + id + "&cname=" + escape(name);
                window.location = url;

            }
        </script>

    </form>
</body>
</html>

<script src="../../javascript/jquery.floatDiv.js" type="text/javascript"></script>

