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
var myGeo = new BMap.Geocoder();
var mylabel = null;
var adds = null;
var geolocation = new BMap.Geolocation();

var mOption = {
    poiRadius: 500,           //半径为1000米内的POI,默认100米
    numPois: 10                //列举出50个POI,默认10个
}


function initialize() {
    var _lat = parseFloat($("#lat").val());
    var _lng = parseFloat($("#lng").val());

    var center = new BMap.Point(_lng, _lat); // 创建点坐标

    map = new BMap.Map("map_canvas");
    map.centerAndZoom(center, gzoom);
    auto_location();

}

//根据地址信息，查询城市信息 {'cid':0,'cname':''} ,没有时城市编号为0
function checkCity(address) {
    var citys = eval("(" + $("#hfcityjson").val() + ")");
    var citydata = { 'cid': 0, 'cname': '' };
    for (var i in citys) {
        if (address.indexOf(citys[i].cname) >= 0) {
            citydata = { 'cid': citys[i].cid, 'cname': '' + citys[i].cname + '' };

            handlecookie("user_cityid", citys[i].cid, { expires: 30, path: "/", secure: false });

            break;
        }
    }
    return citydata;
}


//自动定位
function auto_location()
{
    $("#addressbox").html("");
    $("#keyaddress").val("");
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            center = r.point;
            initpoint = r.point;

            myGeo.getLocation(r.point, function (rs) {
                var addComp = rs.addressComponents;
                adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                setLatLng(initpoint);
                $("#lbautomyaddress").html(adds);
                $("#hfislocate").val("1");
                if (request("change") == null || request("change") == "") {
                    gotoorder(initpoint.lat, initpoint.lng, adds);
                }

                var city = checkCity(adds);
                $("#tbcityname").val(city.cname);

                displayPOI();
            });
        }
        else {
            //alert("定位失败" + this.getStatus());
        }
    }, { enableHighAccuracy: true });
}


function displayPOI() {
    map.addOverlay(new BMap.Circle(initpoint, 500));        //添加一个圆形覆盖物
    myGeo.getLocation(initpoint,
        function mCallback(rs) {


            var poiarray = [];

            var allPois = rs.surroundingPois;       //获取全部POI（该点半径为100米内有6个POI点）
            for (i = 0; i < allPois.length; ++i) {
               // document.getElementById("panel").innerHTML += "<p style='font-size:12px;'>" + (i + 1) + "、" + allPois[i].title + ",地址:" + allPois[i].address + "</p>";
                poiarray.push({ "lat": "" + allPois[i].point.lat, "lng": "" + allPois[i].point.lng, "title": "" + allPois[i].title, "address": "" + allPois[i].address });
                //map.addOverlay(new BMap.Marker(allPois[i].point));
            }



            var html = $("#addressTemplate").render(poiarray);
            $("#POIbox").html(html);


        }, mOption
    );
}



function setLatLng(point) {
    document.getElementById("lat").value = point.lat;
    document.getElementById("lng").value = point.lng;
    return true;
}

//定位相关类
$(function () {
    initialize();
})

function gotoorder(lat, lng, address) {
    handlecookie("mylat", lat, { expires: 30, path: "/", secure: false });
    handlecookie("mylng", lng, { expires: 30, path: "/", secure: false });
    handlecookie("address", encodeURI(address), { expires: 30, path: "/", secure: false });
    var url = "Togolist.aspx?islocal=1&address=" + encodeURI(address) + "&lat=" + lat + "&lng=" + lng;
    window.location = url;
    return false;

}

//搜索，并显示结果
function setPlace() {

    var cityname = $("#tbcityname").val();
    var address = document.getElementById("keyaddress").value;
    if (address == "") {
        alert("输入学校、商务楼、地址");
        return;
    }

    var addressjson = [];

    var options = {
        onSearchComplete: function (results) {
            // 判断状态是否正确
            if (local.getStatus() == BMAP_STATUS_SUCCESS) {
                var s = [];
                for (var i = 0; i < results.getCurrentNumPois() && i < 10 ; i++) {
                    addressjson.push({ "lat": "" + results.getPoi(i).point.lat, "lng": "" + results.getPoi(i).point.lng, "title": "" + results.getPoi(i).title, "address": "" + results.getPoi(i).address });
                }

                var html = $("#addressTemplate").render(addressjson);
                $("#addressbox").html(html);

            }
        }
    };
    var local = new BMap.LocalSearch(cityname, options);
    local.search(address);


}

///用当前位置点餐
function useCurrLocation() {
    var hfislocate = $("#hfislocate").val();
    if (hfislocate == "0") {
        alert("正在获取位置，请稍后");
        return;
    }

    var lat = document.getElementById("lat").value;
    var lng = document.getElementById("lng").value;

    gotoorder(lat, lng, adds);
}