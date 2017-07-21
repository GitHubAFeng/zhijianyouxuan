<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cartdetail.aspx.cs" Inherits="Html5.cartdetail" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />

</head>

<body>
    <input type="hidden" id="hnTogoBusiness" runat="server" />
    <input type="hidden" id="hnTogoStatus" runat="server" />
    <input type="hidden" id="hidTogoName" runat="server" />
    <input type="hidden" runat="server" id="hftogotype" />
    <input type="hidden" runat="server" id="hfminimoney" /><%--起送价--%>
    <input type="hidden" runat="server" id="hffreemoney" /><%--满多少免配送费--%>
    <input type="hidden" runat="server" id="hfsendfree" /><%--配送费--%>

    <div class="page">
        <div id="page_title">
            <a href="showtogo.aspx?id=<% =Request["id"] %>" id="back" class=" top_left"></a>
            <h1>我的购物车</h1>
        </div>

        <div class="container ">
            <ul class="cart_info_list">
                <div id="cart_food_list"></div>
                <li><span class="item name">配送费</span><span class="item price">￥<span id="spansendmoney">0</span></span></li>
                 <li><span class="item name">打包费</span><span class="item price">￥<span id="spanpackagefee">0</span></span></li>
                <li style="border-bottom: none;"><span class="item name">总计</span><span class="item total"><span class="food_num">0</span>个菜 / <span class="red money_num">￥0</span></span></li>
            </ul>
        </div>

        <div class="bom_cart">
            <div class="cart_info" style="display: none;">
                <span class="my_cart"><i class="ico-cart"></i><span class="cart-num food_num">0</span></span>
                <span class="price">共计<span class="f20 money_num">￥0</span></span>
            </div>
            <a class="check_cart_btn" id="submit" runat="server">下一步</a>
        </div>
    </div>


    <script id="catfoodlist" type="text/x-jsrender">
        {{for #data}}  
       
        
        <li style="height:auto;min-height:30px;"><span class="item name">{{:name}}</span>
            <span class="item cart_detail" data-id="{{:id}}" data-name="{{:name}}">
                <span onclick="cutcart(this,{{:#getIndex()}})" class="cicon ">-</span>
                <span  class="cicon mid" id="cart_food_{{:#getIndex()}}">{{:number}}</span>
                <span  onclick="addcart(this,{{:#getIndex()}})" class="cicon ">+</span>

            </span>
            
            <span class="item price">￥<span id="sinmoney_6670">{{:price+addprice}}</span></span>

            <div class="clear"></div>


        </li>



        {{/for}}
    </script>


</body>
</html>


<script src="javascript/jquery.js" type="text/javascript"></script>
<script src="javascript/jCommon.js" type="text/javascript"></script>
<script src="javascript/shopcarttool.js?v=1" type="text/javascript"></script>
<script src="javascript/cartdetail.js?v=2016071451" type="text/javascript"></script>
<script src="javascript/jsrender.js"></script>

