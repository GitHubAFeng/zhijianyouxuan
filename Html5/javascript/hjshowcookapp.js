/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var app = angular.module("hjfoodapp", []);
app.controller("foodCtrl", function ($scope) {
    $scope.shopcart =
    {
        foods: eval("(" + $("#hffoodjson").val() + ")"),
        minimoney: $('#hfminimoney').val(),
        sendfree: $("#hfsendfree").val(),
        AllCount: 0,
        AllPirce: 0
    }
    //把选中的菜谱加到购物车
    $scope.add = function () {
        $(".fooditem").each(function ()
        {
            //debugger;
            var ischecked = $(this).attr("checked");
            if (ischecked == true || ischecked == "checked") {
                for (var x in $scope.shopcart.foods) {
                    if ($(this).attr("data-id") == $scope.shopcart.foods[x].id && $scope.shopcart.foods[x].num > 0) {
                        
                        //调用购物车相关
                        product = {
                            id: $scope.shopcart.foods[x].id,
                            name: $scope.shopcart.foods[x].name,
                            number: $scope.shopcart.foods[x].num,
                            price: $scope.shopcart.foods[x].price,
                            weekday: $scope.shopcart.foods[x].weekday,
                            foodtype: $scope.shopcart.foods[x].foodtype
                        }
                        cart.addproduct(product);

                        break;
                    }
                }
            }
        })

        $scope.getContAndPrice();

        swal("", "已加入购物车", "success", { confirmButtonText: "关闭" });
    }

    $scope.subcount = function (food,foodid) {
        if (food.num > 0) {
            food.num -= 1;
        }
    }

    $scope.getContAndPrice = function () {


        if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
            var productlist = cart.getproductlist();
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
        gourl("shopcart.aspx");
    };


    $scope.getContAndPrice();
});

