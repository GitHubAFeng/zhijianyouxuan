<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="newIndex1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title><%= WebUtility.GetWebName() %></title>
    <link href="css/force.css?v=2" rel="stylesheet" type="text/css" />
    <link href="css/newindex.css?v=2" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="javascript/jCommon.js"></script>
</head>


<body>
    <div class="page-bg">
        <div class="page-content">
            <form id="form1" runat="server">
                <asp:HiddenField runat="server" ID="hfcityname" />
                <asp:HiddenField runat="server" ID="hidLat" />
                <asp:HiddenField runat="server" ID="hidLng" />
                <div class="top-bar">
                    <div class="wrap">
                        <a href="/index.aspx">
                            <img src="images/logo.png" style="height: 50px;" /></a>
                    </div>
                </div>
                <div class="main_content ">
                    <div class="d_seachBox">
                        <div class="d_seachMain">
                            <div class="d_seachCon clearfix">
                                <div class="selectBox  citySelect" id="divcitylist">
                                    <input class="" type="text" value="杭州" disabled="disabled" id="tbcityname" runat="server" />
                                    <a class="dropDown_iconBox" title="" href="javascript:void(0)">
                                        <span class="dropDown_icon"></span>
                                    </a>

                                    <div class="bs-dropdown-menu map-block city-dropdown" style="display: none;">
                                        <div class="city-dropdown-header module-header">请选择你所在的城市 </div>
                                        <div class="cities-wrapper">
                                            <asp:Repeater ID="rptCtiy" runat="server">
                                                <ItemTemplate>
                                                    <div class="city-initial-group">
                                                        <span class="city-initial"><%# Eval("ReveVar") %></span>
                                                        <ul class="city-list">
                                                            <asp:Repeater ID="rptCityJunior" runat="server" DataSource='<%#Eval("CityJuniorList")%>'>
                                                                <ItemTemplate>
                                                                    <li class="city-block">
                                                                        <span onclick="window.location='?cid=<%#Eval("cid")%>';" class="city city_item"><%# Eval("cname") %> </span>
                                                                        <%--<span onclick="go_city_search('<%#Eval("cname") %>',<%#Eval("Lat") %>,<%#Eval("Lng") %>);" class="city city_item"><%# Eval("cname") %> </span>--%>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>

                                <div class="selectBox addressSelect">
                                    <input class="searchAddress searchInput" type="text" id="tbindexsearchbox" onkeydown="return enterIn(event,gomapsearch)" placeholder="请输入您所在的位置（写字楼、小区或街道）">
                                    <ul class="selectList selectListAddress searchResult"></ul>
                                    <input id="ban-searchBtn" class="searchBtn" type="button" onclick="gomapsearch()" value="搜索">
                                </div>

                                <div class="selectBox  historySelect history_option" id="divhistorylist" style="cursor: pointer; z-index: 10000;">
                                    <input class="" type="text" value="历史地址" disabled="disabled">
                                    <a class="dropDown_iconBox" title="" href="javascript:void(0)" style="width: 200px;">
                                        <span class="dropDown_icon" style="margin: -4px 0 0 70px;"></span>
                                    </a>
                                    <ul class="selectList selecthistoryCity ">
                                        <asp:Repeater ID="rptaddress" runat="server">
                                            <ItemTemplate>
                                                <li><a href="shoplist.aspx?lat=<%#Eval("lat")%>&lng=<%#Eval("lng") %>&addr=<%# Server.UrlEncode(Server.UrlEncode( Eval("Address").ToString()))%>&from=m" class="myadd"><%#Eval("Address")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>


                            </div>

                        </div>
                    </div>
                    <div class="mapwrap" id="containerbox">
                        <div class="map-bar">
                            <div class="d_seachCon clearfix">
                                <div class="selectBox citySelect">
                                    <input class="" type="text" value="" id="mapcityname" runat="server" disabled="disabled">

                                    <a class="dropDown_iconBox" title="" href="javascript:void(0)">
                                        <span class="dropDown_icon"></span>
                                    </a>

                                </div>
                                <div class="selectBox addressSelect">
                                    <input class="searchAddress searchInput" type="text" stype="地址" id="tbkeywork" placeholder="请输入您所在的位置（写字楼、小区或街道）">

                                    <input id="map-searchBtn" class="searchBtn" type="button" value="" onclick="go_search()">
                                </div>

                            </div>
                            <a class="map-bar-close"></a>
                        </div>
                        <div class="pg-map clearfix">
                            <div class="guid-map">
                                <span id="guide-close" onclick="$('.guid-map').hide()"></span>
                            </div>
                            <div class="addr-results">
                                <ol id="result_panel"></ol>
                                <div id="page_index" class="result-paginate"></div>
                            </div>

                            <div id="map" class="addr-map" style="width: 1100px;">
                                <div style="width: 100%; height: 500px; background-color: #fff;"
                                    id="map_canvas">
                                </div>
                                <div id="map-tip-div" class="map-tip">
                                    点击地图可以直接定位
                                <a class="close-link" onclick="$('#map-tip-div').toggle(); return false;" href="javascript:">x</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="J_bg" class="ks-ext-mask ks-dialog-mask ks-overlay-mask ks-dialog-mask-shown ks-overlay-mask-shown" style="width: 100%; left: 0px; top: 0px; height: 100%; position: fixed; z-index: 39999; display: none;"></div>
                <script id="mapInfoWindow" type="text/x-jsrender">
                    <p class="bubble-addr">地址：{{:address}}</p>
                    <div class="clearfix show-cont">
                        {{if poi_total_num > 0}}
    	<a onclick='{{:method}}; return false;' class="borderradius-1 bubble-btn">附近有{{:poi_total_num}}家店铺</a>
                        {{else}}
    	<a onclick='return false;' class="borderradius-1 bubble-btn disable-btn" disabled>抱歉，附近的店铺暂时还没有上线，尽请期待！</a>
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
             <li style="width: 200px;"><a href="{{:url}}" class="myadd">{{:label}}</a></li>
                    {{/for}}
                    
                </script>

                <script id="noaddressTemplate" type="text/x-jsrender">
                    <li style="width: 200px;"><a href='javascript:'>还没有历史地址</a></li>
                </script>


            </form>
            <div class="index_code_bg">
                <div class="index_code_title">
                    <span></span>
                    扫一扫，手机订餐更方便！
                    <span></span>
                </div>
                <div class="index_code_con">
                    <div class="index_code_wx">
                        <p>
                            <img src="images/wx_img.jpg" width="108px" height="108px" />
                        </p>
                        <p>微信版</p>
                    </div>
                    <div class="index_code_app">
                        <p>
                            <img src="images/app_img.jpg" width="108px" height="108px" />
                        </p>
                        <p>手机APP</p>
                    </div>
                </div>
            </div>
            <div class="index_bot">
                <div class="index_link">
                    <a href="/mobile_phone.aspx">手机版下载</a><i>|</i><a href="/bbs.aspx">留言版</a><i>|</i><a href="/shop/myindex.aspx">商家登录</a><i>|</i><a href="/Gift/Gift.aspx">积分商城</a><i>|</i><a href="/applyshop.aspx">商家加盟</a>
                </div>
                <div class="index_copyright"><%=SectionProxyData.GetSetValue(26)%></div>
            </div>
        </div>
    </div>


    <script type="text/javascript" src="javascript/jsrender.js"></script>
    <script type="text/javascript" src="javascript/jquery.page.js"></script>
    <script type="text/javascript" src="javascript/historyaddress.js"></script>
    <script type="text/javascript" src="javascript/eventwrapper.min.js"></script>
    <script type="text/javascript" src="javascript/json2.js"></script>
    <script type="text/javascript" src="javascript/newindex/index.js?v=1"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>
    <script type="text/javascript" src="javascript/newindex/indexbaidumap.js?v=2"></script>
</body>
</html>
