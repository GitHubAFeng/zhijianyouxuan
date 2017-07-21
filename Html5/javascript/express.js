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
var currentlocationObject = 'f';//正在定位的对像，f表示发件，t表示收件，坐标赋值时会根据这个来查询
var city = { "cityid": 0, "cityname": "" };
var cityjson = null;
var feejson = null;

function initialize() {
    myGeo = new BMap.Geocoder();

    //建立一个自动完成的对象
    ac = new BMap.Autocomplete({ "input": "keyaddress", "location": "全国" });
    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件

        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        myGeo.getPoint(myValue, function (point) {
            if (point) {
                setLatLng(point);
                locationComplete();


                if (currentlocationObject == "f") {
                    $("#tbAddress").val(myValue);
                    //找出城市
                    for (var i in cityjson) {
                        if (_value.city.indexOf(cityjson[i].cname) >= 0) {
                            city.cityid = cityjson[i].cid;
                            city.cityname = cityjson[i].cname;
                            $("#hfcityname").val(city.cityname);
                            $(".citynamebox").html(city.cityname);
                            break;
                        }
                    }
                }
                else {
                    $("#tbOorderid").val(myValue);
                }
                $("#keyaddress").val("");

            }
        }, "全国");


    });

    /*
    geocoder = new BMap.Geocoder();
    //自动定位
    var geolocation = new BMap.Geolocation();
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            myValue = "";
            geocoder.getLocation(r.point, function (rs) {
                var addComp = rs.addressComponents;
                myValue = addComp.city + addComp.district + addComp.street + addComp.streetNumber
                $("#tbAddress").val(myValue);
            });
        }
    }, { enableHighAccuracy: true });*/

}

function setLatLng(point) {
    document.getElementById("lat").value = point.lat;
    document.getElementById("lng").value = point.lng;

    return true;
}

//定位相关类
$(function () {

    cityjson = eval("(" + $("#hfcityjson").val() + ")");
    //feejson = eval("(" + $("#hffeesjon").val() + ")");

    initialize();
    $("#maptipbox").hide();

    var screenwidth = document.documentElement.clientWidth;
    var screenheight = document.documentElement.clientHeight

    var maskleft = screenwidth * 0.1 / 2; //弹出层90%，left= screenwidth*10%/2
    $(".freeSet").css({ left: maskleft + "px" });
    $("#mymark").css({ height: screenheight + "px" });
    $(".x,.mModal").click(function () {
        $(".mModal,.mDialog").hide();
    });
})


function setPlace() {
    var cityname = $("#hfcityname").val();
    var address = document.getElementById("keyaddress").value;
    searchPlaceOnMap(address);
}

function showmap(address, locationtag) {
    $("#maptipbox").show();
    $(".mModal").show();
    currentlocationObject = locationtag;

    searchPlaceOnMap(address);
}

function searchPlaceOnMap(address) {
    if (address.length == 0) {
        //swal("", "请输入地址", "warning");
        return;
    }

    var cityname = $("#hfcityname").val();
    var local = new BMap.LocalSearch(cityname, {
        renderOptions: {
            map: map,
            autoViewport: true,
            selectFirstResult: false
        }
    });
    local.search(address);
}


function locationComplete() {
    var _lat = parseFloat($("#lat").val());
    var _lng = parseFloat($("#lng").val());

    $("#hid" + currentlocationObject + "lat").val(_lat);
    $("#hid" + currentlocationObject + "lng").val(_lng);
    $(".mModal,.mDialog").hide();

    calDistanceAndFee();
    //todo计算距离，配送费。
}

///计算距离，配送费。
function calDistanceAndFee(callback) {
    caldistance();
    calfee();
    if (typeof callback != 'undefined') {
        callback();
    }
}
//计算直线距离
function caldistance() {

    if (($("#hidflat").val() == "0") && ($("#hidflng").val() == "0")) {
        return -1;
    }

    if (($("#hidtlat").val() == "0") && ($("#hidtlng").val() == "0")) {
        return -2;
    }

    jQuery.ajax(
    {
        type: "post",
        url: "../Ajax/Runner.aspx",
        data: 'fuc=distance&flat=' + $("#hidflat").val() + '&flng=' + $("#hidflng").val() + '&tlat=' + $("#hidtlat").val() + '&tlng=' + $("#hidtlng").val(),
        success: function (msg) {
            $("#lbdistance").text(msg);
            $("#hiddistance").val(msg);

            //var geocoder = new BMap.Geocoder(); //创建一个地图解析器实例
            //var fromadd = new BMap.Point($("#hidflng").val(), $("#hidflat").val()); // 创建寄件人点坐标
            //var toadd = new BMap.Point($("#hidtlng").val(), $("#hidtlat").val()); // 创建收件人点坐标
            //if (polyline) {
            //    map.removeOverlay(polyline);//清除标签
            //}
            //polyline = new BMap.Polyline([fromadd, toadd], { strokeColor: "blue", strokeWeight: 3, strokeOpacity: 0.5 });  //定义折线
            //map.addOverlay(polyline);   //添加折线到地图上
        }
    });
}

