<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TogoList.aspx.cs" Inherits="Html5.TogoList" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/home.css?v=708" />
    <link href="css/pictip.css" rel="stylesheet" type="text/css" />

    <link href="css/idangerous.swiper.css?v=1" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />
    <script type="text/javascript" src="javascript/idangerous.swiper-1.9.1.min.js"></script>

    <style type="text/css">
        body {
            background-color: #eee;
        }

        .bom_menu .icon-food {
            background-image: url(../images/ico_b_food_cur.png);
            color: #f39800;
        }
    </style>
</head>
<body id="page_allMenu">
    <input id="hfpage" runat="server" type="hidden" />
    <input id="hfcpage" runat="server" type="hidden" value="1" />
    <header id="page_title">
        <h1><a href="index.aspx?change=1" id="addresstext" runat="server"></a></h1>

        <a id="togourl" class="top_search top_right" href="search.aspx"></a>


    </header>
    <div class="container">
        <div class="index_img">

            <div class="swiper-container swiper">
                <div class="swiper-wrapper">
                    <asp:Repeater runat="server" ID="rptppt">
                        <ItemTemplate>
                            <div class="swiper-slide">
                                <a href="<%# Eval("PUrl") %>">
                                    <img src="<%# WebUtility.ShowPic(Eval("picture").ToString()) %>" style="width: 100%; height: 100%;" title="<%# Eval("title") %>"></a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="pagination"></div>
            </div>
        </div>
        <div class="index_class">
            <div class="swiper-container1 swiper1">
                <div class="swiper-wrapper">
                    <asp:Repeater ID="rptgroup" runat="server">
                        <ItemTemplate>
                            <ul class="swiper-slide">
                                <asp:Repeater ID="rptsort" runat="server" DataSource='<%# Eval("sortlist") %>'>
                                    <ItemTemplate>
                                        <li>
                                            <p>
                                                <a href="waimaijie.aspx?id=<%# Eval("id") %>">
                                                    <img src="<%# WebUtility.ShowPic(Eval("pic").ToString())  %>">
                                                </a>
                                            </p>
                                            <p><a href="waimaijie.aspx?id=<%# Eval("id") %>" id="sort_<%# Eval("id") %>"><%# WebUtility.Left(Eval("classname").ToString(),4)%></a></p>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="pagination1"></div>
            </div>
        </div>

        <a href="Discountstores.aspx">
            <div class="special_offer clearfix">
                <ul>
                    <li class="t-one">每日特价</li>
                    <li class="t-two">特惠不打烊</li>
                </ul>
                <img src="images/dfood1.jpg" id="img" runat="server" />
            </div>
        </a>

        <div class="moduleitem clearfix">
            <a href="hotstores.aspx" style="color: #f39800">
                <div class="moduleleft">
                    <img src="images/picinfo.png" />
                    <p>大家都在吃</p>

                </div>
            </a>

            <div class="moduleright">
                <a href="shop.aspx" runat="server" id="marketlink">
                    <div class="moduleline" style="border-bottom: #eee 1px solid">
                        <span class="green">超市</span>
                        <img src="images/shops.png" />
                    </div>
                </a>

                <a href="Merchantstous.aspx"style="color:orange">
                    <div class="moduleline">

                        <span>商家加盟</span>
                        <img src="images/joinus.png" />

                    </div>
                </a>


            </div>
        </div>

        <div class="clearfix"></div>
        <div class="index_hot_title">推荐商家</div>
        <ul id="shoplist" class="shoplist" style="border-top: 1px solid #dadada;">
            <asp:Repeater runat="server" ID="rptJoinTogolist">
                <ItemTemplate>
                    <li>

                        <a href="ShowTogo.aspx?id=<%# Eval("Unid")%>" title="<%# Eval("Name")%>">
                            <div class=" pic">
                                <img class="img" src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" />
                                <%# ParseBisness(Eval("Status"),Eval("isbisness"))%>
                            </div>
                            <div class="info">
                                <h2 class="shop-marker">
                                    <%# WebUtility.Left( Eval("Name"),8)%>
                                    <asp:Repeater runat="server" ID="rpttags" DataSource='<%#Eval("pictags")%>'>
                                        <ItemTemplate>
                                            <i style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px;"></i>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </h2>
                                <p class="des"><%# WebUtility.Left( Eval("Address"),12)%></p>
                                <p class="des">配送：<%# WebUtility.getOnepoint( Eval("SendFee"))%>元 | 起送：<%# Eval("SendLimit") %>元</p>
                                <p class="des re">
                                    <i class="icon-time"></i>
                                    <%# WebUtility.Left(Eval("opentimestr"),15) %>
                                </p>

                            </div>

                            <div class="r_msg">
                                <span class="distance"><i class="icon-address"></i><%# Convert.ToDecimal(Eval("Distance")).ToString("f1") %>KM</span>
                                <span class="sendtime">约<%# Eval("senttime")%>分钟</span>
                                <span style="display: <%# Convert.ToInt32(Eval("sentorg")) == 0 ? "none" :""  %>" class="ptzs">平台专送</span>
                            </div>

                            <div class="clear"></div>
                            <div class="shoppromotion-marker" style="margin-top: 5px;">
                                <asp:Repeater runat="server" ID="rptptomotion" DataSource='<%#Eval("promotions")%>'>
                                    <ItemTemplate>
                                        <div><i style="background: url('/images/jian_02.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px;"></i>&nbsp;<%#Eval("Title")%></div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>



    <div class="mModal" id="myordermsgk" runat="server" style="z-index: 900; top: 0; bottom: 0; left: 0; right: 0;">
    </div>

    <div class="mDialog freeSet" runat="server" id="noaddresstip" style="z-index: 901; margin-top: 0px;"
        data-ffix-top="99">
        <div class="content">
            正在获取您的位置，请稍后...

        </div>

    </div>

    <uc2:Foot runat="server" ID="foot" />

</body>
</html>

<script src="javascript/jquery.js" type="text/javascript"></script>
<script src="javascript/jCommon.js" type="text/javascript"></script>

<script type="text/javascript">



    function getUserLoaciont() {
        var lat = request("lat");
        if (lat.length > 0) {
            return false;
        }

        var lat = handlecookie("mylat");
        var lng = handlecookie("mylng");
        var address = handlecookie("address");

        var url = "index.aspx?noaddress=1";

        if (lat != null && lat.length > 1) {
            url = "Togolist.aspx?islocal=1&address=" + address + "&lat=" + lat + "&lng=" + lng;

        }
        window.location = url;
        return;
    }

    getUserLoaciont();


    var swiper = new Swiper('.swiper', {
        pagination: '.pagination',
        loop: true,
        grabCursor: true
    });
    swiper.startAutoPlay();

    //Clickable pagination
    $('.pagination').click(function () {
        swiper.swipeTo($(this).index())
    })
    setInterval("swiper.swipeNext()", 4000);

    var swiper1 = new Swiper('.swiper1', {
        pagination: '.pagination1',
        loop: true,
        grabCursor: true
    });
    swiper1.startAutoPlay();

    //Clickable pagination
    $('.pagination1').click(function () {
        swiper1.swipeTo($(this).index())
    })



</script>


