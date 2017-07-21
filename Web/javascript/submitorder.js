/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
/***************************** 本js主要是关于提交订单部分相关验证 ****************************/

var jmz = {};
jmz.GetLength = function (str) {
    return str.replace(/[\u0391-\uFFE5]/g, "aa").length;
};

$(function () {
    initnav(2);
    payusermoney();
    // 我曾经使用过的历史地址
    $("#historyAddr").click(function () {
        $(".infop").toggle();
    });
    var uid = $("#hidUid").val();
    if (uid === "0") {
        $.jBox($("#showtip").html(), { width: 400, buttons: { '关闭': 'ok' }, title: '您没有登录', persistent: false });
    }


})
//备注标签
function settag(tag) {
    var or = $("#tbremark").val();
    $("#tbremark").val(or + " " + $(tag).attr("mytag"));
    $(tag).addClass("cull");
}

//切换地址
function setaddr(evt) {
    $('input:radio[name="rbaddress"]').removeAttr("checked");
    $(evt).parent().addClass("culbg");
    $(evt).parent().siblings().removeClass("culbg");
    evt.checked = true;
    var v = evt.value + "";
    var names = v.split('^');
    //<%# Eval("Receiver") %>^<%# Eval("Address")%>^<%# Eval("lat") %>^<%# Eval("lng")%>^<%# Eval("dataid") %>^<%# Eval("BuildingID") %>^<%# Eval("Mobilephone")%>^<%# Eval("Phone") %>

    //$("#tbname").val(names[0]);
    $("#tbaddress").val(names[1]);
    //$("#tbtel").val(names[6]);

    handlecookie("mylat", names[2], { expires: 1, path: "/", secure: false });
    handlecookie("mylng", names[3], { expires: 1, path: "/", secure: false });

    $("#hidLat").val(names[2]);
    $("#hidLng").val(names[3]);

}
function CheckCart() {
    var tbname = $("#tbname").val();
    if (tbname == "") {
        tipsWindown('提示信息', 'text:请输入联系人!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    if ($("#tbaddress").val() == "") {
        tipsWindown('提示信息', 'text:请输入送餐地址!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    var tbaddress = $("#tbdetailaddress").val();
    if (tbaddress == "") {
        tipsWindown('提示信息', 'text:请输入详细地址!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    if (jmz.GetLength($("#tbdetailaddress").val()) <10) {
        tipsWindown('提示信息', 'text:请输入5个汉字以上!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    var tbtel = $("#tbtel").val()
    if (tbtel == "") {
        tipsWindown('提示信息', 'text:请输入联系电话!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    var patrn = /^[0-9]{11}$/;
    if (!patrn.exec(tbtel)) {
        tipsWindown('提示信息', 'text:手机号码格式错误!', '250', '150', 'true', '', 'true', 'text');
        return false;
    }
    showload_super();

    //保存用户信息
    address = {
        username: tbname,
        phone: tbtel,
        address: tbaddress
    }
    historyaddress.saveUserInfo(address);

    return true;

}

//购物车份数减小
function cutcount(pid, num, object) {
    if (num <= 1) {
        alert("至少要一份哦 ^-^");
        return false;
    }
}
//购物车份数增加
function addcount(pid, num) {
    if (parseInt(num) >= 500) {
        alert('只能小于500份 ^_^');
        return false;
    }
}

//选择的是余额支付
function payusermoney() {
 
    var val = $("input[name='rblpay']:checked").val()
    if (val == "3") {
        $("#paypassword").show();
    } else {
        $("#paypassword").hide();
    }
}

///转到登录界面
function gologin() {
    var url = "Login.aspx?returnurl=" + escape(window.location);
    gourl(url);
}

///转到注册界面
function goreg() {
    var url = "RegisterByEmail.aspx?returnurl=" + escape(window.location);
    gourl(url);
}

//是否使用优惠券
function usercardchange() {
    var val = $("input[name='rptusercard']:checked").val() + ""
    if (val == "1") {
        $("#giftcardpay_div").show();
    } else {
        $("#giftcardpay_div").hide();
    }
}


/************************礼品卡支付 ***************************/
function card_getallmoney() {

    var lballmoney = parseFloat($("#hffoodprice").val());//总商品金额
    var lbsalemoney = parseFloat($("#hfsalemoney").val());//商家优惠金额
    lballmoney -= lbsalemoney;//要支付的金额

    var shopcardpay = 0;
    var shopcardcount = 0;
    var shopcardpayjson = "[";

    //优惠券
    $(".shopcardcheck").each(function () {
        var card = $(this);
        if ($(this).attr("checked") == "checked" || $(this).attr("checked")) {
            shopcardcount++;

            var cardpay = parseFloat($(this).attr("price"));;
            var cardtype = parseInt(card.attr("cardtype"));// 1->现金折扣（满多少优惠多少）;2->百分比折扣（满多少享受折扣多少）;3->多倍积分（满多少享受）
            switch (cardtype) {
                case 1:
                    shopcardpay += cardpay;
                    break;
                case 2:
                    shopcardpay += cardpay;
                    break;
            }

            shopcardpayjson += "{'CID':'" + $(this).attr("cid") + "','Point':'" + cardpay + "','ReveInt1':'" + $(this).attr("cardtype") + "','ckey':'" + $(this).attr("ckey") + "'},";
        }
    })
    shopcardpayjson = shopcardpayjson.replace(/,$/, "");
    shopcardpayjson += "]";

    $("#hfshopcardinfo").val(shopcardpayjson);
    $("#shopcardnum").html(shopcardcount);
    $("#hfshopcardpay").val(shopcardpay);
    $("#shopcutprice").html(shopcardpay);
    $("#spanshopcardprice").html(shopcardpay);
    $("#spanpaymenry").html((lballmoney - shopcardpay).toFixed(2));

}

//优惠券
//订单页面绑定优惠券
function bindmyshopcard() {
    var cardpwd = $("#tbpwd1").val() + "-" + $("#tbpwd2").val() + "-" + $("#tbpwd3").val();
    if (cardpwd == "--") {
        alert("请输入完整的券号");
        return false;
    }
    showload_super();
    var url = "Ajax/bindmyshopcard.aspx";
    var para = "t=" + new Date().getTime() + "&pwd=" + cardpwd;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function (msg) {
            var myjson = eval("(" + msg + ")");
            alert(myjson.msg);
            //绑定
            if (myjson.code == "200") {
                var tr = $("#cardTemplate").render({ CID: myjson.cid, ckey: myjson.ckey, point: myjson.point, moneyline: myjson.moneyline, ReveInt1: myjson.ReveInt, ReveVar1: myjson.usemsg })
                $(tr).appendTo($("#tabcard_banded"));
            }

            hideload_super();
        }
    })
}

//选中或者取消一个张优惠券
function setthisshopcard(tag) {
    debugger;
    var foodprice = parseFloat($("#hffoodprice").val());
    var hfshopcardcount = parseInt($("#hfshopcardcount").val());
    var card = $(tag);
    var parent_tr = card.parent().parent();
    if (card.attr("checked") == "checked" || card.attr("checked")) {
        var useredcount = 0;

        parent_tr.addClass("invalid");

        $(".shopcardcheck").each(
            function () {
                if ($(this).attr("checked") == "checked" || $(this).attr("checked")) {
                    useredcount++;
                }
            });

        var moneydoor = parseFloat(card.attr("moneydoor"));
        if (foodprice < moneydoor) {
            alert("此券商品" + moneydoor + "元可用");
            card.removeAttr('checked');
            parent_tr.removeClass("invalid");
            return;
        }

        if (useredcount > hfshopcardcount) {
            alert("一个订单只能使用" + hfshopcardcount + "张优惠券");
            card.removeAttr('checked');
            parent_tr.removeClass("invalid");
            return;
        }


        var lbsalemoney = parseFloat($("#hfsalemoney").val());//商家优惠金额
        var allprice = foodprice - lbsalemoney;//要支付的金额


        if (allprice <= 0) {
            alert("已经全额支付了，无需要使用其他优惠券");
            card.removeAttr('checked');
            parent_tr.removeClass("invalid");
            return;
        }

        var cmoney = 0;
        var cardtype = parseInt(card.attr("cardtype"));// 1->现金折扣（满多少优惠多少）;2->百分比折扣（满多少享受折扣多少）;3->多倍积分（满多少享受）
        switch (cardtype) {
            case 1:
                cmoney = parseFloat(card.attr("cprice"));
                break;
            case 2:
                cmoney = parseFloat(card.attr("cprice"));
                cmoney = parseFloat((allprice * (100 - cmoney) / 100).toFixed(2));
                break;
            case 3:
                cmoney = parseFloat(card.attr("cprice"));
                break;
            default:
                cmoney = 0;
                break;
        }

        if (allprice >= cmoney) {
            card.attr("price", cmoney); //此卡支付金额
        }
        else {
            card.attr("price", allprice); //此卡支付金额
        }
    }
    else {
        parent_tr.removeClass("invalid");
        card.attr("price", 0);
    }
    card_getallmoney();
}

///删除商品后,检查当前金额下,是否可用
function checkCardDoor() {
    var allmoney = parseFloat($("#cart_food_allprice").html());
    $(".shopcardcheck").each(function () {
        $(this).removeAttr("checked");
        $(this).parent().parent().removeClass("invalid");
        var moneydoor = parseFloat($(this).attr("moneydoor"));
        if (moneydoor > allmoney) {
            $(this).attr("disabled", "disabled");
        }
    })


    $("#spanshopcardprice").html("0");
    $("#spanpaymenry").html(allmoney + "");
    $("#shopcardnum").html("0");
    $("#shopcutprice").html("0");
    $("#spanfoodprice").html(allmoney + "");
}
/************************礼品卡支付over ***************************/
