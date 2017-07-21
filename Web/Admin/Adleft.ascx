<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adleft.ascx.cs" Inherits="Admin_admin_left" %>
<link href="<%=ResolveClientUrl("~/Admin/css/left.css")%> " rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function showContent(divId) {

        var obj = document.getElementById("div" + divId);

        //判断当前的div状态，如果不是隐藏状态 则执行if中，否则就隐藏
        if (obj.style.display == "none") {
            //如果你需要添加项，请记得修改这里 for 循环的次数刚好符合div(显示答案的div) 的个数 
            //所有的div都先 hidden
            var n = $(".divfan_leftsider_info_nav").length;
            for (var i = 1; i <= n; i++) {
                document.getElementById("div" + i).style.display = "none";
                document.getElementById("img" + i).src = "<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>";
            }
            //再把当前需要显示的div给display 
            // obj.style.display = "block";
            $(obj).slideToggle("slow");
            document.getElementById("img" + divId).src = "<%=WebUtility.GetUrl("~/Admin/images/jianhao.jpg")%>";//"images/jianhao.jpg";
        }
        else {
            // obj.style.display = "none";
            $(obj).slideToggle("slow");
            document.getElementById("img" + divId).src = "<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>";
        }
    }
    function initAdLeft() {
        var $alldivs = $("#adleft>div:odd");
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
                    return;
                }
            }
        }



    }

    $(function () {
        initAdLeft();
        var rightheight = $("#content").height();
        $("#leftToggler").css("height", rightheight + "px");
        var leftopen = handlecookie("leftopen");
        if (leftopen != null && leftopen == "0") {
            closeleft();
        }
    })

    ///关闭左边菜单
    function closeleft() {
        $("#divfan_leftsider").hide();
        $(".main-col").css("margin-left", 0 + "px").css("padding-left", 0 + "px");
        $("#leftToggler").show();
        ///cookie leftopen = 0表示关闭
        handlecookie("leftopen", 0 + "", { expires: 1, path: "/", secure: false });
    }

    ///打开左边菜单
    function showleft() {
        $("#divfan_leftsider").show();
        $(".main-col").css("margin-left", 160 + "px").css("padding-left", 25 + "px");
        $("#leftToggler").hide();
        ///cookie leftopen = 1表示开
        handlecookie("leftopen", 1 + "", { expires: 1, path: "/", secure: false });
    }

</script>

