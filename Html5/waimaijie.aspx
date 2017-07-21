<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="waimaijie.aspx.cs" Inherits="Html5.waimaijie" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/home.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />
    <link href="css/pictip.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        .food_category {
            width: 100%;
            float: none;
        }
    </style>


</head>
<body>
    <input id="hfpage" runat="server" type="hidden" />
    <input type="hidden" id="hfod" runat="server" value="od0" />
    <div class="page">
        <div id="page_title">
            <a href="TogoList.aspx" id="back" runat="server" class=" top_left"></a>
            <h1><a href="index.aspx?auto=1">
                <img src="images/ico_w_address.png"><% = WebUtility.Left(address,12) %><i>></i></a></h1>
           
             <a id="togourl" class="top_search top_right" href="search.aspx"></a>

        </div>
        <div class="container ">


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
                    <li style="display:none;" id='send_l1' class="subsortitem"><a href="<%= getSortUrl("l", "0") %>">平台专送</a></li>
                    <asp:Repeater ID="rpttogosortlist" runat="server">
                        <ItemTemplate>
                            <li id='sortid<%# Eval("id") %>' class="subsortitem"><a href="<%# getSortUrl("s", Eval("id")) %>"><%#Eval("classname")%></a></li>
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
        <div style="top: 0px; left: 0px; width: 100%; background-color: rgb(0, 0, 0); z-index: 2; opacity: 0.3; position: fixed; visibility: hidden; height: 100%;" id="menu_mask"></div>





            <ul class="shoplist">
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
                                <div class="shoppromotion-marker" style="margin-top:5px;">
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
            <div class="con-btn" id="pages" runat="server">
            </div>
        </div>
    </div>


     <uc2:Foot runat="server" ID="foot" />
</body>
</html>

<script src="javascript/jquery.js" type="text/javascript"></script>

<script src="javascript/jCommon.js" type="text/javascript"></script>
<script type="text/javascript">
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




