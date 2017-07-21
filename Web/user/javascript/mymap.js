/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var map = null;
var geocoder = null;
var marker = null;
var center = null;
var lat_lng_all = "";
var latlng_array = null;
var gzoom = 14;
var myadd = "";
var initpoint = null;
var myGeo = null;
var mylabel = null;
var adds = null;

function initialize() {
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());

    map = new BMap.Map("map_canvas");
    map.enableScrollWheelZoom();

    var cityname = $("#hfcityname").val();

    mylabel = new BMap.Label("加载中", { offset: new BMap.Size(-28, -22) });
    mylabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });

    var myIcon = new BMap.Icon("../images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });

    myGeo = new BMap.Geocoder();

    center = new BMap.Point(_lng, _lat);
    initpoint = new BMap.Point(_lng, _lat);
    map.centerAndZoom(initpoint, gzoom);

    marker = new BMap.Marker(center, { icon: myIcon, title: '当前定位' });
    map.addOverlay(marker);
    marker.enableDragging();
    map.addControl(new BMap.NavigationControl());

    marker.setLabel(mylabel);

    myGeo.getLocation(initpoint, function(rs) {
        var addComp = rs.addressComponents;
        adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
        mylabel.setContent(adds);
    });

    marker.addEventListener("dragend", function(e) {
        initpoint = e.point;
        setLatLng(initpoint);

        myGeo.getLocation(initpoint, function(rs) {
            var addComp = rs.addressComponents;
            adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
            mylabel.setContent(adds);
        });
    });

    marker.addEventListener("dragstart", function(e) {
        mylabel.setContent("加载中...");
        adds = "";
    });


    EventWrapper.addListener(map, "click", function(e) {
        map.clearOverlays();
        initpoint = e.point;
        map.removeOverlay(marker);
        marker = new BMap.Marker(initpoint, { icon: myIcon, title: '当前定位' });
        map.addOverlay(marker);
        marker.enableDragging();
        setLatLng(initpoint);
        marker.setLabel(mylabel);
        myGeo.getLocation(initpoint, function(rs) {
            var addComp = rs.addressComponents;
            adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
            mylabel.setContent(adds);
        });


        marker.addEventListener("dragend", function(e) {
            initpoint = e.point;
            marker = new BMap.Marker(initpoint);
            setLatLng(initpoint);

            myGeo.getLocation(initpoint, function(rs) {
                var addComp = rs.addressComponents;
                adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
                mylabel.setContent(adds);

                //$("#tbAddress").val(adds); //返回定位的地址名称

            });
        });

        marker.addEventListener("dragstart", function(e) {
            mylabel.setContent("加载中...");
            adds = "";
        });
    });
}

function setLatLng(point) {
    document.getElementById("hidLat").value = point.lat;
    document.getElementById("hidLng").value = point.lng;
    document.getElementById("hidlocalflag").value = "0";
    return true;
}

///搜索
function setPlace() {
   
    var cityname = $("#hfcityname").val()+"市";
    var address = document.getElementById("tbAddress").value;
    var local = new BMap.LocalSearch(cityname, {
        renderOptions: {
            map: map,
            autoViewport: true,
            selectFirstResult: false
        }
    });
    local.search(address);
}