/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var app = angular.module("hjfoodapp", []);
app.controller("foodCtrl", function ($scope) {
    $scope.shopcart =
    {
        foods:[],
        minimoney: $('#hfminimoney').val(),
        sendfree: $("#hfsendfree").val(),
        AllCount: 0,
        AllPirce: 0
    }
    $scope.addfood = function (foodid) {
        var $this = $("#btaddcart");
        var dataid = $this.attr("data-foodid");
        var name = $this.attr("data-name");
        var price = $this.attr("data-price");
        var weekday = $this.attr("data-sortid"); //保存分类编号
        var allnum = 0;

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


    $scope.getContAndPrice = function () {
        if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
            $scope.shopcart.foods = cart.getproductlist();
        }
        $scope.shopcart.AllCount = orderdetail.totalNumber;
        $scope.shopcart.AllPirce = orderdetail.totalAmount;
    };
    $scope.getContAndPrice();
});

