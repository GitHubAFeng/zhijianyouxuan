<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myorderlist.aspx.cs" Inherits="Html5.Anew.myorderlist" %>

<%@ Register Src="~/Anew/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>订单列表</title>
    <link type="text/css" rel="stylesheet" href="css/reset.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <script type="text/javascript" src="javascript/importantchange.js"></script>
    <style type="text/css">
        .container {
            background-color: #f2f2f2;
            padding-bottom: 0;
        }
    </style>
</head>
<body>

    <div class="page">

        <div class="header_title">
            <%--<a href="myinfolist.aspx" id="back" class=" top_left" data-ajax="false"></a>--%>
            <h1>订单列表</h1>
        </div>

        <div class="container ">
            <div class="orderlists">
                <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                <a href="#">
                    <div class="order_msg clearfix">
                        <div class="pic">
                            <img src="image/n-shop-logopic.jpg" />
                        </div>

                        <ul>
                            <li style="color: #000;">麻辣重庆火锅></li>
                            <li style="color: #666; margin-top: .75rem">共2份，实付￥<span>14：00</span></li>
                        </ul>

                    </div>
                </a>
                <p class="order_nber">订单号：1607201536120000<a href="#">评价</a><a href="#">再来一单</a></p>
            </div>
            <div class="orderlists">
                <p class="order_Status">订单未支付<span>2016/07/01 12：20：25</span></p>
                <a href="#">
                    <div class="order_msg clearfix">
                        <div class="pic">
                            <img src="image/fullimage7.jpg" />
                        </div>

                        <ul>
                            <li>加勒比海盗></li>
                            <li style="color: #666; margin-top: .75rem">共2份，实付￥<span>14：00</span></li>
                        </ul>

                    </div>
                </a>
                <p class="order_nber">订单号：1607201536120000<a href="#">取消</a><a href="#">去付款</a></p>
            </div>
            <div class="orderlists">
                <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                <a href="#">
                    <div class="order_msg clearfix">
                        <div class="pic">
                            <img src="image/fullimage7.jpg" />
                        </div>

                        <ul>
                            <li>加勒比海盗></li>
                            <li style="color: #666; margin-top: .75rem">共2份，实付￥<span>14：00</span></li>
                        </ul>

                    </div>
                </a>
                <p class="order_nber">订单号：1607201536120000<a href="#">已评价</a></p>
            </div>



        </div>
    </div>

    <uc2:Foot runat="server" ID="Foot" />
</body>
</html>
