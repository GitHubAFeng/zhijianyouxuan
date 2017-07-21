<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="shop_Left" %>

<!-- 代码部分begin -->
<div class="containers">
    <input type="hidden" id="retime" runat="server" />
    <div class="topbar">
        <div class="sreachbox">
            <input placeholder="订单序号、订单号、手机号或地址" id="keys" value="" />
            <a href="javascript:Search_Click();" id="btSearch"></a>
        </div>
    </div>
    <div class="leftsidebar_box">
        <div class="customname">
            外卖商户端<a href="logout.aspx" style="color: #f5f5f5; font-size: 12px;">  退出</a>
        </div>
        <dl class="home">
            <dt>商家首页<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/myindex.aspx">渠道主页</a></dd>
        </dl>
        <dl class="ordermanagement">
            <dt>订单管理<i class="thenew"></i>
                <img src="images/select_xl01.png">
            </dt>
            <dd class="first_dd">
                <a href="/shop/OrderList.aspx?id=1">新订单<span class="newmsg" id="newmsg">0</span></a>
            </dd>
            <dd><a href="/shop/OrderList.aspx">所有订单</a></dd>
            <dd><a href="/shop/HurryOrder.aspx">催单<span class="newmsg" id="hurmsg">0</span></a></dd>
            <dd><a href="/shop/cancelOrder.aspx">退单<span class="newmsg" id="canmsg">0</span></a></dd>
            <dd><a href="/shop/expressOrder.aspx">跑腿订单</a></dd>
        </dl>
        <dl class="After_sale">
            <dt>菜单管理<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/FoodSortList.aspx">餐品类别列表</a></dd>
            <dd><a href="/shop/FoodSortDetail.aspx">添加餐品类别</a></dd>
            <dd><a href="/shop/FoodList.aspx">餐品列表</a></dd>
            <dd><a href="/shop/FoodDetail.aspx">添加餐品</a></dd>
            <dd><a href="/shop/Foodimport.aspx">批量导入</a></dd>
        </dl>
        <dl class="financial">
            <dt>财务结算<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/settlelist.aspx">订单结算报表</a></dd>
            <dd><a href="/shop/settlecount.aspx">结算帐号</a></dd>
            <dd><a href="/shop/cachoutlist.aspx">收支记录</a></dd>
            <dd><a href="/shop/cachoutdetail.aspx">提现申请</a></dd>
        </dl>
        <dl class="Business_analysis">
            <dt>评论管理<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/TogoCommentList.aspx">评论管理</a></dd>
        </dl>
        <dl class="activities">
            <dt>代客下单<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/Service/OrderCrm.aspx">代客下单</a></dd>
        </dl>
        <%--        <dl class="features_service">
            <dt>特色服务<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="#">后台用户管理</a></dd>
            <dd><a href="#">角色管理</a></dd>
            <dd><a href="#">客户类型管理</a></dd>
            <dd><a href="#">栏目管理</a></dd>
            <dd><a href="#">微官网模板组管理</a></dd>
            <dd><a href="#">商城模板管理</a></dd>
            <dd><a href="#">微功能管理</a></dd>
            <dd><a href="#">修改用户密码</a></dd>
        </dl>--%>
        <dl class="setting">
            <dt>资料管理<img src="images/select_xl01.png"></dt>
            <dd class="first_dd"><a href="/shop/myshop.aspx">修改资料</a></dd>
            <dd><a href="/shop/TogoLocal.aspx">商家定位</a></dd>
            <dd><a href="/shop/SetStatus.aspx">状态管理</a></dd>
            <dd><a href="/shop/myPromotion.aspx">促销活动</a></dd>
            <dd><a href="/shop/qualification.aspx">商家资质管理</a></dd>
        </dl>
        <input runat="server" id="tbshopid" class="myshopid" style="display: none" />
    </div>
