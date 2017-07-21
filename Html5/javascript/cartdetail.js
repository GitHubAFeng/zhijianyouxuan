/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

var num1 = $('.food_num');
var submit = $('#submit');
var shoppingcart = $('#cart');
var moneyt = $('.money_num');
var totalprice = $('.totalprice');
var hfminimoney = parseFloat($('#hfminimoney').val()); //起送价
var mainbody = $('#shop_main');
var cart_food_container = $('#cart_food_list');
var hfstatus = $("#hfstatus").val() + "";
var canorder = 1;
var shopid = request("id");
var togoStatus = $("#hnTogoStatus").val();//状态
var togoBusiness = $("#hnTogoBusiness").val();//时间
var temptogoname = $("#hidTogoName").val();


$(function () {

    if (hfstatus == "0") {
        alert("商家正在休息中，不能在线下单");
        return;
    }
    if (togoStatus == 0) {
        alert('商家不在营业，不能提交订单');
        return;
    }

    var ShoppingCartdata = utils.getParam("ShoppingCart");
    if (ShoppingCartdata != null && ShoppingCartdata != "") {
        var productlist = cart.getproductlist();
        $("#cart_foodcount").html(orderdetail.totalNumber + "");
        if (orderdetail.shopid + "" != shopid) {
            canorder = 0;
        }

        var art_addhtml = $("#catfoodlist").render(productlist);
        cart_food_container.html("<url>" + art_addhtml + "</url>");
    }

    initcart();
})


function initcart() {
    num1.html(orderdetail.totalNumber + "");
    moneyt.html("￥" + orderdetail.totalAmount.toFixed(1));

    var allprice = parseFloat(orderdetail.totalAmount);
    //满多少免费送费
    var freemoney = parseFloat($("#hffreemoney").val());//满多少免费送费
    var sendfree = parseFloat($("#hfsendfree").val());
    if (orderdetail.totalAmount >= freemoney && freemoney > 0) {
        sendfree = 0;
    }


    $("#spanpackagefee").html(orderdetail.packagefee.toFixed(1));
    

    $("#spansendmoney").html(sendfree + "");
    moneyt.html("￥" + (allprice + sendfree).toFixed(1));

    if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
        var productlist = cart.getproductlist();
    }


    if (parseInt(orderdetail.totalAmount) < hfminimoney) {
        submit.html("还差￥" + (hfminimoney - orderdetail.totalAmount).toFixed(1) + "起送");
        submit.removeClass("check_cart_btn");
        submit.addClass("check_cart_btn1");
        submit.attr("href", "#");
        submit.show();
    }
    else {
        submit.html("下一步");
        submit.removeClass("check_cart_btn1");
        submit.addClass("check_cart_btn");
        submit.show();
        submit.attr("href", "orderdetail.aspx?id=" + orderdetail.shopid + "&sendfree=" + sendfree);
    }
}


function addcart(object, index) {


    var $this = $(object).parent();
    var dataid = $this.attr("data-id");
    var name = $this.attr("data-name");


    var cart_food_box = $("#cart_food_" + index);
    var count = parseInt(cart_food_box.html());
    cart.addnum(dataid, name, 1);
    cart_food_box.html((parseInt(count) + 1) + "");

    initcart();

}

function cutcart(object, index) {


    var $this = $(object).parent();
    var dataid = $this.attr("data-id");

    var name = $this.attr("data-name");

    var cart_food_box = $("#cart_food_" + index);
    var count = parseInt(cart_food_box.html());
    if (count <= 1) {
        count = 0;
        cart.deleteproduct(dataid,name);
        $this.parent().remove();
    }
    else {
        count = parseInt(count) - 1;
        cart.addnum(dataid, name, -1);

    }
    cart_food_box.html(count + "");
    initcart();


}