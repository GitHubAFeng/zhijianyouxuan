<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Discountstores.aspx.cs" Inherits="Html5.Discountstores" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>特惠商家</title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/home.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=2" />
    <link href="css/idangerous.swiper.css?v=1" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="javascript/idangerous.swiper-1.9.1.min.js"></script>
    <style type="text/css">
        .food_category {
            width: 100%;
            float: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hfod" runat="server" value="od0" />
        <div class="page" style="margin-bottom: 0;">
            <div id="page_title">
                <a href="TogoList.aspx" id="back" runat="server" class=" top_left"></a>
                <h1>劲爆折扣专区</h1>
            </div>
        </div>
        <div class="stores_banner">
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
        <!--商家分类-->

        <!--商家分类-->

        <div class="storesitem clearfix">
            <div class="restaurant_nav">
                <ul>
                    <li class="change" id="food_category"><a href="javascript:void(0)">店家分类</a><i class="arrow"></i></li>
                    <li class="change" id="sort_category"><a href="javascript:void(0)">排序</a><i class="arrow"></i></li>
                    <li class="change" id="act_category"><a href="javascript:void(0)">起送价</a><i class="arrow"></i>
                </ul>
            </div>
            <div class="restaurant_nav_detail">
                <div>
                    <ul class="food_category" style="display: none">
                        <li id='sortid0' class="subsortitem"><a href="<%= getSortUrl("s", "") %>">全部</a></li>
                        <%--<li id='send_l1' class="subsortitem"><a href="<%= getSortUrl("l", "1") %>">平台专送</a></li>--%>
                        <asp:Repeater ID="rpttogosortlist" runat="server">
                            <ItemTemplate>
                                <li id='sortid<%# Eval("id") %>' class="subsortitem">
                                    <a href="<%# getSortUrl("s", Eval("id")) %>"><%#Eval("classname")%></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>


                </div>
                <ul class="category_detail sort_category" style="display: none">
                    <li class="selected orderitem" id="od0"><a href="<%= getSortUrl("od", "0") %>"><i class="sort_all"></i>默认</a></li>
                    <li id="od1" class="orderitem"><a href="<%= getSortUrl("od", "1") %>"><i class="sort_hot"></i>销量</a></li>
                    <li id="od3" class="orderitem"><a href="<%= getSortUrl("od", "3") %>"><i class="distance"></i>距离</a></li>
                    <li id="od2" class="orderitem"><a href="<%= getSortUrl("od", "2") %>"><i class="sort_star"></i>最新</a></li>

                </ul>
                <ul class="category_detail act_category" style="display: none">
                    <li class="selected saleitem" id='serverlink0'><a href="<%= getSortUrl("a", "") %>">全部</a></li>
                    <li class=" saleitem" id='serverlink10'><a href="<%= getSortUrl("a", "10") %>">10元以下</a></li>
                    <li class=" saleitem" id='serverlink20'><a href="<%= getSortUrl("a", "20") %>">20元以下</a></li>
                    <li class=" saleitem" id='serverlink30'><a href="<%= getSortUrl("a", "30") %>">30元以下</a></li>
                    <li class=" saleitem" id='serverlink40'><a href="<%= getSortUrl("a", "40") %>">40元以下</a></li>

                </ul>
            </div>
            <asp:Repeater runat="server" ID="rptfoodlist">
                <ItemTemplate>
                    <a href="ShowTogo.aspx?id=<%#Eval("FPMaster")%>">
                        <div class="storeslist">
                            <p><%#Eval("TogoName")%></p>
                            <p class="pic">
                                <img src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" />
                            </p>
                            <ul>
                                <li class="title"><%#Eval("FoodName")%></li>
                                <%--<li class="sales">5<img src="images/dis.png" /><span>月销：200份</span></li>--%>
                                <li class="price">￥<%#Eval("FPrice")%><i>￥<%#Eval("OldPrice")%></i><span>抢购</span></li>
                            </ul>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body> 
</html>
<script src="javascript/jquery.js" type="text/javascript"></script>

<script src="javascript/jCommon.js" type="text/javascript"></script>
<script type="text/javascript">
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

    var hfod = $("#hfod").val();
    $("." + hfod).addClass("hover");
</script>



<script type="text/javascript">


    $(function () {


        var paralist = [];
        var para1 = request("para1").replace(/\/$/, "");



        if (para1 != "") {
            var myparas = para1.split("/");
            for (var i in myparas) {
                var value = myparas[i].replace(/[a-zA-Z]+/, "");
                var key = myparas[i].replace(/[0-9_]+/, "");
                paralist.push({ "key": key, "value": value });

                setCurByKey(key, value);
            }
        }

        function setCurByKey(key, value) {
            switch (key) {
                case "s":
                    $(".subsortitem").removeClass("selected");
                    $("#sortid" + value).addClass("selected");
                    var name = $("#sortid" + value).children().html();
                    $("#food_category").children('a').html(name);
                    $("#food_category").addClass("cul");
                    $("#hffirstsortid").val(value);

                    break;

                case "od":
                    $(".orderitem").removeClass("selected");
                    $("#od" + value).addClass("selected");
                    var name = $("#od" + value).children().html();
                    $("#sort_category").children('a').html(name);
                    $("#sort_category").addClass("cul");
                    break;
                case "a":
                    $(".saleitem").removeClass("selected");
                    $("#serverlink" + value).addClass("selected");
                    var name = $("#serverlink" + value).children().html();
                    $("#act_category").children('a').html(name);
                    $("#act_category").addClass("cul");
                    break;
                case "l":

                    $(".subsortitem").removeClass("selected");
                    $("#send_l" + value).addClass("selected");
                    var name = $("#send_l" + value).children().html();

                    $("#food_category").children('a').html(name);
                    $("#food_category").addClass("cul");
                    $("#hffirstsortid").val(value);



                    break;
                default:
            }
        }
    });



    //排序
    $("#sort_category").click(function () {
        $(".sort_category").show();
        $(".food_category").hide();
        $(".act_category").hide();
        $("#menu_mask").css("visibility", "visible");
        $(".food_two_con").hide();


    });

    //分类
    $("#food_category").click(function () {
        $(".food_category").show();
        $(".sort_category").hide();
        $(".act_category").hide();
        $("#menu_mask").css("visibility", "visible");
    });


    $("#act_category").click(function () {
        $(".act_category").show();
        $(".food_category").hide();
        $(".sort_category").hide();
        $("#menu_mask").css("visibility", "visible");
        $(".food_two_con").hide();
    });

    $("#J_bg").click(function () {

        $(".restaurant_nav_detail").hide();
        $("#J_bg").hide();
        $("#J_bg").css("z-index", "19999");
    });




</script>



