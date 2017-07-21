<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaySuccess.aspx.cs" Inherits="Html5.PaySuccess" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <div class="page">
        <div id="page_title">
            <%--<a href="myorderlist.aspx" id="back" class=" top_left" data-ajax="false"></a>--%>
            <h1>订单支付成功</h1>
            <a href="myorderlist.aspx" class="reg top_right">查看订单</a>
        </div>
        <div class="container">
            <ul class="my_order_list">
                <li data-icon="false">
                    <div data-icon="false" class="order-tit" style="white-space: normal; height: auto">
                        您的订单已经支付成功，请耐心等待您的外卖送达
                    </div>
                    <div class="order-tit">
                        订单号：<label runat="server" id="lborderid"></label>
                    </div>
                    <div class="order-tit">
                        您支付的总金额：<label runat="server" id="lbprice"></label>元
                    </div>
                    <div data-icon="false" class="order-tit" style="white-space: normal; border-bottom: none; height: auto;">
                        提示：您可以在微信里回复'd'或'订单'查看您今日下的订单
                    </div>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    $(document).ready(function () {
        localStorage.setItem("ShoppingCart", "");
    })
</script>

