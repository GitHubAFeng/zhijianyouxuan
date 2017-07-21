/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

function ajaxlogin() {
    var username = document.getElementById("jtbUserName").value;
    var password = document.getElementById("jtbPassword").value;
    var foot_cbMemary = document.getElementById("jcbMemary").checked;
    var flag = 0;
    if (foot_cbMemary == true) {
        flag = 1;
    }
    else {
        flag = 0;
    }

    if (username == "") {
        alert("请输入用户名!");
        return false;
    }
    if (password == "") {
        alert("请输入密码!");
        return false;
    }
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/AjaxLogin.aspx",
        data: "name=" + escape(username) + "&t=" + new Date().getTime() + "&password=" + password + "&flag=" + flag,
        success: function(msg) {
            if (msg == "0") {
                //登录失败
                alert("用户名或者密码错误！");
            }
            else {
                window.location.href = window.location;
            }
        }
    })
}

//隐藏弹出框
function jhidewindow() {
    $("#windownbg").remove();
    $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
}

function goregister() {
    var url = "register.aspx";
    var togoid = $("#hftid").val();
    if (togoid != undefined && togoid != "") {
        url += "?shopid=" + togoid;
    }
    window.location = url;
}



//操作cookie
//value:不为空时,表示设置cookie
//value:为空时,表示得到这个名字的cookie
// add by jijunjian 2010-03-26;
function handlecookie(name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie 
        options = options || {};
        if (value === null || value == "") {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            }
            else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE 
        }
        var path = options.path ? '; path=' + options.path : '';
        var domain = options.domain ? '; domain=' + options.domain : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    }
    else { // only name given, get cookie 
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want? 
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

function $D(ele) {
    return document.getElementById(ele);
}

//获取参与者
function getJoin() {
    var groupid = document.getElementById("groupid").value;
    var userflag = document.getElementById("hfloader").value;

    if (groupid == "") {
        alert("服务器繁忙,请稍后再试");
        window.location = "index.aspx";
    }
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/GetJoinPeople.aspx",
        data: "gid=" + groupid + "&t=" + new Date().getTime() + "&type=1",
        success: function(msg) {
            if (msg != "-1") {
                document.getElementById("divjoinpeople").innerHTML = msg;
                if (userflag == "1") {
                    jQuery(".jflag").removeClass("jflag");
                }
            }
            else {
                alert("error");
            }
        }
    })

}
//获取餐品信息
function getFoods() {
    var groupid = document.getElementById("groupid").value;
    if (groupid == "") {
        alert("服务器繁忙,请稍后再试");
        window.location = "index.aspx";
    }
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/GetGfood.aspx",
        data: "gid=" + groupid + "&t=" + new Date().getTime() + "&type=1",
        success: function(msg) {
            if (msg != "-1") {
                var temp = msg.split('@');
                document.getElementById("divFoods").innerHTML = temp[0];
                document.getElementById("strongTotal").innerHTML = "订单合计：已选餐品" + temp[1] + "份，共计" + temp[2] + "元"
            }
            else {
                alert("error");
            }
        }
    })

}

///发起者删除参与者
function deletethisuser(customerid) {
    if (!confirm("确实要删除这个参与者吗?")) {
        return;
    }
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/ajaxDeleteUser.aspx",
        data: "cid=" + customerid + "&t=" + new Date().getTime() + "&type=1",
        success: function(msg) {
            if (msg != "-1") {
                getJoin(); getFoods();
            }
            else {
                alert("error");
            }
        }
    })
}