//计算配送费
function calfee() {
    if (($("#hidflat").val() == "0") && ($("#hidflng").val() == "0")) {
        return -1;
    }

    if (($("#hidtlat").val() == "0") && ($("#hidtlng").val() == "0")) {
        return -2;
    }

    jQuery.ajax(
    {
        type: "post",
        url: "../Ajax/Runner.aspx",
        data: 'fuc=sendfee&flat=' + $("#hidflat").val() + '&flng=' + $("#hidflng").val() + '&tlat=' + $("#hidtlat").val() + '&tlng=' + $("#hidtlng").val(),
        success: function (msg) {
            $("#lbsendfee").text(msg);
            $("#hidsendfee").val(msg);
        }
    });
}


//转到下一步
function nextstep() {
    var flat = $("#hidflat").val();
    var flng = $("#hidflng").val();
    if (flat == 0 || flng == 0) {
        swal("", "取件地址未定位", "warning");
        return false;;
    }

    var tlat = $("#hidtlat").val();
    var tlng = $("#hidtlng").val();
    if (tlat == 0 || tlng == 0) {
        swal("", "收件地址未定位", "warning");
        return false;;
    }

    var flag = j_submitdata("orderdetail");
    if (false == flag) {
        return false;
    }

    Loader.show("#btnextstep");


    calDistanceAndFee(gotonext);

    return false;
}

function gotonext() {
    //保存信息 
    for (var key in expressInfo) {
        expressInfo[key] = $("#" + key).val();
    }
    expressInfo.cityid = city.cityid;
    express.saveExpressinfo(expressInfo);

    var url = "expresstwo.aspx";
    window.location.href = url;
    return false;

}

var typeicon = ["购", "驾"];

function setexpresstype(target, type) {
    $(".paotui_con .cur").removeClass("cur");
    $(target).addClass("cur");
    $("#tbcallcount").val(type);;
    $("#typeiconbox").html(typeicon[type]);
}

// Date & Time demo initialization

var ndate = new Date();
var mintime = dateAdd("h", 1, ndate);
var maxtime = dateAdd("h", 30, mintime);

$('#tbSentTime').mobiscroll().datetime({
    theme: $.mobiscroll.defaults.theme,     // Specify theme like: theme: 'ios' or omit setting to use default 
    mode: "scroller",       // Specify scroller mode like: mode: 'mixed' or omit setting to use default 
    display: "bottom", // Specify display mode like: display: 'bottom' or omit setting to use default 
    lang: "zh",       // Specify language like: lang: 'pl' or omit setting to use default
    minDate: mintime,  // More info about minDate: http://docs.mobiscroll.com/2-17-0/datetime#!opt-minDate
    maxDate: maxtime,   // More info about maxDate: http://docs.mobiscroll.com/2-17-0/datetime#!opt-maxDate
    stepMinute: 5  // More info about stepMinute: http://docs.mobiscroll.com/2-17-0/datetime#!opt-stepMinute

});

//日期增加函数
function dateAdd(strInterval, NumDay, dtTmp) {
    if (dtTmp == null | dtTmp == "")
        dtTmp = new Date();
    switch (strInterval) {
        case "h":
            return new Date(Date.parse(dtTmp) + (3600000 * NumDay));
        case "d":
            return new Date(Date.parse(dtTmp) + (86400000 * (NumDay + 1)));
        case "w":
            return new Date(Date.parse(dtTmp) + ((86400000 * 7) * NumDay) + 86400000);
        case "m":
            return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + NumDay, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case "y":
            return new Date((dtTmp.getFullYear() + NumDay), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }

}

