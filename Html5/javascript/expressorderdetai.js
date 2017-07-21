/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

///提交订单
function checkorder() {
  
    //商品名称，暂时没有
    //var tbname = $("#tbInve2").val();
    //if (tbname == "") {
    //    swal("", "请输入商品名称", "warning");
    //    return false;
    //}

    var paymodel = $("input[name=ddlpaymode]:checked").val();
    if (paymodel == "3") {
        var tbpaypwd = $("#tbpaypwd").val();
        if (tbpaypwd == "") {
            swal("", "请输入支付密码", "warning");

            return false;
        }
    }
    Loader.show("#btsubmit");

    return true;
}

$(function() {
   
    //初始化信息 
    express.getExpressinfo();
    if (expressInfo.tbAddress != "") {   
        $("#tbfuserinfo").html(expressInfo.tbAddress + "," + expressInfo.tbAddressdetail);
        $("#tbtuserinfo").html(expressInfo.tbOorderid + "," + expressInfo.tbOorderiddetail);
        $("#lbdistance").html(expressInfo.hiddistance);
        $("#lbsendfee").html(expressInfo.hidsendfee);

        
        $("#tbTotalPrice").html(expressInfo.tbTotalPrice+"元");
        $("#tbTel").html(expressInfo.tbTel);
        $("#tbSentTime").html(expressInfo.tbSentTime);
        $("#tbexpressinfo").val(JSON.stringify(expressInfo));

        $("#tballprice").html((parseFloat(expressInfo.hidsendfee) + parseFloat(expressInfo.tbTotalPrice)).toFixed(1));

        if (expressInfo.tbcallcount == "0") {
            $("#buybox").hide();
        }
    }
    else {
        swal({
            title: "",
            text: "请先输入收件,取件信息",
            type: "warning",
            confirmButtonText: "确定",
            closeOnConfirm: false
        },
        function () {
            gourl("express.aspx");
        });
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
        else
        {
            $(".order-psw").hide();
        }
    })

})