///浏览器对象
var ABBrowser =
{
    navi: navigator.userAgent.toLowerCase(),
    isIE: function() {
        var A = this;
        return (A.navi.indexOf("msie") != -1) && (A.navi.indexOf("opera") == -1) && (A.navi.indexOf("omniweb") == -1)
    },
    getBody: function() {
        return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
    },
    getScrollTop: function() {
        return this.isIE() ? this.getBody().scrollTop : window.pageYOffset
    },
    getScrollLeft: function() {
        return this.isIE() ? this.getBody().scrollLeft : window.pageXOffset
    },
    getAvailableHeight: function() {
        return this.getBody().offsetHeight > this.getBody().scrollHeight ? this.getBody().offsetHeight : this.getBody().scrollHeight
    },
    getAvailableWidth: function() {
        return this.getBody().offsetWidth > this.getBody().scrollWidth ? this.getBody().offsetWidth : this.getBody().scrollWidth
    },
    getViewWidth: function() {
        return self.innerWidth || (document.documentElement.clientWidth || document.body.clientWidth)
    },
    getViewHeight: function() {
        return self.innerHeight || (document.documentElement.clientHeight || document.body.clientHeight)
    },
    getPointerPositionInDocument: function(C) {
        var B = C;
        var A = B.pageX || (B.clientX + ABBrowser.getBody().scrollLeft);
        var D = B.pageY || (B.clientY + ABBrowser.getBody().scrollTop);
        return { "x": A, "y": D }
    },
    getElementPosition: function(C) {
        if (typeof C.offsetParent != "undefined") {
            for (var B = 0, A = 0; C; C = C.offsetParent) {
                B += C.offsetLeft;
                A += C.offsetTop;
            }
            return { "x": B, "y": A }
        }
        else {
            return { "x": B, "y": A }
        }
    }
};

function divnone() {
    var _div = document.getElementById("address_drop");
    if (_div.style.display == 'block') {
        _div.style.display = 'none';
        return;
    }
    else {
        return;
    }
}

//不需要弹出选择区域的方法
function hidenSelectBuild() {
    var selectbuild = document.getElementById("selectBuild");
    if (selectbuild == null) {
        return;
    }
    else {
        selectbuild.style.display = "none";
    }
}

//非加盟餐馆信息区别处理

function setNoJoined() {
    /// 1 ,没有大家一起订
    /// 2 ,非加盟图片 换样式 ： jshop1
    /// 3 ,购物车。
    var jtflag = document.getElementById("hftogoGrade");
    var jagouporder = document.getElementById("jagouporder");
    var ddtype = document.getElementById("ddtype");
    var carttail = document.getElementById("cartHead");
    if (jtflag.value == "1" || jtflag.value == 1 || jtflag.value == "2" || jtflag.value == 2) {
        return;
    }
    //1
    if (jagouporder != null) {
        jagouporder.style.display = "none";
    }
    //2
    if (ddtype != null) {
        ddtype.className = "jshop1";
    }
    //3
    if (carttail != null) {
        $("carttail").html("<td style='text-align:center'>此商户不支持网上下订单,请对照“我的外卖盒”的餐品，拨打商家电话订购.<img src='Images/remove.jpg' onclick=\"deleteallcart();\" width=\"90\" height=\"35\" /></td><td></td>");
    }

}

//弹出区域搜索
function jgetkey() {
    var key = document.getElementById("bkey").value;
    if (key != "") {
        getByKey(key, 1);
        return false;
    }
    return false;
}

// js 打印
function Printpart(id_str)//id-str 内容中的id
{
    var el = document.getElementById(id_str);
    var ptimes = parseInt(parseInt(document.getElementById("lbPrintTime").innerHTML) + 1) + "";
    var id = document.getElementById("hfid").value;
    var targethtml = el.innerHTML;
    var doc = document.body;
    var oldbody = doc.innerHTML;
    doc.innerHTML = "";
    // doc.innerHTML += "<link href='css/common.css' rel='stylesheet' type='text/css' />";
    // doc.innerHTML += "<link href='css/shop.css' rel='stylesheet' type='text/css' />";
    doc.innerHTML += targethtml;

    window.print();
    doc.innerHTML = oldbody;
    //更改打印次数
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/updateTime.aspx",
        data: "id=" + id + "&t=" + new Date().getTime() + "&times=" + ptimes,
        success: function(msg) {
            if (msg != "-1") {
                getJoin(); getFoods();
            }
            else {
                alert("error");
            }
        }
    })
}

//设置可见

function setDisplay(ele) {
    var _ele = G(ele);
    if (_ele) {
        _ele.style.display = "";
    }
}

//设置不可见

function setNone(ele) {
    var _ele = G(ele);
    if (_ele) {
        _ele.style.display = "none";
    }
}

function G(ele) {
    return document.getElementById(ele);
}


function $j(ele) {
    return document.getElementById(ele);
}

