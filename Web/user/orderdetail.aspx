<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderdetail.aspx.cs" Inherits="UserHome_orderdetail" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心-订单详细-<%= SectionProxyData.GetSetValue(3)%></title>

    <link href="/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/user/css/userinfo.css" rel="stylesheet" />

    <link href="css/timeline.css" rel="stylesheet" />




    <script src="JavaScript/DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            showorderlocal();
        })
    </script>

    <style type="text/css">
        .left_span {
            float: none;
            padding-left: 10px;
            text-align: left;
        }

        .restaurant-icons {
            display: inline-block;
            height: 15px;
            margin-right: 5px;
            width: 15px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hfcityid" />
        <asp:HiddenField runat="server" ID="hfstate" />
        <asp:HiddenField runat="server" ID="hflatlng" />
        <asp:HiddenField runat="server" ID="hfdeliverid" />
        <asp:HiddenField runat="server" ID="hforderid" />
        <asp:HiddenField runat="server" ID="hfusername" />
        <asp:HiddenField runat="server" ID="hfaddress" />
        <asp:HiddenField runat="server" ID="hfshopname" />
        <asp:HiddenField runat="server" ID="hMsg" />


        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp">
            <uc2:LeftBanner runat="server" ID="Left" />
            <div class="shop_main rightmenu_cont">

                <div class="main-content">

                    <div class="shop_menu">
                        <ul>
                            <li class="cur" id="order1" onclick="settable('order', 1, 2, 'cur', '')"><a href="javascript:">订单状态</a></li>
                            <li id="order2" onclick="settable('order', 2, 2, 'cur', '')"><a href="javascript:">订单详情</a></li>
                        </ul>
                    </div>

                    <div id="order_div1">
                        <div id="cd-timeline" class="cd-container">
                            <asp:Repeater runat="server" ID="rptppt">
                                <ItemTemplate>
                                    <div class="cd-timeline-block">
                                        <div class="cd-timeline-img cd-picture">
                                            <img src="/images/send_ico.png" alt="Picture">
                                        </div>
                                        <div class="cd-timeline-content">
                                            <h2><span class="cd-date" style="float: right; font-weight: normal;"><%# Eval("addtime")%></span><%# Eval("title") %></h2>
                                            <div class="clear;"></div>
                                            <p style="<%#Eval("subtitle").ToString().Length  == 0 ? "none;": ""  %>"><%# Eval("subtitle") %></p>

                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>


                    <div class="" style="display: ;"  id="order_div2">
                        <div class="usermima_order">
                            <div class="process_con" id="process" runat="server">
                            </div>


                            <div class="indis_right_map" style="padding: 10px;">
                                <div id="map_canvas" style="height: 338px; padding: 10px;">
                                </div>
                            </div>




                        </div>


                        <div class="usermima">


                            <div style="margin-bottom: 10px; padding-left: 10px;">
                                <span><span>订单编号：</span><asp:Label ID="lbOrderId" runat="server"></asp:Label></span>

                                <span style="margin-left: 20px;"><span>订单时间：</span><asp:Label ID="lbOrderdate" runat="server"></asp:Label></span>

                                <span style="margin-left: 20px;">
                                    <asp:Button ID="upStatus" OnClick="upStatus_Click" CssClass="subBtn" runat="server" Text="确认收货" /></span>


                            </div>

                            <table width="98%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #ccc;"
                                class="listorder_table clear" align="center">
                                <tr>
                                    <th>商品名称
                                    </th>
                                    <th style="width: 20%">单价
                                    </th>
                                    <th style="width: 15%">数量
                                    </th>
                                    <th style="width: 20%">小计
                                    </th>
                                </tr>
                                <asp:Repeater ID="rtpBooks" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("foodname")%>
                                            </td>
                                            <td class="orange">￥<%# Convert.ToDecimal(Eval("foodPrice")) %>
                                            </td>
                                            <td>
                                                <%#Eval("FCounts") %>
                                            </td>
                                            <td class="orange">￥<%# Convert.ToDecimal(Eval("foodPrice")) * Convert.ToInt32(Eval("FCounts"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="rptorder" runat="server">
                                    <ItemTemplate>
                                        <tr style="">
                                            <td colspan="4" style="text-align: right; border: none;">商品总额：￥<%# Convert.ToDecimal(Eval("OldPrice"))%>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="4" style="text-align: right; border: none;">+ 打包费：￥<%# Convert.ToDecimal(Eval("packagefee"))%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right; border: none;">+ 配送费：￥<%# Convert.ToDecimal(Eval("SendFee"))%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right; border: none;">- 优惠券：￥<%# Convert.ToDecimal(Eval("cardpay"))%>
                                            </td>
                                        </tr>


                                        <asp:Repeater ID="rptpromotons" runat="server" DataSource='<%# Eval("Promotions")%>'>
                                            <ItemTemplate>

                                                <tr>
                                                    <td colspan="4" style="text-align: right; border: none;">

                                                        <span class="restaurant-icons  tooltip_on" style="background: url('/images/jian_02.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0)"></span><%#Eval("revevar1")%>
                                                    
                                                  
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>


                                        <tr>
                                            <td colspan="4" style="text-align: right; border: none; font-size: 16px;">应付总额：￥<span style="color: red;"><%# Convert.ToDecimal(Eval("needpaymoney"))%></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right; border: none;">支付方式： <%# WebUtility.TurnPayModel(Eval("PayMode").ToString())%>[<%# Eval("PayState").ToString()=="1"?"已付":"未付" %> <span style="color: red;"><%# Convert.ToDecimal(Eval("needpaymoney"))%></span>]
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>


                            <div class="usermima_order">
                                <ul style="width: 700px;">
                                    <li><span class="left_span"><strong>物流信息</strong></span></li>
                                    <li><span class="left_span">商家名称：</span><asp:Label ID="togoname" runat="server"></asp:Label></li>
                                    <li><span class="left_span">收货人：</span><asp:Label ID="lbUName" runat="server"></asp:Label>，<asp:Label ID="lbTel" runat="server"></asp:Label></li>

                                    <li><span class="left_span">送餐时间：</span><asp:Label ID="lbmsg" runat="server"></asp:Label></li>
                                    <li><span class="left_span">收货地址：</span><asp:Label ID="lbAddress" runat="server"></asp:Label></li>
                                    <li><span class="left_span">备注：</span><asp:Label ID="lbremark" runat="server"></asp:Label></li>


                                </ul>
                            </div>

                        </div>

                        <div class="process_btn">
                            <asp:Button ID="Button1" OnClick="Button1_Click" CssClass="subBtn" runat="server"
                                Text="返回列表" />
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>

<script src="../javascript/eventwrapper.min.js" type="text/javascript"></script>
<script src="http://api.map.baidu.com/api?v=1.2" type="text/javascript"></script>
<script src="javascript/orderdetail.js" type="text/javascript"></script>


<script type="text/javascript">
    var hMsg = $("#hMsg").val();
    if ($.trim(hMsg) != "") {
        alert(hMsg);
    }

</script>
