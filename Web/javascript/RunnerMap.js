

var map = null;//地图实例
var gzoom = 15;//缩放等级
var marker = null;//地图标签
var adds = null;//当前位置

var markerfrom;//发件标记
var markerto;//收件标记
var polyline;//折线

function initialize() {
    var cityname = $("#hfcityname").val();
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    var center = new BMap.Point(_lng, _lat); // 创建点坐标

    map = new BMap.Map("allmap", { enableMapClick: false }); // 创建地图实例，enableMapClick屏蔽百度地图默认的提示框

    //map.centerAndZoom(cityname, gzoom); // 初始化地图，设置中心点坐标和地图级别
    //后期修改 2015-12-2 
    if ($.trim(cityname) == "") {
        map.centerAndZoom(center, gzoom); // 初始化地图，设置中心点坐标和地图级别
    }
    else {
        map.centerAndZoom(cityname, gzoom); // 初始化地图，设置中心点坐标和地图级别
    }

    map.enableScrollWheelZoom();//允许滚轮缩放
    map.addControl(new BMap.NavigationControl());

    ////寄件地址下拉列表框
    //var fromaddress = new BMap.Autocomplete(    //建立一个自动完成的对象
    //{
    //    "input": "tbAddress",
    //    "location": map
    //});
    //fromaddress.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
    //    var _value = e.item.value;
    //    var add = _value.province + _value.city + _value.district + _value.street + _value.business;

    //    serchAddFrom(add);
    //});

    ////收件地址下拉列表框
    //var toaddress = new BMap.Autocomplete(    //建立一个自动完成的对象
    //{
    //    "input": "tbOorderid",
    //    "location": map
    //});
    //toaddress.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
    //    var _value = e.item.value;
    //    var add = _value.province + _value.city + _value.district + _value.street + _value.business;

    //    serchAddTo(add);
    //});

}

$(document).ready(function () {
    initialize();
});

//搜索地址
function serchAdd(add) {
    var geocoder = new BMap.Geocoder(); //创建一个地图解析器实例
    var cityname = $("#hfcityname").val();

    geocoder.getPoint(add, function (point) {//对指定的地址进行解析，定位成功，则回调坐标point坐标以及函数
        if (point) {
            center = point;
            map.centerAndZoom(center, gzoom);//设置中心点坐标和缩放等级

            marker = new BMap.Marker(center, { icon: new BMap.Icon("http://maps.baidu.com/image/markers_new.png", new BMap.Size(25, 37), { offset: new BMap.Size(12, 37), imageOffset: new BMap.Size(0, -156) }) });
            marker.enableDragging();//允许拖拽

            //map.clearOverlays();//清除所有标签
            //marker.setPosition(center);//设置标签坐标
            //map.addOverlay(marker);//加载标签

            marker.addEventListener("dragend", function (e) {
                center = e.point;
                geocoder.getLocation(center, function (rs) {
                    var addComp = rs.addressComponents;
                    adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber

                    var opts = {
                        width: 200,     // 信息窗口宽度
                        height: 60,     // 信息窗口高度
                        title: "搜索结果", // 信息窗口标题
                        enableMessage: true,//设置允许信息窗发送短息
                        message: ""
                    }
                    var infoWindow = new BMap.InfoWindow(adds, opts);  // 创建信息窗口对象
                    map.openInfoWindow(infoWindow, center); //开启信息窗口

                });
            });

            map.addOverlay(marker);//加载标签
            marker.setLabel(alabel);//设置标签
            map.setCenter(center);//重新设置地图中心点
        }
        else {
            if (marker) {
                map.removeOverlay(marker);//清除标签
            }
            cleardistance();
            alert("当前地址不存在，请重新输入");

        }

    }, cityname);
}


