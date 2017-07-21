<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotstores.aspx.cs" Inherits="Html5.hotstores" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
       <title><%= SectionProxyData.GetSetValue(2) %></title>
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
        body {background-color:#ff6d00;
        }
        .storesitem {
            background-color:#ff6d00;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hfod" runat="server" value="od0" />
        <div class="page" style="padding-bottom: 0;">
            <div id="page_title">
                <a href="TogoList.aspx" id="back" runat="server" class=" top_left"></a>
                <h1>大家都在吃</h1>
            </div>
        </div>
        <div class="ad_picuter"><img src="images/ad_banner.png" style="width:100%; display:none;" /></div>
        
        <div class="stores_banner" style="display:none">
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

        <div class="storesitem clearfix">
          
         
            <asp:Repeater runat="server" ID="rptfoodlist">
                <ItemTemplate>
                    <a href="ShowTogo.aspx?id=<%#Eval("FPMaster")%>&addfood=<%#Eval("unid")%>">
                        <div class="storeslist">
                            <span class ="hotno1"><%#(Container.ItemIndex+1) %></span>
                            <span class ="hotno2" style="display:none">2</span>
                            <span class ="hotno3" style="display:none">3</span>
                            <p class="pic">
                                <img src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" />
                            </p>
                            <p>【<%# WebUtility.Left(Eval("TogoName").ToString(),7)%>】</p>
                            <ul>
                                <li class="title" style="white-space:pre-line;"><%# Eval("FoodName") %></li>
                                <li class="sales"><span>月销：<%# Eval("SortName") %>份</span></li>
                                <li class="price">￥<%#Eval("FPrice")%></i><span>来一份</span></li>
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
    });






</script>



