/// <reference path="../../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />
//地图初始化
function mapinit() {
    map = new BMap.Map("map_canvas"); // 创建地图实例
    map.enableScrollWheelZoom();
    myGeo = new BMap.Geocoder();
    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
    //图标
    myIcon = new BMap.Icon("/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
    marker = new BMap.Marker(initpoint, { icon: myIcon });
    map.addOverlay(marker);
    marker.enableDragging();
    marker.setTitle("拖动修改位置");
    map.centerAndZoom(initpoint, gzoom); // 初始化地图，设置中心点坐标和地图级别
    map.addControl(new BMap.NavigationControl());  //缩放工具

    //map.addEventListener("click", mapclick);
    EventWrapper.addListener(map, "click", function(e) {
        map.clearOverlays();
        initpoint = e.point;
        map.removeOverlay(marker);
        marker = new BMap.Marker(initpoint, { icon: myIcon });
        map.addOverlay(marker);
        marker.enableDragging();
        marker.openInfoWindow(infoWindow);
        setLatLng(initpoint);
        marker.addEventListener("dragend", function(e) {
            initpoint = e.point;
            marker = new BMap.Marker(initpoint);
            this.openInfoWindow(infoWindow);
            setLatLng(initpoint);
        });
        marker.addEventListener("dragstart", function(e) {
            map.closeInfoWindow()
        });
    });

    //建立一个自动完成的对象
    ac = new BMap.Autocomplete(
    {
        "input": "left_tbaddress",
        "location": "全国"
    });
    ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        j_getGeocoder();
    });

    ac.setInputValue($("#left_hfaddress").val());


    marker.addEventListener("dragend", function(e) {
        initpoint = e.point;
        marker = new BMap.Marker(initpoint);
        this.openInfoWindow(infoWindow);
        setLatLng(initpoint);
    });

    marker.addEventListener("dragstart", function(e) {
        map.closeInfoWindow()
    });

    setfirstaddr();
    
}

function setLatLng(point) {
    document.getElementById("hidLat").value = point.lat;
    document.getElementById("hidLng").value = point.lng;
	
    //反馈添加 2015-8-12 
    handlecookie("mylat", point.lat, { expires: 1, path: "/", secure: false });
    handlecookie("mylng", point.lng, { expires: 1, path: "/", secure: false });
    return true;
}

//地址解析
function searchshop() {
    var address = $(".keyaddress").val();
    var cityname=$("#hfcityname").val();
   

    var local = new BMap.LocalSearch(cityname, {
        renderOptions: {
            map: map,
            autoViewport: true,
            selectFirstResult: true
        }
    });
    local.search(address);

}

///开始标注(搜索)
function j_getGeocoder() {
    var keyaddress = $(".keyaddress").val() + "";
    if (keyaddress == "") {
        alert("请输入用户地址");
        return;
    }
    searchshop();
    $("#shopdiv").hide();
    $("#orderdiv").hide();
    $("#mapdiv").show();
    $("#mapdiv").css("visibility", "visible");
}

//查看位置
function show_local() {
    $("#shopdiv").hide();
    $("#mapdiv").show();
}

//地址相关js
//添加地址
function add_addr() {
    var left_tbuname = escape($("#left_tbuname").val() + "");
    var left_tbtel = $("#left_tbtel").val() + "";
    var left_tbaddress = escape($("#left_tbaddress").val() + "");
    var left_tbbuildname = $("#left_tbbuildname").val() + "";
    var hidLat = $("#hidLat").val();
    var hidLng = $("#hidLng").val();

    if (left_tbuname == "") {
        alert("请输入姓名");
        return;
    }
    if (left_tbtel == "") {
        alert("请输入电话");
        return;
    }
    if (left_tbuname == "") {
        alert("请输入姓名");
        return;
    }
    if (left_tbbuildname == "") {
        alert("请选择建筑物");
        return;
    }
    showload_super();
    var url = "ajax/addaddress.aspx";
    var para = "UserName=" + left_tbuname + "&Tel=" + left_tbtel;
    para += "&Address=" + left_tbaddress + "&DataId=0";
    para += "&lat=" + hidLat + "&lng=" + hidLng;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {

            if (msg == "-1") {
                alert("服务器繁忙，请稍后再试");
            }
            else {
                alert("添加成功");
                hideload_super();
                $(".myadd_ul").html(msg);
                setfirstaddr();
            }
        }
    })

}

