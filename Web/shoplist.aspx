<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shoplist.aspx.cs" Inherits="shoplist" %>

<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=9" />
    <title>订餐 - <%=SectionProxyData.GetSetValue(3) %></title>
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link href="css/shop.css?v=6" rel="stylesheet" />
    <link href="css/index.css?v=4" rel="stylesheet" />
    <link href="css/loaders.min.css" rel="stylesheet" />
    <script type="text/javascript" src="javascript/jquery-1.7.min.js"></script>


    <style type="text/css">
        .loader-inner > div {
            background: #f39800;
        }
    </style>

</head>
<body>
    <form runat="server">
        <top:banner ID="Banner" runat="server" />
        <uc1:header ID="header" runat="server" />

        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <input type="hidden" id="hfod" runat="server" value="od0" />
        <input type="hidden" id="hfcursid" runat="server" value="0" />
        <input type="hidden" id="hfcursortid" runat="server" value="0" />
        <input type="hidden" id="hfcursaleid" runat="server" value="0" />
        <input type="hidden" id="hfcursendmoney" runat="server" value="0" />
        <input type="hidden" id="hfaddresskey" runat="server" value="0" />

        <input type="hidden" id="hfdefaulturl" runat="server" value="" />

        <asp:HiddenField ID="hidUid" runat="server" Value="-1" />
        <div class="wrap">
            <div class="addr-search ">
                <span class="placebg fl"><span class="txt"><a href="javascript:">
                    <img src="images/mapsite.png" /></a>我的地址</span>
                    <span class="arrow"></span>
                </span>
                <div class="fl f14 detailsite">
                    <a href="javascript:" style="color: #333;" id="myAddress" runat="server"></a>[<a href="/Index.aspx?change=1">更换地址</a>]
                </div>
                <div class="fr">
                    <div class="msearch fl">
                        <input type="text" runat="server" id="tbshopkeyword" style="width: 240px;" class="a_text" value="" placeholder="餐厅列表搜索,搜索店铺和美食" />
                        <asp:Button ID="search" value="搜索" runat="server" Text="搜索" CssClass="a_btn" Style="cursor: pointer;" OnClientClick="checksearch()" OnClick="search_Click" />
                    </div>
                    <div class="repos hzind fl hisbg" style="margin-left: 12px;">
                        <div class="history">
                            <a href="javascript:" id="a_addaddress">更换地址<span class="arrow"></span></a>
                        </div>
                        <div class="history_option" id="haddress" runat="server" style="display: none">
                            <ul>
                                <asp:Repeater ID="rptaddress" runat="server">
                                    <ItemTemplate>
                                        <li><a href="shoplist.aspx?lat=<%#Eval("lat")%>&lng=<%#Eval("lng") %>&addr=<%# Server.UrlEncode(Server.UrlEncode( Eval("Address").ToString()))%>&from=m&addressid=<%#Eval("dataid")%>" class="myadd"><%#Eval("Address")%></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                </div>
            </div>
            <div id="focus">
                <ul>
                    <asp:Repeater ID="rptppt" runat="server">
                        <ItemTemplate>
                            <li>
                                <a title="<%# Eval("title") %>" href="<%# Eval("PUrl") %>" target="_blank">
                                    <img alt="" style="width: 980px; height: 160px;" src="<%# WebUtility.ShowPic(Eval("picture").ToString()) %>" />
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

            <div class="border-shadow mgb10" id="shopboxdiv">
                <div class="mainbord" id="shopli">


                    <div class="shop-sort">
                        <div class="mid-one fl">
                            <strong class="fl">排序：</strong>
                            <a class="sort_btn od0 " href="<%=getSortUrl("od","0") %>"><span>默认</span><i class=""></i></a>
                            <a class="sort_btn od1" href="<%=getSortUrl("od","1") %>"><span>销量</span><i class="b"></i></a>
                            <a class="sort_btn od2" href="<%=getSortUrl("od","2") %>"><span>最新</span><i class="b"></i></a>
                            <a class="sort_btn od3" href="<%=getSortUrl("od","3") %>"><span>距离</span><i class="t"></i></a>
                        </div>
                        <div class="mid-two fl">
                            <input type="radio" name="saleradio" id="allshopradio" checked="checked" onclick="gourl('<%=getSortUrl("l","") %>    ')" />
                            <label for="allshopradio">全部餐厅</label>
                            <input type="radio" name="saleradio" id="saleshopradio" onclick="    gourl('<%=getSortUrl("l","1") %>    ')" />
                            <label for="saleshopradio" style="background: url('/images/waimai.png') no-repeat scroll 0 0 rgba(0, 0, 0, 0); padding-left: 18px;">平台专送</label>
                        </div>
                        <div class="mid-three fr">
                            <strong>起送价：</strong>
                            <span class="price_label">
                                <span>全部</span>
                                <span>10</span>
                                <span>20</span>
                                <span>30</span>
                                <span>40</span>
                            </span>
                            <span class="orange-bg">
                                <a href="<%=getSortUrl("a","") %>" class="radius" id="minmoney0"></a>
                                <a href="<%=getSortUrl("a","10") %>" id="minmoney10" class="radius"></a>
                                <a href="<%=getSortUrl("a","20") %>" id="minmoney20" class="radius"></a>
                                <a href="<%=getSortUrl("a","30") %>" id="minmoney30" class="radius"></a>
                                <a href="<%=getSortUrl("a","40") %>" class="radius" id="minmoney40"></a>
                            </span>
                        </div>
                    </div>
                    <div class="choce_type">

                        <div class="type_name clearfix">
                            <strong class="fl">分类：</strong>
                            <span class="fl midwid">
                                <a href="<%= getSortUrl("s", "") %>" id="sortid0">全部</a>
                                <asp:Repeater ID="rpttogosortlist" runat="server">
                                    <ItemTemplate>
                                        <a href="<%# getSortUrl("s", Eval("id")) %>" id='sortid<%# Eval("id") %>'>
                                            <%#Eval("classname")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </span>
                        </div>

                    </div>
                </div>
            </div>


            <div style="text-align: center; margin-bottom: 20px; margin-top: 30px;" runat="server" id="loaderbox">

                <div class="loader">
                    <div class="loader-inner line-scale">
                        <div></div>
                        <div></div>
                        <div></div>
                        <div></div>
                        <div></div>
                    </div>
                </div>

                <div style="margin-top: 10px;">正在载入更多商家...</div>

            </div>


            <asp:Repeater ID="rptgroup" runat="server">
                <ItemTemplate>

                    <div class="index-tit">
                        <%#Eval("headhtml")%>
                    </div>
                    <div class="shoplist mgt10" id="hotlist">
                        <ul class="clearfix">
                            <asp:Repeater ID="rpttogohot" runat="server" DataSource='<%#Eval("shops")%>'>
                                <ItemTemplate>
                                    <li s_how='<%#(Container.ItemIndex) % 4 < 3 ? "s_left":"s_right"%>' style='<%#(Container.ItemIndex+1) % 4 == 0 ? "margin-right: 0px;": ""%>' id="shopnum_<%# Eval("Unid") %>_1" num="<%# Eval("Unid") %>">
                                        <div class="bord">
                                            <div class='img'>
                                                <a href="shop.aspx?id=<%#Eval("unid")%>" title="<%#Eval("name") %>" target="_blank">
                                                    <img src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" alt="<%#Eval("name")%>" />
                                                </a>
                                            </div>

                                            <div class="info">

                                                <p class="f14" title="<%#Eval("name") %>" style="white-space: nowrap; text-overflow: ellipsis; overflow: hidden;">
                                                    <%#Eval("name") %>
                                                </p>
                                                <div class="shop_info_item" style="position:relative;">
                                                    <span class="item">配送费：<%#Eval("SendFee")%>元</span>
                                                    <span class="item">起送价：<%#Eval("SendLimit") %>元</span>
                                                    <span class="item" style="position:absolute;right:5px; top:20px;"><a style="<%#Convert.ToInt32(Eval("showpicture")) == 1 ? "": "display:none"%>" href="javascript:" title="下单即送饮料一份" class="zengbg">赠</a></span>
                                                </div>

                                                <div class="shop_info_item">
                                                    <span class="item"><i class="icon_address"></i><%# Convert.ToDecimal(Eval("Distance")).ToString("#0.0")%></span>
                                                    <span class="item"><i class="icon_time"></i><%#Eval("senttime")%></span>
                                                    <span class="item red"><i class="icon_time" style="background-image:url(/images/sale_cout.png)"></i><%#Eval("pop") %>份</span>
                                                </div>
                                                <div class="icons">
                                                    <asp:Repeater runat="server" ID="rpttags" DataSource='<%#Eval("pictags")%>'>
                                                        <ItemTemplate>
                                                            <span class="restaurant-icons  tooltip_on" style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0)" title="<%#Eval("Title")%>"></span>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="layer" id="mylayer_<%#Eval("unid")%>" style="display: none;">
                                            <div class="layer_right">
                                                <div class="layer_left"></div>
                                                <div class="layer_right_01 clearfix">
                                                    <h2 style="font-size: 14px; color: #333;"><%#Eval("name") %></h2>
                                                    <div class=" ">
                                                        <ul>
                                                            <asp:Repeater runat="server" ID="Repeater1" DataSource='<%#Eval("pictags")%>'>
                                                                <ItemTemplate>
                                                                    <li style="line-height: 20px; margin: 3px 0; width: 350px;">
                                                                        <span class="restaurant-icons  tooltip_on" style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0);"></span><span class="desc"><%#Eval("Title")%></span>

                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                    <p>地址：<%#Eval("address") %></p>
                                                    <p>营业时间：<%#Eval("opentimestr") %></p>
                                                    <p style="color: #f39800; line-height: 22px;">公告：<%#Eval("special") %></p>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

            <div style="display: none; width: 980px; background-color: #fff; box-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);" id="divnocord" runat="server">
                <div class="list_ck_shop_no">
                    <p>
                        抱歉，没有搜索任何餐厅，请重新选择！
                    </p>
                    <p>
                        <input name="" type="button" class="ck_shop_no_btn" value="返回首页" onclick="gourl('index.aspx');" />
                    </p>
                </div>
            </div>
            <div class="pages">
                <webdiyer:AspNetPager CssClass="anpager" ID="AspNetPager1" runat="server" Width="100%"
                    CustomInfoHTML="共有记录 <font>%RecordCount%</font> 条 当前为 <font>%CurrentPageIndex%</font>/<font>%PageCount%</font> 页"
                    HorizontalAlign="Left" ShowCustomInfoSection="Left" CustomInfoTextAlign="Center"
                    CustomInfoSectionWidth="28%" ShowPageIndexBox="Always" PageIndexBoxType="TextBox"
                    TextBeforePageIndexBox="转到 " PageIndexBoxClass="flattext" ShowPageIndex="True"
                    PageSize="100" SubmitButtonClass="flatbutton" SubmitButtonText=" GO " TextAfterPageIndexBox=" 页 "
                    Wrap="False" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </div>

        </div>

        <foot:foot ID="Foot1" runat="server" />

        <script id="mapInfoWindow" type="text/x-jsrender">
            <p class="bubble-addr">地址：{{:address}}</p>
            <div class="clearfix show-cont">
                {{if poi_total_num > 0}}
    	            <a onclick='{{:method}}; return false;' class="borderradius-1 bubble-btn">附近有{{:poi_total_num}}家外卖餐厅</a>
                {{else}}
    	            <a onclick='return false;' class="borderradius-1 bubble-btn disable-btn" disabled>对不起，这里还没有开通，我们正在努力</a>
                {{/if}}
            </div>
        </script>

        <script id="mapResultWindow" type="text/x-jsrender">
            <li id='result_item_{{:index}}' onclick='{{:method}}' class="result-item">
                <span class='icon icon-{{:index}}'></span>
                <div class="addr-info-wrap">
                    <p class="addr-name">{{:title}}</p>
                    <p class="addr-position">地址：{{:address}}</p>
                </div>
            </li>
        </script>
        <script id="mapEmptyResultWindow" type="text/x-jsrender">
            <li class="no-result-item">没有找到任何搜索结果，换个关键字试试。</li>
        </script>

        <script id="historyaddressTemplate" type="text/x-jsrender">
            {{for #data}}
             <li><a href="{{:url}}" class="myadd">{{:label}}</a></li>
            {{/for}}
                    
        </script>

        <script id="noaddressTemplate" type="text/x-jsrender">
            <li><a href='javascript:'>还没有历史地址</a></li>
        </script>

    </form>
</body>
</html>
<script type="text/javascript" src="javascript/jCommon.js"></script>
<script type="text/javascript" src="javascript/newppt.js"></script>
<script type="text/javascript" src="javascript/jquery.page.js"></script>
<script type="text/javascript" src="javascript/shoplist.js?v=1"></script>
<script type="text/javascript" src="javascript/jsrender.js"></script>
<script type="text/javascript" src="javascript/historyaddress.js"></script>
<script type="text/javascript">
    var hover_rst_count = 1;
    $(document).ready(function () {  
        intilayer();
        getUserLoaciont();
    });
    function hidelayer(id) {
        $(".shoplayer").hide();
    }

    function intilayer() {
        $('.shoplist li').hover(function () {
            var box = $(this);
            current_rst_block = box.attr('id');
            var s_how = box.attr('s_how');       
            var _this_rst_block = current_rst_block;
            hover_rst_count++;
            var _this_rst_count = hover_rst_count;
            var id = box.attr('num');
            window.setTimeout(function () {
                if (_this_rst_count == hover_rst_count && _this_rst_block == current_rst_block) {   
                  
                    showlayer(box[0],"mylayer_" + id,241,0,s_how);
                }
            }, 360);

        }, function () {
            var id = $(this).attr('num');
            $("#mylayer_" + id).hide();
            hidelayer(id);
            current_rst_block = '';
        });
    }

    //显示一个div
    //显示需要定位
    //obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
    //addx、addy是相对与obj的偏移量，就是div显示的位置
    function showlayer(obj, objdiv, addx, addy , lside) {
       
        var x = getposOffset_top(obj, 'left');
        var y = getposOffset_top(obj, 'top');
        var div_obj = document.getElementById(objdiv);
        if (lside == "s_left") {
            div_obj.style.left = (x + addx) + 'px';
            div_obj.style.top = (y + addy) + 'px';
            div_obj.style.display = "inline";
            $("#" + objdiv).removeClass("s_right");
        }
        else {
            $("#" + objdiv).addClass("s_right");
            div_obj.style.left = (x -360-12-14) + 'px';
            div_obj.style.top = (y + addy) + 'px';
            div_obj.style.display = "inline";
        }
        return;
    }

    function getUserLoaciont() {
        
        var lat = request("lat");
        if (lat.length > 0) {
            return false;
        }
        //如果没有历史地址，就读取默认地址，如果没有，就转到首页。
        if ($(".myadd").length > 0) {
            $(".myadd")[0].click();
            return false;
        }

        var url =  "index.aspx?noaddress=1";
       
        window.location = url;
        return false;

    }

</script>



