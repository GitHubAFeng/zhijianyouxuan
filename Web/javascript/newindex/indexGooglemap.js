var marker = null;
var map = null;
var center = null;
var geocoder = null;
var gzoom = 13;
var service;
var searchmarkers=null;
var adds;
var infoWindow = null;
var postcode = "";
var onlypostcode = false;//如果只是用postcode搜索为真，否则为假
var lat;
var lng;
var isLoadMap = false;

//初始化地图
function initialize() {
    geocoder = new google.maps.Geocoder();
    searchmarkers =new Array();

    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    center = new google.maps.LatLng(_lat, _lng)
    var mapOptions = {
        center: center,
        zoom: gzoom,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    if (marker != null) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: center,
        draggable: true
    });
    marker.setMap(map);


    //GetLatLng();//后期添加 初始化地址 2015-12-3 

    //拖动
    google.maps.event.addListener(marker, 'dragend', function () {
        map.setCenter(marker.getPosition());//移入中心点
        GetLatLng();
    });

    //点击
    google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng);
    });

    var input = document.getElementById('tbindexsearchbox');
    var searchBox = new google.maps.places.SearchBox(input);

    isLoadMap = true;

}



//点击地图，生成marker
function placeMarker(location) {
    marker.setMap(null);
    marker = new google.maps.Marker({
        position: location,
        draggable: true,
        title: "current location",
        map: map
    });

    GetLatLng();
    google.maps.event.addListener(marker, 'dragend', function () {
        map.setCenter(marker.getPosition());
        GetLatLng();
    });
}

//获取标注点的经纬度 并查询商家
function GetLatLng() {
    document.getElementById("hidLat").value = marker.getPosition().lat();
    document.getElementById("hidLng").value = marker.getPosition().lng();
    onlypostcode = false;

    geocoder.geocode({ location: new google.maps.LatLng(marker.getPosition().lat(), marker.getPosition().lng()) },
    function geoResults(results, status) {

        if (status == google.maps.GeocoderStatus.OK) {

        center = marker.getPosition();

        map.setCenter(center);//移入中心点


            adds = results[0].formatted_address + "";
            postcode = results[0].address_components[results[0].address_components.length - 1].long_name;//这里是根据他们的地址对像来获取邮编的
            GetShopListFix(adds, marker.getPosition().lat(), marker.getPosition().lng())
        }
        else {
            alert("搜索失败，请重新搜索");
        }
    });
}


//ajax请求处理
function GetShopListFix(address, lat, lng) {
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

//显示商家数据
function processShopContentView(data, adds, lat, lng) {
    var infohtml = $("#mapInfoWindow").render({ address: adds, method: "openAroundWaimaiForClick(" + lat + "," + lng + ")", poi_total_num: data.data });
    infoWindow = new google.maps.InfoWindow({
        content: infohtml
    });
    infoWindow.open(map, marker);
}

//用于绑定的点击事件
function openAroundWaimaiForClick(lat, lng) {
    var url = "shoplist.aspx?lat=" + lat + "&lng=" + lng + "&addr=" + encodeURI(encodeURI(adds)) + "&from=m&postcode=" + postcode;
    saveLocation(lat, lng);
    return window.location.href = url;
}

//保存地址到cookie
function saveLocation(lat, lng) {
    handlecookie("mylat", lat, { expires: 365, path: "/", secure: false });
    handlecookie("mylng", lng, { expires: 365, path: "/", secure: false });
    handlecookie("myaddress", adds, { expires: 365, path: "/", secure: false });
}


///搜索按钮事件 10-19
function go_search() {

    $("#containerbox").animate({ "top": "50%" });

    $(".addr-results").show();
    $("#map").css({ "width": "765px", "float": "left" });
    if (!isLoadMap ) {
        initialize();
    }

    var key = $("#tbkeywork").val();
    if ($.trim(key) == "") {
        alert("please enter your address or postcode");
        return false;
    }


    for (var i = 0; i < searchmarkers.length; i++) {
        searchmarkers[i].setMap(null);
    }
    searchmarkers.length = 0;

    var search_address = key;
    geocoder.geocode({ 'address': search_address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var addressjson = [];
            for (var i = 0; i < results.length && i < 7 ; i++) {
                lat = results[i].geometry.location.lat();
                lng = results[i].geometry.location.lng();
                adds = results[i].formatted_address + "";
                postcode = results[0].address_components[results[0].address_components.length - 1].long_name;//这里是根据他们的地址对像来获取邮编的
                addressjson.push({ index: i, title:postcode , address:adds , method: "GetShopListFix(\"" + adds + "\",\"" + lat + "\",\"" + lng + "\")" });
            }
          

            placeMarker(results[0].geometry.location);

        }
        else {
            alert("没有找到任何搜索结果，换个关键字试试。")

        }
    });

}

