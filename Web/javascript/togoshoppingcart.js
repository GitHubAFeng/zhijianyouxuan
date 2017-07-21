// TogoShoppingCart.js
// 在线点餐购物车实现脚本
// CopyRight (c) 2009 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010－03－23

var reference_url;
var book_id;
var Jflag = -1;
var JxmlHttp;
var XxmlHttp;
var Xflag = 0;
//将书加入购物车 并跳转到购物车页面 返回页面url
function AddToShoppingCart(pid, pname, pprice, flag) {

    var hfBuildingID = $D("hfBuildingID");
    var hfBuildingName = $D("hfBuildingName");
    var cookiebn = handlecookie("buildingName");
    var cookieid = handlecookie("buildingID");
    var temptogoname = $D("hidTogoName").value;
    var userid = $D("hidUid").value;
    var togoStatus = $D("hnTogoStatus").value;//状态
    var togoBusiness = $D("hnTogoBusiness").value;//时间
    var togoid = $D("hidTogoId").value;

    //商家不营业.
    if (togoStatus == 0) {
        alert('抱歉,' + temptogoname + '现在不营业,请稍后再点餐或选择别的餐厅')
        return;
    }
    if (togoBusiness == "0") {
        alert("商家“" + temptogoname + "”已打烊，请选择其他餐厅。");
        return;
    }

    var togoid = document.getElementById("hidTogoId").value;
    var togoname = escape(document.getElementById("hidTogoName").value);

    jQuery.ajax(
    {
        type: "post",
        url: "../../Ajax/TogoShoppingCart.aspx",
        data: "fuc=add&uid=" + 0 + "&togoid=" + togoid + "&togoname=" + togoname + "&pid=" + pid + "&pname=" + escape(pname) + "&pprice=" + pprice + "&pnum=1&time=" + new Date().getTime() + "",
        success: function (msg) {
            if (msg == "1") {
                ListCart(pid);
            }
            if (msg == "-1") {
                alert("添加失败");
            }
            if (msg == "0") {
                alert("请清空购物车再点餐。");
            }
        }
    })
}


//修改购物车中商品数量
function modcart(pid, pnum, lastpum) {
    ///判断输入是否合法.
    try {
        var nowpum = parseInt(pnum);
        if (nowpum < 500 && nowpum > 0) {
            pnum = nowpum;
        }
        else {
            alert("请输入1-500之间的整数");
            pnum = lastpum;
        }
    }
    catch (e) {
        pnum = lastpum;
    }
    var uid = document.getElementById("hidUid").value;

    //购物车数量加m+
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/TogoShoppingCart.aspx",
        data: "fuc=mod&uid=" + uid + "&pid=" + pid + "&pnum=" + pnum + "",
        success: function (msg) {
            if (msg == "1") {
                ListCart();
            }
            else {
                alert("修改数量失败");
            }
        }
    })
}

///删除购物车中商品
function delcart(pid) {
    var uid = document.getElementById("hidUid").value;

    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/TogoShoppingCart.aspx",
        data: "fuc=del&t=" + new Date().getTime() + "&uid=" + uid + "&pid=" + pid,
        success: function (msg) {
            if (msg == "1") {
                ListCart();
                $("#lifood" + foodid).removeClass("active");
            }
            else {
                alert("服务器忙...");
            }
        }
    })
}


///清空购物车中商品
function deleteallcart() {
    if (!confirm('你确认清空购物车?')) {
        return;
    }
    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=delall&uid=",
        success: function (msg) {
            if (msg == "1") {
                ListCart();
                $("#lifood" + foodid).removeClass("active");
            }
            else {
                alert("服务器忙...");
            }
        }
    })

}


function ListCart(pid) {
    var userid = document.getElementById("hidUid").value;//用户编号
    //是否定位m
    var blat = handlecookie("mylat");
    if (blat == null || blat == "" || blat == "0") {
        return;
    }
    var togoid = document.getElementById("hidTogoId").value;//商家编号
    jQuery.ajax(
    {
        type: "post",
        url: "../../Ajax/TogoShoppingCart.aspx",
        data: "fuc=list&t=" + new Date().getTime() + "&uid=" + userid + "&togoid=" + togoid + "&grade=" + 1,
        success: function (msg) {
            if (msg == "0") {
                alert("读取失败！");
            }
            else {
                //更新右上角数量就问题....
                jQuery("#cartContent").html(msg);

                if (typeof pid != 'undefined') { 
                    addsuccess(document.getElementById("add_bt_"+pid), 0, -70);
                }

            }
        }
    })
}