//设置登录框位置
function locationlogin() {
    var jw = "divmylogin";
    var obj = $j("lilogin");
    setDisplay(jw);
    ShowTip(obj, jw, -43, 28);
}
//隐藏
function closelogin() {
    var jw = $j("divmylogin");
    if (jw.style.display == '' || jw.style.display == 'block' || jw.style.display == 'inline') {
        setNone("divmylogin");
    }
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function sShowTip(obj, objdiv, addx, addy) {
    var x = getposOffset_top(obj, 'left');
    var y = getposOffset_top(obj, 'top');

    var div_obj = document.getElementById(objdiv);
    div_obj.style.left = (x + addx) + 'px';
    div_obj.style.top = (y + addy) + 'px';
    div_obj.style.display = "inline";
}

//获取偏移量
function getposOffset_top(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;
}

function showhot(dataid, x, y) {
    var obj = document.getElementById("bt_" + dataid);
    var objdiv = "divhot_" + dataid;
    $("#" + objdiv).show();
    obj.className = "waimai_button_hover";
}
function hidehot(dataid) {
    var obj = document.getElementById("bt_" + dataid);
    var objdiv = "divhot_" + dataid;
    $("#" + objdiv).hide();
    obj.className = "waimai_button";
}


//直接订购
function goorder() {
    var togoid = $D("hidTogoId").value;
    var hfBuildingID = $D("hfBuildingID").value;
    var url = "ShowTogofix.aspx?id=" + togoid;
    if (hfBuildingID != "" && hfBuildingID != "0") {
        url += "&bid=" + hfBuildingID;
    }
    //生成cookie
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/buildcookie.aspx",
        data: "t=" + new Date().getTime() + "",
        async: false,
        success: function(msg) {
            url += "&uid=" + msg;
            window.location = url;
        }
    })
}

// 区域cookie,
// 最多可有3个
// 名称  bnamefix = "我,我"
//  编号 bidfix = "1,1";
//先进先出
function buildcookie(id, name) {
    var cookiename = "bnamefix";
    var cookieid = "bidfix";
    var names = handlecookie(cookiename);
    var ids = handlecookie(cookieid);

    if (id == "")
        return;
    //第一个
    if (names == null || names == "" || ids == null || ids == "") {
        handlecookie(cookiename, name, { expires: 1, path: "/", secure: false });
        handlecookie(cookieid, id, { expires: 1, path: "/", secure: false });
    }
    else {
        var namearray = names.split(',');
        var idarray = ids.split(',');
        //存在相同的直接返回
        for (var i = 0; i < idarray.length; i++) {
            if (id == idarray[i]) {
                return;
            }
        }

        //大于3,替换最早的
        if (idarray.length >= 3) {
            namearray[0] = name;
            idarray[0] = id;
            names = ArrayToString(namearray);
            ids = ArrayToString(idarray);
            handlecookie(cookiename, names, { expires: 1, path: "/", secure: false });
            handlecookie(cookieid, ids, { expires: 1, path: "/", secure: false });
        }
        //少于3添加
        else {
            names += "," + name;
            ids += "," + id;
            handlecookie(cookiename, names, { expires: 1, path: "/", secure: false });
            handlecookie(cookieid, ids, { expires: 1, path: "/", secure: false });
        }
    }
}

// 区域cookie,
// 最多可有3个
// 名称  tnamefix = "我,我"
//  编号 tidfix = "1,1";
//先进先出
function togocookie(id, name) {
    var cookiename = "tnamefix";
    var cookieid = "tidfix";
    var names = handlecookie(cookiename);
    var ids = handlecookie(cookieid);
    //第一个
    if (names == null || names == "" || ids == null || ids == "") {
        handlecookie(cookiename, name, { expires: 1, path: "/", secure: false });
        handlecookie(cookieid, id, { expires: 1, path: "/", secure: false });
    }
    else {
        var namearray = names.split(',');
        var idarray = ids.split(',');
        //存在相同的直接返回
        for (var i = 0; i < idarray.length; i++) {
            if (id == idarray[i]) {
                return;
            }
        }

        //大于3,替换最早的
        if (idarray.length >= 3) {
            namearray[0] = name;
            idarray[0] = id;
            names = ArrayToString(namearray);
            ids = ArrayToString(idarray);
            handlecookie(cookiename, names, { expires: 1, path: "/", secure: false });
            handlecookie(cookieid, ids, { expires: 1, path: "/", secure: false });
        }
        //少于3添加
        else {
            names += "," + name;
            ids += "," + id;
            handlecookie(cookiename, names, { expires: 1, path: "/", secure: false });
            handlecookie(cookieid, ids, { expires: 1, path: "/", secure: false });
        }
    }
}


