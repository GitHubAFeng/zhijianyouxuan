/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var app = angular.module("hjcookapp", []);
app.controller("cookCtrl", function ($scope) {
    $scope.foodsorts = eval("(" + $("#hffoodsortjson").val() + ")");
    $scope.shopcart =
    {
        AllCount: 0,
        minimoney: $('#hfminimoney').val(),
        sendfree: $("#hfsendfree").val(),
        isdisabled: true,
        btbgColor: {"background-color":"#ccc"},
        AllPirce: 0
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
    $scope.getCurSortName = function () {
        var sortid = request("sortid");
        if (sortid == "") {
            sortid = "0";
        }
        var curname = "全部";
        for (var i in $scope.foodsorts) {
            if ($scope.foodsorts[i].ID == sortid) {
                curname = $scope.foodsorts[i].classname;
                break;
            }
        }
        return curname;

    };
    $scope.showcart = function () {
        gourl("shopcart.aspx");
    };

    $scope.getContAndPrice();

});



$(function () {
    var sortid = request("sortid");
    if (sortid == "") {
        sortid = "0";
    }
    $("#sort_" + sortid).addClass("cur");
})
