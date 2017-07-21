<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adleft.ascx.cs" Inherits="AreaAdmin_a1dmin_left" %>
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
                document.getElementById("img" + i).src = "<%=WebUtility.GetUrl("~/AreaAdmin/images/jiahao.jpg")%>";
            }
            //再把当前需要显示的div给display 
            // obj.style.display = "block";
            $(obj).slideToggle("slow");
            document.getElementById("img" + divId).src = "<%=WebUtility.GetUrl("~/AreaAdmin/images/jianhao.jpg")%>";//"images/jianhao.jpg";
        }
        else {
            // obj.style.display = "none";
            $(obj).slideToggle("slow");
            document.getElementById("img" + divId).src = "<%=WebUtility.GetUrl("~/AreaAdmin/images/jiahao.jpg")%>";
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
                <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img1" />商家管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div1" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopList.aspx")%>">商家列表</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopDetail.aspx")%>">商家信息(点击新增)</a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/packagelist.aspx")%>"></a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/packageDetail.aspx")%>"></a>
                            </div>
                        </li>
                        <li class="hide">
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/AddPrinter.aspx")%>"></a>
                            </div>
                        </li>
                     
                   
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ShopReviewList.aspx")%>">商家评论</a>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            
            <div class="divfan_leftsider_info_nav" onclick="showContent('2')" id="left_head3">
                <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img2" />订单管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div2" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/OrderList.aspx?day=1")%>">今日订单</a>
                        </li>
                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/OrderList.aspx")%>">订单列表</a>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/ExpressOrderList.aspx")%>">跑腿订单</a>
                            </div>
                        </li>

                        <li class=" level1">
                            <a href="<%=WebUtility.GetUrl("~/AreaAdmin/Shop/webStatisticsYear.aspx")%>">订单统计</a>
                        </li>
                        <li class=" level1 last">
                            <a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/OrderCrm.aspx")%>'>客服代客下单</a>
                        </li>
                    </ul>
                </div>
            </div>            
                       
            
            <div class="divfan_leftsider_info_nav" onclick="showContent('3')">
                <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/jiahao.jpg")%>" width="16" height="16"
                    id="img3" />订单调度管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div3" style="display: none">
                <div class="divfan_leftsider_menuContent_nav">
                </div>
                <div class="divfan_leftsider_ul">
                    <ul>
                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/OrderDelive.aspx")%>'
                            class=""><span>调度管理系统</span></a></li>


                          <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/Service/locationmonitor.aspx")%>'
                        class=""><span>实时监控</span></a></li>

                        
                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/DeliverList.aspx")%>'
                            class=""><span>配送员管理</span></a></li>
                        <li class="level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/DeliverDetail.aspx")%>'
                            class=""><span>添加配送员</span></a></li>


                        <li class=" level1"><a href='<%=WebUtility.GetUrl("~/AreaAdmin/manage/Deliverordersum.aspx")%>'
                            class=""><span>配送员结算</span></a></li>


                        <%--<li class="last level1">
                            <a href="<%=WebUtility.GetUrl("~/AreaAdmin/manage/citylist.aspx")%>">自动调度配置</a>
                        </li>--%>
                    </ul>
                </div>
            </div>
            <div class="divfan_leftsider_info_nav" onclick="showContent('4')">
                <img src="<%=WebUtility.GetUrl("~/Admin/images/jiahao.jpg")%>" width="16"
                    height="16" id="img4" />财务管理
            </div>
            <div class="divfan_leftsider_menuContent" id="div4" style="display: none;">
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
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/shopStatisticsYear.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/leftsider_fan_bg2.jpg")%>" />商家结算</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/cashoutList.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/leftsider_fan_bg2.jpg")%>" />提现申请</a>
                            </div>
                        </li>
                        <li>
                            <div class="">
                                <a href="<%=WebUtility.GetUrl("~/AreaAdmin/ShopData/ProportionList.aspx")%>">
                                    <img src="<%=WebUtility.GetUrl("~/AreaAdmin/images/leftsider_fan_bg2.jpg")%>" />区域交易额统计</a>
                            </div>
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
