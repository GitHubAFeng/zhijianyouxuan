<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Banner.ascx.cs" Inherits="HomeDemo_Banner" %>

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
                登录用户 <a href='<%=ResolveClientUrl("~/Admin/Permission/updateadmin.aspx") %>'><span id="snUsername" runat="server"></span></a><span class="separator">|</span>
                <span id="snDate" runat="server"></span><span class="separator">|</span><a href='<%=ResolveClientUrl("~/Admin/login.aspx?out=1") %>'>
                    <strong>退出</strong></a>
            </p>
            <p class="super" style="padding-right: 400px;">
                <img style="margin-left: 3px; margin-top: 2px;" src='<%=ResolveClientUrl("~/Admin/images/msgtip.gif")%>' />
                <span style="margin-left: 4px;">当前未处理订单<a href='<%=WebUtility.GetUrl("~/Admin/Shop/OrderList.aspx?type=1") %>'><label
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
                class="parent level0"><a href='<%=WebUtility.GetUrl("~/admin/basic.aspx")%>'><span>系统首页</span>
                </a></li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div1"><a href='#'><span>商家管理</span></a>
                <ul>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/admin/Shop/ShopList.aspx")%>"
                        id="A1"><span>商家列表</span></a></li>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/Admin/Shop/ShopDetail.aspx")%>"
                        id="A2"><span>商家信息(点击新增)</span></a></li>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/Admin/Shop/shopdatalist.aspx")%>"
                        id="A5"><span>商家分类</span></a></li>
                    <li class="  level2"><a href="<%=WebUtility.GetUrl("~/Admin/Shop/piclist.aspx")%>"><span>商家标签</span></a></li>
                    <li class=" last level2"><a href="<%=WebUtility.GetUrl("~/admin/Shop/ShopReviewList.aspx")%>"
                        id="A6"><span>商家评论</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div2"><a href='#'><span>会员管理</span></a>
                <ul>
                    <%-- <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/uservipgradelist.aspx")%>'
                        class=""><span>用户等级管理</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/gradefavourable.aspx")%>'
                        class=""><span>用户等级优惠</span></a></li>--%>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/user/UserList.aspx")%>'
                        class=""><span>会员管理</span></a></li>



                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/user/distributorList.aspx")%>'
                        class=""><span>分销商管理</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/user/userdrawcashrecords.aspx")%>'
                        class=""><span>会员提现申请</span></a></li>


                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/User/UserDistributionList.aspx")%>'
                        class=""><span>分销记录</span></a></li>


                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/User/UserAddMoneyLog.aspx")%>'
                        class=""><span>充值记录</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/User/UserDelMoneyLog.aspx")%>'
                        class=""><span>消费记录</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/pointrecordlist.aspx")%>'
                        class=""><span>积分记录</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/UserActivityStatis.aspx")%>'
                        class=""><span>用户活跃度统计</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/UserOrderStatis.aspx")%>'
                        class=""><span>用户订单统计</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/user/UserWorkOnceOrderStatis.aspx")%>'
                        class=""><span>一次下单用户统计</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div3"><a href='#'><span>订单管理</span></a>
                <ul>
                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/Admin/Shop/OrderList.aspx?day=1")%>'
                        class=""><span>今日订单</span></a></li>
                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/admin/Shop/OrderList.aspx")%>'
                        class=""><span>订单列表</span></a></li>

                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/admin/Shop/ExpressOrderList.aspx")%>'
                        class=""><span>跑腿订单</span></a></li>

                    <li class=" level2"><a href='<%=WebUtility.GetUrl("~/admin/Shop/webStatisticsYear.aspx")%>'
                        class=""><span>订单统计</span></a></li>
                    <li class=" level2 last"><a href='<%=WebUtility.GetUrl("~/Admin/Service/OrderCrm.aspx")%>'
                        class=""><span>代客下单</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div5"><a href="javascript:" onclick="return false" class=""><span>积分兑换</span> </a>
                <ul>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/admin/Gifts/IntegralList.aspx") %>'
                        class=""><span>积分兑换管理</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/admin/Gifts/GiftsList.aspx") %>'
                        class=""><span>礼品管理</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/admin/Gifts/GiftsDetail.aspx") %>'
                        class=""><span>新增礼品</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/admin/Gifts/GiftsClassList.aspx")%>'
                        class=""><span>礼品分类管理</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/admin/Gifts/GiftsClassDetail.aspx")%>'
                        class=""><span>增加礼品分类</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div8"><a href="javascript:" onclick="return false"><span>网站参数设置</span></a>
                <ul>
                    <li class="   level1"><a href="<%=WebUtility.GetUrl("~/admin/manage/setweixin.aspx")%>"><span>微信公众平台帐号</span></a></li>
                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/NewsList.aspx")%>'
                        class=""><span>公告管理</span></a></li>
                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/shop/citylist.aspx") %>'
                        class=""><span>城市管理</span></a></li>
                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/Building.aspx") %>'
                        class=""><span>地标管理</span></a></li>


                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/distributeRatioset.aspx?id=1") %>'
                        class=""><span>分销比例设置</span></a></li>

                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/distributeRatioset.aspx?id=2") %>'
                        class=""><span>成为分销商比例设置</span></a></li>



                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/abouts/aboutClasslist.aspx")%>'
                        class=""><span>文章分类管理</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/abouts/aboutuslist.aspx")%>'
                        class=""><span>文章管理</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/abouts/aboutusdetail.aspx")%>'
                        class=""><span>文章添加</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/AdList.aspx")%>'
                        class=""><span>广告管理</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/PptList.aspx")%>'
                        class=""><span>幻灯片管理</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/Email.aspx")%>' class="">
                        <span>邮箱设置</span></a></li>


                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/webpaytype.aspx?id=1") %>'
                        class=""><span>支付方式管理</span></a></li>

                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/zhifubao.aspx?id=1") %>'
                        class=""><span>支付宝信息</span></a></li>



                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/suggestionList.aspx") %>'
                        class=""><span>留言版</span></a></li>

                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/STemplatelist.aspx?type=2") %>'
                        class=""><span>备注选项管理</span></a></li>
                    <%--                     <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/STemplatelist.aspx?type=1") %>'
                        class=""><span>跑腿服务类型</span></a></li>--%>
                    <li class="last  level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/WebBasic.aspx")%>'
                        class=""><span>系统设置</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0" id="head_div9"><a href='#'><span>权限管理</span></a>
                <ul>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Permission/AdminList.aspx") %>'
                        class=""><span>管理员</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Permission/Modulelist.aspx") %>'
                        class=""><span>模块管理</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Permission/moduledetail.aspx") %>'
                        class=""><span>添加模块</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/Permission/rolelist.aspx") %>'
                        class=""><span>角色管理</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="parent level0"><a href='#'><span>打印机管理</span></a>
                <ul>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Printer/AddPrinterSecret.aspx")%>'
                        class=""><span>添加打印机</span></a></li>
                    <li class="  level1"><a href='<%=WebUtility.GetUrl("~/Admin/Printer/PrinterSecretList.aspx")%>'
                        class=""><span>打印机管理</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/Printer/showprinter.aspx") %>'
                        class=""><span>打印机状态</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0"><a href="#" onclick="return false"><span>订单调度管理</span></a>
                <ul>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Service/OrderDelive.aspx")%>'
                        class=""><span>调度管理系统</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Service/locationmonitor.aspx")%>'
                        class=""><span>实时监控</span></a></li>


                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverGroupList.aspx")%>'
                        class=""><span>配送员群组管理</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverList.aspx")%>'
                        class=""><span>配送员管理</span></a></li>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/Deliverordersum.aspx")%>'
                        class=""><span>配送员结算</span></a></li>


                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverDetail.aspx")%>'
                        class=""><span>添加配送员</span></a></li>
                    <%--<li class="last  level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/autodispatchconfig.aspx") %>'
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
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/ShopData/shopStatisticsYear.aspx") %>'
                        class=""><span>商家结算</span></a></li>

                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/ShopData/cashoutList.aspx") %>'
                        class=""><span>商家提现申请</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/user/userdrawcashrecords.aspx")%>'
                        class=""><span>会员提现申请</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/user/delivercashoutList.aspx")%>'
                        class=""><span>骑士提现申请</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/ShopData/ProportionList.aspx") %>'
                        class=""><span>区域交易额统计</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0"><a href="#" onclick="return false"><span>数据分析</span></a>
                <ul>

                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/DeliverOutputValue.aspx")%>'
                        class=""><span>订单量对比</span></a></li>


                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/shopTOP10.aspx")%>'
                        class=""><span>商家销量TOP10</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/foodTOP10.aspx")%>'
                        class=""><span>商品销量TOP10</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/userTOP10.aspx")%>'
                        class=""><span>会员订餐TOP10</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/timeorder.aspx")%>'
                        class=""><span>订单时间分布</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/DataAnalysis/ordersource.aspx")%>'
                        class=""><span>订单来源分布</span></a></li>
                </ul>
            </li>
            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0"><a href='javascript:'><span>优惠券管理</span></a>
                <ul>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/shopcard/batshopcardlist.aspx") %>'
                        class=""><span>优惠券批次管理</span></a></li>
                    <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/shopcard/importshopcart.aspx") %>'
                        class=""><span>生成优惠券</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/shopcard/shopcardlist.aspx") %>'
                        class=""><span>优惠券管理</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/shopcard/shopCardUserrecord.aspx") %>'
                        class=""><span>优惠券使用记录管理</span></a></li>
                </ul>
            </li>

            <li
                class="   parent level0"
                onmouseout="objectRemoveClass(this,'over')" onmouseover="objectAddClass(this,'over')" style="display: <%= SectionProxyData.GetSetValue(18).ToString().Trim()== "0" ? "none" :""  %>;"><a href='javascript:'><span>平台促销</span></a>
                <ul>


                    <li class="   level1"><a href="<%=WebUtility.GetUrl("~/admin/Promotion/ordersourcelist.aspx")%>"
                        id="A9"><span>充值优惠管理</span></a></li>

                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/Promotion/Promotionlist.aspx") %>'
                        class=""><span>促销策略</span></a></li>
                    <li class=" last level1"><a href='<%=WebUtility.GetUrl("~/Admin/Promotion/addPromotion.aspx") %>'
                        class=""><span>添加促销</span></a></li>


                </ul>
            </li>

            <li onmouseover="objectAddClass(this,'over')" onmouseout="objectRemoveClass(this,'over')"
                class="   parent level0">
                <a href='javascript:'><span>红包管理</span></a>
                <ul>


                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/packet/userpacketlist.aspx") %>'
                        class=""><span>红包管理</span></a></li>
                    <li class="level1"><a href='<%=WebUtility.GetUrl("~/Admin/packet/configpacketList.aspx") %>'
                        class=""><span>红包配置</span></a></li>
                    <li class="last level1"><a href='<%=WebUtility.GetUrl("~/Admin/packet/msgpacketrecord.aspx") %>'
                        class=""><span>红包使用记录管理</span></a></li>
                </ul>
            </li>

        </ul>
    </div>
    <!-- menu end -->
</div>
<asp:HiddenField runat="server" ID="hfroles" />