function ArrayToString(arr) {
    var temp = "";
    for (var i = 0; i < arr.length; i++) {
        temp += arr[i];
        if (i != arr.length - 1)
            temp += ",";
    }
    temp = temp.toString().replace(/,/g, ",");
    return temp;
}

//店铺，楼cookie
function getcookie() {
    var bcookiename = "bnamefix";
    var bcookieid = "bidfix";
    var bnames = handlecookie(bcookiename);
    var bids = handlecookie(bcookieid);
    var bcookies = "";
    var myaddress = "";
    //地址
    if (bnames != null && bids != null && bnames != "" && bids != "") {
        var namearray = bnames.split(',');
        var idarray = bids.split(",");
        for (var i = 0; i < idarray.length; i++) {
            bcookies += "<span style='display:block;color:#67B32D'><a href='waimaijie.aspx?id=" + idarray[i] + "'>" + namearray[i] + "</a><span>";
            myaddress += "<a  onmouseout=\"mouseOutDiv(this)\" onmouseover=\"mouseOverAddr(this)\" href='waimaijie.aspx?id=" + idarray[i] + "' >" + namearray[i] + "</a>";
        }
        myaddress += " <a onclick=\"SetDivDisplayType('address_drop','none')\" href=\"javascript:void(0)\" class=\"off\">关闭</a>"
        $("#div_address").html(bcookies);
        $("#ssss").html(myaddress);
    }

    var tcookies = "";
    var tcookiename = "tnamefix";
    var tcookieid = "tidfix";
    var tnames = handlecookie(tcookiename);
    var tids = handlecookie(tcookieid);

    //商家
    if (tnames != null && tids != null && tnames != "" && tids != "") {
        var namearray = tnames.split(',');
        var idarray = tids.split(",");
        for (var i = 0; i < idarray.length; i++) {
            tcookies += "<span style='display:block;color:#67B32D'><a href='ShowTogo.aspx?id=" + idarray[i] + "'>" + namearray[i] + "</a><span>";
        }
        $("#div_togo").html(tcookies);
    }
    //选择框下的曾用地址
}

///获取列表页面距离
function getdistance() {
    //区域
    var blat = $("#hfblat").val() + "";
    var blng = $("#hfblng").val() + "";
    //商家
    var tlat = $("#hfalllat").val() + "";
    var tlng = $("#hfalllng").val() + "";
    var togoids = $("#hfallids").val() + "";
    if (blat == "" || tlat == "") {
        //no to do
    }
    else {
        var lats = tlat.split(',');
        var lngs = tlng.split(',');
        var ids = togoids.split(',');

        for (var i = 0; i < lats.length; i++) {
            if (lats[i] != "0") {
                var latlng_array = new GLatLng(blat, blng);
                var latLng = new GLatLng(lats[i], lngs[i]);
                var distance = mTokm(latLng.distanceFrom(latlng_array));
                $("#jjm_" + ids[i]).html(distance);
            }
        }
    }
}

//米转化成公里
function mTokm(num) {
    var km = num / 1000;
    return double_round(km, 1);
}

function double_round(Dight, How) {
    return Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
}

function gourl(url) {
    window.location = url;
}

function settable(name, cursel, n, css, defaut) {
    for (i = 1; i <= n; i++) {
        var menu = document.getElementById(name + i);
        if (i == cursel) {
            menu.className = css;
            $("#" + name + "_div" + i).show();
        }
        else {
            if (typeof defaut != 'undefined') {
                menu.className = defaut;
            }
            else {
                menu.className = "";
            }
            $("#" + name + "_div" + i).hide();
        }
    }
}

