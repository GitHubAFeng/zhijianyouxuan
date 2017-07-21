/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
var app = angular.module("hjapp", []);
app.controller("shopCtrl", function ($scope, $compile) {
    $scope.foodsorts = eval("(" + $("#hffoodsortjson").val() + ")");
    $scope.shopcart =
    {
        AllCount: 0,
        minimoney: $('#hfminimoney').val(),
        sendfree: $("#hfsendfree").val(),
        isdisabled: true,
        btbgColor: { "background-color": "#ccc" },
        AllPirce: 0
    }
    $scope.news = $("#hfnews").val();
    $scope.shownews = function () {
        alert($scope.news);
    }
    $scope.addfood = function (foodid, hasadd,isother) {
        debugger;
        if (hfstatus == "0") {
            alert("商家正在休息中，不能在线下单");
            return;
        }


        var $this = $("#handlerbox_" + foodid);
        var dataid = $this.attr("data-id");
        var name = $this.attr("data-name");
        var price = $this.attr("data-price");
        var weekday = $this.attr("data-sortid"); //保存分类编号
        var allnum = 0;
        var box = $("#box_" + dataid);
        var count = box.html();

        $scope.addfoodCountBySortID(weekday, 1);

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

        box.show();
        box.html((parseInt(count) + 1) + "");
        $scope.getContAndPrice();

        if (hasadd == 1) {
            swal({
                title: "",
                confirmButtonText: "关闭",
                text: $("#addfoodobx_" + foodid).html(),
                html: true
            });

            //新生成的元素在动态编译
            var html = $(".sweet-alert .addfood").html();
            var ele = $compile(html)($scope);
            $(".sweet-alert .addfood").html(ele);
        }

        if (isother == 1) {
            $(".sweet-alert .addfood .additem" + foodid).find("a").html("已点");
        }
    }
    $scope.subfood = function (foodid) {

        var $this = $("#handlerbox_" + foodid);
        var dataid = $this.attr("data-id");
        var name = $this.attr("data-name");
        var price = $this.attr("data-price");
        var weekday = $this.attr("data-sortid"); //保存分类编号
        var box = $("#box_" + dataid);
        var count = parseInt(box.html());
        if (count == 0) {
            return;
        }

        $scope.addfoodCountBySortID(weekday, -1);

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

        box.show();
        box.html(count + "");
        $scope.getContAndPrice();
    }
    $scope.addfoodCountBySortID = function (sortid, addcount) {
        for (var i in $scope.foodsorts) {
            if ($scope.foodsorts[i].SortID == sortid) {
                $scope.foodsorts[i].TogoNum += addcount;
                break;
            }
        }
    }
    $scope.InitSortCount = function () {
        if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
            var productlist = cart.getproductlist();
            for (var i = 0; i < productlist.length; i++) {
                var box = $("#box_" + productlist[i].id);
                box.html(productlist[i].num + "");
                box.show();

                for (var x in $scope.foodsorts) {
                    if ($scope.foodsorts[x].SortID == productlist[i].weekday) {
                        $scope.foodsorts[x].TogoNum += productlist[i].num;
                        break;
                    }
                }
            }
        }
        $scope.getContAndPrice();
    }
    $scope.getContAndPrice = function () {
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

    $scope.InitSortCount();
});