//搜索寄件人地址
function serchAddFrom(add) {
    var geocoder = new BMap.Geocoder(); //创建一个地图解析器实例
    var cityname = $("#hfcityname").val();

    geocoder.getPoint(add, function (point) {//对指定的地址进行解析，定位成功，则回调坐标point坐标以及函数
        if (point) {
            center = point;
            map.centerAndZoom(center, gzoom);//设置中心点坐标和缩放等级
            //map.clearOverlays();//清除所有标签
            //marker.setPosition(center);//设置标签坐标
            //map.addOverlay(marker);//加载标签

            if (markerfrom) {
                map.removeOverlay(markerfrom);//清除原来的标签
            }

            //初始化图标,设置寄件人图标
            var myIcon = new BMap.Icon("/images/mapIcon.png", new BMap.Size(32, 42), {
                offset: new BMap.Size(0, 0),
                imageOffset: new BMap.Size(-57, 0)
            });
            markerfrom = new BMap.Marker(center, { icon: myIcon });//初始化标签,坐标和图标初始化
            markerfrom.enableDragging();//允许图标拖拽

            markerfrom.addEventListener("dragend", function (e) {
                center = e.point;
                geocoder.getLocation(center, function (rs) {
                    var addComp = rs.addressComponents;
                    adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
                    $("#tbAddress").val(adds);
                });

                setfromLatLng(center);
                caldistance();
                calfee();
            });

            alabel = new BMap.Label("寄件人地址", { offset: new BMap.Size(-40, -38) });
            alabel.setStyle({
                color: "#fff",
                width: '96px',
                height: '31px',
                lineHeight: '31px',
                textAlign: 'center',
                background: '#ec7b01',
                fontSize: "14px",
                padding: "2px 5px",
                border: "2px #fff solid"
            });
            map.addOverlay(markerfrom);//加载标签
            markerfrom.setLabel(alabel);//设置标签
            map.setCenter(center);//重新设置地图中心点
            setfromLatLng(center);
           
            caldistance();
            calfee();

        }
        else {
            if (markerfrom) {
                map.removeOverlay(markerfrom);//清除标签
            }
            cleardistance();
            alert("当前寄件地址不存在，请重新输入");

        }

    }, cityname);
}

//搜索收件人地址
function serchAddTo(add) {
    var geocoder = new BMap.Geocoder(); //创建一个地图解析器实例
    var cityname = $("#hfcityname").val();

    geocoder.getPoint(add, function (point) {//对指定的地址进行解析，定位成功，则回调坐标point坐标以及函数
        if (point) {
            center = point;
            map.centerAndZoom(center, gzoom);//设置中心点坐标和缩放等级

            if (markerto) {
                map.removeOverlay(markerto);//清除标签
            }

            //初始化图标,设置收件人图标
            var myIcon = new BMap.Icon("/images/mapIcon.png", new BMap.Size(32, 42), {
                offset: new BMap.Size(0, 0),
                imageOffset: new BMap.Size(0, -48)
            });
            markerto = new BMap.Marker(center, { icon: myIcon });//初始化标签,坐标和图标初始化
            markerto.enableDragging();//允许图标拖拽

            markerto.addEventListener("dragend", function (e) {
                center = e.point;
                geocoder.getLocation(center, function (rs) {
                    var addComp = rs.addressComponents;
                    adds = addComp.city + addComp.district + addComp.street + addComp.streetNumber
                    $("#tbOorderid").val(adds);
                });
                settoLatLng(center);
                caldistance();
                calfee();
            });

            alabel = new BMap.Label("收件人地址", { offset: new BMap.Size(-40, -38) });
            alabel.setStyle({
                color: "#fff",
                width: '96px',
                height: '31px',
                lineHeight: '31px',
                textAlign: 'center',
                background: '#ec7b01',
                fontSize: "14px",
                padding: "2px 5px",
                border: "2px #fff solid"
            });
            map.addOverlay(markerto);//加载标签
            markerto.setLabel(alabel);//设置标签

            map.setCenter(center);//重新设置地图中心点
            settoLatLng(center);
           
            caldistance();
            calfee();
        }
        else {
            if (markerto) {
                map.removeOverlay(markerto);//清除标签
            }
            cleardistance();
            alert("当前收件地址不存在，请重新输入");
            //settoLatLng(center);
        }
    }, cityname);
}


//保存寄件点
function setfromLatLng(point) {
    document.getElementById("hidflat").value = point.lat;
    document.getElementById("hidflng").value = point.lng;
}

//保存收件点
function settoLatLng(point) {
    document.getElementById("hidtlat").value = point.lat;
    document.getElementById("hidtlng").value = point.lng;
}

//清除数据
function cleardistance() {

    $("#lbdistance").text("0");
    $("#hiddistance").val("0");

    $("#lbsendfee").text("0");
    $("#hidsendfee").val("0");
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

            var geocoder = new BMap.Geocoder(); //创建一个地图解析器实例
            var fromadd = new BMap.Point($("#hidflng").val(), $("#hidflat").val()); // 创建寄件人点坐标
            var toadd = new BMap.Point($("#hidtlng").val(), $("#hidtlat").val()); // 创建收件人点坐标
            if (polyline) {
                map.removeOverlay(polyline);//清除标签
            }
            polyline = new BMap.Polyline([fromadd, toadd], { strokeColor: "blue", strokeWeight: 3, strokeOpacity: 0.5 });  //定义折线
            map.addOverlay(polyline);   //添加折线到地图上
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