<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myorder.aspx.cs" Inherits="myorder" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的订单 -
        <%= WebUtility.GetWebName() %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="css/Common.css" rel="stylesheet" />
    <link type="text/css" href="css/myorder.css" rel="stylesheet" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="javascript/jCommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" runat="server">
        <asp:HiddenField runat="server" ID="hfuid" Value="0" />
        <top:banner ID="Banner1" runat="server" />
        <uc1:header ID="header" runat="server" />
        <div class="wrap">
            <div class="hplace_bg">
                <span class="color_1 hplace_house" id="dangqian" runat="server"><a href="/Index.aspx">首页</a> >>
                <a href="javascript:">我的订单</a></span>
            </div>
            <div class="content_primary">
                <asp:Repeater ID="rptorder" runat="server">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="ordertab">
                            <tr>

                                <th colspan="1" style="background-color: #fff;">
                                    <%# Eval("togoname") %>
                                </th>

                                <th colspan="3" style="background-color: #fff;">订单号：<%# Eval("orderid") %>
                                    <font color="red">[<%# WebUtility.TurnOrderState(Eval("orderstatus")) %>]</font>
                                </th>
                            </tr>
                            <tr>
                                <th width="60%">美食名称
                                </th>
                                <th width="10%">价格
                                </th>
                                <th width="10%">数量
                                </th>
                                <th width="10%">小计
                                </th>
                            </tr>
                            <asp:Repeater ID="rptfood" runat="server" DataSource='<%# Getfood(Eval("orderid")) %>'>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: left; padding-left: 10px;">

                                            <%# Eval("foodname")%>
                                        </td>
                                        <td>
                                            <%# Eval("FoodPrice")%>元
                                        </td>
                                        <td>
                                            <%# Eval("FCounts")%>份
                                        </td>
                                        <td>
                                            <%# Convert.ToDecimal(Convert.ToDecimal(Eval("FoodPrice")) * Convert.ToInt32(Eval("FCounts"))).ToString("#0.0")%>元
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td class="f14 " colspan="4" style="text-align: left; padding-left: 10px;">收货信息:<%# Eval("OrderRcver")%>，<%# Eval("AddressText")%>，<%# Eval("OrderComm")%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right" class=" f14 red" style="text-align: left; padding-left: 10px;">总金额:<%# Eval("OrderSums")%>元&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 配送费:
                                            <%# Eval("SendFee")%>元&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <div class=" order_content" runat="server" id="divno">
                    <h3 style="font-size: 16px; margin: 10px 0;">暂无订单
                    </h3>
                </div>
            </div>
        </div>
        <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