function CheckCart() {
    if ($(".myshop").length <= 0) {
        alert('你的餐盒是空的,不的提交!');
        return false;
    }
    var togoStatus = $D("hnTogoStatus").value;
    if (togoStatus != "1") {
        alert('商家不在营业，不能提交订单');
        return false;
    }

    /* 3、配送费设置0元的情况下，则必须满足起送价格； */
    var totalmoney = parseFloat(document.getElementById("allprice").innerHTML.substr(1));
    var limitmoney = parseFloat($("#hfminmoney").val());
   // var hfsendmoney = $("#hfsendmoney").val();
    if ( totalmoney < limitmoney) {    //hfsendmoney == "0" &&
        alert('亲~本餐厅到您地址起送价格为' + limitmoney + '元噢！');
        return false;
    }

    var togoid = $D("hidTogoId").value;

    window.location = "OrderDetail.aspx?togoid=" + togoid;
}

//ajax获取菜品,直接获取
function getmyfood() {
    var tid = $("#hidTogoId").val() + "";
    var hfsortid = $("#hfsortid").val() + "";
    var hfsortidflag = $("#hfsortidflag").val() + "";
    var dids = $("#dids").val() + "";
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/getfoods.aspx",
        data: "t=" + new Date().getTime() + "&tid=" + tid + "&sid=" + hfsortid + "&flag=" + hfsortidflag + "&dids=" + dids,
        success: function (msg) {
            $("#shop_inot_t3").html(msg);
        }
    })
}
///传入id.标志
function getmyfood_id(hfsortid, hfsortidflag) {
    var tid = $("#hidTogoId").val() + "";
    var dids = $("#dids").val() + "";
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/getfoods.aspx",
        data: "t=" + new Date().getTime() + "&tid=" + tid + "&sid=" + hfsortid + "&flag=" + hfsortidflag + "&dids=" + dids,
        success: function (msg) {
            $("#shop_inot_t3").html(msg);
        }
    })
}

//获得URL参数
function Request(paramkey) {
    var a = document.createElement('a');
    a.href = window.location;
    var urlobj = {
        source: a.href,
        protocol: a.protocol.replace(':', ''),
        host: a.hostname,
        port: a.port,
        query: a.search,
        params: (function () {
            var ret = {},
                seg = a.search.replace(/^\?/, '').split('&'),
                len = seg.length, i = 0, s;
            for (; i < len; i++) {
                if (!seg[i]) { continue; }
                s = seg[i].split('=');
                ret[s[0]] = s[1];
            }
            return ret;
        })(),
        file: (a.pathname.match(/\/([^\/?#]+)$/i) || [, ''])[1],
        hash: a.hash.replace('#', ''),
        path: a.pathname.replace(/^([^\/])/, '/$1'),
        relative: (a.href.match(/tps?:\/\/[^\/]+(.+)/) || [, ''])[1],
        segments: a.pathname.replace(/^\//, '').split('/')
    };

    var paramvalue = urlobj.params[paramkey];
    if (paramvalue == undefined) {
        paramvalue = "";
    }
    else {
        var pattern = new RegExp("[`~!@#$^&*()=|{}':;',\\[\\]<>/?~*|{}']")
        paramvalue = paramvalue.replace(pattern, '').replace("%3D", "").replace("%3E", "").replace("%3F", "").replace("%3C", "").replace("%27", "").replace("%22", "");
    }
    return paramvalue;
}


//页面滚动
function winscroll() {
    var carttop = $("#mytarget").offset().top;
    //浏览器可视高度
    var _mysrcreen = parseInt($(window).height());
    var cartheight = parseInt($("#basketTitleWrap").height());

    var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop); //获取滚动条的当前位置 距离页面最顶部

    if (_mysrcreen < cartheight) {
        $("#basketTitleWrap").removeClass("fixed-top");
        if (scrollTop > (carttop + cartheight)) {
            $("#cart_fix_hint").show();
        }
        else {
            $("#cart_fix_hint").hide();
        }
    }
    else {
        $("#cart_fix_hint").hide();
        if (scrollTop >= carttop) {
            $("#basketTitleWrap").addClass("fixed-top");
        }
        else {
            $("#basketTitleWrap").removeClass("fixed-top");
        }
    }
}