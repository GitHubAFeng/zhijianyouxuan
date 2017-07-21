/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />


//地图搜索
var myValue = "";
var cityname = "全国";//$("#hfcityname").val();
var ac = null;
var geocoder = null;
function initialize() {
    //建立一个自动完成的对象
    ac = new BMap.Autocomplete({ "input": "keyaddress", "location": "全国" });
    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        $("#keyaddress").val(myValue);
        setPlace();

    });
    ac.setInputValue($("#hfdefaultvalue").val());

    geocoder = new BMap.Geocoder();


}

function auto_location() {
    //自动定位
    var geolocation = new BMap.Geolocation();
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            myValue = "";
            geocoder.getLocation(r.point, function (rs) {
                var addComp = rs.addressComponents;
                myValue = addComp.city + addComp.district + addComp.street + addComp.streetNumber
                $("#keyaddress").val(myValue);
                setLatLng(r.point);
            });
        }
    }, { enableHighAccuracy: true });
}

//0:地址输入不正确，请重新输入
//1:当前地址附近无商家
//2:地图地址可以使用
var flag = 0;
function setPlace() {// 创建地址解析器实例
    var myGeo = new BMap.Geocoder();
    var tbkeyword = $("#keyaddress").val();
    if (tbkeyword == "") {
        alert("请输入小区名或某路某号");
        return;
    }
    myGeo.getPoint(tbkeyword, function (point) {
        if (point) {
            setLatLng(point);
        }
        else {
            flag = 0;
            alert("地址输入不正确，请重新输入");
        }
    }, cityname);
}



function setLatLng(point) {
    document.getElementById("hidlat").value = point.lat;
    document.getElementById("hidlng").value = point.lng;
    return true;
}