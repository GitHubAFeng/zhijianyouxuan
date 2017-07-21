/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

var num1 = $('.food_num');
var num2 = $('#food_num_net');
var submit = $('#submit');
var shoppingcart = $('#cart');
var moneyt = $('.money_num');
var hfminimoney = parseFloat($('#hfminimoney').val()); //起送价
var mainbody = $('#shop_main');
var cart_food_container = $('#cart_food_list');
var hfstatus = $("#hfstatus").val() + "";
var cart_returnbutton = $("#cartreturnbtn");
var canorder = 1;
var shopid = request("id");
var togoStatus = $("#hnTogoStatus").val();//状态
var togoBusiness = $("#hnTogoBusiness").val();//时间
var temptogoname = $("#hidTogoName").val();
var pictip = $("#pictip");

var foodstyles = null;
var foodattrs = null;

var stylebox = $("#divstyle");
var attrbox = $("#cartmaterial");


$(function () {


    foodstyles = $.parseJSON($("#hfstyle").val());
    foodattrs = $.parseJSON($("#hfattr").val());

    var ShoppingCartdata = utils.getParam("ShoppingCart");
    if (ShoppingCartdata != null && ShoppingCartdata != "") {
        var productlist = cart.getproductlist();
        $("#cart_foodcount").html(orderdetail.totalNumber + "");
        if (orderdetail.shopid + "" != shopid) {
            canorder = 0;
        }
    }

    var sortid = request("sortid");
    if (sortid == "") {
        sortid = "0";
    }
    $("#sort_" + sortid).addClass("cur");

    //这里用了 onTouchEnd，只对移动设置有用

    //$(".additem").bind("click", function (event)
    $(".additem").bind("touchend", function (event)
    {
        if (canorder == 0) {
            if (confirm("换了饭店，您必须先清空餐车,点击确认清空餐车")) {
                cart.deleteallproduct();
                canorder = 1;
                //清空数据
                num1.html("0");
                $(".countbox").html("0");
            }
            return;
        }
        if (hfstatus == "0") {
            alert("商家正在休息中，不能在线下单");
            return;
        }
        if (togoStatus == 0) {
            alert('抱歉,' + temptogoname + '现在不营业,请稍后再点餐或选择别的餐厅')
            return;
        }
        if (togoBusiness == "0") {
            alert("商家“" + temptogoname + "”已打烊，请选择其他餐厅。");
            return;
        }

        var $this = $(this).parent();
        var dataid = $this.attr("data-id");
        var name = $this.attr("data-name");

        var price = parseFloat($this.attr("data-price"));
        var packagefee = parseFloat($this.attr("data-package"));

        var isspecial = parseInt($this.attr("data-isspecial"));//规格数量
        var isauth = parseInt($this.attr("data-isauth"));//属性数量


        if (isspecial > 1 || isauth > 0) {
            $("#cartname").html(name);
            showstyle(dataid);
            $("#hfpid").val(dataid);
            $("#hfcname").val(name);
            return;
        }

        var box = $("#box_" + dataid);
        var count = box.html();

        //调用购物车相关
        product = {
            id: dataid,
            name: name,
            number: 1,
            price: price,
            material: "",
            sid: 0,
            packagefee:packagefee,
            addprice: 0.0,
            sname: "",
        }
        cart.addproduct(product);

        box.show();
        box.html((parseInt(count) + 1) + "");
        initcart();

    });


    //减分数
    //$(".subitem").bind("click", function (event) 
    $(".subitem").bind("touchend", function (event)
    {
        var $this = $(this).parent();
        var dataid = $this.attr("data-id");
        var name = $this.attr("data-name");
        var price = parseFloat($this.attr("data-price"));
        var packagefee = parseFloat($this.attr("data-package"));
        var box = $("#box_" + dataid);
        var count = parseInt(box.html());
        if (count <= 1) {
            count = 0;
            cart.deleteproduct(dataid,name);
        }
        else {


            var isspecial = parseInt($this.attr("data-isspecial"));//规格数量
            var isauth = parseInt($this.attr("data-isauth"));//属性数量
            if (isspecial > 1 || isauth > 0) {
                //有规格要弹出购物车的菜单，因为不能直接间少份数
                onclick_my_cart();
                return;
            }
            else
            {
                count = parseInt(count) - 1;
                //调用购物车相关
                product = {
                    id: dataid,
                    name: name,
                    number: 1,
                    price: price,
                    material: "",
                    packagefee:packagefee,
                    sid: 0,
                    addprice: 0.0,
                    sname: "",
                }
                cart.delproduct(product);
            }
        }

        box.show();
        box.html(count + "");
      
    })
   

    $(".foodpic").click(function () {
        $(".mModal").show();
        $("#pictip").show();
        $("#showitem").attr("src", $(this).attr("src"));
        $("#showpich2").html(left($(this).attr("data-name"), 20));
        $("#tbremark").html($(this).attr("data-remark"));
    }
        );
    $(".x,.mModal").click(function () {
        $(".mModal,.mDialog").hide();
    });


    $(".x,.mModal,.my_cart_other").click(function () {
        $(".mModal,.mDialog,.mDialog_cart").hide();
        $(".my_cart").show();  //上方购物车
        $(".my_cart_other").hide();  //上方购物车
        $(".clear_cart").hide();
        intiBookedFood();
    });

    var foodids = request("foodids");
    if (foodids.length > 0) {
        var ids = foodids.split(",");
        for (var i in ids) {
            $("#add_bt_" + ids[i]).click();
        }
        initcart();
        onclick_my_cart();

    }

    window.scrollTo(0, 0); //每次F5刷新把滚动条置顶
    intiBookedFood();




})