//编辑地址
function edit_addr() {
    var left_tbuname = escape($("#left_tbuname").val() + "");
    var left_tbtel = $("#left_tbtel").val() + "";
    var left_tbaddress = escape($("#left_tbaddress").val() + "");
    var dataid = $("#left_add_dataid").val() + "";
    var hidLat = $("#hidLat").val();
    var hidLng = $("#hidLng").val();

    if (dataid == "0" || dataid == "") {
        alert("请选择要编辑的地址");
        return;
    }
    if (left_tbuname == "") {
        alert("请输入姓名");
        return;
    }
    if (left_tbtel == "") {
        alert("请输入电话");
        return;
    }
    if (left_tbaddress == "") {
        alert("请输入地址");
        return;
    }
    showload_super(); ;
    var url = "ajax/addaddress.aspx";
    var para = "UserName=" + left_tbuname + "&Tel=" + left_tbtel;
    para += "&Address=" + left_tbaddress + "&lat=" + hidLat + "&lng=" + hidLng + "&DataId=" + dataid;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            if (msg == "-1") {
                alert("服务器繁忙，请稍后再试");
            }
            else {
                alert("编辑成功");
                hideload_super();
                $(".myadd_ul").html(msg);
                setfirstaddr();
            }

        }
    })
}

//删除地址
function del_addr() {
    var dataid = $("#left_add_dataid").val() + "";
    if (dataid == "0" || dataid == "") {
        alert("请选择要删除的地址");
        return;
    }
    var left_tbtel = $("#left_tbtel").val() + "";
    showload_super(); ;
    var url = "ajax/deladdress.aspx";
    var para = "DataId=" + dataid + "&Tel=" + left_tbtel;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            if (msg == "-1") {
                alert("服务器繁忙，请稍后再试");
            }
            else {
                alert("删除成功");
                hideload_super();
                $(".myadd_ul").html(msg);
                setfirstaddr();
            }

        }
    })
}

//选择地址
function setaddress(evt) {
    $('input:radio[name="addressradio1"]').removeAttr("checked");
    evt.checked = true
    var v = evt.value + "";
    var names = v.split('^');
    //<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("BuildingName")%>^<%# Eval("BuildingID") %>^<%# Eval("dataid") %>
    if (names[0] != "新地址") {
        $("#left_tbuname").val(names[0]);
        $("#left_tbaddress").val(names[1]);
        $("#left_add_dataid").val(names[4]);
        $("#hidLat").val(names[2]);
        $("#hidLng").val(names[3]);
        //保存当前地址编号
        handlecookie("used_addressid", names[4], { expires: 1, path: "/", secure: false });
        handlecookie("mylat", names[2], { expires: 1, path: "/", secure: false });
        handlecookie("mylng", names[3], { expires: 1, path: "/", secure: false });
        $("#left_ddlsection").val(names[5]);
        show_map();

        $("#hidLat").val(names[2]);
        $("#hidLng").val(names[3]);

        $("#tbsearch").click();
    }
    else {
        $("#left_tbaddress").val("");
        $("#left_add_dataid").val(0);
        handlecookie("used_addressid", "0", { expires: 1, path: "/", secure: false });

    }
}

//设置第一个地址
function setfirstaddr() {
    var first_addr = $(".first_addr");
    if (first_addr.length > 0) {
        var v = first_addr.val() + "";
        var names = v.split('^');
        $("#left_tbuname").val(names[0]);
        $("#left_tbaddress").val(names[1]);
        $("#left_tbaddress").attr("address", names[1]);
        $("#left_add_dataid").val(names[4]);
        //保存当前地址编号
        handlecookie("used_addressid", names[4], { expires: 1, path: "/", secure: false });
        $("#hidLat").val(names[2]);
        $("#hidLng").val(names[3]);
        //保存当前地址编号
        handlecookie("used_addressid", names[4], { expires: 1, path: "/", secure: false });
        handlecookie("mylat", names[2], { expires: 1, path: "/", secure: false });
        handlecookie("mylng", names[3], { expires: 1, path: "/", secure: false });

        $("#hidLat").val(names[2]);
        $("#hidLng").val(names[3]);

        $("#tbsearch").click();
    }
}

///关闭地图
function hide_map() {
    $("#shopdiv").show();
    $("#orderdiv").show();
    $("#mapdiv").hide();
}

///打开地图
function show_map() {
    //$("#shopdiv").hide();
    $("#mapdiv").show();
    $("#mapdiv").css("visibility", "visible");

    map.removeOverlay(marker);

    var _lat = parseFloat($("#hidLat").val());
    var _lng = parseFloat($("#hidLng").val());
    initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
    myIcon = new BMap.Icon("/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
    marker = new BMap.Marker(initpoint, { icon: myIcon });
    map.addOverlay(marker);
    marker.enableDragging();


    map.panTo(initpoint);
}

//点击获取用户地址
function getmyaddress() {
    var tel = $("#left_tbtel").val();
    if (tel == "") {
        alert("请输入电话");
        return;
    }
    window.location = "OrderCrm.aspx?tel=" + tel + "&start=1";
    return false;
}

//确定地址
function addressOK()
{
    map.closeInfoWindow();

    var dataid = $("#left_add_dataid").val() + "";
    if (dataid == 0) {
        add_addr();
    }
    else {
        edit_addr();
    }
    hide_map();
}