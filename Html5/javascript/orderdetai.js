/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

///提交订单
function checkorder() {
    var tbname = $("#tbname").html();
    if (tbname == "") {
        alert("请输入您的称呼.");
        //$("#nofoodnotice").html("请输入您的称呼.");
        //$("#nofoodnotice").popup("open");
        return false;
    }


    var tbtel = $("#tbtel").html();
    if (tbtel == undefined) {//说明没有地址 2015-12-09
        alert("亲，新增地址没有填呦，没法配送噢！")
        return false;
    }
    if (tbtel == "") {
        alert("请输入您的电话.")
        return false;
    }
    var myreg = /^1\d{10}$/;
    if (!myreg.test(tbtel)) {
        alert("手机(电话)号码格式错误.")
        return false;
    }


    var ddltime = $("#ddltime").val();
    if (ddltime == "当前不配送") {
        alert("当前不配送.")
        return false;
    }

    var tbaddress = $("#tbaddress").html();
    if (tbaddress == "") {
        alert("请输入您的详细地址.")
        //$("#nofoodnotice").html("请输入您的详细地址.");
        //$("#nofoodnotice").popup("open");
        return false;
    }
    var paymodel = $("input[name=ddlpaymode]:checked").val();
    if (paymodel == "3") {

        var tbpaypwd = $("#tbpaypwd").val();
        if (tbpaypwd == "") {
            alert("请输入支付密码.")
            return false;
        }
    }


    //保存用户信息
    orderdetail = {
        username: tbname,
        phone: tbtel,
        address: tbaddress
    }
    cart.saveUserInfo(orderdetail);
    $("#btsubmit").hide();
    return true;
}

$(function () {

    var foodprice = 0;

    //获取所有购物车中的商品
    var ShoppingCart = utils.getParam("ShoppingCart");
    if (ShoppingCart != null && ShoppingCart != "") {
        $("#hfproductjson").val(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = cart.getproductlist();
        $(".cart-num").html(orderdetail.totalNumber + "");

        foodprice = parseFloat(orderdetail.totalAmount);
        var sendfree = request("sendfree");
        if (sendfree.length == 0) {
            sendfree = "0";
        }
        sendfree = parseFloat(sendfree);

        $(".money_num").html("￥" + (foodprice + sendfree).toFixed(1));
    }
    else {
        window.location = "ShowTogo.aspx?id=" + request("id");
    }

    $("#tbmydate").click(function () {
        $(".ui-input-datebox a").click();
    })

    //读取用户信息
    cart.getUserInfo();
    var tbname = $("#tbname").val() + "";
    if (orderdetail.username != "" && tbname == "") {
        $("#tbname").val(orderdetail.username);
        $("#tbtel").val(orderdetail.phone);
        $("#tbaddress").val(orderdetail.address);
    }

    $(".order-title").click(function () {
        $(this).find("label")
            .removeClass("no-checked")
            .addClass("checked").parent().parent().siblings()
            .find("label")
            .removeClass("checked")
            .addClass("no-checked");
        //当点击时将intput设置为选中并且将其他input设置为未选中
        $(this).find("input[name=ddlpaymode]").attr("checked", true).parent().parent().siblings().find("input[name=ddlpaymode]").attr("checked", false);

        if ($(this).find("input[name=ddlpaymode]").val() == "3") {
            $(".order-psw").show();
        }
        else {
            $(".order-psw").hide();
        }
    })

    $("#btcartbox").click(function () {
        var url = "cartdetail.aspx?id=" + request("id");
        window.location = url;
        return false;
    });

    $("#btsubmit").click(function () {

        var flag = checkorder();
        if (false == flag) {
            return false;
        }

        WeUI.showLoading();
        var options = {
            url: 'orderdetail.aspx?method=addorder&id=' + request("id"),
            type: 'post',
            dataType: 'text',
            data: $("#form1").serialize(),
            success: function (data) {
               

                var json = eval("(" + data + ")");
                if (json.state == "1") {

                    //如果是微信支付，直接开始支付

                    // alert(json.data.paydata.timeStamp);

                    localStorage.setItem("ShoppingCart", "");

                    if (json.data.paydata.timeStamp != null)//微信支付
                    {

                        setTimeout(function () {

                           var paydata =
                           {
                               "appId": json.data.paydata.appId, //公众号名称，由商户传入
                               "timeStamp": json.data.paydata.timeStamp, //时间戳
                               "nonceStr": json.data.paydata.nonceStr, //随机串
                               "package": json.data.paydata.packageValue,//扩展包
                               "signType": "MD5", //微信签名方式:md5
                               "paySign": json.data.paydata.paySign //微信签名
                           }

                            WeixinJSBridge.invoke('getBrandWCPayRequest', paydata, function (res) {
                                if (res.err_msg == "get_brand_wcpay_request:ok") {

                                    window.location = "/showtogoorder.aspx?id=" + orderid;

                                }
                                else {
                                    alert("支付失败，请联系客服");
                                }
                                // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                                //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
                            });


                        }, 2000);

                    }
                    else {
                        swal({
                            title: "温馨提示",
                            text: json.msg,
                            type: "success",
                            closeOnConfirm: false
                        },
                        function () {
                            window.location = json.data.url;
                        });
          
                    }


                    //删除购物车
                }
                else {
                    $("#btsubmit").show();
                    WeUI.hideLoadingslowly();
                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "warning"
                    });
                }

            }
        };
        $.ajax(options);
        return false;
    });

    $("#ddlpackage option").each(function () {
        if (foodprice < parseFloat($(this).attr("data-moneyline"))) {
            $(this).remove();
        }
    })




})

function addremark(id) {
    window.location.href = "remarkdetail.aspx?id=" + id;
}