</div>
<script src="../javascript/jCommon.js?v=1" type="text/javascript"></script>
<% if (isorder == "-1")
   { %>
<script src="js/soundmanager2.js" type="text/javascript"></script>
<%} %>
<script>
    $(function () {
        leftclick();
        $(".leftsidebar_box dt").css({ "background-color": "#4e87ce" });
        $(".leftsidebar_box dt img").attr("src", "images/select_xl01.png");
        $(".leftsidebar_box dd").hide();
        initAdLeft();

        var key = request("key");
        if (key != "") {
            $("#keys").val(unescape(key));
        }
    })

    function leftclick() {
        $(".leftsidebar_box dt").click(function () {
            $(".leftsidebar_box dt").css({ "background-color": "#4e87ce" })
            $(this).css({ "background-color": "#2768b9" });
            $(this).parent().find('dd').removeClass("menu_chioce");
            $(".leftsidebar_box dt img").attr("src", "images/select_xl01.png");
            $(this).parent().find('img').attr("src", "images/select_xl.png");
            $(".menu_chioce").slideUp();
            $(this).parent().find('dd').slideToggle();
            $(this).parent().find('dd').addClass("menu_chioce");
        });
    }
    function Search_Click() {
        var key = escape($("#keys").val());
        window.location = "OrderList.aspx?key=" + key;

        return true;
    }
    function getUrl(url) {
        var index = url.lastIndexOf("/");
        var paramIndex = url.lastIndexOf("?");
        if (paramIndex != -1) {
            url = url.substring(index + 1, paramIndex);
        }
        else {
            url = url.substr(index + 1);
        }
        return url;
    }

    function initAdLeft() {
        var $alldivs = $(".leftsidebar_box>dl");
        var fileUrl = getUrl(document.location.href).toLowerCase();
        for (var i = 0; i < $alldivs.length; i++) {
            var $div = $($alldivs[i]);
            var $alllinks = $div.find("a[href!=#]");

            for (var j = 0; j < $alllinks.length; j++) {
                var $link = $($alllinks[j]);
                var url = $link.attr("href");
                url = getUrl(url).toLowerCase();
                if (url == fileUrl) {
                    showContent(i + 1);
                    $link.addClass("cur");
                    return;
                }
            }
        }
    }
    function showContent(div) {
        var ddt = $(".leftsidebar_box>dl").eq(div - 1);
        $(".leftsidebar_box dt").css({ "background-color": "#4e87ce" })
        ddt.children("dt").css({ "background-color": "#2768b9" });
        ddt.find('dd').removeClass("menu_chioce");
        $(".leftsidebar_box dt img").attr("src", "images/select_xl01.png");
        ddt.parent().find('img').attr("src", "images/select_xl.png");
        $(".menu_chioce").slideUp();
        ddt.find('dd').slideToggle();
        ddt.find('dd').addClass("menu_chioce");
    }
</script>
<!-- 代码部分end -->

<script src="../javascript/jquery.floatDiv.js"></script>

<script type="text/javascript">
    function getorder() {
        var tid = $(".myshopid").val() + "";
        jQuery.ajax(
         {
             type: "post",
             url: "/Ajax/gettogoorder.aspx",
             data: "tid=" + tid + "&t=" + new Date().getTime(),
             success: function (msg) {
                 //有订单
                 var ret = msg.split("&&");
                 if (ret[0] != "0") {
                     j_ShowWindow(ret[3], "success");
                     $("#divMsg").floatdiv("rightbottom");
                     $("#newmsg").html(ret[0]);
                     var fileUrl = getUrl(document.location.href).toLowerCase();
                     if (fileUrl != "orderlist.aspx") {
                         soundManager.debugMode = false;
                         soundManager.debugFlash = false;
                         soundManager.url = "soundmanager2.swf";
                         soundManager.play('mySound2', 'notify.mp3');
                     }
                 }
                 if (ret[0] != "0" || ret[1] != "0" || ret[2] != "0") {
                     $(".ordermanagement>dt>i").addClass("thenew");
                 }
                 else {
                     $(".ordermanagement>dt>i").removeClass("thenew");
                 }
                 $("#hurmsg").html(ret[1]);
                 $("#canmsg").html(ret[2]);
             }
         })
    }
    $(document).ready(function () {

        var fileUrl = getUrl(document.location.href).toLowerCase();
        if (fileUrl != "orderlist.aspx") {
        }
        getorder();
        var time = parseInt($("#Left_retime").val()) * 1000;
        setInterval("getorder()", time);
    })

</script>
