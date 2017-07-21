function initializingMap() {
    var cityname = $("#hfcityname").val();
    //建立一个自动完成的对象
    map = new BMap.Map("map_canvas", { enableMapClick: !1 }), map.enableScrollWheelZoom();
    var e = cityname;
    map.centerAndZoom(e, 15);
    map.setDefaultCursor("crosshair");
    map.addControl(new BMap.NavigationControl);
    map.addEventListener("click",
        function (e) {
            singleclick(e)
        });
    local = new BMap.LocalSearch(e, { onSearchComplete: completeSearch });
    mapAutoComplete = new BMap.Autocomplete({ input: "tbkeywork", location: e });
    mapAutoComplete.addEventListener("onconfirm",
        function (e) {
            e.item.value; searchAddress()
        });
    scmarker = new BMap.Marker(new BMap.Point(116.404, 39.915), { icon: new BMap.Icon("http://maps.baidu.com/image/markers_new.png", new BMap.Size(25, 37), { offset: new BMap.Size(12, 37), imageOffset: new BMap.Size(0, -156) }) });
    scinfowindow = new BMap.InfoWindow("", { enableMessage: !1 });
    scmarker.addEventListener("click", function () { scmarker.openInfoWindow(scinfowindow) });
    scmarker.addEventListener("infowindowclose", function () { map.removeOverlay(scmarker) });
    decoder = new BMap.Geocoder();
}
function singleclick(e) {
    e.overlay || (scmarker.setPosition(e.point)
    decoder.getLocation(e.point,
        function (a) {
            $.post("/ajax/MapFindShop.aspx", { lat: e.point.lat, lng: e.point.lng },
                function (e) {
                    alert(e.data);
                    scinfowindow.setContent($("#mapInfoWindow").render({ title: a.business, address: a.address, method: "openAroundWaimaiForClick()", poi_total_num: e.data }));
                    map.addOverlay(scmarker);
                    scmarker.openInfoWindow(scinfowindow);
                })
        }),
    last_selected_index >= 0 && $("#result_item_" + last_selected_index).hasClass("selected") && $("#result_item_" + last_selected_index).toggleClass("selected"))
}
function openAroundWaimaiForClick() {
    return window.location.href = "shoplist.aspx?lat=" + scmarker.getPosition().lat + "&lng=" + scmarker.getPosition().lng + "&addr=" + encodeURI(encodeURI(scaddress)) + "&from=m", !1
}
function searchAddress() {
    var e = $("#sear_add_key").val();
    e = $.trim(e),
    "" != e && (isFirstSearch && (isFirstSearch = !1, $(".pg-map .addr-results").show()), searchAddressInner(e))
}
function searchAddressInner(e) {
    local.setPageCapacity(6), local.search(e, { forceLocal: !0 }), newSearchFirstPage = !0
}
function completeSearch(e) {
    var a = e.getNumPois(), n = e.getNumPages(), t = local.getPageCapacity(), o = e.getPageIndex();
    if ($("#result_panel").html(""), map.clearOverlays(), last_selected_index = -1, 1 > a)
        return $("#result_panel").html($("#mapEmptyResultWindow").render({})),
            void $("#page_index").html("");
    newSearchFirstPage && ($("#page_index").html(""),
    n > 1 && $("#page_index").paginate({ count: Math.min(MAX_PAGE_NUM, n), start: 1, display: 4, border_color: "#d2d2d2", images: !1, text_color: "#434343", background_color: "#fff", text_hover_color: "black", background_hover_color: "#eee", onChange: selectPageIndex }), $("#result_panel").html(""), map.clearOverlays(), newSearchFirstPage = !1); for (var i = new Array, s = 0; s < Math.min(t, a - o * t) ; s++) { var r = e.getPoi(s); i.push(r.point), addMarker(s, r) } map.setViewport(i)
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
    $.post("/poi/ajax/nbpoinum", { lat: a.point.lat, lng: a.point.lng },
        function (n) {
            var t = new BMap.InfoWindow($("#mapInfoWindow").render({ title: a.title, address: a.address, method: "openAroundWaimai(" + e + ");", poi_total_num: n.data }), { enableMessage: !1 });
            infowindows[e] = t, 0 > last_selected_index && 0 == e && openMarkerById(0)
        }),
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
function openAroundWaimai(e) {
    return window.location.href = "/geo/geohash?lat=" + markers[e].getPosition().lat + "&lng=" + markers[e].getPosition().lng + "&addr=" + encodeURI(encodeURI(addresses[e])) + "&from=m", !1
}
function selectPageIndex(e)
{ local.gotoPage(e - 1) }
function changeHistorySeq(e) {
    var a = $.cookie("w_ah"); if ("" != a) { var n = a.split("|"); if (!(0 >= e || e >= n.length)) { for (var t = n[e], o = 0; o < n.length; o++) o != e && (t += "|" + n[o]); var i = new Date; i.setTime(i.getTime() + 252288e7), $.cookie("w_ah", t, { path: "/", expires: i }) } }
}
function printMarkerList() {
    $("#markerlist ul li").each(function () {
        var e = $(this).text(), a = e.split("|"), n = parseInt(a[2], 10),
            t = new BMap.Point(a[1], a[0]), o = a[3], i = a[4], s = {}; n >= 100 ? (s.height = 65, s.width = 47, s.offsetX = -567, s.offsetY = -131, s.hover = -217, s.labelTop = -34) : n >= 10 && 99 >= n ? (s.height = 59, s.width = 42, s.offsetX = -650, s.offsetY = -134, s.hover = -220, s.labelTop = -31) : n >= 0 && 9 >= n && (s.height = 47, s.width = 34, s.offsetX = -722, s.offsetY = -139, s.hover = -225, s.labelTop = -36);
        var r = new BMap.Marker(t, {
            icon: new BMap.Icon(MT.STATIC_ROOT + "/img/sprite/sprite.png",
                new BMap.Size(s.width, s.height), { anchor: new BMap.Size(13, 36), imageOffset: new BMap.Size(s.offsetX, s.offsetY) })
        }); r.addEventListener("mouseout", function (e) { var a = e.target; a.setIcon(new BMap.Icon(MT.STATIC_ROOT + "/img/sprite/sprite.png", new BMap.Size(s.width, s.height), { anchor: new BMap.Size(13, 36), imageOffset: new BMap.Size(s.offsetX, s.offsetY) })) }), r.addEventListener("mouseover", function (e) { var a = e.target; a.setIcon(new BMap.Icon(MT.STATIC_ROOT + "/img/sprite/sprite.png", new BMap.Size(s.width, s.height), { anchor: new BMap.Size(13, 36), imageOffset: new BMap.Size(s.offsetX, s.hover) })) }), r.addEventListener("click", function () { var e = this, a = new BMap.InfoWindow("", { enableMessage: !1 }); if (n > 0) var s = "<a href='/geo/geohash?lat=" + t.lat + "&lng=" + t.lng + "&addr=" + i + '&from=m\' class="borderradius-1 bubble-btn">附近有' + n + "家外卖餐厅</a>"; else; a.setContent("<h3>" + o + '</h3><p class="bubble-addr">地址：' + o + '</p> <div class="clearfix show-cont">' + s + "</div>"), e.openInfoWindow(a) }), map.addOverlay(r); var d = new BMap.Label(n, { position: t, offset: new BMap.Size(0, s.labelTop) }); d.setStyle({ color: "#FFFFFF", fontSize: "12px", height: "20px", lineHeight: "20px", fontWeight: "bold", backgroundColor: "transparent", border: 0 }), map.addOverlay(d)
    })
}
var map, local, markers = new Array, addresses = new Array, infowindows = new Array, newSearchFirstPage = !0, mapAutoComplete, scmarker = null, scinfowindow = null, scaddress = "", decoder, last_selected_index = -1, MAX_PAGE_NUM = 4, isFirstSearch = !0;
window.onload = function () {
    initializingMap(),
    $("#sea_add_btn").click(
        function ()
        { searchAddress() }),
    $("#sear_add_key").on("keydown",
        function (e)
        { 13 === e.keyCode && searchAddress() }),
    $("#address_history ul li a").click(
        function () {
            var e = $(this).attr("data_seq");
            return "" != e && $.isNumeric(e) ? (changeHistorySeq(parseInt(e)), !0) : !0
        }), printMarkerList()
},
$(".hist-link").click(
    function () {
        $("#address_history").show()
    });