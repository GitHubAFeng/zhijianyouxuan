<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myexpresslist.aspx.cs" Inherits="Html5.myexpresslist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>

    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />

    <script type="text/javascript" src="javascript/jquery.js"></script>
</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" class=" top_left" data-ajax="false"></a>
            <h1>跑腿订单</h1>
        </div>
        <div class="container ">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>
                        <li>
                            <a href="showexpressorder.aspx?id=<%# Eval("dataid") %>" data-ajax="false">
                                <div class="order-tit">
                                    <span class="time"><%# Eval("OrderTime")%></span>
                                    <span class="state"><%# Hangjing.WebCommon.WebHelper.TurnExpressOrderState(Eval("State").ToString())%></span>
                                </div>
                                <div class="order-info">
                                    <p class="grey">订单编号：<%# Eval("orderid") %></p>
                                    <p class="grey">配送费：￥<%# Eval("sendmoney")%></p>
                                    <p class="grey" style="<%# Convert.ToInt32(Eval("callcount")) == 0 ? "display:none;": ""  %>">商品价格：￥<%# Eval("TotalPrice")%></p>
                                </div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="con-btn" id="pages" runat="server">
            </div>
        </div>
    </div>
</body>
</html>
