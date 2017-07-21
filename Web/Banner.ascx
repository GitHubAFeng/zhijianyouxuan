<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Banner.ascx.cs" Inherits="Banner" %>
<script src="<%= WebUtility.GetUrl("~/javascript/jCommon.js?v=0110") %>" type="text/javascript"></script>
<link href="javascript/jbox/Skins/jbox.css" rel="stylesheet" />



<div class="top-bar">
    <div class="wrap">
        <div runat="server" class="top-left" id="divUnlogin">欢迎光临<%= SectionProxyData.GetSetValue(2) %>！
            <a href="<%=WebUtility.GetUrl("~/Login.aspx")%>">[请登录]</a>
            <a href="<%=WebUtility.GetUrl("~/RegisterByEmail.aspx") %>">[免费注册]</a>
        </div>
        <div runat="server" class="top-left" id="divLoinged">
            欢迎光临<%= SectionProxyData.GetSetValue(2) %>！
            <a runat="server" href="#" id="lbusername" class="lbusername"></a>
            <a href="<%=WebUtility.GetUrl("~/logout.aspx") %>"  class="lbusername" >[退出]
            </a>
        </div>
        <div class="top-right">
            <ul>
               
                <li class="dropli">
                    <a href="<%=WebUtility.GetUrl("~/mobile_phone.aspx") %>" class="drop">
                        <img src="<%=WebUtility .GetUrl("~/images/mobile.png") %>" />手机版<i class="vertical-bar"></i></a>
                </li>
                 <li class="dropli">
                    <a href="/user/MyOrderList.aspx" class="drop" runat="server" id="orderlink">我的订单<span class="arrow"></span><i class="vertical-bar"></i></a>
                </li>
                <li class="dropli">
                    <a href="<%=WebUtility.GetUrl("~/user/MyInfo.aspx") %>" class="drop">会员中心<span class="arrow"></span><i class="vertical-bar"></i></a>
                </li>
                <li class="dropli">
                    <a href="<%=WebUtility.GetUrl("~/OrderDetail.aspx") %>" class="drop">
                        <img src="<%=WebUtility .GetUrl("~/images/topcart.png") %>" />购物车<span id="countshop" runat="server" class="red padlr5">0</span>件<span class="arrow"></span><i class="vertical-bar"></i></a> 
                </li>
                <li class="dropli">
                    <a href="<%=WebUtility.GetUrl("~/shop/myindex.aspx") %>" class="drop">商家登录<span class="arrow"></span><i class="vertical-bar"></i></a>
                </li>
            </ul>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(".top-right .dropli").mouseover(function () {
        $(this).addClass("top-option-open");

    });
    $(".top-right .dropli").mouseout(function () {
        $(this).removeClass("top-option-open");

    });


</script>
