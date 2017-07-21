/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />


//定位相关类
$(function () {

    //初始化信息 
    express.getExpressinfo();
    if (expressInfo.tbAddress != "") {
        $("#lbdistance").html(expressInfo.hiddistance);
        $("#lbsendfee").html(expressInfo.hidsendfee);
        if (expressInfo.tbcallcount == "0") {
            $("#buybox").hide();
        }

        $("#tbTel").val(expressInfo.tbTel);
        $("#tbRemark").val(expressInfo.tbRemark);

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

})


//转到下一步
function nextstep() {

    var tbusername = $("#tbusername").val();
    if (tbusername.length == 0) {
        swal("", "请输入发件人", "warning");
        return false;;
    }
    var tbTel = $("#tbTel").val();
    if (tbTel.length == 0) {
        swal("", "请输入发件人手机", "warning");
        return false;;
    }
    var tbcallmsg = $("#tbcallmsg").val();
    if (tbcallmsg.length == 0) {
        swal("", "请输入收件人", "warning");
        return false;;
    }
    var tbReveVar = $("#tbReveVar").val();
    if (tbTel.length == 0) {
        swal("", "请输入收件人手机", "warning");
        return false;;
    }

    //var tbTotalPrice = $("#tbTotalPrice").val();
    //if (expressInfo.tbcallcount == "1" && (tbTotalPrice.length == 0 || tbTotalPrice == "0")) {
    //    swal("", "请输入代买商品价格", "warning");
    //    return false;;
    //}


    expressInfo.tbusername = tbusername;
    expressInfo.tbTel = tbTel;
    expressInfo.tbcallmsg = tbcallmsg;
    expressInfo.tbReveVar = tbReveVar;
    expressInfo.tbRemark = $("#tbRemark").val();
    //expressInfo.tbTotalPrice = tbTotalPrice;
    expressInfo.tbTotalPrice = 0;

    var flag = j_submitdata("orderdetail");
    if (false == flag) {
        return false;
    }

    Loader.show("#btnextstep");

    express.saveExpressinfo(expressInfo);

    var url = "submitexpressorder.aspx";
    window.location = url;

    return false;

}


