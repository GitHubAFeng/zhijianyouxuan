<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Html5.Anew.index" %>

<%@ Register Src="~/Anew/footer.ascx" TagName="Foot" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>首页</title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/index.css" rel="stylesheet" type="text/css" />
    <link href="css/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="css/owl.theme.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="javascript/importantchange.js"></script>
    <script type="text/javascript" src="javascript/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="javascript/owl.carousel.js"></script>
    <style type="text/css">
        .wapper {
            height: 1.5rem;
            position:fixed;
            z-index:999;
            top:1.8rem;
            background-color: #ff2d4b;
        }
        .container {
            padding-top:3.3rem;
        }
        .footer .ico_b_index {
            background-image: url(/image/toolbar-index-selected@2x.png);
        }
    </style>
</head>
<script type="text/javascript">
    $(function () {
        $('#owl-demo').owlCarousel({
            items: 1,
            autoPlay: true
        });
    });
</script>
<body>
    <form id="Form1" runat="server">

        <div class="page">
            <header class="header_title" style="height: 1.8rem;">
                <h1 style="line-height: 1.8rem;">
                    <img src="image/btn-home-position@2x.png" />正华企业广场<i class="icon-arrow"></i></h1>
            </header>
            <div class="wapper">
                <div class="top_serach">
                    <img src="image/input-serach@2x.png" />
                    <input placeholder="请输入商家/商家名称" />
                </div>
            </div>

            <div class="container">
                <!--banner-->
                <div id="owl-demo" class="owl-carousel">
                    <a class="item" href="#">
                        <img src="image/banner@2x.png" alt="" /></a>
                    <a class="item" href="#">
                        <img src="image/banner@2x.png" alt="" /></a>
                    <a class="item" href="#">
                        <img src="image/banner@2x.png" alt="" /></a>
                    <a class="item" href="#">
                        <img src="image/banner@2x.png" alt="" /></a>
                    <a class="item" href="#">
                        <img src="image/fullimage7.jpg" /></a>
                </div>



                <!--标签分类-->
                <div class="icon-list clearfix">
                    <a href="waimaijie.aspx">
                        <img src="image/Sort_icon_index@2x.png" /><span>全部</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class1@2x.png" /><span>中式快餐</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class2@2x.png" /><span>水果生鲜</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class3@2x.png" /><span>花卉</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class4@2x.png" /><span>水果生鲜</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class5@2x.png" /><span>水果生鲜</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class6@2x.png" /><span>水果生鲜</span></a>
                    <a href="#">
                        <img src="image/Sort_icon_class7@2x.png" /><span>水果生鲜</span></a>
                </div>
                <!--导购-->
                <div class="link-in">
                    <a href="#" class="alink clearfix">
                        <span class="fleft">
                            <strong>每日特价</strong>
                            <small>特惠不打烊</small> </span>
                        <span class="fright">
                            <img src="image/pic-link1@2x.png" /></span>
                    </a>
                </div>
                <div class="link-in clearfix">
                    <div class="fleft">
                        <a href="#" class="leact">
                            <img src="image/pic-linkl@2x.png" /><strong class="red">大家都在吃</strong>
                        </a>
                    </div>
                    <div class="fleft react">
                        <a href="#">
                            <strong class="green">超市</strong>
                            <img src="image/shops@2x.png" />
                        </a>
                        <a href="#" style="border-bottom: none">
                            <strong class="orange">商家加盟</strong>
                            <img src="image/joinus@2x.png" />
                        </a>
                    </div>
                </div>
                <!--附近商家推荐-->
                <div class="N-shop-recommend">
                    <p class="n-top-title"><span>附近推荐商家</span></p>

                    <div class="n-shoplist clearfix">
                        <a href="Showtogo.aspx">
                            <div class="logopic  fleft">
                                <img src="image/n-shop-logopic.jpg" />
                            </div>
                            <div class="n-listinfo fright">
                                <div class="n-shopmsg">
                                    <ul>
                                        <li class="name">麻辣重庆火锅</li>
                                        <li class="sales">
                                            <span class="star"><span style="width:80%"></span></span>
                                            <span>月销售1024单</span></li>
                                        <li class="distance">起送价￥20 | 配送费￥5</li>
                                    </ul>
                                    <div class="infotypt">
                                        <p class="ptzs">平台专送</p>
                                        <p>1.6km | <i class="red">33分钟</i></p>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <div class="n-shopactivity">
                            <span>3个活动<i class="downarrow"></i></span>
                            <ul>
                                <li>实际支付30元返3元商家代金券</li>
                                <li>实际支付30元返3元商家代金券</li>
                            </ul>
                        </div>
                    </div>

                    <div class="n-shoplist clearfix">
                        <a href="Showtogo.aspx">
                            <div class="logopic  fleft">
                                <img src="image/fullimage7.jpg" />
                            </div>
                            <div class="n-listinfo fright">
                                <div class="n-shopmsg">
                                    <ul>
                                        <li class="name">加勒比海盗</li>
                                        <li class="sales">
                                        <span class="star"><span style="width:80%"></span></span>
                                            <span>月销售1024单</span></li>
                                        <li class="distance">起送价￥20 | 配送费￥5</li>
                                    </ul>
                                    <div class="infotypt">
                                        <p class="ptzs">平台专送</p>
                                        <p>1.6km | <i class="red">33分钟</i></p>
                                    </div>
                                </div>


                            </div>
                        </a>
                        <div class="n-shopactivity">
                            <span>3个活动<i class="uparrow"></i></span>
                            <ul>
                                <li>实际支付30元返3元商家代金券</li>
                                <li>实际支付30元返3元商家代金券</li>
                            </ul>
                        </div>
                    </div>
                    <div class="n-shoplist clearfix">
                        <a href="Showtogo.aspx">
                            <div class="logopic  fleft">
                                <img src="image/picinfo.png" />
                            </div>
                            <div class="n-listinfo fright">
                                <div class="n-shopmsg">
                                    <ul>
                                        <li class="name">石锅拌饭 </li>
                                        <li class="sales">
                                           <span class="star"><span style="width:80%"></span></span>
                                            <span>月销售1024单</span></li>
                                        <li class="distance">起送价￥20 | 配送费￥5</li>
                                    </ul>
                                    <div class="infotypt">
                                        <p class="ptzs">平台专送</p>
                                        <p>1.6km | <i class="red">33分钟</i></p>
                                    </div>
                                </div>


                            </div>

                        </a>
                        <div class="n-shopactivity">
                            <span>3个活动<i class="downarrow"></i></span>
                            <ul>
                                <li>实际支付30元返3元商家代金券</li>
                                <li>实际支付30元返3元商家代金券</li>
                            </ul>
                        </div>
                    </div>
                </div>



                <uc2:Foot runat="server" ID="Foot" />
            </div>
        </div>
    </form>
</body>
</html>