function orderflow() {
    var speed = 50;
    var FGDemo1_111 = document.getElementById('Cust1');
    var FGDemo1_211 = document.getElementById('p_2');
    var FGDemo1_311 = document.getElementById('p_3');
    var f2html1 = FGDemo1_211.innerHTML;
    //FGDemo1_31.innerHTML = f2html;
    jQuery("#p_3").html(f2html1);
    function Marquee211() {
        if (FGDemo1_311.offsetHeight - FGDemo1_111.scrollTop <= 0)
            FGDemo1_111.scrollTop -= FGDemo1_211.offsetHeight
        else {
            FGDemo1_111.scrollTop++
        }
    }
    var MyMar211 = setInterval(Marquee211, speed)
    FGDemo1_111.onmouseover = function() { clearInterval(MyMar211) }
    FGDemo1_111.onmouseout = function() { MyMar211 = setInterval(Marquee211, speed) }

    //兑换

}


function j_showbuild() {

    var windownbg = "<div style=\"height: 2183px; opacity: 0.5; z-index: 1001; display: none;  width:100%\" id=\"windownbg\"></div>";
    $("body").append(windownbg);

    var bg = $("#windownbg");
    var bgwidth = parseInt(screen.width) - 17;
    bg.css("width", bgwidth + "px");
    bg.show();
    getbuild(1, -1, -1, -1, 1, -1, 1)
    var height = 320;
    var width = 700;
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#tabpopup1").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#tabpopup1").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#tabpopup1").show();
}

// type 1,2,3分别表示口味,速度,服务
// index表示第几个星
function setxin(type, index) {

    switch (type) {
        case 1:
            var obj = $(".fpic");
            var leng = obj.length;
            for (var i = 0; i < index; i++) {
                obj[i].src = "images/easy_xingxing.jpg";
            }
            for (var i = index; i < 5; i++) {
                obj[i].src = "images/easy_xingxing2.jpg";
            }
            $("#hff").val(index);
            break;
        case 2:
            var obj = $(".spic");
            var leng = obj.length;
            for (var i = 0; i < index; i++) {
                obj[i].src = "images/easy_xingxing.jpg";
            }
            for (var i = index; i < 5; i++) {
                obj[i].src = "images/easy_xingxing2.jpg";
            }
            $("#hfs").val(index);
            break;
        case 3:
            var obj = $(".vpic");
            var leng = obj.length;
            for (var i = 0; i < index; i++) {
                obj[i].src = "images/easy_xingxing.jpg";
            }
            for (var i = index; i < 5; i++) {
                obj[i].src = "images/easy_xingxing2.jpg";
            }
            $("#hfv").val(index);
            break;
    }
}


function showloadx(msg, time, ele) {

    var logindiv = new String;
    logindiv = "<div id=\"loading-mask\">";
    logindiv += "<p class=\"loader\" id=\"loading_mask_loader\">";
    logindiv += "<img src=\"/images/loading.gif\" /><br />";
    logindiv += "请稍后...<br></p></div>";

    $("body").append(logindiv);
    var width = 125;
    var height = 96;
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading-mask").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading-mask").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };

    var moveX = 0, moveY = 0, moveTop, moveLeft = 0, moveable = false;
    if (_version == 6.0) {
        moveTop = est;
    } else {
        moveTop = 0;
    }

    //定时器 事件
    var closeWindown = function() {
        $("#loading-mask").fadeOut("slow", function() { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {

    }
    else {
        setTimeout(closeWindown, time);
    }
    $("#" + ele).hide();
}

//隐藏加载中，设置按钮有郊
function hideload(btid) {
    $("#loading-mask").remove();
    $("#" + btid).show();
}


function showloadfix(msg, time, ele) {
    var width = 125;
    var height = 96;
    var logindiv = new String;
    logindiv = "<div id=\"loading_mask_j\">";
    logindiv += "<div class=\"loader\" id=\"loading_mask_loader\"><a href='javascript:hideloadfix(\"" + ele + "\")' style='float:right;padding-right:5px;'>关闭</a>";
    logindiv += "<div class=\"clear\"></div>";
    logindiv += "<p><img src=\"images/loading.gif\" /><br />";
    logindiv += "请稍后...<br></p></div>";
    $("body").append(logindiv);
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading_mask_j").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading_mask_j").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#loading_mask_j").show();
    $('.' + ele).attr('disabled', true);
}

//隐藏加载中，设置按钮有效
function hideloadfix(btid) {
    $("#loading_mask_j").remove();
    $('.' + btid).removeAttr("disabled");
}


//首页就直接弹出,如果不是首页就转到首页去再显示
function showtab(tag) {
    var objpath = window.location.pathname.toLowerCase();
    if (objpath == "/index.aspx" || objpath == "/" || objpath == "") {//在首页

        sShowTip(tag, "tabpopup1", -566, 30);
        getbuild(1, 0, -1, -1, 1, -1, 1);
    }
    else {
        window.location = "/index.aspx?show=1"
    }
}

///获取url参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        returnValue = "";
    }

    return returnValue;
}



