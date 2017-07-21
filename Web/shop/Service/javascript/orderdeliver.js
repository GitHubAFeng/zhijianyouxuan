/*
* 此js都是调度中用到的js 
* by jijunjian
*
*
*/
// hf_curr_dids这个控件保存当前在地图的配送员编号（可能有一个或者3个）,主要用来定时更新位置(30s)
//保存格式：1。点击配送员：d_配送员编号
//          2.点击订单:o_配送员编号(可能是一个编号或者3个编号)

//点击配送员，在地图上显示位置,订单情况
// ordernum 未处理订单个数
//name   名称
//lat 纬度
//lng 经度
//deleiverid 配送员编号
//gpsimei 配送员对应的imei
//phone
function showme(ordernum, name, lat, lng, deleiverid, gpsimei, Phone) {
    //获取配送员当前位置
    map.clearOverlays();
    //清空
    markerlist.length = 0;
    var url = "../ajax/GetDeliverModel.aspx";
    var para = "id=" + deleiverid + "&t=" + new Date().getTime();
    jQuery.ajax(
        {
            type: "post",
            url: url,
            data: para,
            success: function(msg) {
                //以配送员为中心
                var deli_latlng = eval("(" + msg + ")");

                var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat); // 创建点坐标

                $("#hidLat").val(deli_latlng.lat);
                $("#hidLng").val(deli_latlng.lng);

                var d_marker = new BMap.Marker(d_point, { icon: myIcon });
                d_marker.setTitle(deli_latlng.d_name + "(未处理订单" + deli_latlng.ordernum + "个)");
                map.addOverlay(d_marker);

                $("#hf_curr_dids").val("d_" + deleiverid);

                var opts = {
                    width: 350,     // 信息窗口宽度  
                    height: 100    // 信息窗口高度
                }

                var myordercount = deli_latlng.orderlist.length;

                var winhtml = "<div><p>姓名：" + deli_latlng.d_name + "</p>";
                winhtml += "<p>电话：" + deli_latlng.Phone + "</p>";
                winhtml += "<p>行驶方向： <img id=\"theimage\" border=\"0\" src=\"direction.jpg\" onload=\"rotateRight('theimage'," + deli_latlng.direction + ");\" /></p>";
                winhtml += "<p>车况：" + deli_latlng.carstate + "</p>";
                winhtml += "<p>未处理订单：" + deli_latlng.ordernum + "个</p>";

                winhtml += "<ul>";

                for (var xx = 0; xx < myordercount; xx++) {
                    winhtml += "<li><span>" + deli_latlng.orderlist[xx].UserName + "</span>(" + deli_latlng.orderlist[xx].Tel + ")(" + deli_latlng.orderlist[xx].Address + ")<span style='margin-left:10px;'>" + deli_latlng.orderlist[xx].TogoName + "</span></li>";
                }

                winhtml += "</ul></div>";

                var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象
                // 打开信息窗口
                d_marker.openInfoWindow(infoWindow)

                map.panTo(d_point);

                d_marker.addEventListener("click", function(e) {
                    ;
                    this.openInfoWindow(infoWindow);
                });
                markerlist.push(d_marker);
            }
        })
}

