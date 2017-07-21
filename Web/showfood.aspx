<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showfood.aspx.cs" Inherits="showfood" %>

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
    <title></title>
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/shop.css?v=6" rel="stylesheet" />
    <link href="css/cart.css" rel="stylesheet" />
    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            ListCart();
            var shopid = request("id");
            if (shopid == 1) {
                initnav(3);
            }
            else {
                initnav(2);
            }

            if ($("#hfisLicense").val() == "0") {

                $("#div_licensePic").remove();
            }
            if ($("#hfisCatering").val() == "0") {

                $("#div_cateringPic").remove();
            }
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

        <asp:HiddenField runat="server" ID="hfisLicense" Value="0" />
        <asp:HiddenField runat="server" ID="hfisCatering" Value="0" />

        <top:banner ID="Banner1" runat="server" />
        <uc1:header ID="header" runat="server" />
        <!--主体部分-->
        <div class="wrap">

            <asp:Repeater ID="rpshop" runat="server">
                <ItemTemplate>

                    <div class="hplace_bg">
                        <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt;<a href="shop.aspx?id=<%#Eval("unid") %>"><%#Eval("name") %></a> &gt;&gt;菜品详情</span>
                    </div>


                    <div class="border-shadow mgb15">
                        <div class="mainbord">

                            <div class="shopinfo clearfix ">
                                <div class="img">
                                    <img src="<%# WebUtility.ShowPic(Eval("picture").ToString()) %>" />
                                </div>

                                <div class="info">
                                    <div style="margin-bottom: 12px;">
                                        <span class="name"><%#Eval("name") %></span>
                                        <%# ParseBisness(Eval("Status"), Eval("isbisness"))%>
                                        <span class="resinfo_fav">
                                            <a
                                                id="ultarget3"
                                                href="javascript:" onclick="addtogo('ultarget3','<%#Eval("unid") %>','0',-22 , -70);return false;"><%#Convert.ToInt32(Eval("iscollect")) == 1 ? "已经收藏" : "收藏本店"%></a>


                                        </span>
                                    </div>

                                    <table width="100%" class="shoptable">
                                        <tr>
                                            <td colspan="2"><span class="mocn">商家地址：</span><span><%#Eval("Address") %></span></td>
                                        </tr>
                                        <tr>
                                            <td><span class="mocn">起送价格：</span><span class="orange"><%#Eval("SendLimit") %>元起送</span></td>
                                            <td><span class="mocn">配送费用：</span><span class="orange"><%#Eval("SendFee")%>元</span></td>
                                        </tr>
                                        <tr>
                                            <td><span class="mocn">累计销售：</span><span class="orange"><%#Eval("pop") %>次</span></td>
                                            <td><span class="mocn">营业时间：</span><span><%#Eval("opentimestr") %></span></td>
                                        </tr>
                                        <tr>

                                            <td><span class="mocn">预计到达：</span><span class="ornge"><%#Eval("senttime") %>分钟</span></td>
                                            <td><span class="mocn">优惠活动：</span>
                                                <span>
                                                    <asp:Repeater runat="server" ID="Repeater1" DataSource='<%#Eval("pictags")%>'>
                                                        <ItemTemplate>
                                                            <span class="restaurant-icons  tooltip_on" style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0)" title="<%#Eval("Title")%>"></span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </span>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><span class="mocn">店铺活动：</span><span style="line-height: 20px;"><%# WebUtility.Left(Eval("special"),155)%></span></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>


            <div class="clearfix">
                <div class="left-side mgb10">
                    <div class="border-shadow">
                        <div class="mainbord">
                            <div class="shop-tit">
                                <ul class="left">
                                    <li class="cul shop-tit-cul"><a>菜品详情</a></li>

                                </ul>
                            </div>

                            <div id="food" runat="server">




                                <asp:Repeater ID="rptSortList2" runat="server">
                                    <ItemTemplate>

                                        <div class="cailicon">


                                            <div class="span_img fl" style="overflow: hidden; padding: 20px;">
                                                <img alt="" src="<%# WebUtility.ShowPic(Eval("Picture").ToString())%>" style="width: 200px; height: 200px;" />
                                            </div>

                                            <div style="margin-left: 20px; float: left; padding: 20px; padding-left: 0;">

                                                <div>
                                                    <span class="name fl"><%#Eval("foodName") %></span>

                                                    <span class="price "><%#Eval("FPrice")%><%# Convert.ToInt32(Eval("IsSpecial")) <= 1 ? "" :"<strong  style='color:red'>+</strong>"  %></span>

                                                </div>
                                                <div class="clear"></div>
                                                <div style="margin-top: 10px;">
                                                    <a href="javascript:" style="float:none;" id="add_bt_<%#Eval("Unid") %>" onclick="showdiv(<%# Eval("unid") %>, '<%# Eval("foodName") %>','份', '<%# Eval("FPrice") %>',<%# Eval("isspecial") %>,<%# Eval("isauth") %>);" class="white_btn fr">加入餐车</a>
                                                </div>
                                                <div class="clear"></div>

                                                <div style="width:399px; margin-top:20px; line-height:25px;">

                                                    <%#Eval("Taste") %>

                                                </div>


                                            </div>
                                            <div class="clear"></div>








                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                        </div>
                    </div>





                </div>

                <div class="right-side mgb10">
                    <div id="mytarget">
                    </div>
                    <div class="mainbord" id="basketTitleWrap" style="width: 240px;">
                        <div class="mycart" style="background-color: #fff;">
                            <div class="shop-tit">
                                <ul class="left">
                                    <li class="cul">我的餐车</li>
                                </ul>
                            </div>
                            <div class="shop_cart_con">
                                <div class="shop_table_div">
                                    <div id="cartContent">
                                    </div>
                                </div>
                                <div class="sendlimit">
                                    <span>起送价格:</span>
                                    <span id="limit" runat="server">0</span>
                                    <span>元</span>
                                </div>
                            </div>
                            <div class="btn">
                                <a href="javascript:" class="check_btn" onclick="return CheckCart();">立即结算</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <foot:foot ID="Foot1" runat="server" />

        <uc4:stylebanner runat="server" ID="mycart1" />

        <div class="fav_bg" id="dpop" style="display: none">
            <h3 id="lbMsg">请登录</h3>
            <a title="Go to my favorite" href="user/MyShops.aspx">查看收藏夾</a>
        </div>
    </form>