/*弹出选择写字楼的table*/
function showBuild_Box(content, width, height, showbg) {
    $("#build-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 999901;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
        simpleWindown_html += "<div id=\"build-box\" style=' display:none'>";
        simpleWindown_html += "<div id=\"build-content\"><img src='http://www.ihangjing.com/images/loading.gif' class='loading' /></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;
    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html(html);
                }
            });
            break;
        case "iframe":
            $.ajax({
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + parseInt(height) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
                }
            });
    }
    if (showbg == "true") {
        $("#windownbg").show();
    }
    else {
        $("#windownbg").remove();
    };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度  
    if (height >= 527) {
        $("#build-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#build-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        alert((parseInt((ch) / 2) + est));
        $("#build-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#build-box").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#build-box").show();
}

function jshow_box() {
    showBuild_Box('url:get?/buildtable_1.aspx', 702, 450, 'true');
    getbuild(1, 0, -1, -1, 1, -1, 1);
}


// myclass 以哪个元素定位
function localdom(myclass, x, y) {
    $("." + myclass).each(function() {
        var b = this.getAttribute("sub_id");
        var g = this;
        var d = document.getElementById(b);
        document.getElementsByTagName("body")[0].appendChild(d);
        var a = false;
        var e = function() {
            var h = $(g).offset();
            $(d).css({ top: h.top + g.offsetHeight + y, left: h.left + x });
            $(d).show();
            a = true
        };
        var f = function() {
            if (a == false) {
                $(d).hide()
            }
        };
        var c = function() {
            a = false; window.setTimeout(f, 200)
        };

        $(this).hover(e, c);
        $(d).hover(e, c)
    })
}

function $D(ele) {
    return document.getElementById(ele);
}

//导航高亮
function initnav(index) {
    $(".nav_left ul li").removeClass("nav_hover");
    $("#nav_li" + index).addClass("nav_hover");
}



//显示商家派送的写字楼
function showaccessbuild(tid) {
    var windownbg = "<div style=\"height: 2183px; opacity: 0.5; z-index: 1001; display: none;  width:100%\" id=\"windownbg\"></div>";
    $("body").append(windownbg);
    var bg = $("#windownbg");
    var bgwidth = parseInt(screen.width) - 17;
    bg.css("width", bgwidth + "px");
    bg.show();
    getBySfix(tid);

    var height = 320;
    var width = 700;
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#tabpopup2").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#tabpopup2").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#tabpopup2").show();
}

//获取商家配送的区域.
function getBySfix(togoid) {
    var url = "ajax/TogoBuilding.aspx";
    var para = "fuc=selectbuild&s=" + togoid;
    setBuild(url, para);
}


function setBuild(url, para) {
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            $("#div_sectionfix").html(msg);
        }
    })
}

//关闭
function closedivfix() {
    document.getElementById("tabpopup2").style.display = "none";
    $("#windownbg").remove();
}

///跳转到商家页面
function InitAddress1(bname, bid, tid, tuan) {
    if (tuan == 1)//等1表示预订商家
    {
        window.location = "TabShow.aspx?id=" + tid + "&bid=" + bid;
    }
    else {
        window.location = "ShowTogo.aspx?id=" + tid + "&bid=" + bid;
    }
}

