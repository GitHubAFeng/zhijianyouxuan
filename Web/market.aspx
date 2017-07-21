<%@ Page Language="C#" AutoEventWireup="true" CodeFile="market.aspx.cs" Inherits="shop_market" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/stylebanner.ascx" TagName="stylebanner" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />
    <title>超市</title>
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/shop.css?v=6" rel="stylesheet" />
    <link href="css/market.css" rel="stylesheet" />
    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            ListCart();
            initnav(3);
        })
    </script>

</head>
<body>
    <form runat="server">

        <input type="hidden" id="hnTogoBusiness" runat="server" />
        <input type="hidden" id="hnTogoStatus" runat="server" />
        <input type="hidden" id="hftogoGrade" runat="server" />
        <asp:HiddenField runat="server" ID="hfcode" />
        <asp:HiddenField runat="server" ID="hidTogoId" />
        <asp:HiddenField runat="server" ID="hidTogoName" />
        <asp:HiddenField runat="server" ID="hidUid" Value="-1" />
        <asp:HiddenField runat="server" ID="hfsendmoney" Value="0" />
        <asp:HiddenField runat="server" ID="hfminmoney" Value="0" />

        <top:banner ID="Banner1" runat="server" />
        <uc1:header ID="header" runat="server" />

        <!--主体部分-->
        <div class="market_top" style="" runat="server" id="market_bgbox">

            <asp:Repeater ID="rpshop" runat="server">
                <ItemTemplate>

                    <div class="wrap">

                        <div style="position: relative;">

                            <div class="market_shopcat" onclick="$('#basketTitleWrap').toggle()" style="position: relative; z-index: 1;">
                                <span>我的购物车</span>
                                <img src="images/market/arrow_down.png" />
                            </div>


                            <div class="mainbord" id="basketTitleWrap" style="position: absolute; right: 0; top: 40px; width: 248px; z-index: 999; display: none;">

                                <div class="mycart" style="background-color: #fff;">

                                    <div class="shop_cart_con">
                                        <div class="shop_table_div">
                                            <div id="cartContent">
                                            </div>
                                        </div>


                                    </div>

                                    <div class="btn">
                                        <a href="javascript:" class="check_btn" onclick="return CheckCart();">立即结算</a>
                                    </div>
                                </div>

                            </div>

                        </div>




                        <div class="market_img">
                            <img src="<%# WebUtility.ShowPic(Eval("picture").ToString()) %>" style="width: 120px; height: 120px; border-radius:50%;" />
                        </div>
                        <div class="market_infor">
                            <div class="market_infor_con">
                                <ul class="market_style">
                                    <li>
                                        <h4><%#Eval("SendLimit") %></h4>
                                        <p>起送价格/元起</p>
                                    </li>
                                    <li>
                                        <h4><%#Eval("SendFee")%></h4>
                                        <p>配送费用/元</p>
                                    </li>
                                    <li>
                                        <h4><%#Eval("senttime")%></h4>
                                        <p>预计到达/分钟</p>
                                    </li>
                                </ul>
                                <div class="market_time">
                                    <span>营业时间：<%#Eval("opentimestr") %></span>

                                </div>
                                <div class="market_search">
                                    <input class="market_text" type="text" id="keyword" name="keyword" placeholder="请输入商品名称" onkeydown="return enterIn(event,searchfoodname)" />
                                    <input class="market_btn" type="button" value="搜索" name="search" onclick="searchfoodname()" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>


        <div class="wrap" style="position: relative;">

            <asp:Repeater ID="rptSortList2" runat="server">
                <ItemTemplate>

                    <div class="market_title" id="sort<%#Eval("sortid")%>_1">
                        <div class="market_f"><%# Convert.ToInt32(Container.ItemIndex + 1)%>F</div>
                        <div class="market_name"><%#Eval("sortname") %></div>
                        <div class="market_back"><a href="javascript:" onclick="$('html,body').animate({ scrollTop: 0 }, 600)">回到顶部</a></div>
                    </div>
                    <div class="market_column">
                        <ul>
                            <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#Eval("Foodlist")%>'>
                                <ItemTemplate>

                                    <li style='<%#(Container.ItemIndex+1) % 4 == 0 ? "margin-right:0": ""%>;' data-name="<%#Eval("foodName") %>" class="food_items">
                                        <div class="img_name">
                                            <h5>
                                                <img alt="<%#Eval("foodName") %>" src="<%# WebUtility.ShowPic(Eval("Picture").ToString())%>" width="216" height="216" /></h5>
                                            <h4><%#WebUtility.Left(Eval("foodName"),14) %></h4>
                                        </div>
                                        <div class="market_price">

                                            <h3 style="line-height: 40px;">￥<%#Eval("FPrice")%></h3>
                                        </div>

                                        <div class="market_addcart" id="add_bt_<%#Eval("Unid") %>">
                                            <a href="javascript:" onclick="showdiv(<%# Eval("unid") %>, '<%# Eval("foodName") %>','份', '<%# Eval("FPrice") %>',<%# Eval("isspecial") %>,<%# Eval("isauth") %>);" >
                                                <img src="images/market/shopcat_btn.png"></a>
                                        </div>
                                    </li>

                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="market_nav navbar-wrapper" id="market_nav">
            <ul class="">

                <asp:Repeater runat="server" ID="rptSortList">
                    <ItemTemplate>
                        <li class=" menuItem" data-menu="sort<%#Eval("sortid")%>_1"><a href="#sort<%#Eval("sortid")%>_1" class="smooth"><span><%# Convert.ToInt32(Container.ItemIndex + 1)%>F</span><%# Eval("sortname")%></a></li>
                    </ItemTemplate>
                </asp:Repeater>


            </ul>
        </div>

        <foot:foot ID="Foot1" runat="server" />
        <uc4:stylebanner runat="server" ID="mycart1" />
    </form>
</body>
</html>

<script src="javascript/ShowDivDialog.js" type="text/javascript"></script>

<script src="/javascript/supperCart.js?V=2" type="text/javascript"></script>
<script src="javascript/float.js" type="text/javascript"></script>

<script src="javascript/tip.js?v=0815" type="text/javascript"></script>
<script src="javascript/stickUp.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function () {
        $(".smooth").click(function () {
            var href = $(this).attr("href");
            var pos = $(href).offset().top;
            $("html,body").animate({ scrollTop: pos }, 600);
            return false;
        });
    })

    function searchfoodname() {
        var key = $("#keyword").val() + "";
        if (key == "" || key == "请输入餐品") {
            $(".food_items").show();
            return;
        }
        $(".food_items").each(function () {
            var cname = $(this).attr("data-name");
            if (cname.indexOf(key) >= 0) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        })
    }

    jQuery(function ($) {

        var cw = document.documentElement.clientWidth;
        var navleft = (cw - 980) / 2 - 90 - 7;
        $("#market_nav").css({ "left": navleft + "px" })

        $(document).ready(function () {
            $('.navbar-wrapper').stickUp({
                itemClass: 'menuItem',
                itemHover: 'cur',
                topMargin: 'auto'
            });
        });
    });


</script>
