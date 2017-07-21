/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var map = null;
var geocoder = null;
var marker = null;
var center = null;
var lat_lng_all = "";
var latlng_array = null;
var gzoom = 15;
var bounds = null;
var ac = null;
var myValue, adds, newSearchFirstPage = !0, last_selected_index = -1, MAX_PAGE_NUM = 4, isFirstSearch = !0, markers = new Array, addresses = new Array, infowindows = new Array;

//初始化地图
function initialize() {
    var cityname = $("#hfcityname").val();
    map = new BMap.Map("map_canvas"); // 创建地图实例
    map.enableScrollWheelZoom();
    map.addControl(new BMap.NavigationControl());

    geocoder = new BMap.Geocoder();
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    var center = new BMap.Point(_lng, _lat); // 创建点坐标

    if ($("#hidLat").val() == "") {
        map.centerAndZoom(cityname, gzoom); // 初始化地图，设置中心点坐标和地图级别
    }
    else {
        map.centerAndZoom(center, gzoom); // 初始化地图，设置中心点坐标和地图级别
    }
   

    marker = new BMap.Marker(center, { icon: new BMap.Icon("http://maps.baidu.com/image/markers_new.png", new BMap.Size(25, 37), { offset: new BMap.Size(12, 37), imageOffset: new BMap.Size(0, -156) }) });
    marker.enableDragging();

    //建立一个自动完成的对象
    ac = new BMap.Autocomplete({ "input": "tbkeywork", "location": cityname + "市" });

    EventWrapper.addListener(map, "click", mapclick);
    marker.addEventListener("dragend", function (e) {
        var cpoint = e.point;
        center = e.point;
        map.clearOverlays();
        marker.setPosition(cpoint);
        map.addOverlay(marker);
        GetShopListFix("", cpoint.lat, cpoint.lng);
        setLatLng(cpoint);

    });

    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        go_search();
    });

    map.addEventListener("tilesloaded", function () {
        var type = request("type");
        if (type === "search") {
            $("#tbkeywork").val($("#hfaddresskey").val());
            go_search();
        }
    });
}

function setPlace() {// 创建地址解析器实例
    var myGeo = new BMap.Geocoder(); // 将地址解析结果显示在地图上,并调整地图视野
    var cityname = $("#hfcityname").val();
    myGeo.getPoint(myValue, function (point) {
        if (point) {
            map.clearOverlays();
            var cpoint = point;
            center = e.point;
            map.centerAndZoom(cpoint, gzoom);
            marker = new BMap.Marker(cpoint, { title: "当前定位" });
            map.panTo(cpoint);
            map.addOverlay(marker);
            marker.enableDragging();

            $("#shop_list").html(""); //商家列表div
            //获取此范围内的标志建筑物并显示
            //获取此坐标内的配送商家
            GetShopListFix("", cpoint.lat, cpoint.lng);

            setLatLng(cpoint);
            setsearchkey(myValue);

            marker.addEventListener("dragend", function (e) {

                var cpoint = e.point;
                map.clearOverlays();
                marker.setPosition(cpoint);
                map.addOverlay(marker);
                $("#shop_list").html(""); //商家列表div
                //获取此范围内的标志建筑物并显示
                //获取此坐标内的配送商家
                GetShopListFix("", cpoint.lat, cpoint.lng);
                setLatLng(cpoint);
                setsearchkey(myValue);

            });
        }
    }, cityname);

}

function setsearchkey(key) {
    handlecookie("search_key", key, { expires: 1, path: "/", secure: false });
    handlecookie("useraddr_id", null, { expires: 1, path: "/", secure: false });
}


function mapclick(target) {
    //flag == 0表示点击的是标注
    //flag > 0表示点击的是地图
    var flag = target.pixel.x;
    if (target && flag > 0) {
        var cpoint = target.point;
        center = target.point;
        map.clearOverlays();
        marker.setPosition(cpoint);
        map.addOverlay(marker);
        GetShopListFix("", cpoint.lat, cpoint.lng);
        setLatLng(cpoint);
    }
}

function GetShopListFix(address, lat, lng) {/////ajax请求处理
    $.ajax({
        type: "get",
        url: "ajax/MapFindShop.aspx",
        cache: false,
        dataType: "json",
        data: 'lat=' + lat + "&lng=" + lng + "",
        success: function (data, textStatus) {
            processShopContentView(data, address, lat, lng);
        }
    });
}

//data商家数最
function processShopContentView(data, location, lat, lng) {
    geocoder.getLocation(center, function (rs) {
        var addComp = rs.addressComponents;
        adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
        var infoWindow = new BMap.InfoWindow("", { enableMessage: !1 });
        infoWindow.setContent($("#mapInfoWindow").render({ address: adds, method: "openAroundWaimaiForClick()", poi_total_num: data.data }));
        marker.openInfoWindow(infoWindow);
    });
}


///搜索
function go_search() {
    setshowmodel(0);
    var key = $("#tbkeywork").val();
    if (key == "" || key == "请输入写字楼，小区，学校等地址信息") {
        alert("请输入写字楼，小区，学校等地址信息");
        return false;
    }

    var cityname = $("#hfcityname").val();

    local = new BMap.LocalSearch(map, { onSearchComplete: completeSearch });
    local.setPageCapacity(6);
    local.search(cityname + "市" + key, { forceLocal: !0 });
}

