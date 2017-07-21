<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="Html5.Anew.header" %>
<link href="css/common.css" rel="stylesheet" type="text/css" />
<div class="shop_head">
    <a href="index.aspx" class="back"></a>
    <h1>平台专送</h1>
    <a href="#" class="share"></a>
    <a href="#" class="search"></a>
    <div class="shopinfo">
        <div class="logo">
            <img src="image/n-shop-logopic.jpg" /> </div>
        <p class="shopname">麻辣重庆火锅</p>
        <p class="sign">公告：下单拼饭类赠送蛋花汤下单拼饭类赠送蛋花汤下单拼饭类赠送蛋花汤下单拼饭类赠送蛋花汤</p>
        <div class="btn-faved">
            <a class="fav_sed">
                <img src="image/btn-collect-n@2x.png" />收藏</a>
            <a style="display:none">
                <img src="image/btn-collected@2x.png" />已收藏</a>
        </div>
    </div>
    <div class="discount">
        <marquee direction="left" width="96%" behavior="scroll" scrollamount="4" scrolldelay="100" behavior="scroll" ><span>返</span>实际支付30元返3元商家代金券,实际支付30元返3元商家代金券实际支付30元返3元商家代金券</marquee>
    </div>
    
</div>
<div class="shop-nav clear">
<a href="Showtogo.aspx" class="redcur">点菜</a><a href="shopcomment.aspx">评价</a><a href="#">商家</a>
</div>
<script>
    $(".btn-faved a").click(
        function () {
            $(this).hide().siblings().show();
        })
    $(".shop-nav a").click(
    function () {
        $(this).addClass("redcur").siblings().removeClass("redcur");
    })


</script>