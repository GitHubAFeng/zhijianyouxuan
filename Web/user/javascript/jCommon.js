function inithead(foodcount, username) {
    document.getElementById("ausername").innerHTML = username;
    setDisplay("liusername");
    setNone("lilogin");
    setNone("liregister");
    var ss = document.getElementById("divNologin");
    var mm = document.getElementById("divLoinged");
    if (ss)
    {
        document.getElementById("divNologin").style.display = "none";
    }
    if (mm)
    {
        document.getElementById("divLoinged").style.display = "";
    }
}

function abc()
{ }

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
        simpleWindown_html += "<div id=\"windown-title\"><h2></h2><span id=\"windown-close\">关闭</span></div>";
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
            logindiv += " </td><td class=\"td3\">&nbsp;<a href=\"RegisterByEmail.aspx\">免费注册>></a></td> </tr>";
            logindiv += "<tr><td> &nbsp;</td> <td class=\"td22\"></td><td class=\"td3\">&nbsp;</td> </tr> </table></div>";

            $("#windown-content").html(logindiv);
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
    DragHead.onmouseover = function(e) {
        if (drag == "true") { DragHead.style.cursor = "move"; } else { DragHead.style.cursor = "default"; }
    };
    DragHead.onmousedown = function(e) {
        if (drag == "true") { moveable = true; } else { moveable = false; }
        e = window.event ? window.event : e;
        var ol = Drag_ID.offsetLeft, ot = Drag_ID.offsetTop - moveTop;
        moveX = e.clientX - ol;
        moveY = e.clientY - ot;
        document.onmousemove = function(e) {
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
        document.onmouseup = function() { moveable = false; };
        Drag_ID.onselectstart = function(e) { return false; }
    }
    $("#windown-content").attr("class", "windown-" + cssName);

    //定时器 事件
    var closeWindown = function() {
        $("#windownbg").remove();
        $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {
        //关闭事件
        $("#windown-close").click(function() {
            $("#windownbg").remove();
            $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
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
    $("#windown-box").fadeOut("slow", function() { $(this).remove(); });
}

//获取商家信息
function getTogoInstroduce() {
    var togoid = document.getElementById("hidTogoId").value;
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/ajaxGetTogoInfo.aspx",
        data: "togoid=" + togoid + "&type=1",
        success: function(msg) {
        if (msg != "-1") {
                $("#divTogoInfo").html(msg);
            }
        }
    })
}

//购物车隐藏 , 显示
function cartHiden(flag) {
    var cartimg = document.getElementById("cartimg");
    var carthead = document.getElementById("cartHead").style.display;
    //购物车已经隐藏 , 点击显示
    if (carthead == "none") 
    {
        jQuery("#tablecart tr").css("display", "");
        // ie
        cartimg.src = "images/arrow_down_03.jpg";
        // firefox
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
            cartimg.setAttribute('src', 'images/arrow_down_03.jpg');
        }
        ListCart();
    }
    else 
    {
        //购物车已经显示 , 点击隐藏
        jQuery("#tablecart tr").css("display", "none");
        //jQuery("#tablecart tr").css("display", "none");
        jQuery("#tablecart tr:first").css("display", "");
       // jQuery("#cartContent tr:last").css("display", "");//显示最后一行实现不了。。奇怪。。。
        // ie
        cartimg.src = "images/shopinfo_up.jpg";
        // firefox
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
            cartimg.setAttribute('src', 'images/arrow_up_03.jpg');
        }
        document.getElementById("cartHead").style.display = "none"
    }
    if (typeof flag != 'undefined') {
        jQuery("#tablecart tr").css("display", "");
        // ie
        cartimg.src = "images/arrow_up_03.jpg";
        // firefox
        if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
            cartimg.setAttribute('src', 'images/arrow_up_03.jpg');
        }
    }
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
                if (userflag == "1") 
                {
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
                document.getElementById("strongTotal").innerHTML = "订单合计：已选餐品"+temp[1]+"份，共计"+temp[2]+"元"
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
        carttail.innerHTML = "<td style='text-align:center'>此商户不支持网上下订单,请对照“我的外卖盒”的餐品，拨打商家电话订购.<img src='Images/remove.jpg' onclick=\"deleteallcart();\" width=\"90\" height=\"35\" /></td><td></td>";
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
function Printpart(id_str )//id-str 内容中的id
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
        data: "id=" + id + "&t=" + new Date().getTime() + "&times="+ptimes,
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

function setDisplay(ele)
{
    var _ele = G(ele);
    if (_ele)
    {
        _ele.style.display = "";
    }
}

//设置不可见

function setNone(ele)
{
    var _ele = G(ele);
    if (_ele)
    {
        _ele.style.display = "none";
    }
}

function G(ele)
{
    return document.getElementById(ele);
}


function $j(ele)
{
    return document.getElementById(ele);
}

//设置登录框位置
function locationlogin()
{
    var jw = "divmylogin";
    var obj = $j("lilogin");
    setDisplay(jw);
    ShowTip(obj , jw , -43 , 28);
}
//隐藏
function closelogin()
{
    var jw = $j("divmylogin");
    if (jw.style.display == '' || jw.style.display == 'block' || jw.style.display == 'inline')
    {
        setNone("divmylogin");
    }
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function sShowTip(obj,objdiv,addx,addy)
{
	var x=getposOffset_top(obj,'left');
    var y=getposOffset_top(obj,'top');
    
    var div_obj=document.getElementById(objdiv);
	div_obj.style.left=(x+addx)+'px';
	div_obj.style.top=(y+addy)+'px';
	div_obj.style.display="inline";
}

//获取偏移量
function getposOffset_top(what, offsettype)
{ 
    var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop; 
    var parentEl=what.offsetParent; 
    while (parentEl!=null)
    { 
        totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop; 
         parentEl=parentEl.offsetParent; 
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
    if (wordlength < 500) {
        youmsg.innerHTML = 500 - wordlength;
    }
    else {
        youmsg.innerHTML = 0;
        tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '1000', 'true', 'text');
    }
}


function checkQ() {
    var wordlength = document.getElementById("textarea").value.length;
    if (wordlength < 1) {
        tipsWindown('提示信息', 'text:字数太少了,请至少输入法1个汉字!', '250', '150', 'true', '1000', 'true', 'text');
        return false;
    }
    else {
        if (wordlength > 500) {
            tipsWindown('提示信息', 'text:已经超过字数限制!', '250', '150', 'true', '1000', 'true', 'text');
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


