/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
String.prototype.trim = function () {
    return this.replace(/(\s*$)|(^\s*)/g, '');
}
function inithead(foodcount, username) {
    var ss = document.getElementById("divNologin");
    var mm = document.getElementById("divLoinged");
    $("#lbmyname").html(username);
    if (ss) {
        document.getElementById("divNologin").style.display = "none";
    }
    if (mm) {
        document.getElementById("divLoinged").style.display = "";
    }
    getcookie();
}

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
        success: function (msg) {
            if (msg == "0") {
                //登录失败
                alert("用户名或者密码错误！");
            }
            else {
                var hidTogoId = $("#hidTogoId").val();
                window.location = window.location.href;

            }
        }
    })
}


/*弹出div对话框*/
var showWindown = true; //设置弹出窗是否已经显示
var templateSrc = "";  //设置loading.gif路径
function jtipsWindown(title, content, width, height, drag, time, showbg, cssName) {
    $("#windown-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 999901;background: none repeat scroll 0% 0% rgb(20, 20, 20); \"></div>";
        simpleWindown_html += "<div id=\"windown-box\">";
        simpleWindown_html += "<div id=\"windown-title\"><h2></h2><span id=\"windown-close\"><a href='javascript:'> </a></span></div>";
        simpleWindown_html += "<div id=\"windown-content-border\"><form><div id=\"windown-content\"></div></form></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;

    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "id":
            var logindiv = new String;
            logindiv = "<h1>&nbsp;</h1><h2> &nbsp;</h2>";
            logindiv += "<div class=\"info_div\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            logindiv += "<tr><td class=\"td1\" valign=\"middle\"><span>帐号：</span></td>";
            logindiv += "<td class=\"td2\"><label> <input class=\"logon_text\" type=\"text\" name=\"textfield\" id=\"jtbUserName\" /></label></td>";
            logindiv += "<td rowspan=\"2\" class=\"td3\" style=\"padding-top: 0px;\"> <label>";
            logindiv += " <input class=\"logon_btn\" type=\"submit\" name=\"button\" onclick=' ajaxlogin();return false;' id=\"button\" value=\"提交\" />";
            logindiv += "</label></td></tr><tr><td class=\"td1\"><span>密码：</span></td>";
            logindiv += "<td class=\"td2\"> <label><input type=\"password\" class=\"logon_text\" name=\"textfield2\" id=\"jtbPassword\" />";
            logindiv += "  </label> </td> </tr><tr><td> &nbsp; </td>";
            logindiv += "<td class=\"td2\"> <label style=\"color: #000000\"><input type=\"checkbox\" name=\"checkbox\" id=\"jcbMemary\" />";
            logindiv += "记住登陆状态</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"forgetpassword.aspx\">忘记密码？</a>";
            logindiv += " </td><td class=\"td3\">&nbsp;<a href=\"RegisterByEmail.aspx?ReturnUrl=" + escape(window.location) + "\">免费注册>></a></td> </tr>";
            logindiv += "<tr><td> &nbsp;</td> <td class=\"td22\"></td><td class=\"td3\">&nbsp;</td> </tr> </table></div>";

            $("#windown-content").html(logindiv);
            break;
        case "url":
            var content_array = content.split("?");
            $("#windown-content").ajaxStart(function () {
                $(this).html("<img src='" + templateSrc + "images/loading.gif' class='loading' />");
            });
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function () {
                    $("#windown-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function (html) {
                    $("#windown-content").html(html);
                }
            });
            break;
    }
    $("#windown-title h2").html(title);
    if (showbg == "true") { $("#windownbg").show(); } else { $("#windownbg").remove(); };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度
    $("#windown-box").show();
    if (height >= 527) {
        $("#windown-title").css({ width: (parseInt(width) + 22) + "px" });
        $("#windown-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#windown-title").css({ width: (parseInt(width) + 10) + "px" });
        $("#windown-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#windown-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#windown-box").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    var Drag_ID = document.getElementById("windown-box"), DragHead = document.getElementById("windown-title");

    var moveX = 0, moveY = 0, moveTop, moveLeft = 0, moveable = false;
    if (_version == 6.0) {
        moveTop = est;
    } else {
        moveTop = 0;
    }
    var sw = Drag_ID.scrollWidth, sh = Drag_ID.scrollHeight;
    DragHead.onmouseover = function (e) {
        if (drag == "true") { DragHead.style.cursor = "move"; } else { DragHead.style.cursor = "default"; }
    };
    DragHead.onmousedown = function (e) {
        if (drag == "true") { moveable = true; } else { moveable = false; }
        e = window.event ? window.event : e;
        var ol = Drag_ID.offsetLeft, ot = Drag_ID.offsetTop - moveTop;
        moveX = e.clientX - ol;
        moveY = e.clientY - ot;
        document.onmousemove = function (e) {
            if (moveable) {
                e = window.event ? window.event : e;
                var x = e.clientX - moveX;
                var y = e.clientY - moveY;
                if (x > 0 && (x + sw < cw) && y > 0 && (y + sh < ch)) {
                    Drag_ID.style.left = x + "px";
                    Drag_ID.style.top = parseInt(y + moveTop) + "px";
                    Drag_ID.style.margin = "auto";
                }
            }
        }
        document.onmouseup = function () { moveable = false; };
        Drag_ID.onselectstart = function (e) { return false; }
    }
    $("#windown-content").attr("class", "windown-" + cssName);

    //定时器 事件
    var closeWindown = function () {
        $("#windownbg").remove();
        $("#windown-box").fadeOut("slow", function () { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {
        //关闭事件
        $("#windown-close").click(function () {
            $("#windownbg").remove();
            $("#windown-box").fadeOut("slow", function () { $(this).remove(); });
            //alert("执行回调函数");
        });
    }
    else {
        setTimeout(closeWindown, time);
    }
}

//隐藏弹出框
function jhidewindow() {
    $("#windownbg").remove();
    $("#windown-box").fadeOut("slow", function () { $(this).remove(); });
}

//获取商家信息
function getTogoInstroduce() {
    var togoid = document.getElementById("hidTogoId").value;
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/ajaxGetTogoInfo.aspx",
        data: "togoid=" + togoid + "&type=1",
        success: function (msg) {
            if (msg != "-1") {
                $("#divTogoInfo").html(msg);
            }
        }
    })
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
        success: function (msg) {
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
        success: function (msg) {
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
        success: function (msg) {
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
    isIE: function () {
        var A = this;
        return (A.navi.indexOf("msie") != -1) && (A.navi.indexOf("opera") == -1) && (A.navi.indexOf("omniweb") == -1)
    },
    getBody: function () {
        return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
    },
    getScrollTop: function () {
        return this.isIE() ? this.getBody().scrollTop : window.pageYOffset
    },
    getScrollLeft: function () {
        return this.isIE() ? this.getBody().scrollLeft : window.pageXOffset
    },
    getAvailableHeight: function () {
        return this.getBody().offsetHeight > this.getBody().scrollHeight ? this.getBody().offsetHeight : this.getBody().scrollHeight
    },
    getAvailableWidth: function () {
        return this.getBody().offsetWidth > this.getBody().scrollWidth ? this.getBody().offsetWidth : this.getBody().scrollWidth
    },
    getViewWidth: function () {
        return self.innerWidth || (document.documentElement.clientWidth || document.body.clientWidth)
    },
    getViewHeight: function () {
        return self.innerHeight || (document.documentElement.clientHeight || document.body.clientHeight)
    },
    getPointerPositionInDocument: function (C) {
        var B = C;
        var A = B.pageX || (B.clientX + ABBrowser.getBody().scrollLeft);
        var D = B.pageY || (B.clientY + ABBrowser.getBody().scrollTop);
        return { "x": A, "y": D }
    },
    getElementPosition: function (C) {
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
        // $("#cartHead").html("<td style='text-align:center'>此商户不支持网上下订单,请对照“我的外卖盒”的餐品，拨打商家电话订购.<input class=\"btn send_info_btn\"  value=\"清空餐盒\" type=\"button\" onclick=\"deleteallcart();\"/></td><td></td>");
    }

}

//弹出写字楼搜索
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
        success: function (msg) {
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


function hideimg() {
    $(".dish_img").hide();

}
function showimg() {
    $(".dish_img").show();
}

function showmyli(index) {
    for (var i = 1; i < 4; i++) {
        document.getElementById("tb_" + i).className = index == i ? "tabcheck" : "";
        document.getElementById("tbc_" + i).style.display = index == i ? "" : "none";
    }
}


function ttowrdcheck() {
    var youmsg = document.getElementById("wordqutility");
    var wordlength = document.getElementById("textarea").value.length;
    if (wordlength < 200) {
        youmsg.innerHTML = 200 - wordlength;
    }
    else {
        youmsg.innerHTML = 0;
        tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '1000', 'true', 'text');
    }
}


function checkQ() {
    var wordlength = document.getElementById("textarea").value.length;
    if (wordlength < 1) {
        tipsWindown('提示信息', 'text:字数太少了,请至少输入法1个汉字!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    else {
        if (wordlength > 200) {
            tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '', 'true', 'text');
            return false;
        }
        else {
            return true;
        }
    }
}

function GogoGroup(togoid) {
    var uid = document.getElementById("hidUid").value;
    if (uid == "-1") {
        tipsWindown('提示信息', 'text:登录后才能创建大家一起订!', '250', '150', 'true', '2000', 'true', 'text');
        return;
    }
    window.location = ' GroupEat.aspx?id=' + togoid;
}

function checkRecoform() {
    var name = document.getElementById("tbObectName").value.trim();
    var address = document.getElementById("tbObjectAddress").value.trim();
    if (name.trim() == "") {
        tipsWindown('提示信息', 'text:请输入商家和写字楼名称!', '250', '150', 'true', '3000', 'true', 'text');
        return false;
    }
    if (address.trim() == "") {
        tipsWindown('提示信息', 'text:请输入商家和写字地址!', '250', '150', 'true', '3000', 'true', 'text');
        return false;
    }
    return true;
}

function geterror() {
    if (document.getElementById("tterror1").value == "") {
        tipsWindown('提示信息', 'text:请输入内容!', '250', '150', 'true', '3000', 'true', 'text');
        return false;
    }
    else {
        return true;
    }
}

//用户历史地址，点过的就保存，保存最新3个
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

// 写字楼cookie,
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
            bcookies += "<span style='display:block;color:#67B32D'><a href='shoplist.aspx?id=" + idarray[i] + "'>" + namearray[i] + "</a><span>";
            myaddress += "<a  onmouseout=\"mouseOutDiv(this)\" onmouseover=\"mouseOverAddr(this)\" href='shoplist.aspx?id=" + idarray[i] + "' >" + namearray[i] + "</a>";
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
            tcookies += "<span style='display:block;color:#67B32D'><a href='shop.aspx?id=" + idarray[i] + "'>" + namearray[i] + "</a><span>";
        }
        $("#div_togo").html(tcookies);
    }
    //选择框下的曾用地址
}

function showorder(ele, objdiv, x, y) {
    var obj = document.getElementById(ele);
    sShowTip(obj, objdiv, x, y);
}


function hideorder(ele) {
    $("#" + ele).hide();
}

///显示加载中，防止多次点
///msg现在用，time,也没用，btid按钮的id
function showload(msg, time, btid) {
    var logindiv = new String;
    logindiv = "<div id=\"loading-mask\">";
    logindiv += "<p class=\"loader\" id=\"loading_mask_loader\">";
    logindiv += "<img src=\"images/loading.gif\" /><br />";
    logindiv += "请稍候...</p></div>";

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
    var closeWindown = function () {
        $("#loading-mask").fadeOut("slow", function () { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {

    }
    else {
        setTimeout(closeWindown, time);
    }
    $("#" + btid).hide();
}
//隐藏加载中，设置按钮有郊
function hideload(btid) {
    $("#loading-mask").empty();
    $("#" + btid).show();
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
        return "";
    } else {
        return returnValue;
    }
}




function TopOrderd() {
    location.href = "user/MyOrderList.aspx";

}

function Logintop() {

    // alert(document.getElementById("topname").value);
    var name = document.getElementById("topname").value;


    var password = document.getElementById("toppassword").value;
    if (name == "") {
        alert('请输入用户名!');
        return false;
    }
    else if (password == "") {
        alert('请输入密码!');
        return false;
    }
    else {
        return true;
    }
}

function showborder(evt, classname) {
    evt.className = classname;
}


function Orderb() {
    document.getElementById("divpl").style.display = "none";
    document.getElementById("orderdj").style.display = "block";
    document.getElementById("orderclass").className = "htitle_switch_hover";
    document.getElementById("plclass").className = "";
}

function pl() {

    document.getElementById("divpl").style.display = "block";
    document.getElementById("orderdj").style.display = "none";
    document.getElementById("orderclass").className = "";
    document.getElementById("plclass").className = "htitle_switch_hover";


}

function Giftsb() {
    document.getElementById("lpdiv").style.display = "none";
    document.getElementById("Giftsdiv").style.display = "block";
    document.getElementById("Giftsli").className = "htitle_switch_hover";
    document.getElementById("lpli").className = "";
}

function Lp() {

    document.getElementById("lpdiv").style.display = "block";
    document.getElementById("Giftsdiv").style.display = "none";
    document.getElementById("Giftsli").className = "";
    document.getElementById("lpli").className = "htitle_switch_hover";


}

//此项目js
function orderflow() {
    var speed = 50;
    var FGDemo1_111 = document.getElementById('divpl');
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
    FGDemo1_111.onmouseover = function () { clearInterval(MyMar211) }
    FGDemo1_111.onmouseout = function () { MyMar211 = setInterval(Marquee211, speed) }

}

function gourl(url) {
    window.location = url;
}


function showload_super(msg, time, ele) {
    hideload_super();
    var width = 125;
    var height = 96;
    var logindiv = new String;
    logindiv = "<div id=\"loading_mask_my\">";
    logindiv += "<p class=\"loader\" id=\"loading_mask_loader\">";
    logindiv += "<img src=\"/images/ajax-loader-tr.gif\" /><br />";
    logindiv += "请稍候...<br></p></div>";
    logindiv += "<div id=\"windownbg_notice\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.2);opacity:0.2;z-index: 99\"></div>"
    $("body").append(logindiv);
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("loading_mask_my").show();
}

//隐藏加载中，设置按钮有郊
function hideload_super(btid) {
    $("#loading_mask_my").remove();
    $("#windownbg_notice").remove();
}

//导航高亮
function initnav(index) {
    $("#my_nav_li" + index).addClass("hover");
}
//商家中心导航
function initshopnav(index) {
    $("#shop_nav_li" + index).addClass("cur");
}




///回车事件 onkeydown="return enterIn(event,ajax_login)
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
}

/*弹出加载页面*/
function showfreme_Box(content, width, height, showbg) {

    $("#build-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 650 ? this.height = 650 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + ($(document).height() + height) + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 100;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
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
                error: function () {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function (html) {
                    $("#build-content").html(html);
                }
            });
            break;
        case "iframe":
            $.ajax({
                error: function () {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function (html) {
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
    if (height >= 650) {
        $("#build-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#build-content").css({ width: width + "px", height: height + "px" });
    }

    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        //$("#build-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });

        var _scrollHeight = $(document).scrollTop(); //获取当前窗口距离页面顶部高度
        var _windowHeight = $(window).height(); //获取当前窗口高度
        var _windowWidth = $(window).width(); //获取当前窗口宽度
        var _popupHeight = height;  //获取弹出层高度
        var _popupWeight = width; //获取弹出层宽度

        var _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight + 200;
        // 以下有bug
        // $("#build-box").css({ left: "50%", top: _posiTop + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
        $("#build-box").css({ left: "50%", top: (_scrollHeight + 50) + "px", marginTop: 0 + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });

    } else {
        var _scrollHeight = $(document).scrollTop(); //获取当前窗口距离页面顶部高度
        var _windowHeight = $(window).height(); //获取当前窗口高度
        var _windowWidth = $(window).width(); //获取当前窗口宽度
        var _popupHeight = height;  //获取弹出层高度
        var _popupWeight = width; //获取弹出层宽度

        var _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight + 200;
        // 以下有bug
        // $("#build-box").css({ left: "50%", top: _posiTop + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
        $("#build-box").css({ left: "50%", top: (_scrollHeight + 50) + "px", marginTop: 0 + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });

    };
    $("#build-box").show();
}



//加载页面内容框 pagename 页面名称,width 宽度 ,height 高度
function shwo_j_freme_Box(pagename, width, height) {
    var url = pagename;
    var objpath = window.location + "";
    showfreme_Box('url:get?' + url, width, height, 'true');
}
//关闭加载页面内容框
function close_j_freme_Box() {
    $("#build-box").remove();
    $("#windownbg").remove();

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
    showload_super();
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


///根据分类显示菜品
function showfoodbysort(sortid, sortname) {
    var tid = request("id");
    $(".mysortitem").removeClass("shop_inot_hover").css("color", "#FF6000");
    $("#s_li_ad_" + sortid).addClass("shop_inot_hover").css("color", "blue");


    $("#div_sortname").html(sortname);

    var url = "webcontrol/getfoodbysortid.aspx?tid=" + tid + "&sortid=" + sortid;
    $("#div_fooods").html("<div style=\"text-align: center; margin-top: 20px;\"><img src='images/loading.gif' class='loading' /></div>");

    $.ajax({
        type: "get",
        url: url,
        error: function () {
            $("#div_fooods").html("<p class='windown-error'>加载数据出错...</p>");
        },
        success: function (html) {
            $("#div_fooods").html(html);
            test();
        }
    });
}


//设置字体颜色
function setcolor(tag, colorstr) {
    $(tag).css("color", colorstr);
}

//右边地址
function showaddress() {
    $("#addresslist").toggle();
}

//选择地址
function setaddress(evt) {
    $('input:radio[name="rbaddress"]').removeAttr("checked");
    evt.checked = true
    var v = evt.value + "";
    var names = v.split('^');
    //<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("lat") %>^<%# Eval("lng")%>^<%# Eval("dataid") %>^<%# Eval("BuildingID") %>^<%# Eval("Mobilephone")%>^<%# Eval("Phone") %>

    $("#tbname").val(names[0]);
    $("#tbaddress").val(names[1]);
    $("#tbtel").val(names[6]);
    if (names[6] == "先生") {
        $("#sexm").attr("checked", "true");
    }
    else {
        $("#sexf").attr("checked", "true");
    }
    handlecookie("mylat", names[2], { expires: 1, path: "/", secure: false });
    handlecookie("mylng", names[3], { expires: 1, path: "/", secure: false });

    $("#hidLat").val(names[2]);
    $("#hidLng").val(names[3]);

}


//选择的是余额支付
function payusermoney(val) {
    //var checkvalue = $("#rblpay").val();
    if (val == "3") {
        $("#paypassword").show();
    } else {
        $("#paypassword").hide();
    }
}

//购物车份数减小
function cutnum(pid, num) {
    var onum = num;
    var newnum = onum;
    if (num <= 1) {
        alert("至少要一份哦 ^-^");
        return;
    }
    else {
        newnum = parseInt((parseInt(num) - 1)) + "";
    }
    modcart(pid, newnum, onum);
}

//购物车份数增加
function addnum(pid, num) {
    var onum = num;
    var newnum = onum;
    if (parseInt(num) >= 500) {
        alert('只能小于500份 ^_^');
        return;
    }
    else {
        newnum = parseInt((parseInt(num) + 1)) + "";
    }
    modcart(pid, newnum, onum);
}

//登录5s后再跳转
function timeoutlogin(url) {
    showload_super();
    setTimeout("gourl('" + url + "')", 4000);
}

function gourl(url) {
    window.location = url;
}

// myclass 以哪个元素定位
function localdom(myclass, x, y) {
    $("." + myclass).each(function () {
        var b = this.getAttribute("sub_id");
        var g = this;
        var d = document.getElementById(b);
        document.getElementsByTagName("body")[0].appendChild(d);
        var a = false;
        var e = function () {
            var h = $(g).offset();
            $(d).css({ top: h.top + g.offsetHeight + y, left: h.left + x });
            $(d).show();
            a = true
        };
        var f = function () {
            if (a == false) {
                $(d).hide()
            }
        };
        var c = function () {
            a = false; window.setTimeout(f, 200)
        };

        $(this).hover(e, c);
        $(d).hover(e, c)
    })
}



//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function Showwindow(obj, objdiv, addx, addy, lside) {
    var x = getposOffset_top(obj, 'left');
    var y = getposOffset_top(obj, 'top');
    var div_obj = document.getElementById(objdiv);
    if (lside == "s_left") {
        div_obj.style.left = (x + addx) + 'px';
        div_obj.style.top = (y + addy) + 'px';
        div_obj.style.display = "inline";
        $("#" + objdiv).removeClass("s_right");
    }
    else {
        $("#" + objdiv).addClass("s_right");
        div_obj.style.left = (x - addx * 2 - 105) + 'px';
        div_obj.style.top = (y + addy) + 'px';
        div_obj.style.display = "inline";
    }
    return;
}

//输入完成后，到下一个输入框
function nextinput(curele, nextele) {
    var a = document.getElementById(curele).value + "";
    if (a.length >= 4) {
        if (nextele != "") {
            document.getElementById(nextele).focus();
        }
    }
}


function j_ShowWindow(msg) {
    var innerHTML = "<div style='background-color: #0484cd;height:32px;line-height:30px;padding-left: 10px;padding-right: 10px;'>";
    innerHTML += "<div style='float:left;font-size:14px;color:#fff'>未处理订单</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='Images/window_close.gif' alt='Close the window ' /> </a>";
    innerHTML += "</div></div>";
    innerHTML += "<div style='text-align:left;font-size:12px;overflow:hidden;padding:2px 10px;position:relative;' id='divMassage'>";
    innerHTML += msg;
    innerHTML += "</div></div><div style='position:absolute;right:10px;top:215px'><a href='OrderList.aspx?id=1' class='orange'>查看详情</a></div></div>";

    if (!document.getElementById("divMsg")) {
        var div = document.createElement('div');
        div.id = 'divMsg';
        div.setAttribute('style', 'bottom:0px; right:20px; width:330px; height:120px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #0484cd;');
        div.innerHTML = innerHTML;
        document.body.appendChild(div);
        with (document.getElementById("divMsg").style) {
            bottom = "0px";
            right = "5px";
            width = "350px";
            height = "240px";
            position = "absolute";
            background = "#FFFFFF";
            border = "2px solid #0484cd";
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
            $('body').append('<div id="ajax_spin" style="position:absolute;background:#FFF;filter:alpha(opacity=30);opacity:0.3"><div id="ajax_spin_inner" style="position:relative;height:20px;"></div></div>');
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