// onkeypress="return only_num(event)"
function only_num(e) {
    ee = e ? e : window.event ? event : null;
    var keyNum = ee.keyCode == 0 ? ee.which : ee.keyCode;
    if ((keyNum >= 48 && keyNum <= 57) || keyNum == 8) {
        return true;
    }
    else {
        return false;
    }
}



//js登录
function ajax_login() {
    var username = document.getElementById("tbemail").value;
    var password = document.getElementById("tbpassword").value;
    var foot_cbMemary = document.getElementById("jcbMemary").checked;
    var flag = 0;
    if (foot_cbMemary == true) {
        flag = 1;
    }
    else {
        flag = 0;
    }

    if (username == "") {
        alert("请输入用户名!");
        return false;
    }
    if (password == "") {
        alert("请输入密码!");
        return false;
    }
    showloadx("", "", "dd");
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/AjaxLogin.aspx",
        data: "name=" + escape(username) + "&t=" + new Date().getTime() + "&password=" + password + "&flag=" + flag,
        success: function(msg) {
            if (msg == "0") {
                //登录失败
                alert("用户名或者密码错误！");
                hideload("dddd");
                return;
            }
            var type = request("type");
            if (type != "") {
                var tid = request("id");
                var url = "OrderDetail.aspx";
                if (tid != "") {
                    url += "?id=" + tid;
                }
                window.location = url;
                return;
            }
            //
            var pp = request("returnurl");
            if (pp != "") {
                window.location = pp;
                return;
            }
            window.location = "index.aspx";
        }
    })
}

//index logo
function ajax_index_login(name, pwd) {
    var username = $.trim(document.getElementById(name).value);
    var password = $.trim(document.getElementById(pwd).value);
    var flag = 1;
    if (username == "") {
        alert("请输入用户名!");
        return false;
    }
    if (password == "") {
        alert("请输入密码!");
        return false;
    }
    showloadx("", "", "dd");
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/AjaxLogin.aspx",
        data: "name=" + escape(username) + "&t=" + new Date().getTime() + "&password=" + password + "&flag=" + flag,
        success: function(msg) {
            if (msg == "0") {
                //登录失败
                alert("用户名或者密码错误！");
                hideload("dddd");
                return;
            }

            window.location = "index.aspx";
        }
    })
}

///回车事件
function enterIn(evt, callback) {
    if (evt.keyCode == 13 || evt.which == 13) {
        if (navigator.userAgent.indexOf("MSIE") > 0) {
            callback();
            event.returnValue = false;
        }
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
            callback();
            return false;
        }
        return false;
    }
    else {

    }
}

function ad_478_174() {
    $('.index_ad_478_174 .bd').kxbdSuperMarquee({
        time: 5,
        distance: 478,
        duration: 30,
        direction: 'left',
        navId: '.index_ad_478_174 .ctrl'
    });
}


///只是设置显示div
function settablefix(name, cursel, n) {

    for (i = 1; i <= n; i++) {
        if (i == cursel) {
            $("#" + name + "_div" + i).show();
        }
        else {
            $("#" + name + "_div" + i).hide();
        }

    }
}


//显示首页公告框
function j_meaasge_bog() {
    var url = "/meaagefreme.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/meaagefreme.aspx";
    }
    showfreme_Box('url:get?' + url, 478, 300, 'true');
}

//关闭公告框
function close_j_meaasge_bog() {
    $("#build-box").remove();
    $("#windownbg").remove();
}


/*弹出加载页面*/
function showfreme_Box(content, width, height, showbg) {
    $("#build-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 100;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
        simpleWindown_html += "<div id=\"build-box\" style=' display:none'>";
        simpleWindown_html += "<div id=\"build-content\"><img src='http://www.ihangjing.com/images/loading.gif' class='loading' /></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;
    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html(html);
                }
            });
            break;
        case "iframe":
            $.ajax({
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + parseInt(height) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
                }
            });
    }
    if (showbg == "true") {
        $("#windownbg").show();
    }
    else {
        $("#windownbg").remove();
    };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度  
    if (height >= 527) {
        $("#build-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#build-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#build-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#build-box").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#build-box").show();
}