<div id="leftmain" class="closeLeft">
    <div class="divfan_leftsider" id="divfan_leftsider">
        <h3>商家管理<a href="javascript:void(0)" style="font-size: 12px; color: Red" class="leftred"
            onclick="closeleft()" title="关闭菜单">[关闭]</a></h3>
        <div class="divfan_leftsider_info" id="adleft">
            <div class="divfan_leftsider_info_nav" onclick="showContent('1')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img1" />商家管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div1" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/admin/Shop/ShopList.aspx")%>">商家列表</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/ShopDetail.aspx")%>">商家信息(点击新增)</a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/packagelist.aspx")%>"></a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/packageDetail.aspx")%>"></a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/AddPrinter.aspx")%>"></a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/shopdatalist.aspx")%>">商家分类</a>
                            </div>
                        </li>
                        <li class="  level2"><a href="<%=WebUtility.GetUrl("~/Admin/Shop/piclist.aspx")%>"><span>商家标签</span></a></li>

                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/admin/Shop/ShopReviewList.aspx")%>">商家评论</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('2')" id="left_head2">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img2" />会员管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div2" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <%-- <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/User/uservipgradelist.aspx")%>">
                                    用户等级管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/User/gradefavourable.aspx")%>">
                                    用户等级优惠</a>
                            </div>
                        </li>--%>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/User/UserList.aspx")%>">会员管理</a>
                            </div>
                        </li>

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
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('3')" id="left_head3">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img3" />订单管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div3" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/Admin/Shop/OrderList.aspx?day=1")%>">今日订单</a>
                        </li>
                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/Admin/Shop/OrderList.aspx")%>">订单列表</a>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Shop/ExpressOrderList.aspx")%>">跑腿订单</a>
                            </div>
                        </li>

                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/Admin/Shop/webStatisticsYear.aspx")%>">订单统计</a>
                        </li>
                        <li class=" level1 last">
                            <a href='<%=WebUtility.GetUrl("~/Admin/Service/OrderCrm.aspx")%>'>客服代客下单</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('4')" id="left_head4">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img4" />积分兑换
            </div>
            <div class="divfan_leftsider_menuContent" id="div4" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Gifts/IntegralList.aspx")%>">积分兑换管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Gifts/GiftsList.aspx")%>">礼品管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Gifts/GiftsDetail.aspx")%>">新增礼品</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Gifts/GiftsClassList.aspx")%>">礼品分类管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Gifts/GiftsClassDetail.aspx")%>">增加分类管理</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('5')" id="left_head6">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img5" />网站参数设置
            </div>
            <div class="divfan_leftsider_menuContent" id="div5" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/setweixin.aspx")%>">微信公众平台帐号</a>
                            </div>
                        </li>

                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/NewsList.aspx")%>">公告管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/shop/citylist.aspx")%>">城市管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/Building.aspx")%>">地标管理</a>
                            </div>
                        </li>


                          <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/distributeRatioset.aspx?id=1") %>'
                        class=""><span>分销比例设置</span></a></li>

                    <li class="   level1"><a href='<%=WebUtility.GetUrl("~/Admin/manage/distributeRatioset.aspx?id=2") %>'
                        class=""><span>成为分销商比例设置</span></a></li>



                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/abouts/aboutClasslist.aspx")%>">文章分类管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/abouts/aboutuslist.aspx")%>">文章管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/abouts/aboutusdetail.aspx")%>">文章添加</a>
                            </div>
                        </li>

                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/AdList.aspx")%>">广告管理</a>
                            </div>
                        </li>

                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/PptList.aspx")%>">幻灯片管理</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/Email.aspx")%>">邮箱设置</a>
                            </div>
                        </li>


                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/webpaytype.aspx")%>">支付方式管理</a>
                            </div>
                        </li>

                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/zhifubao.aspx?id=1")%>">支付宝信息</a>
                            </div>
                        </li>
                        <li class="   level1">
                            <div class="">
                                <a href='<%=WebUtility.GetUrl("~/Admin/manage/suggestionList.aspx") %>'
                                    class=""><span>留言版</span></a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/STemplatelist.aspx?type=2")%>">备注选项管理</a>
                            </div>
                        </li>
                        <%--                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/STemplatelist.aspx?type=1")%>">跑腿服务类型</a>
                            </div>
                        </li>--%>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/manage/WebBasic.aspx")%>">系统设置</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('6')" id="left_head7">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img6" />权限管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div6" style="display: none;">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Permission/AdminList.aspx")%>">管理员</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Permission/Modulelist.aspx")%>">模块管理</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Permission/moduledetail.aspx")%>">添加模块</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Permission/rolelist.aspx")%>">角色管理</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('7')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img7" />打印机管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div7" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Printer/AddPrinterSecret.aspx")%>">添加打印机</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Printer/PrinterSecretList.aspx")%>">打印机列表</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/Admin/Printer/showprinter.aspx")%>">打印机状态</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('8')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img8" />订单调度管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div8" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Service/OrderDelive.aspx")%>'
                            class=""><span>调度管理系统</span></a></li>


                          <li class=" level1"><a href='<%=WebUtility.GetUrl("~/Admin/Service/locationmonitor.aspx")%>'
                        class=""><span>实时监控</span></a></li>



                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverGroupList.aspx")%>'
                            class=""><span>配送员群组管理</span></a></li>
                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverList.aspx")%>'
                            class=""><span>配送员管理</span></a></li>
                        <li class="level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/DeliverDetail.aspx")%>'
                            class=""><span>添加配送员</span></a></li>


                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/admin/manage/Deliverordersum.aspx")%>'
                            class=""><span>配送员结算</span></a></li>


                        <%--<li class="last level1">
                            <a href="<%=WebUtility.GetUrl("~/Admin/manage/autodispatchconfig.aspx")%>">自动调度配置</a>
                        </li>--%>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('9')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16"
                    height="16" id="img9" />财务统计
            </div>
            <div class="divfan_leftsider_menuContent" id="div9" style="display: none;">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <%-- <li>
                            <div class="divfan_leftsider_ul_2_b">
                                <a href="/Admin/ShopData/shouldpayment.aspx">
                                    <img src="<%=WebUtility.GetUrl("~/Admin/images/leftsider_fan_bg2.jpg")%>" />应结账款查询</a>
                            </div>
                        </li>
                        <li>
                            <div class="divfan_leftsider_ul_2_b">
                                <a href="/Admin/ShopData/income.aspx">
                                    <img src="<%=WebUtility.GetUrl("~/Admin/images/leftsider_fan_bg2.jpg")%>" />营业收入查询</a>
                            </div>
                        </li>--%>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/ShopData/shopStatisticsYear.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/Admin/images/leftsider_fan_bg2.jpg")%>" />商家结算</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/ShopData/cashoutList.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/Admin/images/leftsider_fan_bg2.jpg")%>" />提现申请</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/Admin/ShopData/ProportionList.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/Admin/images/leftsider_fan_bg2.jpg")%>" />区域交易额统计</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('10')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16"  height="16" id="img10" />数据分析
            </div>
            <div class="divfan_leftsider_menuContent" id="div10" style="display: none;">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
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
                        <%--                        <li class=" level1 last"><a onclick="alert('正在全力开发中')"
                            href='javascript:'><span>更多数据分析</span></a></li>--%>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('11')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16"
                    height="16" id="img11" />优惠券管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div11" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li><a href="<%=WebUtility.GetUrl("~/admin/shopcard/batshopcardlist.aspx")%>">优惠券批次管理</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/admin/shopcard/importshopcart.aspx")%>">生成优惠券</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/admin/shopcard/shopcardlist.aspx")%>">优惠券管理</a>
                        </li>
                        <li><a href="<%=WebUtility.GetUrl("~/admin/shopcard/shopCardUserrecord.aspx")%>">优惠券使用记录管理</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div title="开启或关闭侧边栏" id="leftToggler" style="height: 226px; display: none" onclick="showleft()">
        &nbsp;
    </div>
</div>
