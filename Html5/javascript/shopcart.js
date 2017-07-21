/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

$(function() {
    showproduct();

    //清空所有商品
    $("#clearcart_a").click(function() {
        cart.deleteallproduct();
        $("#allcountlb").html(0);
        $("#allpricelb").html(0);
        $("#submitcontianer").hide();

        $("#nofoodnotice").show();
        $("#productcontainer").remove();
    })

    function showproduct() {
        //获取所有购物车中的商品
        if (utils.getParam("ShoppingCart") != null && utils.getParam("ShoppingCart") != "") {
            var productlist = cart.getproductlist();

            var producthtml = "";
            for (var i = 0; i < productlist.length; i++) {
                var otherclass = "";
                if (i == 0) {
                    otherclass = " ui-corner-top ";
                }
                if (i == productlist.length - 1) {
                    otherclass = " ui-corner-bottom ui-li-last ";
                }
                producthtml += "  <li data-icon=\"false\" class=\"ui-li ui-li-static ui-btn-up-d " + otherclass + "\"><div class=\"ui-li-aside\" style='width:25%'>  ";
                producthtml += "<div class=\"wxdemo_number ui-corner-all ui-controlgroup ui-controlgroup-horizontal\" data-type=\"horizontal\" data-role=\"controlgroup\"> <div class=\"ui-controlgroup-controls\"> "
                producthtml += " <a data-theme=\"green\" data-id=\"" + productlist[i].id + "\"  data-icon=\"delete\" data-role=\"button\" href=\"#\" data-corners=\"true\" data-shadow=\"true\" data-iconshadow=\"true\" data-wrapperels=\"span\" class=\"ui-btn ui-btn-up-green ui-btn-icon-left ui-corner-left ui-corner-right ui-controlgroup-last cartitem\"><span class=\"ui-btn-inner ui-corner-left ui-corner-right ui-controlgroup-last\"><span class=\"ui-btn-text\"></span><span class=\"ui-icon ui-icon-delete ui-icon-shadow\">&nbsp;</span></span></a> "
                producthtml += "  </div></div></div><p class=\"ui-li-desc\"><span class=\"wxdemo_name\">" + left(productlist[i].name,5) + "</span> <span class=\"wxdemo-red-text\">￥<b>" + productlist[i].price + "x" + productlist[i].num + "</b></span></p></li> ";

            }

            $("#productcontainer").html(producthtml);
            $("#allcountlb").html(orderdetail.totalNumber + "");
            $("#allpricelb").html(orderdetail.totalAmount + "");

            $(".cartitem").bind('click', function(e) {
                var dataid = $(this).attr("data-id");
                cart.deleteproduct(dataid);
                showproduct();
            });
        }
    }

    function left(data, count) {
        if (data.toString().length > count) {
            return data.toString().substr(0, count) + "...";
        }
        else {
            return data;
        }
    }
})

