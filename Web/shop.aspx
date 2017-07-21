<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shop.aspx.cs" Inherits="shop" %>

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
    <meta http-equiv="X-UA-Compatible" content="IE=5; IE=8" />
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
            <div class="border-shadow mgb15">
                <div class="mainbord">
                    <asp:Repeater ID="rpshop" runat="server">
                        <ItemTemplate>
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
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="clearfix">
                <div class="left-side mgb10">
                    <div class="border-shadow">
                        <div class="mainbord">
                            <div class="shop-tit">
                                <ul class="left">
                                    <li class="cul shop-tit-cul" onclick="cutNavigation(0,this);"><a>菜单</a></li>
                                    <li class="shop-tit-cul" onclick="cutNavigation(1,this);"><a>评价</a></li>
                                    <li class="shop-tit-cul" onclick="cutNavigation(2,this);"><a>商家资质</a></li>
                                </ul>
                            </div>

                            <div id="food" runat="server">
                                <div class="classy-tit clearfix">
                                    <span class="tit-name fl">菜品分类：</span>
                                    <span class="fl allclassy">
                                        <a class="cul" href="#sort0" id="sort0" runat="server" sortid="0" onclick="sortitemclick(0);return false;">精品套餐</a>
                                        <asp:Repeater runat="server" ID="rptSortList">
                                            <ItemTemplate>
                                                <a href="#sort<%#Eval("sortid")%>" onclick="sortitemclick(<%#Eval("sortid")%>);return false;"
                                                    id="s_li_ad_<%#Eval("sortid")%>" sortid="<%#Eval("sortid")%>">
                                                    <%# Eval("sortname")%></a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </span>
                                </div>
                                <div class="grey-tit" id="sort0_1" runat="server">精品套餐</div>
                                <div class="tcaicon">
                                    <asp:Repeater ID="rptfoodpackage" runat="server">
                                        <ItemTemplate>
                                            <div class="norbord mgb20 taocanitem">
                                                <div class="clearfix">
                                                    <asp:Repeater ID="rptpackagedetail" runat="server" DataSource='<%#Eval("ItemList") %>'>
                                                        <ItemTemplate>
                                                            <div class="tc_img">
                                                                <a href="javascript:">
                                                                    <img style="width: 120px; height: 120px; border: 1px solid #DBDBDB" alt="<%#Eval("foodname") %>" src="<%#WebUtility.ShowPic(Eval("picture").ToString()) %>"></a>
                                                                <p><%#Eval("foodname") %></p>
                                                            </div>
                                                            <div style="<%# Convert.ToInt32(Container.ItemIndex + 1)%4 == 0 ? "display:none": ""%>" class="tc_icon">
                                                                <a href="javascript:">
                                                                    <img src="images/tc_add_icon.gif"></a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                                <div class="arrowlink">
                                                    <span class="leftlight"></span><span class="leftarrow"></span>
                                                    <span class="rightlight"></span><span class="rightarrow"></span>
                                                    <div class="arrowbord"></div>
                                                </div>
                                                <div class="tc_info">
                                                    <span class="orange"><%#Eval("title") %></span><span class="padlr10">|</span><span class="though">原价：￥<%#Eval("oldprice") %></span>
                                                    <span class="padlr10">|</span>套餐价：<span class="orange">￥<%#Eval("price") %>
                                                    </span>
                                                    <a style="margin-left: 30px;" class="white_btn" href="javascript:"
                                                        onclick="AddToShoppingCart('<%#Eval("pid") %>','<%#Eval("title")%>','<%#Eval("price")%>',1,<%#Eval("oldprice")%>);">加入餐车</a>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <asp:Repeater ID="rptSortList2" runat="server">
                                    <ItemTemplate>
                                        <div class="grey-tit" id="sort<%#Eval("sortid")%>_1"><%#Eval("sortname") %></div>
                                        <div class="cailicon">
                                            <ul class="cailist clearfix">
                                                <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#Eval("Foodlist")%>'>
                                                    <ItemTemplate>
                                                        <li style='<%#(Container.ItemIndex+1) % 2 == 0 ? "float:right;": ""%>'>





                                                            <span class="name fl"><a href="showfood.aspx?id=<%#Eval("FPMaster")%>&fid=<%# Eval("unid")%>" title="<%#Eval("foodName")%>">
                                                                <%#WebUtility.Left(Eval("foodName"),9) %>
                                                            </a></span>

                                                            <span class="span_img"><a class="tooltip fl" href="<%# WebUtility.ShowPic(Eval("Picture").ToString())%>">
                                                                <img alt="<%#Eval("foodName") %>" src="<%# WebUtility.ShowPic(Eval("Picture").ToString())%>" />
                                                            </a></span>

                                                            <span class="price fl"><%#Eval("FPrice")%><%# Convert.ToInt32(Eval("IsSpecial")) <= 1 ? "" :"<strong  style='color:red'>+</strong>"  %></span>
                                                            <a href="javascript:" id="add_bt_<%#Eval("Unid") %>" onclick="showdiv(<%# Eval("unid") %>, '<%# Eval("foodName") %>','份', '<%# Eval("FPrice") %>',<%# Eval("isspecial") %>,<%# Eval("isauth") %>);" class="white_btn fr">加入餐车</a>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="shopmenu-list clearfix" style="display: none">
                                    <div class="shopmenu-food clearfix">
                                        <div class="pic">
                                            <img src="images/picinfo.png" />
                                        </div>
                                        <div class="info">
                                            <span class="name">招牌石锅拌饭套餐</span>
                                            <span class="txt">什素拌饭+韩国泡菜+紫菜汤</span>
                                            <span class="sales">月售180份</span>
                                            <span class="price">￥<i>26</i></span>
                                        </div>
                                        <a class="btn">加入餐车</a>
                                    </div>
                                    <div class="shopmenu-food clearfix">
                                        <div class="pic">
                                            <img src="images/nopic_02.jpg" />
                                        </div>
                                        <div class="info">
                                            <span class="name">招牌石锅拌饭套餐</span>
                                            <span class="txt">什素拌饭+韩国泡菜+紫菜汤</span>
                                            <span class="sales">月售180份</span>
                                            <span class="price">￥<i>26</i></span>
                                        </div>
                                        <a class="btn">加入餐车</a>
                                    </div>
                                    <div class="shopmenu-food clearfix">
                                        <div class="pic">
                                            <img src="images/fruit01.png" />
                                        </div>
                                        <div class="info">
                                            <span class="name">招牌石锅拌饭套餐</span>
                                            <span class="txt">什素拌饭+韩国泡菜+紫菜汤</span>
                                            <span class="sales">月售180份</span>
                                            <span class="price">￥<i>26</i></span>
                                        </div>
                                        <a class="btn">加入餐车</a>
                                    </div>
                                    <div class="shopmenu-food clearfix">
                                        <div class="pic">
                                            <img src="images/weixindefault.png" />
                                        </div>
                                        <div class="info">
                                            <span class="name">招牌石锅拌饭套餐</span>
                                            <span class="txt">什素拌饭+韩国泡菜+紫菜汤</span>
                                            <span class="sales">月售180份</span>
                                            <span class="price">￥<i>26</i></span>
                                        </div>
                                        <a class="btn">加入餐车</a>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div id="comment" style="display: none;">
                        <!--最新评论 start-->
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hidaddress" runat="server" Value="0" />
                                <div class="htitle">
                                    <div class="htitle_bg">
                                        <h4>最新评论</h4>
                                    </div>
                                </div>
                                <div class="view_show">
                                    <asp:Repeater runat="server" ID="rptopion">
                                        <ItemTemplate>
                                            <div class="view_unit">
                                                <img class="photo_img" src="<%# WebUtility.ShowPic(Eval("picture").ToString()) %>"
                                                    width="48" height="48" />
                                                <div class="view_unit_info">
                                                    <p>
                                                        <span class="time">
                                                            <%#Eval("PostTime")%></span><span class="bold_blue"><%# Eval("username")%></span><span
                                                                class="at">@</span><span class="bold_blue"><%# Eval("togoname")%></span>
                                                    </p>
                                                    <div class="start_div" style="margin-top: 8px;">
                                                        <span class="word">口味</span> <span class="start start<%# Convert.ToInt32(Eval("FlavorGrade")) *2+1 %>"></span><span class="word">服务</span> <span class="start start<%# Convert.ToInt32(Eval("ServiceGrade"))*2+1 %>"></span><span class="word">速度</span> <span class="start start<%# Convert.ToInt32(Eval("SpeedGrade"))*2+1 %>"></span>
                                                    </div>
                                                    <div class="view_info_con">
                                                        <p>
                                                            <%# Eval("comment")%>
                                                        </p>
                                                    </div>
                                                    <%# Eval("rcontent").ToString() == "" ? "" : "<div class=\"view_info_con\"><p>回复：" + Eval("rcontent") + "</p></div>"%>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="pages">
                                        <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                                            CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                                            HorizontalAlign="Right" ShowCustomInfoSection="Left" CustomInfoTextAlign="Right"
                                            CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                                            TextBeforePageIndexBox="转到 " OnPageChanged="AspNetPager1_PageChanged" PageIndexBoxClass="flattext"
                                            ShowPageIndex="True" PageSize="5" SubmitButtonClass="flatbutton" SubmitButtonText=" GO "
                                            TextAfterPageIndexBox=" 页 " Wrap="False" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" PrevPageText="上一页">
                                        </webdiyer:AspNetPager>
                                    </div>
                                </div>
                                <!--最新评论 end-->
                                <!--发表评论 start-->
                                <div class="htitle">
                                    <div class="htitle_bg">
                                        <h4>发表评论</h4>
                                    </div>
                                </div>
                                <div class="send_view">
                                    <div class="view_div">
                                        <div class="view_warm">
                                            <span class="yellow_bold_big">评分有奖</span><span class="gray">享用美食后，在这里对餐馆进行评分，可获<%=SectionProxyData.GetSetValue(23)%>个积分，不要错过哦！</span>
                                        </div>
                                        <p class="point_check">
                                            <span>口味：</span>
                                            <asp:DropDownList runat="server" ID="ddlflover" CssClass="drop_list">
                                                <asp:ListItem Value="5">5星</asp:ListItem>
                                                <asp:ListItem Value="4">4星</asp:ListItem>
                                                <asp:ListItem Value="3">3星</asp:ListItem>
                                                <asp:ListItem Value="2">2星</asp:ListItem>
                                                <asp:ListItem Value="1">1星</asp:ListItem>
                                            </asp:DropDownList>
                                            <span>服务：</span>
                                            <asp:DropDownList runat="server" ID="ddlservice" CssClass="drop_list">
                                                <asp:ListItem Value="5">5星</asp:ListItem>
                                                <asp:ListItem Value="4">4星</asp:ListItem>
                                                <asp:ListItem Value="3">3星</asp:ListItem>
                                                <asp:ListItem Value="2">2星</asp:ListItem>
                                                <asp:ListItem Value="1">1星</asp:ListItem>
                                            </asp:DropDownList>
                                            <span>速度：</span>
                                            <asp:DropDownList runat="server" ID="ddlspeed" CssClass="drop_list">
                                                <asp:ListItem Value="5">5星</asp:ListItem>
                                                <asp:ListItem Value="4">4星</asp:ListItem>
                                                <asp:ListItem Value="3">3星</asp:ListItem>
                                                <asp:ListItem Value="2">2星</asp:ListItem>
                                                <asp:ListItem Value="1">1星</asp:ListItem>
                                            </asp:DropDownList>
                                        </p>
                                        <p>
                                            <span style="float: left; margin-right: 5px;">评论内容：</span>
                                            <textarea class="area_box" name="" style="padding: 5px;" runat="server" id="textarea"></textarea>
                                        </p>
                                        <div class="clear">
                                        </div>
                                        <p style="margin-top: 10px;">
                                            <asp:Button ID="btPostCommond" runat="server" Text="确认发布" CssClass="send_view_btn" OnClick="Postt_Click"
                                                OnClientClick="return checkQ();" />

                                        </p>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--发表评论 end-->

                    </div>

                    <div id="qualification" style="display: none;">
                        <!--商家资质 start-->
                        <div class="htitle">
                            <div class="htitle_bg">
                                <h4>商家资质</h4>
                            </div>
                        </div>
                        <div class="view_show">
                            <div class="qualification-list">
                                <div class="img-wrapper" id="div_licensePic">
                                    <img id="img_licensePic" runat="server" src="" alt="" onclick="showimg(this);" />
                                </div>
                                <div class="img-wrapper" id="div_cateringPic">
                                    <img id="img_cateringPic" runat="server" src="" alt="" onclick="showimg(this);" />
                                </div>
                            </div>

                            <div class="mask img-show-mask" style="display: none;"></div>
                            <div class="img-show-wrapper" style="left: 231.5px; top: 291px; display: none; opacity: 1;">
                                <a href="javascript:closeimg();" class="icon i-img-show-close img-show-close"></a>
                                <div class="img-show-content">
                                    <img src="" id="img-show" alt="">
                                </div>
                            </div>
                        </div>
                        <!--商家资质 end-->
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
<script src="javascript/jCommon.js" type="text/javascript"></script>
<script src="javascript/ShowDivDialog.js" type="text/javascript"></script>
<script src="javascript/supperCart.js?V=21" type="text/javascript"></script>
<script src="javascript/float.js" type="text/javascript"></script>



<script src="javascript/tip.js" type="text/javascript"></script>
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
