/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var app = angular.module("hjshopcartapp", []);
app.controller("cartCtrl", function ($scope) {
    $scope.shopcart =
    {
        foods:[],
        AllCount: 0,
        minimoney: $('#hfminimoney').val(),
        sendfree: $("#hfsendfree").val(),
        isdisabled: true,
        btbgColor: {"background-color":"#ccc"},
        AllPirce: 0
    }
    $scope.addfood = function (foodid) {
        var $this = $("#handlerbox_" + foodid);
        var dataid = $this.attr("data-foodid");
        var name = $this.attr("data-name");
        var price = $this.attr("data-price");
        var weekday = $this.attr("data-sortid"); //保存分类编号
        var allnum = 0;
        var box = $("#box_" + dataid);
        var count = box.html();


        //调用购物车相关
        product = {
            id: dataid,
            name: name,
            number: 1,
            price: price,
            weekday: weekday,
            foodtype: allnum
        }
        cart.addproduct(product);
        $scope.getContAndPrice();

    }
    $scope.subfood = function (foodid) {

        var $this = $("#handlerbox_" + foodid);
        var dataid = $this.attr("data-foodid");
        var name = $this.attr("data-name");
        var price = $this.attr("data-price");
        var weekday = $this.attr("data-sortid"); //保存分类编号
        var box = $("#box_" + dataid);
        var count = parseInt(box.html());
        if (count == 0) {
            return;
        }


        if (count <= 1) {
            count = 0;
            cart.deleteproduct(dataid);
        }
        else {
            count = parseInt(count) - 1;
            //调用购物车相关
            product = {
                id: dataid,
                name: name,
                number: 1,
                price: price
            }
            cart.delproduct(product);

        }

        $scope.getContAndPrice();
    }

    $scope.getContAndPrice = function () {
        if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
            $scope.shopcart.foods = cart.getproductlist();
        }
        $scope.shopcart.AllCount = orderdetail.totalNumber;
        $scope.shopcart.AllPirce = orderdetail.totalAmount;
        if ($scope.shopcart.AllPirce > $scope.shopcart.minimoney) {
            $scope.shopcart.isdisabled = false;
            $scope.shopcart.btbgColor = { "background-color": "#4eba58" };
        }
        else {
            $scope.shopcart.isdisabled = true;
            $scope.shopcart.btbgColor = { "background-color": "#ccc" };
        }
    };
    $scope.showcart = function () {
        gourl("orderdetail.aspx");
    };

    $scope.getContAndPrice();
});