</body>
</html>

<script src="javascript/ShowDivDialog.js" type="text/javascript"></script>
<script src="/javascript/supperCart.js?V=1"></script>
<script src="javascript/float.js"></script>



<script src="javascript/tip.js"></script>
<script src="javascript/tippic.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {

        //当没有精品分类的时候，下一个高亮
        if ($("#sort0").css("display") == "none") {
            $("#sort0").next().addClass("cul");
        }
        $("a.tooltip").each(function () {
            var srcstr = $(this).attr("href");
            if (srcstr == "/images/nopic_02.jpg") {
                $(this).find("img").attr("style", "display:none");
            }
        });
    })

    //分类切换
    $(".allclassy a").click(function () {
        $(".allclassy a").each(function () {
            $(this).removeClass("cul");
        });
        $(this).addClass("cul");
    });


    $(document).ready(function () {
        window.onscroll = function () { winscroll() } //给滚动绑定事件
        $(".taocanitem").find(".tc_icon:last").hide();

    })

    ///分类点击,页面到相应位置
    function sortitemclick(sortid) {
        var targetele = "";
        targetele = "sort" + sortid + "_1";
        var pos = $("#" + targetele).offset().top;
        $("html,body").animate({ scrollTop: pos }, 1000);
        return false;
    }

    //资质放大
    function showimg(object) {

        var $this = $(object);
        $("#img-show").attr("src", $this.attr("src"));
        $(".img-show-mask").show();
        $(".img-show-wrapper").show();
    }

    function closeimg() {
        $(".img-show-mask").hide();
        $(".img-show-wrapper").hide();
    }

    //切换导航
    function cutNavigation(i, object) {
        $(".shop-tit-cul").removeClass("cul");
        $(object).addClass("cul");
        if (i == 0) {
            $("#food").show();
            $("#comment").hide();
            $("#qualification").hide();
        } else if (i == 1) {
            $("#food").hide();
            $("#comment").show();
            $("#qualification").hide();
        } else if (i == 2) {
            $("#food").hide();
            $("#comment").hide();
            $("#qualification").show();
        }
    }

    //提交CheckCart
    function CheckCart() {
        if ($(".myshop").length <= 0) {
            alert('你的餐盒是空的,不的提交!');
            return false;
        }
        var togoStatus = $D("hnTogoStatus").value;
        if (togoStatus != "1") {
            alert('商家不在营业，不能提交订单');
            return false;
        }

        /* 3、配送费设置0元的情况下，则必须满足起送价格； */
        var totalmoney = parseFloat(document.getElementById("allprice").innerHTML.substr(1));
        var limitmoney = parseFloat($("#hfminmoney").val());
        // var hfsendmoney = $("#hfsendmoney").val();
        if (totalmoney < limitmoney) {    //hfsendmoney == "0" &&
            alert('亲~本餐厅到您地址起送价格为' + limitmoney + '元噢！');
            return false;
        }

        var togoid = $D("hidTogoId").value;

        window.location = "OrderDetail.aspx?togoid=" + togoid;
    }
</script>