//设置已经点商品的数据
function intiBookedFood()
{
    $(".countbox").html("0");
    //获取所有购物车中的商品
    if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
        var productlist = cart.getproductlist();
        initcart();
        for (var i = 0; i < productlist.length; i++) {
            var box = $("#box_" + productlist[i].id);
            box.html((parseInt(box.html()) + productlist[i].number) + "");
        }
    }
}

//加载规格，属性
function showstyle(foodid) {
    stylebannerInit();
    $(".mModal").show();
    $("#pictip").show();

    var clientHeight = document.documentElement.clientHeight;
    pictip.css({ "margin-top": (-clientHeight * 0.4) + "px" });

    {
        var thestyles = new Array();
        $.each(foodstyles, function (i) {
            if (foodstyles[i].FoodtId == foodid) {
                thestyles.push(foodstyles[i]);
            }
        })
        stylebox.html($("#stylemodel").render(thestyles));

        $("input[name='mystyle']:first").attr("checked", true);

    }

    {
        var theattrs = new Array();
        $.each(foodattrs, function (i) {
            if (foodattrs[i].FoodtId == foodid) {
                theattrs.push(foodattrs[i]);
            }
        })

        attrbox.html($("#attrmodel").render(theattrs));

    }

}


var showCart = function () {
    if (shoppingcart.css("display") == "none") {
        shoppingcart.show();
        mainbody.hide();
    } else {
        mainbody.show();
        shoppingcart.hide();
    }
    if (cart_returnbutton.css("display") == "none") {
        cart_returnbutton.show();
        $("#cart_button").hide();
    }
    else {
        cart_returnbutton.hide();
        $("#cart_button").show();
    }
};

function initcart() {


    num1.html(orderdetail.totalNumber + "");
    moneyt.html("￥" + orderdetail.totalAmount.toFixed(1));

    //满多少免费送费
    var freemoney = parseFloat($("#hffreemoney").val());//满多少免费送费
    var sendfree = $("#hfsendfree").val();
    if (orderdetail.totalAmount >= freemoney && freemoney > 0) {
        $("#spansendmoney").html("0.00");
    }
    else {
        $("#spansendmoney").html(sendfree);
    }


    if (orderdetail.totalNumber == 0) {
        submit.hide();
    }

    var isgocart = true;
    if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
        var productlist = cart.getproductlist();
        if (productlist.length <= 0) {
            isgocart = false;
        }

    }
    else {
        isgocart = false;
    }
    if (isgocart == false) {
        submit.hide();
    }
    else if (parseInt(orderdetail.totalAmount) < hfminimoney) {
        submit.html("还差￥" + (hfminimoney - orderdetail.totalAmount) + "起送");
        submit.removeClass("check_cart_btn");
        submit.addClass("check_cart_btn1");
        submit.show();
    }
    else {
        submit.html("立即结算");
        submit.removeClass("check_cart_btn1");
        submit.addClass("check_cart_btn");
        submit.show();

        var url = "cartdetail.aspx?id=" + orderdetail.shopid;
        submit.attr("href", url);
    }
}



/****************
规格部分操作
********************************/

function selectme(tag) {
    var mystyle = $("input[name='mystyle']").attr("checked", false);
    $(tag).attr("checked", true);
}


//配料单选
// 商品编号码， 配料子项名称 ， 价格 , sid 配料编号
function selectme0(pid, aname, aprice, sid, parentname) {
    var str = "<strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")";
    var o = $("#other_" + sid);
    if (o.length > 0) { //重新选择
        $("#other_" + sid).html(str);
    }
    else {//第一次选择
        var str = "<div class=\"lunch_box_02\" id=\"other_" + sid + "\"><strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")</div>";
        $("#cartother").append(str);
    }
}

function cancelRadio(pid, sid) {
    var name = "attr_" + sid;
    $("input[name='" + name + "']").each(function () {
        $(this).removeAttr("checked");
        $("#other_" + sid).remove();
    })
}


//配料多选
// 商品编号码， 配料子项名称 ， 价格
function selectme1(pid, aname, aprice, sid, parentname) {

    var str = "<strong>" + parentname + "：</strong>";
    var o = $("#other_" + sid);
    if (o.length > 0) { //重新选择
        $("#other_" + sid).html(""); //清除 
        $("input[name='box_" + sid + "']").each(function () {
            if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                str += "+" + $(this).attr("myname") + "(￥" + $(this).attr("value") + "),";
            }
        })
        $("#other_" + sid).html(str); //重新输出
    }
    else {//第一次选择
        var str = "<div class=\"lunch_box_02\" id=\"other_" + sid + "\"><strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")</div>";
        $("#cartother").append(str);
    }
}

