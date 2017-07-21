/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

/*
    历史地址js
    用户历史地址，点过的就保存，保存最新3个
    先进先出
*/

var utils =
{
    setParam: function (name, value) {
        name = "historyaddress";
        handlecookie(name, value, { expires: 365, path: "/", secure: false });
    },
    getParam: function (name) {
        name = "historyaddress";
        var data = handlecookie(name);
        if (data == null || data == "" || data == undefined) {
            data = "";
        }
        return data;
    }
}

var address =
{
    time: "",
    url: "",
    lat: "",
    lng: "",
    label: ""
};

var historyaddress = {
    add: function (address)
    {
        var historyaddress = utils.getParam("historyaddress");

        if (historyaddress == null || historyaddress == "") {
            var jsonstr = { "productlist": [{ "url": address.url, "label": address.label, "lat": address.lat, "lng": address.lng, "time": address.time }] };
            utils.setParam("historyaddress", "'" + JSON.stringify(jsonstr));
        }
        else {
            var jsonstr = JSON.parse(historyaddress.substr(1, historyaddress.length));
            var productlist = jsonstr.productlist;
            var result = false;
            for (var i in productlist) {
                if (productlist[i].lat == address.lat && productlist[i].lng == address.lng) {
                    result = true;
                }
            }
            if (!result) {
                productlist.push({ "url": address.url, "label": address.label, "lat": address.lat, "lng": address.lng, "time": address.time });
            }

            var last3address = new Array();
            var j = 0;
         
            for (var i = productlist.length-1,j=0; i >=0 && j < 3; i--,j++) {
                last3address.push(productlist[i])
            }
            jsonstr.productlist = last3address;

            utils.setParam("historyaddress", "'" + JSON.stringify(jsonstr));
        }
    },
    //获取购物车中的所有商
    getproductlist: function () {
        var historyaddress = utils.getParam("");
        var jsonstr = JSON.parse(historyaddress.substr(1, historyaddress.length));
        var productlist = jsonstr.productlist;
        return productlist;
    },
    //删除所有地址
    deleteallproduct: function () {
        utils.setParam("historyaddress", "");
    }
};

$(document).ready(function () {
    
    var hisaddressbox = $(".history_option").find("ul");

    if (utils.getParam("") != "") {
        var productlist = historyaddress.getproductlist();
        for (var i in productlist) {
            productlist[i].label = decodeURI(decodeURI(productlist[i].label));   
        }
        var addhtml = $("#historyaddressTemplate").render(productlist); 
        hisaddressbox.append(addhtml);
    }

    if (hisaddressbox.find("li").length == 0) {
        hisaddressbox.append($("#noaddressTemplate").render({}));
    }
})