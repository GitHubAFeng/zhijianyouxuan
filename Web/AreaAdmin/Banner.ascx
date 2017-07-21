<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Banner.ascx.cs" Inherits="AreaAdmin_Banner" %>

<script type="text/javascript">

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

    function initBanner() {
        var $allLis = $("#navigation>ul>li");
        var fileUrl = getUrl(document.location.href).toLowerCase();
        for (var i = 0; i < $allLis.length; i++) {
            var li = $allLis[i];
            var $li = $(li);
            $alllinks = $li.find("a[href!=#]");
            var str = "";
            for (var j = 0; j < $alllinks.length; j++) {
                var $link = $($alllinks[j]);
                var url = $link.attr("href");
                url = getUrl(url).toLowerCase();
                if (fileUrl == url) {
                    $li.addClass(" parent level0 active");
                    return;
                }
            }
        }


    }

    $(function () {
        initBanner();
    })

</script>

<style type="text/css">
    .myhide {
        display: none;
    }
</style>
<div class="header" id="header">
    <div class="header-top">
        <a href="<%=WebUtility.GetUrl("~/index.aspx")%>">
            <img src='<%=ResolveClientUrl("~/images/logo.png")%>' class="logo" style="height: 49px" />
        </a>
        <div class="header-right">
            <p class="super">
                登录用户 <a href='<%=ResolveClientUrl("~/AreaAdmin/Permission/updateadmin.aspx") %>'><span id="snUsername" runat="server"></span></a><span class="separator">|</span>
                <span id="snDate" runat="server"></span><span class="separator">|</span><a href='<%=ResolveClientUrl("~/AreaAdmin/login.aspx?out=1") %>'>
                    <strong>退出</strong></a>
            </p>
            <p class="super" style="padding-right: 400px;">
                <img style="margin-left: 3px; margin-top: 2px;" src='<%=ResolveClientUrl("~/AreaAdmin/images/msgtip.gif")%>' />
                <span style="margin-left: 4px;">当前未处理订单<a href='<%=WebUtility.GetUrl("~/AreaAdmin/Shop/OrderList.aspx?type=1") %>'><label
                    runat="server" id="tcount"></label></a>个</span>
            </p>
        </div>
    </div>
    <div class="clear">
    </div>
    <!-- menu start -->
    <div class="nav-bar" id="navigation">
        <ul id="nav">
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/basic.aspx")%>'><span>系统首页</span>
                </a></li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div1"><a href='#'><span>商家管理</span></a>
                <ul>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopList.aspx")%>"
                        id="A1"><span>商家列表</span></a></li>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopDetail.aspx")%>"
                        id="A1"><span>商家信息(点击新增)</span></a></li>
                 
                    <li class=" last level2"><a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopReviewList.aspx")%>"
                        id="A6"><span>商家评论</span></a></li>
                </ul>
            </li>
            
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div3"><a href='#'><span>订单管理</span></a>
                <ul>
                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Shop/OrderList.aspx?day=1")%>'
                        class=""><span>今日订单</span></a></li>
                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Shop/OrderList.aspx")%>'
                        class=""><span>订单列表</span></a></li>

                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ExpressOrderList.aspx")%>'
                        class=""><span>跑腿订单</span></a></li>

                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Shop/webStatisticsYear.aspx")%>'
                        class=""><span>订单统计</span></a></li>
                    <li class=" level2 last"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/OrderCrm.aspx")%>'
                        class=""><span>代客下单</span></a></li>
                </ul>
            </li>
            
            
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0"><a href="#" onclick="return false"><span>订单调度管理</span></a>
                <ul>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/OrderDelive.aspx")%>'
                        class=""><span>调度管理系统</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/locationmonitor.aspx")%>'
                        class=""><span>实时监控</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/DeliverList.aspx")%>'
                        class=""><span>配送员管理</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/Deliverordersum.aspx")%>'
                        class=""><span>配送员结算</span></a></li>


                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/DeliverDetail.aspx")%>'
                        class=""><span>添加配送员</span></a></li>
                    <%--<li class="last  level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/citylist.aspx") %>'
                        class=""><span>自动调度配置</span></a></li>--%>
                </ul>
            </li>
            
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0" id="head_div11"><a href="#" onclick="return false" class=""><span>财务统计</span></a>
                <ul>
                    <%-- <li class="level1"><a href='/Admin/ShopData/shouldpayment.aspx'
                        class=""><span>应结账款查询</span></a></li>
                    <li class="level2"><a href='/Admin/ShopData/income.aspx'
                        class=""><span>营业收入查询</span></a></li>--%>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/shopStatisticsYear.aspx") %>'
                        class=""><span>商家结算</span></a></li>

                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/cashoutList.aspx") %>'
                        class=""><span>提现申请</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/ProportionList.aspx") %>'
                        class=""><span>区域交易额统计</span></a></li>

                </ul>
            </li>

        </ul>
    </div>
    <!-- menu end -->
</div>
<asp:HiddenField runat="server" ID="hfroles" />