function cartok()
{
 
    var dataid =  $("#hfpid").val();
    var name =  $("#hfcname").val();

  
    var box = $("#box_" + dataid);
    var count = box.html();


    var price = 0;
    var sid = 0;
    var sname = "";
    var mystyle = $("input[name='mystyle']");
    mystyle.each(function () {
    
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            sid = $(this).attr("value");
            sname = $(this).attr("stylename");
            price = $(this).attr("price");
        }
    })


    var om = getmaterial();
    var addinfo = $("#cartother").html();

    var name = (sname.length == 0 ? "" : "[" + sname + "]") + name + om.addnames;

    var foodbox = $("#food_op_" + dataid);
    

    //调用购物车相关
    product = {
        id: dataid,
        name: name,
        number: 1,
        price: parseFloat(price),
        material: om.info,
        packagefee: parseFloat(foodbox.attr("data-package")),
        sid: sid,
        addprice: parseFloat(om.addp),
        sname: sname,
    }
    cart.addproduct(product);

    box.show();
    box.html((parseInt(count) + 1) + "");
    initcart();
    $(".mModal,.mDialog").hide();
}

function stylebannerInit()
{
    $("#cartother").html("");
}


///加料连接成字符串.
function getmaterial() {
    var l = $(".cartattr").length; //个数
    var str = "";
    var attritems = "";
    var tempprice = 0;
    for (var i = 0; i < l; i++) {
        var aname = $("#jh4_" + i).html();
        var x = 0;
        //  每个加料中间用'#'分开.比如： 冰?去冰^3#糖?红糠^4@白糠^4
        $("input[cart='jj_" + i + "']").each(function () {
            if ($(this).attr("type") == "radio") //单选
            {
                if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                    attritems += "+" + $(this).attr("myname");
                    str += aname + "?" + $(this).attr("myname") + "^" + $(this).attr("value") + "#"; //拼接一个加料
                    tempprice += parseFloat($(this).attr("value")); //累加加料加价


                }
            }
            else {
                if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                    attritems += "+" + $(this).attr("myname");
                    if (x == 0) {
                        str += aname + "?" + $(this).attr("myname") + "^" + $(this).attr("value") + ""; //拼接一个加料
                    }
                    else {
                        str += "@" + $(this).attr("myname") + "^" + $(this).attr("value") + ""; //拼接一个加料
                    }
                    tempprice += parseFloat($(this).attr("value")) //累加加料加价
                    x = 1;
                }
            }
        })
        if (x == 1) {
            str += "#";
        }
    }
    //去最后一个'#';
    var h =str.replace(/#$/, "");
    if (attritems.length > 0) {
        attritems = "(" + attritems + ")";
    }

    return { info: h, addp: tempprice, addnames: attritems };
}




function onclick_my_cart() {
    if (parseInt(orderdetail.totalNumber) > 0) {
        $(".my_cart").hide();  //下方购物车
        $(".my_cart_other").show();  //上方购物车
        $(".mDialog_cart").show();   //列表
        $(".mModal").show();
        $(".clear_cart").show();

        my_cart_data();
    }
}
//获取购物车列表
function my_cart_data() {
    if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
        var productlist = cart.getproductlist();
        var art_addhtml = $("#catfoodlist").render(productlist);
        $(".my_cart_con").html(art_addhtml);
        num2.html(orderdetail.totalNumber + "");
        if (orderdetail.totalNumber == 0) {
            $(".my_cart").show();  //下方购物车
            $(".my_cart_other").hide();  //上方购物车
            $(".mDialog_cart").hide();   //列表
            $(".mModal").hide();
            $(".clear_cart").hide();
            var objpath = window.location + "";
            if (objpath.toLowerCase().indexOf("orderdetail") > 0) {
                location.href = location.href;
            }

        }
    } else {
        $(".my_cart").show();  //下方购物车
        $(".my_cart_other").hide();  //上方购物车
        $(".mDialog_cart").hide();   //列表
        $(".mModal").hide();
    }

    var mDialog_cart_height = $("#mDialog_cart").height();

    $("#my_cart_other").css({"bottom":(mDialog_cart_height+20) +"px"});

}




function additem(object, index) {
    var $this = $(object).parent();
    var dataid = $this.attr("data-id");
    var name = $this.attr("data-name");


    var cart_food_box = $("#cart_food_" + index);
    var count = parseInt(cart_food_box.html());
    cart.addnum(dataid, name, 1);
    cart_food_box.html((parseInt(count) + 1) + "");
    initcart();
}

function subitem(object, index) {
    var $this = $(object).parent();
    var dataid = $this.attr("data-id");

    var name = $this.attr("data-name");
  
    var cart_food_box = $("#cart_food_" + index);
    var count = parseInt(cart_food_box.html());
    if (count <= 1) {
        count = 0;
        cart.deleteproduct(dataid, name);
        $this.parent().parent().remove();
    }
    else {
        count = parseInt(count) - 1;
        cart.addnum(dataid, name, -1);

    }
    cart_food_box.html(count + "");
    initcart();
}