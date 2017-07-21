<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderlist.aspx.cs" Inherits="Html5.orderlist" %>
<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单列表</title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />
    <style type="text/css">
        .container {
            background-color:#f2f2f2;
            padding-bottom:0;
        }
        
        .bom_menu .icon-order {
            background-image: url(/images/ico_b_order_cur.png);
             color:#f39800;
        }
  
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">

            <div id="page_title">
                <a href="myinfolist.aspx" id="back" class=" top_left" data-ajax="false"></a>
                <h1>订单列表</h1>
            </div>

            <div class="container ">
                <div class="orderlists">
                    <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                    <a href="#"><div class="order_msg clearfix">
                        <img src="images/foodpho.png" />
                        <ul>
                            <li class="f16">顺旺基></li>
                            <li class="f14" style="color:#666">共2份，实付￥<span>14：00</span></li>
                        </ul>
                        
                    </div></a>
                    <p class="order_Status" style="font-size:12px;">订单号：1607201536120000<a href="#">评价</a><a href="#">再来一单</a></p>
                </div>
                <div class="orderlists">
                    <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                    <a href="#"><div class="order_msg clearfix">
                        <img src="images/foodpho.png" />
                        <ul>
                            <li class="f16">顺旺基></li>
                            <li class="f14" style="color:#666">共2份，实付￥<span>14：00</span></li>
                        </ul>
                        
                    </div></a>
                    <p class="order_Status" style="font-size:12px;">订单号：1607201536120000<a href="#">评价</a><a href="#">再来一单</a></p>
                </div>
                                <div class="orderlists">
                    <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                    <a href="#"><div class="order_msg clearfix">
                        <img src="images/foodpho.png" />
                        <ul>
                            <li class="f16">顺旺基></li>
                            <li class="f14" style="color:#666">共2份，实付￥<span>14：00</span></li>
                        </ul>
                        
                    </div></a>
                    <p class="order_Status" style="font-size:12px;">订单号：1607201536120000<a href="#">评价</a><a href="#">再来一单</a></p>
                </div>
                                <div class="orderlists">
                    <p class="order_Status">订单已完成<span>2016/07/01 12：20：25</span></p>
                    <a href="#"><div class="order_msg clearfix">
                        <img src="images/foodpho.png" />
                        <ul>
                            <li class="f16">顺旺基></li>
                            <li class="f14" style="color:#666">共2份，实付￥<span>14：00</span></li>
                        </ul>
                        
                    </div></a>
                    <p class="order_Status" style="font-size:12px;">订单号：1607201536120000<a href="#">评价</a><a href="#">再来一单</a></p>
                </div>
            </div>
        </div>
    </form>
        <uc2:Foot runat="server" ID="foot" />
</body>
</html>
