<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="waimaijie.aspx.cs" Inherits="Html5.Anew.waimaijie" %>

<%@ Register Src="~/Anew/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>商家列表</title>
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/index.css" rel="stylesheet" type="text/css" />
    <link href="css/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="css/owl.theme.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="javascript/importantchange.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <header class="header_title">
                <a href="index.aspx" class="back"></a>
                <h1>平台专送</h1>
                <a href="#" class="search"></a>
            </header>

            <div class="container">
                <div class="cater-list-item">
                    <menu class="cater-list-tool">
                        <li>餐厅分类<i class="downarrow"></i></li>
                        <li>智能排序<i class="downarrow"></i></li>
                        <li class="selected">筛选<i class="uparrow"></i></li>
                    </menu>
                    <div class="cater-list-subnav">
                        <ul class="thirdnav">
                            <li class="sorting">智能排序</li>
                            <li class="sales">销量</li>
                            <li class="distance">距离</li>
                            <li class="new">最新</li>
                        </ul>

                    </div>
                </div>
                <div class="N-shop-recommend">
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
                                            <span class="star"><span style="width: 80%"></span></span>
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
                                            <span class="star"><span style="width: 80%"></span></span>
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
                                            <span class="star"><span style="width: 80%"></span></span>
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
            </div>
        </div>
        <uc2:Foot runat="server" ID="Foot" />
    </form>

</body>
</html>
