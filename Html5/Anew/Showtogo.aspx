<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Showtogo.aspx.cs" Inherits="Html5.Anew.Showtogo" %>

<%@ Register Src="~/Anew/header.ascx" TagName="header" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>商家详细</title>
    <link type="text/css" rel="stylesheet" href="css/reset.css" />
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" rel="stylesheet" href="css/shop.css" />
    <script type="text/javascript" src="javascript/importantchange.js"></script>
    <script type="text/javascript" src="javascript/jquery.min.js"></script>
    <style>
        

    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <uc3:header runat="server" ID="header" />
        <div class="shoppage">
            <div class="menu_nav">
                <a href="#" class="cur">
                    <img src="image/pic-hotfood@2x.png" />热销</a>
                <a href="#">
                    <img src="image/pic-discount-h@2x.png" />折扣</a>
                <a href="#">锅底</a>
                <a href="#">主食</a>
                <a href="#">招牌菜</a>
                <a href="#">小吃</a>
                <a href="#">饮料</a>

            </div>

            <div class="foodcontainer">
                <div class="foodgroup">
                    <p class="sort-title">锅底</p>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/yuanyangguo_3596420.jpg" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a><input placeholder="1" /><a class="btn plus"><img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/picinfo.png" />
                        </div>
                        <ul>
                            <li class="foodname">麻辣锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/yuanyangguo_3596420.jpg" />
                        </div>
                        <ul>
                            <li class="foodname">黄焖鸡（小）+金针菇+米饭+饮料</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a><input placeholder="1" /><a class="btn plus"><img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/picinfo.png" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/yuanyangguo_3596420.jpg" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a><input placeholder="1" /><a class="btn plus"><img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/picinfo.png" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/yuanyangguo_3596420.jpg" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a><input placeholder="1" /><a class="btn plus"><img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                    <div class="foodlist">
                        <div class="pic">
                            <img src="image/picinfo.png" />
                        </div>
                        <ul>
                            <li class="foodname">鸳鸯锅</li>
                            <li class="price"><span>30</span>元/份</li>
                        </ul>
                        <div class="btn_item">
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </div>
                </div>
            </div>
            <div id="global-black"></div>
            <div class="shopmenu-cart-list"id="showmenu" >
                <div class=" cl-top-tit fixedtit">
                    <img src="image/icon-bag@2x.png" />
                    一号袋
                    <a>
                        <img src="image/icon-trash@2x.png" />删除购物车</a>
                </div>
                <ul class="hidelist">
                    <li style="list-style-type: disc;"><span>鸳鸯锅</span><span>￥30</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                    <li style="list-style-type: disc;"><span>河虾ya河虾ya河虾ya河虾ya河虾ya</span><span>￥38</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                    <li style="list-style-type: disc;"><span>娃娃菜</span><span>￥10</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                                        <li style="list-style-type: disc;"><span>娃娃菜</span><span>￥10</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                </ul>
                <div class="cl-top-tit">
                    <img src="image/icon-bag@2x.png" />
                    二号袋
                    <span class="fright">若需分装长按商品拖入此口袋</span>
                </div>
                <ul class="hidelist">
                    <li style="list-style-type: disc;"><span>麻辣小龙虾</span><span>￥30</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                    </ul>
                                <div class="cl-top-tit">
                    <img src="image/icon-bag@2x.png" />
                    二号袋
                    <span class="fright">若需分装长按商品拖入此口袋</span>
                </div>
                <ul class="hidelist">
                    <li style="list-style-type: disc;"><span>麻辣小龙虾</span><span>￥30</span>
                        <div class="btn_item">
                            <a class="btn minus">
                                <img src="image/btn-minus-b@2x.png" /></a>
                            <input placeholder="1" />
                            <a class="btn plus">
                                <img src="image/btn-add-red@2x.png" /></a>
                        </div>
                    </li>
                    </ul>
            </div>
            <div class="shopmenu-cart-bar">

                <div class="btn-cart">
                    <span>20</span>
                </div>
                <div class="row-cart">共￥<span>100</span></div>
                <a href="#" class="row-status local-disable"></a>
            </div>
        </div>
    </form>
</body>
</html>
<script>
    $(document).ready(function () {
        $(".menu_nav a").click(
        function () {
            $(this).addClass("cur").siblings().removeClass("cur");

        })
        $("#showmenu").hide()
        $("#global-black").hide()
        $(".btn-cart").click(
        function () {
            $("#showmenu").slideUp(600).show();
            $("#global-black").toggle()
        })
        $("#global-black").click(
            function () {
                $("#showmenu").slideDown(600).hide();
                $("#global-black").hide()
            })

    });
</script>