function completeSearch(e) {
    $(".addr-results").show();
    $("#map").css({ "width": "645px", "float": "left" });
    var a = e.getNumPois(), n = e.getNumPages(), t = local.getPageCapacity(), o = e.getPageIndex();
    if ($("#result_panel").html(""), map.clearOverlays(), last_selected_index = -1, 1 > a) {
        return $("#result_panel").html($("#mapEmptyResultWindow").render({})), void $("#page_index").html("");
    }
    newSearchFirstPage && ($("#page_index").html(""),
    n > 1 && $("#page_index").paginate({ count: Math.min(MAX_PAGE_NUM, n), start: 1, display: 4, border_color: "#d2d2d2", images: !1, text_color: "#434343", background_color: "#fff", text_hover_color: "black", background_hover_color: "#eee", onChange: selectPageIndex }), $("#result_panel").html(""), map.clearOverlays(), newSearchFirstPage = !1);
    for (var i = new Array, s = 0; s < Math.min(t, a - o * t) ; s++) {
        var r = e.getPoi(s); i.push(r.point), addMarker(s, r);
    }
    map.setViewport(i)
}

function addMarker(e, a) {
    var n = !1, t = n ? 34 : 24, o = n ? 26 : 19, i = n ? 13 : 9, s = n ? 36 : 27, r = n ? -73 : -199,
        d = new BMap.Marker(a.point, {
            icon: new BMap.Icon("http://maps.baidu.com/image/markers_new.png",
                new BMap.Size(o, s), {
                    offset: new BMap.Size(i, s),
                    imageOffset: new BMap.Size(0 - e * t, r)
                })
        });
    markers[e] = d, addresses[e] = a.title,
    $.post("ajax/MapFindShop.aspx", { lat: a.point.lat, lng: a.point.lng },
        function (n) {
            var t = new BMap.InfoWindow($("#mapInfoWindow").render({ title: a.title, address: a.address, method: "openAroundWaimai(" + e + ");", poi_total_num: n.data }), { enableMessage: !1 });
            infowindows[e] = t, 0 > last_selected_index && 0 == e && openMarkerById(0)
        }, "json"),
    d.addEventListener("click",
        function () {
            openMarkerById(e)
        }),
    d.addEventListener("infowindowclose",
        function () {
            var a = new BMap.Icon("http://maps.baidu.com/image/markers_new.png", new BMap.Size(o, s), { offset: new BMap.Size(i, s), imageOffset: new BMap.Size(0 - e * t, r) }); this.setIcon(a)
        }),
    $("#result_panel").append($("#mapResultWindow").render({ index: e, title: a.title, address: a.address, method: "openMarkerById(" + e + ")" })),
    $("#result_item_" + e).hover(function () { e !== last_selected_index && $(this).toggleClass("selected") }), map.addOverlay(d)
}


function openMarkerById(e) {
    var a = markers[e],
      n = !0, t = n ? 34 : 24, o = n ? 26 : 19, i = n ? 13 : 9, s = n ? 36 : 27, r = n ? -73 : -199, d = new BMap.Icon("http://maps.baidu.com/image/markers_new.png",
        new BMap.Size(o, s), { offset: new BMap.Size(i, s), imageOffset: new BMap.Size(0 - e * t, r) });
    a.setIcon(d), a.openInfoWindow(infowindows[e]), last_selected_index !== e && (last_selected_index >= 0 && $("#result_item_" + last_selected_index).hasClass("selected") && $("#result_item_" + last_selected_index).toggleClass("selected"), $("#result_item_" + e).hasClass("selected") || $("#result_item_" + e).toggleClass("selected"), last_selected_index = e)
}

function selectPageIndex(e) {
    local.gotoPage(e - 1)
}

function setLatLng(point) {
    document.getElementById("hidLat").value = point.lat;
    document.getElementById("hidLng").value = point.lng;
}

$(document).ready(function () {
    var hfshowtype = $("#hfshowtype").val();
    $("#index_menu_" + hfshowtype).click();
    setshowmodel(hfshowtype);
    initialize();
    $("#tbkeywork").on("keydown",
           function (e) {
               13 === e.keyCode && go_search()
           });
    //没定位转到首页
    var noaddress = request("noaddress");
    if (noaddress != "") {
        $(".guid-map").show();
        var pos = $("#containerbox").offset().top;
        $("html,body").animate({ scrollTop: pos }, 1000);
    }

})

///初始化显示模式
function setshowmodel(hfshowtype)
{
    $(".modelitem").hide();
    $(".model" + hfshowtype).show();
}

///初始化显示模式
function setshowmodelMenu(cul,type,tag) {
    $(".indexmenuitem").removeClass("map_model_hover");
    $(".indexmenuitem").removeClass("landmark_model_hover");
    $(tag).addClass(cul);
    
    setshowmodel(type);
}

function openAroundWaimai(e) {

    address =
    {
        time: new Date().getTime(),
        url: "shoplist.aspx?lat=" + markers[e].getPosition().lat + "&lng=" + markers[e].getPosition().lng + "&addr=" + encodeURI(encodeURI(addresses[e])) + "&from=m",
        lat: markers[e].getPosition().lat,
        lng: markers[e].getPosition().lng,
        label: encodeURI(encodeURI(addresses[e]))
    };

    historyaddress.add(address);

    return window.location.href = address.url, !1;
}


function openAroundWaimaiForClick() {
    address =
    {
        time: new Date().getTime(),
        url: "shoplist.aspx?lat=" + center.lat + "&lng=" + center.lng + "&addr=" + encodeURI(encodeURI(adds)) + "&from=m",
        lat: center.lat,
        lng: center.lng,
        label: encodeURI(encodeURI(adds))
    };

    historyaddress.add(address);
    return window.location.href = address.url, !1;
}