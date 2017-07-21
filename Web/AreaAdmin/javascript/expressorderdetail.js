/// <reference path="../../javascript/jquery-1.7.min.js" />

var gzoom = 12;
var marker = null;
var markerlist = new Array();
var map = new BMap.Map("map_canvas"); // 创建地图实例
map.enableScrollWheelZoom();
var myGeo = new BMap.Geocoder();
var _lat = parseFloat($("#hidLat").val());
var _lng = parseFloat($("#hidLng").val());
var initpoint = new BMap.Point(_lng, _lat); // 创建点坐标
//图标
var myIcon = new BMap.Icon("http://www.ihangjing.com/images/marker50.png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });

var cityname = $("#hfcityname").val();
map.centerAndZoom(cityname, gzoom);

map.addControl(new BMap.NavigationControl());  //缩放工具

function setLatLng(point) {
    document.getElementById("hidLat").value = point.lat;
    document.getElementById("hidLng").value = point.lng;
    return true;
}

function showorder(shopid, orderid) {
    var sheight = window.screen.height - 70;
    var swidth = window.screen.width - 10;

    var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=yes,menubar=yes,location=yes,status=yes,scrollbars=yes,resizable=yes";
    var url = "updateorder.aspx?id=" + shopid + "&oid=" + orderid;
    window.open(url, "newwindow", winoption);
}


function j_ShowWindow(msg) {
    var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
    innerHTML += "<div style='float:left;font-size:12px;color:#fff'>温馨提醒</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='/Images/window_close.gif' alt='关闭窗口' /> </a>";
    innerHTML += "</div></div>";
    innerHTML += "<div style='text-align:left;font-size:12px;width:95%;overflow:hidden;padding:5px; padding-left:10px;' id='divMassage'>";
    innerHTML += msg;
    innerHTML += "</div></div></div>";

    if (!document.getElementById("divMsg")) {
        var div = document.createElement('div');
        div.id = 'divMsg';
        div.setAttribute('style', 'bottom:0px; right:20px; width:350px; height:200px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;display:none;');
        div.setAttribute('innerHTML', innerHTML);
        div.innerHTML = innerHTML;
        document.body.appendChild(div);
        with (document.getElementById("divMsg").style) {
            bottom = "0px";
            right = "0px";
            width = "350px";
            height = "200px";
            position = "absolute";
            background = "#FFFFFF";
            border = "2px solid #447AA9";
        }
    }
    else {
        document.getElementById("divMsg").style.display = "block";
        document.getElementById("divMsg").setAttribute('innerHTML', innerHTML);
        document.getElementById("divMsg").innerHTML = innerHTML;
    }
    //setTimeout(HiddenWindow, 3000);
}
function HiddenWindow() {
    if (document.getElementById("divMsg")) {
        document.getElementById("divMsg").style.display = "none";
    }
}

function select_mycity(name, id) {
    var url = "OrderDelive.aspx?cid=" + id + "&cname=" + escape(name);
    window.location = url;

}

//外卖订单
function showorderlocal() {
    map.clearOverlays();
    markerlist.length = 0;

    var state = $("#hfstate").val();
    var _latlng = $("#hflatlng").val();
    var deliverid = $("#hfdeliverid").val();
    var orderid = $("#hforderid").val();
    var username = $("#hfusername").val();
    var address = $("#hfaddress").val();
    var shopname = $("#hfshopname").val();

    var jsonlatlng = eval("(" + _latlng + ")");
    //用户坐标
    map.removeOverlay(marker);
    var userpoint = new BMap.Point(jsonlatlng.ulng, jsonlatlng.ulat); // 创建点坐标
    var userIcon = new BMap.Icon("images/tack.gif", new BMap.Size(33, 33), { anchor: new BMap.Size(17, 0) });
    var usermarker = new BMap.Marker(userpoint, { icon: userIcon });

    var userlabel = new BMap.Label("用户：" + username + "(" + address + ")", { offset: new BMap.Size(-28, -22) });
    userlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
    usermarker.setLabel(userlabel);

    map.addOverlay(usermarker);
    //商家坐标
    var shoppoint = new BMap.Point(jsonlatlng.slng, jsonlatlng.slat); // 创建点坐标
    var shopmarker = new BMap.Marker(shoppoint);

    var shoplabel = new BMap.Label("商家：" + shopname, { offset: new BMap.Size(-28, -22) });
    shoplabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
    shopmarker.setLabel(shoplabel);

    map.addOverlay(shopmarker);

    showload_super("", "", "dddd");


    switch (state) {
        case "3":
        case "6":
        case "4":
        case "5":
        case "1":
        case "2":
            {
                //这几个状态只显示用户位置
                hideload_super('dddd');
                return;
                break;
            }
        case "7": //在地图上显示位置,用户，商家，配送员    
            var url = "/admin/ajax/GetDeliverModel.aspx";
            var para = "id=" + deliverid + "&t=" + new Date().getTime();
            jQuery.ajax(
                {
                    type: "post",
                    url: url,
                    data: para,
                    success: function (msg) {
                        hideload_super('dddd');
                        //以配送员为中心
                        var deli_latlng = eval("(" + msg + ")");

                        var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat);// 创建点坐标
                        var d_icon = new BMap.Icon("../Service/images/mapmarker/marker" + (parseInt(1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                        var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                        var deliverlabel = new BMap.Label(deli_latlng.d_name + "(" + deli_latlng.ordernum + ")", { offset: new BMap.Size(-28, -22) });
                        deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                        d_marker.setLabel(deliverlabel);
                        map.addOverlay(d_marker);

                        map.addOverlay(d_marker);
                        map.panTo(d_point);
                        //$("#hf_curr_dids").val("o_" + deliverid);
                        markerlist.push(d_marker);
                    }
                })


            break;
    }
    $("html,body").animate({ scrollTop: 0 }, 1000);

}


//跑腿订单
function showorderexpress(state, _latlng, deliverid, orderid, username, address, shopname, sectionid) {
    map.clearOverlays();
    markerlist.length = 0;

   // var jsonlatlng = eval("(" + _latlng + ")");
    var jsonlatlng = eval("(" + $("#latlng" ).html() + ")");
    //用户坐标
    map.removeOverlay(marker);
    var userpoint = new BMap.Point(jsonlatlng.ulng, jsonlatlng.ulat); // 创建点坐标
    var userIcon = new BMap.Icon("images/tack.gif", new BMap.Size(33, 33), { anchor: new BMap.Size(17, 0) });
    var usermarker = new BMap.Marker(userpoint, { icon: userIcon });

    var userlabel = new BMap.Label("收件人：" + username + "(" + address + ")", { offset: new BMap.Size(-28, -22) });
    userlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
    usermarker.setLabel(userlabel);

    map.addOverlay(usermarker);
    //商家坐标
    var shoppoint = new BMap.Point(jsonlatlng.slng, jsonlatlng.slat); // 创建点坐标
    var shopmarker = new BMap.Marker(shoppoint);

    var shoplabel = new BMap.Label("寄件人：" + shopname, { offset: new BMap.Size(-28, -22) });
    shoplabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
    shopmarker.setLabel(shoplabel);

    map.addOverlay(shopmarker);

    showload_super("", "", "dddd");

    switch (state) {
        case "3":
        case "5":
        case "6":
      
            {
                //这几个状态只显示用户位置
                hideload_super('dddd');
                return;
                break;
            }
        case "0":
            {
                map.panTo(shoppoint);
               // $("#hforderuserlatlng").val(jsonlatlng.slat + "," + jsonlatlng.slng);
                //显示3个最近的配送员
                var sname = $("#ddlsection option:selected").text();
                var hfcityid = $("#hfcityid").val();
                var url = "../ajax/GetDeliverListbySpan.aspx";
                var para = "sname=" + escape(sname) + "&t=" + new Date().getTime() + "&lat=" + jsonlatlng.slat + "&lng=" + jsonlatlng.slng + "&cid=" + hfcityid;
                jQuery.ajax(
                {
                    type: "get",
                    url: url,
                    data: para,
                    success: function (msg) {
                        hideload_super('dddd');

                        var d_list = eval("(" + msg + ")");
                       // var ddl_d = document.getElementById("ddldeliver");
                        //ddl_d.options.length = 0;
                        var see_didsstr = "o_";
                        for (var i = 0; i < d_list.length; i++) {
                            if (i < 3) {   //前3个添加标注
                                if (d_list[i].distance != "99999999") {


                                    var d_point = new BMap.Point(d_list[i].Lng, d_list[i].Lat); // 创建点坐标
                                    var d_icon = new BMap.Icon("../Service/images/mapmarker/marker" + (parseInt(i + 1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                                    var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                                    var deliverlabel = new BMap.Label(d_list[i].Name + "(" + d_list[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                                    deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                                    d_marker.setLabel(deliverlabel);
                                    map.addOverlay(d_marker);
                                    see_didsstr += d_list[i].DataId + ",";
                                    markerlist.push(d_marker);
                                    //初始化右边配送员信息
                                    if (i == 0) {
                                       // $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                }
                            }

                            //ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                        }
                       // getsubsort();
                       // var mydids = see_didsstr.replace(/,$/, "");
                       // $("#hf_curr_dids").val(mydids);
                    }
                })
                break;
            }


        case "2":
        case "4":
        case "1":
            {
                var url = "/admin/ajax/GetDeliverModel.aspx";
                var para = "id=" + deliverid + "&t=" + new Date().getTime();
                jQuery.ajax(
                    {
                        type: "post",
                        url: url,
                        data: para,
                        success: function (msg) {
                            hideload_super('dddd');
                            //以配送员为中心
                            var deli_latlng = eval("(" + msg + ")");

                            var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat);
                            var d_icon = new BMap.Icon("../Service/images/mapmarker/marker" + (parseInt(1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                            var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                            var deliverlabel = new BMap.Label("配送员" + deli_latlng.d_name + "(" + deli_latlng.ordernum + ")", { offset: new BMap.Size(-28, -22) });
                            deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                            d_marker.setLabel(deliverlabel);
                            map.addOverlay(d_marker);
                            map.addOverlay(d_marker);
                            map.panTo(d_point);
                            //$("#hf_curr_dids").val("o_" + deliverid);
                            markerlist.push(d_marker);      
                        }
                    })
                break;
            }
    }
    $("html,body").animate({ scrollTop: 0 }, 1000);
}