/*
* 点击订单，如果已经分配，显示
* by jijunjian
* state 订单状态
*laglng 用户，商家地址（json）
*deliverid 配送员编号
*orderid 订单编号
*username 用户名称，
address 用户地址
shopname 商家名称
sectionid 订单区域编号
*/
function showorderlocal(state, _latlng, deliverid, orderid, username, address, shopname, sectionid) {
    map.clearOverlays();
    markerlist.length = 0;
    $("#hfexpressororder").val("0");//0代表外卖订单
    $("#hforderid").val(orderid);
    $("#hfordersection").val(sectionid)
    $("#hfstate").val(state); ;


    var jsonlatlng = eval("(" + $("#latlng_" + orderid).html() + ")");
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

    getorderinfo(orderid);

    switch (state) {
        case 3:
        case 6:
        case 4:
        case 5:
            {
                //这几个状态只显示用户位置
                hideload_super('dddd');
                return;
                break;
            }
        case 1:
        case 2: //在地图上显示位置,用户，商家,还有推荐的3个配送员(离商家最近的),以商家为中心
            {
                map.panTo(shoppoint);
                $("#hforderuserlatlng").val(jsonlatlng.slat + "," + jsonlatlng.slng);
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
                    success: function(msg) {
                        var d_list = eval("(" + msg + ")");
                        var ddl_d = document.getElementById("ddldeliver");
                        ddl_d.options.length = 0;
                        var see_didsstr = "o_";
                        for (var i = 0; i < d_list.length; i++) {
                            if (i < 3) {   //前3个添加标注
                                if (d_list[i].distance != "99999999") {


                                    var d_point = new BMap.Point(d_list[i].Lng, d_list[i].Lat); // 创建点坐标
                                    var d_icon = new BMap.Icon("images/mapmarker/marker" + (parseInt(i + 1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                                    var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                                    var deliverlabel = new BMap.Label(d_list[i].Name + "(" + d_list[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                                    deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                                    d_marker.setLabel(deliverlabel);
                                    map.addOverlay(d_marker);
                                    see_didsstr += d_list[i].DataId + ",";
                                    markerlist.push(d_marker);
                                    //初始化右边配送员信息
                                    if (i == 0) {
                                        $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                }
                            }

                            ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                        }
                        hideload_super('dddd');
                        getsubsort();
                        var mydids = see_didsstr.replace(/,$/, "");
                        $("#hf_curr_dids").val(mydids);
                    }
                })
                break;

            }
      
        case 7: //在地图上显示位置,用户，商家，配送员

            map.panTo(shoppoint);
            $("#hforderuserlatlng").val(jsonlatlng.slat + "," + jsonlatlng.slng);
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
                    success: function(msg) {
                        var d_list = eval("(" + msg + ")");
                        var ddl_d = document.getElementById("ddldeliver");
                        ddl_d.options.length = 0;
                        var see_didsstr = "o_";
                        for (var i = 0; i < d_list.length; i++) {
                            if (i < 3) {   //前3个添加标注
                                if (d_list[i].distance != "99999999") {
                                  

                                    var d_point = new BMap.Point(d_list[i].Lng, d_list[i].Lat); // 创建点坐标
                                    var d_icon = new BMap.Icon("images/mapmarker/marker" + (parseInt(i + 1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                                    var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                                    var deliverlabel = new BMap.Label(d_list[i].Name + "(" + d_list[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                                    deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                                    d_marker.setLabel(deliverlabel);
                                    map.addOverlay(d_marker);


                                    see_didsstr += d_list[i].DataId + ",";
                                    markerlist.push(d_marker);
                                    //初始化右边配送员信息
                                    if (i == 0) {
                                        $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                }
                            }

                            ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                        }
                        hideload_super('dddd');
                        getsubsort();
                        var mydids = see_didsstr.replace(/,$/, "");
                        $("#hf_curr_dids").val(mydids);
                    }
                })

            if (deliverid == 0) {

                hideload_super('dddd');
                break;
            }
            var url = "../ajax/GetDeliverModel.aspx";
            var para = "id=" + deliverid + "&t=" + new Date().getTime();
            jQuery.ajax(
                {
                    type: "post",
                    url: url,
                    data: para,
                    success: function(msg) {
                        //以配送员为中心
                        var deli_latlng = eval("(" + msg + ")");
                      
                        var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat); // 创建点坐标

                        var d_icon = new BMap.Icon("images/mapmarker/marker" + (parseInt(1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                        var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                        var deliverlabel = new BMap.Label(deli_latlng.d_name + "(" + deli_latlng.ordernum + ")", { offset: new BMap.Size(-28, -22) });

                        deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                        d_marker.setLabel(deliverlabel);
                        map.addOverlay(d_marker);


                        map.addOverlay(d_marker);

                        map.panTo(d_point);
                        $("#hf_curr_dids").val("o_" + deliverid);
                        hideload_super('dddd');

                        markerlist.push(d_marker);

                        var sname = $("#ddlsection option:selected").text();
                        var url = "../ajax/GetDeliverListbySpan.aspx";
                        var hfcityid = $("#hfcityid").val();
                        var para = "sname=" + escape(sname) + "&t=" + new Date().getTime() + "&lat=" + jsonlatlng.slat + "&lng=" + jsonlatlng.slng + "&cid=" + hfcityid;
                        jQuery.ajax(
                        {
                            type: "get",
                            url: url,
                            data: para,
                            success: function(msg) {
                                var d_list = eval("(" + msg + ")");
                                var ddl_d = document.getElementById("ddldeliver");
                                ddl_d.options.length = 0;
                                var see_didsstr = "o_";
                                for (var i = 0; i < d_list.length; i++) {

                                    //初始化右边配送员信息
                                    if (i == 0) {
                                        $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                    ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                                }

                            }
                        })

                    }
                })


            break;
    }
    $("html,body").animate({ scrollTop: 0 }, 1000);

}

///返回订单信息,和进度信息
function getorderinfo(orderid) {
    var url = "ajax/orderinfoandprocesser.aspx";
    var para = "orderid=" + orderid + "&t=" + new Date().getTime();
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            var data = eval("(" + msg + ")");
            $("#tborderinfo").val(data.messagebody);
        }
    })
}


///发短信验证
function checkmsg() {
    var d_id = $("#hfdid_hh").val() + "";
    if (d_id == "0") {
        alert("请选择配送员。");
        return false;
    }
    var tborderinfo = $("#tborderinfo").val() + "";
    if (tborderinfo == "") {
        alert("请输入短信内容");
        return false;
    }
    showload_super("", "", "dddd");
    return true;
}

///发短信验证
function checkdeliver() {
    var d_id = $("#hfdid_hh").val() + "";
    if (d_id == "0") {
        alert("请选择配送员。");
        return false;
    }
    showload_super("", "", "dddd");
    return true;
}

///发群验证
function checksendgroup() {
    var d_id = $("#ddlgroup").val() + "";
    if (d_id == "0") {
        alert("请选择群组。");
        return false;
    }
    var tborderinfo = $("#tborderinfo").val() + "";
    if (tborderinfo == "") {
        alert("请先选择订单");
        return false;
    }

    showload_super("", "", "dddd");
    return true;
}

//选择配送员
function getsubsort() {
    $("#hfdid_hh").val(document.getElementById("ddldeliver").value);
}

//各个提醒
function Tellmsg() {
    var hfcityid = $("#hfcityid").val();
    var url = "ajax/mymsg.aspx";
    var para = "t=" + new Date().getTime() + "&cid=" + hfcityid;
    jQuery.ajax(
    {
        type: "get",
        url: url,
        data: para,
        success: function(msg) {
            if (msg != "0") {
                j_ShowWindow(msg);
                $("#divMsg").floatdiv("rightbottom");
            }
        }
    })

    LoadDeliver(9);
}

function j_ShowWindow(msg) {
    var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
    innerHTML += "<div style='float:left;font-size:12px;color:#fff'>温馨提醒</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='../Images/window_close.gif' alt='关闭窗口' /> </a>";
    innerHTML += "</div></div>";
    innerHTML += "<div style='text-align:left;font-size:12px;width:100%;overflow:hidden;padding-left:5px;' id='divMassage'>";
    innerHTML += msg;
    innerHTML += "</div></div></div>";

    if (!document.getElementById("divMsg")) {
        var div = document.createElement('div');
        div.id = 'divMsg';
        div.setAttribute('style', 'bottom:0px; right:20px; width:330px; height:120px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;');
        div.setAttribute('innerHTML', innerHTML);
        div.innerHTML = innerHTML;
        document.body.appendChild(div);
        with (document.getElementById("divMsg").style) {
            bottom = "0px";
            right = "5px";
            width = "350px";
            height = "240px";
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

///获取统计信息,和配送员当前位置
function getsuminfo() {

    //配送员当前位置
    var hf_curr_dids = $("#hf_curr_dids").val() + "";
    if (hf_curr_dids != "") {
        var tempdelver = hf_curr_dids.split('_');
        if (tempdelver[0] == "d" && (tempdelver[1] + "") != "") {//点击配送员
            showme("", "", "", "", tempdelver[1], "");
        }
        else {
            //删除配送员。再标注 removeOverlay
            for (var i = 0; i < markerlist.length; i++) {
                map.removeOverlay(markerlist[i]);
            }
            var o_delivers = (tempdelver[1] + "").split(',');

            var hfstate = parseInt($("#hfstate").val());
            if (hfstate < 3) {//未调度的信息
                var shoplatlgn = $("#hforderuserlatlng").val().split(',');

                var url = "../ajax/GetDeliverListbySpan.aspx";
                var para = "t=" + new Date().getTime() + "&lat=" + shoplatlgn[0] + "&lng=" + shoplatlgn[1]
                jQuery.ajax(
                {
                    type: "post",
                    url: url,
                    data: para,
                    success: function(msg) {
                        var d_list = eval("(" + msg + ")");
                        var see_didsstr = "o_";
                        for (var i = 0; i < d_list.length; i++) {
                            if (i < 3) {   //前3个添加标注
                                if (d_list[i].distance != "99999999") {
                                    var d_point = new BMap.Point(d_list[i].Lng, d_list[i].Lat); // 创建点坐标
                                    var d_marker = new BMap.Marker(d_point);

                                    var deliverlabel = new BMap.Label(d_list[i].Name + "(" + d_list[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                                    deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                                    d_marker.setLabel(deliverlabel);
                                    map.addOverlay(d_marker);
                                    see_didsstr += d_list[i].DataId + ",";
                                    markerlist.push(d_marker);
                                }
                            }
                        }
                    }
                })

            }
            else {//已经有在配送中的订单变位置
                if ((tempdelver[1] + "") != "") {
                    // showme("", "", "", "", tempdelver[1], "");
                    var url = "../ajax/GetDeliverModel.aspx";
                    var para = "id=" + tempdelver[1] + "&t=" + new Date().getTime();
                    jQuery.ajax(
                    {
                        type: "post",
                        url: url,
                        data: para,
                        success: function(msg) {
                            //以配送员为中心
                            markerlist.length = 0;
                            map.removeOverlay(markerlist[0]);

                            var deli_latlng = eval("(" + msg + ")");
                            var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat); // 创建点坐标

                            $("#hidLat").val(deli_latlng.lat);
                            $("#hidLng").val(deli_latlng.lng);

                            var d_marker = new BMap.Marker(d_point, { icon: myIcon });
                            d_marker.setTitle(deli_latlng.d_name + "(未处理订单" + deli_latlng.ordernum + "个)");
                            map.addOverlay(d_marker);

                            var opts = {
                                width: 250,     // 信息窗口宽度  
                                height: 50    // 信息窗口高度
                            }
                            var infoWindow = new BMap.InfoWindow("<div><p>姓名：" + deli_latlng.d_name + "</p><p>未处理订单：" + deli_latlng.ordernum + "个</p></div>", opts);  // 创建信息窗口对象
                            // 打开信息窗口
                            d_marker.openInfoWindow(infoWindow);

                            map.panTo(d_point);

                            d_marker.addEventListener("click", function(e) {
                                this.openInfoWindow(infoWindow);
                            });
                            markerlist.push(d_marker);
                        }
                    })

                }
            }
        }
    }

}
//图片旋转
/*
id 图片id
angle 度数  
*/
function rotate(id, angle, whence) {
    var p = document.getElementById(id);

    // we store the angle inside the image tag for persistence  
    if (!whence) {
        p.angle = ((p.angle == undefined ? 0 : p.angle) + angle) % 360;
    } else {
        p.angle = angle;
    }

    if (p.angle >= 0) {
        var rotation = Math.PI * p.angle / 180;
    } else {
        var rotation = Math.PI * (360 + p.angle) / 180;
    }
    var costheta = Math.cos(rotation);
    var sintheta = Math.sin(rotation);

    if (document.all && !window.opera) {
        var canvas = document.createElement('img');

        canvas.src = p.src;
        canvas.height = p.height;
        canvas.width = p.width;

        canvas.style.filter = "progid:DXImageTransform.Microsoft.Matrix(M11=" + costheta + ",M12=" + (-sintheta) + ",M21=" + sintheta + ",M22=" + costheta + ",SizingMethod='auto expand')";
    } else {
        var canvas = document.createElement('canvas');
        if (!p.oImage) {
            canvas.oImage = new Image();
            canvas.oImage.src = p.src;
        } else {
            canvas.oImage = p.oImage;
        }

        canvas.style.width = canvas.width = Math.abs(costheta * canvas.oImage.width) + Math.abs(sintheta * canvas.oImage.height);
        canvas.style.height = canvas.height = Math.abs(costheta * canvas.oImage.height) + Math.abs(sintheta * canvas.oImage.width);

        var context = canvas.getContext('2d');
        context.save();
        if (rotation <= Math.PI / 2) {
            context.translate(sintheta * canvas.oImage.height, 0);
        } else if (rotation <= Math.PI) {
            context.translate(canvas.width, -costheta * canvas.oImage.height);
        } else if (rotation <= 1.5 * Math.PI) {
            context.translate(-costheta * canvas.oImage.width, canvas.height);
        } else {
            context.translate(0, -sintheta * canvas.oImage.width);
        }
        context.rotate(rotation);
        context.drawImage(canvas.oImage, 0, 0, canvas.oImage.width, canvas.oImage.height);
        context.restore();
    }
    canvas.id = p.id;
    canvas.angle = p.angle;
    p.parentNode.replaceChild(canvas, p);
}

function rotateRight(id, angle) {
    rotate(id, angle == undefined ? 90 : angle);
}

function rotateLeft(id, angle) {
    rotate(id, angle == undefined ? -90 : -angle);
}


//add by lcc 
//跑腿订单的调度

function showorderexpress(state, _latlng, deliverid, orderid, username, address, shopname, sectionid) {
    map.clearOverlays();
    markerlist.length = 0;
    $("#hfexpressororder").val("1");//代表跑腿订单
    $("#hforderid").val(orderid);
    $("#hfordersection").val(sectionid);
    $("#hfstate").val(state);



    var jsonlatlng = eval("(" + $("#latlng_" + orderid).html() + ")");
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

    getexpressorderinfo(orderid);

    switch (state) {
        case 3:
        case 5:
        case 6:
            {
                //这几个状态只显示用户位置
                hideload_super('dddd');
                return;
                break;
            }
      
        case 0:
            {
                map.panTo(shoppoint);
                $("#hforderuserlatlng").val(jsonlatlng.slat + "," + jsonlatlng.slng);
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
                        var d_list = eval("(" + msg + ")");
                        var ddl_d = document.getElementById("ddldeliver");
                        ddl_d.options.length = 0;
                        var see_didsstr = "o_";
                        for (var i = 0; i < d_list.length; i++) {
                            if (i < 3) {   //前3个添加标注
                                if (d_list[i].distance != "99999999") {


                                    var d_point = new BMap.Point(d_list[i].Lng, d_list[i].Lat); // 创建点坐标
                                    var d_icon = new BMap.Icon("images/mapmarker/marker" + (parseInt(i + 1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                                    var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                                    var deliverlabel = new BMap.Label(d_list[i].Name + "(" + d_list[i].OrderNum + ")", { offset: new BMap.Size(-28, -22) });
                                    deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                                    d_marker.setLabel(deliverlabel);
                                    map.addOverlay(d_marker);
                                    see_didsstr += d_list[i].DataId + ",";
                                    markerlist.push(d_marker);
                                    //初始化右边配送员信息
                                    if (i == 0) {
                                        $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                }
                            }

                            ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                        }
                        hideload_super('dddd');
                        getsubsort();
                        var mydids = see_didsstr.replace(/,$/, "");
                        $("#hf_curr_dids").val(mydids);
                    }
                })
                break;
            }


        case 2:
        case 4:
        case 1:
           
            var url = "../ajax/GetDeliverModel.aspx";
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
                        var d_icon = new BMap.Icon("images/mapmarker/marker" + (parseInt(i + 1)) + ".png", new BMap.Size(20, 34), { anchor: new BMap.Size(10, 0) });
                        var d_marker = new BMap.Marker(d_point, { icon: d_icon });

                        var deliverlabel = new BMap.Label("配送员" + deli_latlng.d_name + "(" + deli_latlng.ordernum + ")", { offset: new BMap.Size(-28, -22) });
                        deliverlabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                        d_marker.setLabel(deliverlabel);
                        map.addOverlay(d_marker);


                        map.addOverlay(d_marker);

                        map.panTo(d_point);
                        $("#hf_curr_dids").val("o_" + deliverid);

                        markerlist.push(d_marker);

                        var sname = $("#ddlsection option:selected").text();
                        var url = "../ajax/GetDeliverListbySpan.aspx";
                        var hfcityid = $("#hfcityid").val();
                        var para = "sname=" + escape(sname) + "&t=" + new Date().getTime() + "&lat=" + jsonlatlng.slat + "&lng=" + jsonlatlng.slng + "&cid=" + hfcityid;
                        jQuery.ajax(
                        {
                            type: "get",
                            url: url,
                            data: para,
                            success: function (msg) {
                                var d_list = eval("(" + msg + ")");
                                var ddl_d = document.getElementById("ddldeliver");
                                ddl_d.options.length = 0;
                                var see_didsstr = "o_";
                                for (var i = 0; i < d_list.length; i++) {

                                    //初始化右边配送员信息
                                    if (i == 0) {
                                        $("#hfdid_hh").val(d_list[0].DataId);
                                    }
                                    ddl_d.options.add(new Option(d_list[i].Name, d_list[i].DataId));
                                }

                            }
                        })

                    }
                })
            break;
    }
    $("html,body").animate({ scrollTop: 0 }, 1000);
}


///返回订单信息,和进度信息
function getexpressorderinfo(orderid) {
    var url = "ajax/expressorderinfo.aspx";
    var para = "orderid=" + orderid + "&t=" + new Date().getTime();
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function (msg) {
            var data = eval("(" + msg + ")");
            $("#tborderinfo").val(data.messagebody);
        }
    })
}


function showDeliverBox(deleiverid, delivermarker) {
    var url = "../ajax/GetDeliverModel.aspx";
    var para = "id=" + deleiverid + "&t=" + new Date().getTime();
    jQuery.ajax(
        {
            type: "post",
            url: url,
            data: para,
            success: function (msg) {
                //以配送员为中心
                var deli_latlng = eval("(" + msg + ")");

                var d_point = new BMap.Point(deli_latlng.lng, deli_latlng.lat); // 创建点坐标

                $("#hidLat").val(deli_latlng.lat);
                $("#hidLng").val(deli_latlng.lng);


                var opts = {
                    width: 350,     // 信息窗口宽度  
                    height: 100    // 信息窗口高度
                }

                var winhtml = "<div>";
                winhtml += ' <table width="330px" border="0" cellpadding="0" cellspacing="0" class="delivertipbox">'
                winhtml += '<tr><td>姓名：' + deli_latlng.d_name + '</td><td>电话：' + deli_latlng.Phone + '</td></tr>'
                winhtml += '<tr><td>车况：' + deli_latlng.carstate + '<img id=\"theimage\" border=\"0\" src=\"direction.jpg\" onload=\"rotateRight(\'theimage\',' + deli_latlng.direction + ');\" /></td><td>订单：' + deli_latlng.ordernum + '</td></tr>'

                winhtml += ' <tr><td colspan="2">商家名称：' + deli_latlng.TogoName + '</td></tr>'
                winhtml += ' <tr><td colspan="2">商家地址：' + deli_latlng.TogoAddress + '</td></tr>'
                winhtml += ' <tr><td colspan="2">客户地址：' + deli_latlng.AddressText + '</td></tr>'


                winhtml += " </table></div>";

                var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象

                delivermarker.openInfoWindow(infoWindow)

                map.panTo(d_point);
            }
        })
}