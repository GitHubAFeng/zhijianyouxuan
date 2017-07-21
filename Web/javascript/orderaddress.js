/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

/*
    用户提交订单保存的地址信息
*/

var utils =
{
    setParam: function (name, value) {
        handlecookie(name, value, { expires: 365, path: "/", secure: false });
    },
    getParam: function (name) {
        var data = handlecookie(name);
        if (data == null || data == "" || data == undefined) {
            data = "";
        }
        return data;
    }
}

var address =
{
    username: "",
    phone: "",
    address: ""
};

var historyaddress = {
    //保存用户信息
    saveUserInfo: function (userifno) {
        var jsonstr = { "username": userifno.username, "phone": userifno.phone, "address": userifno.address };
        utils.setParam("orderaddress", escape(JSON.stringify(jsonstr)));
    },
    //保存用户信息
    getUserInfo: function () {
        var UserInfo = unescape(handlecookie("orderaddress"));
     
        if (UserInfo != null && UserInfo != "null" && UserInfo != "") {
            var jsonstr = JSON.parse(UserInfo);
            address.username = jsonstr.username;
            address.phone = jsonstr.phone;
            address.address = jsonstr.address;
        }
    }
};

$(document).ready(function () {  
    //读取用户信息
    historyaddress.getUserInfo();
    if (address.username != "") {
        $("#tbname").val(address.username);
        $("#tbtel").val(address.phone);
        $("#tbdetailaddress").val(address.address);
    }

})