function j_ShowWindow(msg) {
    var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
    innerHTML += "<div style='float:left;font-size:12px;color:#fff'>订单提醒</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='Images/window_close.gif' alt='关闭窗口' /> </a>";
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


// myclass 以哪个元素定位(点击事件)
function localdom_click(myclass, x, y) {
    $("." + myclass).each(function() {
        var b = this.getAttribute("sub_id");
        var g = this;
        var d = document.getElementById(b);
        var h = $(g).offset();
        $(d).css({ top: h.top + g.offsetHeight + y, left: h.left + x });
        $(d).show();


    })
}

//显示加载器  html5上用
function showLoader() {
    $("#phone_loader").remove();
    $("body").append("<div class=\"ui-loader ui-corner-all ui-body-a ui-loader-verbose\" id='phone_loader'><span class=\"ui-icon ui-icon-loading\"></span><h1>加载中...</h1></div>");
    $("#phone_loader").show();
}

//隐藏加载器.for jQuery Mobile 1.2.0   html5上用 
function hideLoader() {
    $("#phone_loader").hide();
}

//开始提交数据：mycontainter 是一个Class,表示中只验证这个元素内的控件
function j_submitdata(mycontainter) {
    var comflag = true;
    $("." + mycontainter).find("[reg],[url]:not([reg])").each(function () {
        if ($(this).attr("reg") != undefined) {
            if (!validate($(this))) {
                comflag = false;
                return false;
            }
        }
    });

    if (comflag == false) {
        return false;
    }
    return true;
}


//验证,根据当前对像设置的正则表达式，判断是否通，不通过alert(当前对像的tip属性)
//CanBeNull="n" 表示不可为空，格式按reg 
//CanBeNull="y" 表示可为空，如果不空，格式按reg 
function validate(obj) {
    var reg = new RegExp(obj.attr("reg"));
    var objValue = obj.attr("value");
    var CanBeNull = "";
    if (obj.attr("canbenull") != undefined && obj.attr("canbenull") == "y") {

        CanBeNull = "y"; //可为空，如果不空，才判断
        if (objValue != "") {
            if (!reg.test(objValue)) {
                alert(obj.attr("tip"));
                return false;
            }
            return true;
        }
        else {
            return true;
        }
    }
    else {
        CanBeNull = "n"; //不能为空
        if (!reg.test(objValue)) {
            alert(obj.attr("tip"));
            return false;
        }
        else {

        }
        return true;
    }

}
var Loader =
{
    show: function (btn) {
        if (btn == null || btn == undefined || $(btn).length == 0) return;
        var left = $(btn).offset().left;
        var top = $(btn).offset().top;
        var width = $(btn).outerWidth();
        var height = $(btn).height();
        var opts = {
            lines: 9, // The number of lines to draw
            length: 0, // The length of each line
            width: 10, // The line thickness
            radius: 15, // The radius of the inner circle
            corners: 1, // Corner roundness (0..1)
            rotate: 0, // The rotation offset
            direction: 1, // 1: clockwise, -1: counterclockwise
            color: '#000', // #rgb or #rrggbb or array of colors
            speed: 1, // Rounds per second
            trail: 81, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: '50%', // Top position relative to parent
            left: '50%' // Left position relative to parent
        };
        $('#ajax_spin').remove();
        $('body').append('<div id="ajax_spin" style="position:absolute;background:#FFF;filter:alpha(opacity=30);opacity:0.3"><div id="ajax_spin_inner" style="position:relative;height:50px;"></div></div>');
        $('#ajax_spin').css({
            'top': top,
            'left': left,
            'width': width,
            'height': height
        });
        var target = document.getElementById('ajax_spin_inner');
        var spinner = new Spinner(opts).spin(target);
    },
    hide: function (btn) {
        $('#ajax_spin').remove();
    }
}


/**************微信相关 ui *****************/

var WeUI =
{
    //提示成功
    toastSuccess: function () {
        $('#weui_toast_success').show();
        setTimeout(function () {
            $('#weui_toast_success').hide();
        }, 2000);
    },
    //加载
    showLoading: function () {
        $('#weui_loadingToast').show();
    },

    //加载消失
    hideLoading: function () {
        $('#weui_loadingToast').hide();
    },
    //延时加载消失
    hideLoadingslowly: function () {

        setTimeout(function () {
            $('#weui_loadingToast').hide();
        }, 1000);

    }


}



/***************************/