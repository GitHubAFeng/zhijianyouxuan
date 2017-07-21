/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
utils = {
    setParam: function (name, value) {
        localStorage.setItem(name, value)
    },
    getParam: function (name) {
        return localStorage.getItem(name)
    }
}

product = {
    id: 0,
    name: "",
    number: 0,
    price: 0.00,
    material: "",
    sid: 0,
    packagefee: 0.0,
    addprice: 0.0,
    sname: "",
};
orderdetail = {
    username: "",
    phone: "",
    address: "",
    zipcode: "",
    shopid: 0,
    totalNumber: 0,
    totalAmount: 0.00,
    packagefee: 0.00
}
cart = {
    //向购物车中添加商品

    addproduct: function (product) {
        var shopid = request("id");
        var ShoppingCart = utils.getParam("ShoppingCart");
        if (ShoppingCart == null || ShoppingCart == "") {

            var jsonstr = { "productlist": [], "totalNumber": product.number, "shopid": shopid, "totalAmount": 0 };



            orderdetail.totalNumber = product.number;
            orderdetail.totalAmount = (product.price + product.addprice+product.packagefee) * product.number
            orderdetail.shopid = shopid;

            jsonstr.totalAmount = orderdetail.totalAmount;
            jsonstr.productlist.push(product);
            utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));

        } else {
            var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
            var productlist = jsonstr.productlist;
            var result = false;
            for (var i in productlist) {
                if (productlist[i].name == product.name) {
                    productlist[i].number = parseInt(productlist[i].number) + parseInt(product.number);
                    result = true;
                }
            }
            if (!result) {
                productlist.push(product);
            }

           

            utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));
        }

        cart.calculate();

    },

    //修改给买商品数量
    updateproductnum: function (id, num) {
        var ShoppingCart = utils.getParam("ShoppingCart");
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;

        for (var i in productlist) {
            if (productlist[i].id == id) {
                productlist[i].number = parseInt(num);
                utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));
            }
        }
        cart.calculate();
    },


    //加减份数
    addnum: function (id,name, num) {
        var ShoppingCart = utils.getParam("ShoppingCart");
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;

        for (var i in productlist) {
            if (productlist[i].id == id && productlist[i].name == name) {
                productlist[i].number += parseInt(num);
                utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));
            }
        }
        cart.calculate();
    },



    //获取购物车中的所有商品
    getproductlist: function () {
        var ShoppingCart = utils.getParam("ShoppingCart");
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;

        cart.calculate();
        
        return productlist;
    },
    //判断购物车中是否存在商品
    existproduct: function (id) {
        var ShoppingCart = utils.getParam("ShoppingCart");
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;
        var result = false;
        for (var i in productlist) {
            if (productlist[i].id == product.id) {
                result = true;
            }
        }
        return result;
    },
    //删除所有菜品
    deleteallproduct: function () {
        utils.setParam("ShoppingCart", "");
    },
    //保存用户信息
    saveUserInfo: function (userifno) {
        var jsonstr = { "username": userifno.username, "phone": userifno.phone, "address": userifno.address };
        //这里保存cookie , localStorage有时会没有
        handlecookie("userjson", escape(JSON.stringify(jsonstr)), { expires: 1, path: "/", secure: false });
        utils.setParam("UserInfo", JSON.stringify(jsonstr));
    },
    //保存用户信息
    getUserInfo: function () {
        var UserInfo = utils.getParam("UserInfo");
        if (UserInfo == null || UserInfo == "") {
            UserInfo = unescape(handlecookie("userjson"));
        }
        if (UserInfo != null && UserInfo != "null" && UserInfo != "") {
            var jsonstr = JSON.parse(UserInfo);
            orderdetail.username = jsonstr.username;
            orderdetail.phone = jsonstr.phone;
            orderdetail.address = jsonstr.address;
        }
    },

    //计算
    calculate: function () {
        var ShoppingCart = utils.getParam("ShoppingCart");
        if (ShoppingCart == null || ShoppingCart == "") {
            orderdetail.totalNumber = 0;
            orderdetail.totalAmount = 0;
            orderdetail.shopid = 0;
            return;

        }
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;

        var totalNumber = 0;
        var totalAmount = 0;
        var packagefee = 0;

        for (var i in productlist) {
            totalNumber += parseInt(productlist[i].number);
            totalAmount += productlist[i].number * parseFloat(productlist[i].price + productlist[i].addprice + productlist[i].packagefee);

            packagefee += productlist[i].number * parseFloat(productlist[i].packagefee);
        }
        orderdetail.totalNumber = totalNumber
        orderdetail.totalAmount = totalAmount;
        orderdetail.shopid = jsonstr.shopid;
        orderdetail.packagefee = packagefee;
        return;
    },


    //删除购物车中商品
    deleteproduct: function (id,name) {
        //debugger;
        var ShoppingCart = utils.getParam("ShoppingCart");
        var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
        var productlist = jsonstr.productlist;
        var list = [];
        for (var i in productlist) {
            if (productlist[i].name != name)
            {
                list.push(productlist[i]);
            }
        }
        jsonstr.productlist = list;
      
        utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));

        cart.calculate();

    },
    //购物车中商品减一操作
    delproduct: function (product) {
        var ShoppingCart = utils.getParam("ShoppingCart");
        if (ShoppingCart == null || ShoppingCart == "") {
            
        }
        else {
            var jsonstr = JSON.parse(ShoppingCart.substr(1, ShoppingCart.length));
            var productlist = jsonstr.productlist;
            for (var i in productlist) {
                if (productlist[i].name == product.name) {
                    productlist[i].number = parseInt(productlist[i].number) - parseInt(product.number);
                }
            }
           

            utils.setParam("ShoppingCart", "'" + JSON.stringify(jsonstr));
        }

        cart.calculate();
    }


};



function left(data, count) {

    if (data.toString().length > count) {
        return data.toString().substr(0, count) + "...";
    }

    else {
        return data;
    }
}


expressInfo = {
    hidflat: "",
    hidflng: "",
    hidtlat: "",
    hidtlng: "",

    hiddistance: "",
    hidsendfee: "",

    tbAddress: "",
    tbTel: "",
    tbUserName: "",
    tbAddressdetail: "",

    tbOorderid: "",//请输入收件人地址
    tbReveVar: "",//请输入收件人电话
    tbcallmsg: "",//请输入收件人姓名
    tbOorderiddetail: "",//取件详情地址

    tbSentTime: "",//取件时间
    tbcallcount: "",//类型 0表示代取，1表示代买

    tbRemark: "",//商品备注
    tbTotalPrice: "0",//商品费用
    cityid: "0",//城市编号

}

express = {
    //删除跑腿信息
    delExpressinfo: function () {
        utils.setParam("expressinfo", "");
        handlecookie("expressinfo", "", { expires: 1, path: "/", secure: false });
    },
    //保存用户信息
    saveExpressinfo: function (expressInfo) {
        //这里保存cookie , localStorage有时会没有
        handlecookie("expressinfo", escape(JSON.stringify(expressInfo)), { expires: 1, path: "/", secure: false });
        utils.setParam("expressinfo", JSON.stringify(expressInfo));
    },
    //保存用户信息
    getExpressinfo: function () {
        var UserInfo = utils.getParam("expressinfo");
        if (UserInfo == null || UserInfo == "") {
            UserInfo = unescape(handlecookie("expressinfo"));
        }
        if (UserInfo != null && UserInfo != "null" && UserInfo != "") {
            expressInfo = JSON.parse(UserInfo);
        }
    }
};