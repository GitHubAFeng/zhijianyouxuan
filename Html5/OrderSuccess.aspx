<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSuccess.aspx.cs" Inherits="Html5.OrderSuccess" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <script src="javascript/jquery.js"></script>
</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="myorderlist.aspx" id="back" class=" top_left" data-ajax="false"></a>
            <h1>订单提交成功</h1>
        </div>
        <div class="container">
            <ul class="my_order_list">
                <li data-icon="false">
                    <div data-icon="false" class="order-tit" style="white-space: normal; height: auto">
                        您的订单已经提交成功，请耐心等待您的外卖送达
                    </div>
                    <div class="order-tit" id="reghid" runat="server">
                        <label id="regnotice" runat="server"></label>
                    </div>
                    <div class="order-tit">
                        订单号：<label runat="server" id="lborderid"></label>
                    </div>
                    <div class="order-tit">
                        您选定的支付方式：<label id="payway" runat="server"></label>
                    </div>
                    <div class="order-tit">
                        您应付的总金额：<label runat="server" id="lbprice"></label>元
                    </div>
                    <div data-icon="false" class="order-tit">
                        送餐可能需要<label id="sendtime" runat="server"></label>分钟左右，请耐心等候，谢谢！
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

