<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oldIndex.aspx.cs" Inherits="Index_bak" %>

<%@ Register Src="~/Banner.ascx" TagName="Banner" TagPrefix="uc2" %>
<%@ Register Src="~/header.ascx" TagName="header" TagPrefix="uc3" %>
<%@ Register Src="~/Foot.ascx" TagName="Foot" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=SectionProxyData.GetSetValue(3)%></title>
    <link type="text/css" href="css/common.css" rel="stylesheet" />
    <link type="text/css" href="css/index.css?v=2" rel="stylesheet" />
    <script src="javascript/jquery-1.7.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="javascript/jCommon.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initnav(1);

        })
    </script>
</head>

<body>
    <form runat="server">
        <uc2:Banner ID="Banner" runat="server" />
        <uc3:header ID="header" runat="server" />
        <asp:HiddenField runat="server" ID="hfshowtype" Value="0" />
        <asp:HiddenField runat="server" ID="hfcityname" />
        <asp:HiddenField runat="server" ID="hidLat" />
        <asp:HiddenField runat="server" ID="hidLng" />
        <div class="wrap" id="containerbox">
            <div class="map_landmark">
                <ul>
                    <li class="map_model map_model_hover indexmenuitem" id="index_menu_0" onclick="setshowmodelMenu('map_model_hover',0,this)"><a href="javascript:">地图模式</a></li>
                    <li class="landmark_model indexmenuitem" id="index_menu_1" onclick="setshowmodelMenu('landmark_model_hover',1,this)"><a href="javascript:">地标模式</a></li>
                </ul>
            </div>
            <div class="border-shadow">
                <div class="mainbord">
                    <div class="addr-search  ">
                        <span class="placebg fl"><span class="txt" runat="server" id="lbcityname"></span><span class="arrow"></span></span>
                        <div class="msearch fl">
                            <input type="text" placeholder="地图搜索,请输入写字楼，小区，学校等地址信息" class="a_text" id="tbkeywork" autocomplete="off" />
                            <input type="button" value="搜索" class="a_btn" id="search" style="cursor: pointer;" onclick="go_search()" />
                        </div>
                        <div class="repos hzind fl hisbg" style="margin-left: 12px;">

                            <div class="history  ">
                                <a href="javascript:">热门地标<span class="arrow"></span></a>
                            </div>
                            <div class="history_option">
                                <ul>
                                    <asp:Repeater ID="rpthotbuild" runat="server">
                                        <ItemTemplate>
                                            <li><a href="shoplist.aspx?lat=<%#Eval("lat")%>&lng=<%#Eval("lng") %>&addr=<%# Server.UrlEncode(Server.UrlEncode( Eval("name").ToString()))%>&from=m" class="myadd"><%#Eval("name")%></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="repos hzind fl hisbg" style="margin-left: 150px;">
                            <div class="history  ">
                                <a href="javascript:">历史地址<span class="arrow"></span></a>
                            </div>
                            <div class="history_option" id="haddress" runat="server">
                                <ul>

                                    <asp:Repeater ID="rptaddress" runat="server">
                                        <ItemTemplate>
                                            <li><a href="shoplist.aspx?lat=<%#Eval("lat")%>&lng=<%#Eval("lng") %>&addr=<%# Server.UrlEncode(Server.UrlEncode( Eval("Address").ToString()))%>&from=m" class="myadd"><%#Eval("Address")%></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>

                        </div>
                    </div>

                    <div class="pg-map clearfix modelitem model0" style="display: none;">
                        <div class="guid-map">
                            <span id="guide-close" onclick="$('.guid-map').hide()"></span>
                        </div>
                        <div class="addr-results">
                            <ol id="result_panel"></ol>
                            <div id="page_index" class="result-paginate"></div>
                        </div>

                        <div id="map" class="addr-map" style="width: 976px; padding: 1px;">
                            <div style="width: 100%; height: 577px; background-color: #fff;"
                                id="map_canvas">
                            </div>
                            <div id="map-tip-div" class="map-tip">
                                点击地图可以直接定位
                                <a class="close-link" onclick="$('#map-tip-div').toggle(); return false;" href="javascript:">x</a>
                            </div>
                        </div>
                    </div>
                    <div class="pg-landmark modelitem model1">
                        <ul>
                            <asp:Repeater ID="rptbuild" runat="server">
                                <ItemTemplate>
                                    <li><a href="shoplist.aspx?lat=<%#Eval("lat")%>&lng=<%#Eval("lng") %>&addr=<%# Server.UrlEncode(Server.UrlEncode( Eval("name").ToString()))%>&from=m"><%#WebUtility.Left(Eval("name"),13) %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <uc4:Foot ID="Foot" runat="server" />


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


<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=fMnzyhYs0D1cBEl5iGMQ0Dlg"></script>
<script src="javascript/json2.js" type="text/javascript"></script>
<script type="text/javascript" src="javascript/jsrender.js"></script>
<script type="text/javascript" src="javascript/eventwrapper.min.js"></script>
<script type="text/javascript" src="javascript/indexbaidumap.js?v=0513"></script>
<script type="text/javascript" src="javascript/jquery.page.js"></script>
<script type="text/javascript" src="javascript/historyaddress.js?v=0120"></script>


<script type="text/javascript">

    $(".hisbg").mouseover(function () {
        var $this = $(this);
        $this.find(".history_option").show();
        $this.addClass("option-open");

    });

    $(".hisbg").mouseout(function () {
        var $this = $(this);
        $this.find(".history_option").hide();
        $this.removeClass("option-open");

    });

    $(".history_option").mouseover(function () {
        var $this = $(this);
        $this.show();
        $this.parent().addClass("option-open");

    });
    $(".history_option").mouseout(function () {
        var $this = $(this);
        $this.hide();
        $this.parent().removeClass("option-open")

    });


</script>
