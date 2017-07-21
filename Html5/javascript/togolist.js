/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

$(function() {
    var lat = request("lat");
    var lng = request("lng");
    var addr = request("address");
    var hfod = $("#hfod").val();
    $("." + hfod).addClass("hover");

    $("#ShowMore").click(function() {
        //只有一页或者没有
        var allpage = $("#hfpage").val();
        if (parseInt(allpage) > 1) {
            showLoader();
        }

        setTimeout(function() {
            //这里就是异步获取内容的地方，这里简化成一句话，可以根据需要修改
            var page = $("#hfcpage").val();
            var allpage = $("#hfpage").val();
            if (page >= parseInt(allpage)) {
                //$("#Loadding").html("the end！");
                $("#ShowMore").hide();
                hideLoader();
                return;
            }

            var currentpage = parseInt(page) + 1;
            var mydata = "method=loadshop&pageindex=" + currentpage + "&lat=" + lat + "&lng=" + lng;
            jQuery.ajax
                ({
                    type: "post",
                    url: "/ajaxHandler.ashx",
                    dataType: "json",
                    data: mydata,
                    success: function(data) {
                        shoplisthandlier(data);
                        $("#hfcpage").val(currentpage + "");

                        if (currentpage >= parseInt(allpage)) {
                            $("#ShowMore").hide();
                        }

                    }
                })

        }, 1000);

    })

    function shoplisthandlier(data) {
        if (typeof (data) == "object" && typeof (data.shoplist) == "object") {
            var str = "";
            $.each(data.shoplist, function(i, item) {
                str += " <li data-corners=\"false\" data-shadow=\"false\" data-iconshadow=\"true\" data-wrapperels=\"div\" data-icon=\"arrow-r\" data-iconpos=\"right\" data-theme=\"d\"   ";
                str += " class=\"ui-btn ui-btn-icon-right ui-li-has-arrow ui-li ui-li-has-thumb ui-btn-up-d\"> <div class=\"ui-btn-inner ui-li\"> <div class=\"ui-btn-text\"> ";
                str += " <a href=\"" + item.Introduce + "\" data-ajax=\"false\" class=\"ui-link-inherit\"><div class=\"ui-li-aside\"><p class=\"wxdemo-" + (item.Status == "0" ? "gray2" : "orange") + "-text ui-li-desc\">" + (item.Status == "0" ? "休息中" : "营业中") + "</p></div> ";
                str += " <img width=\"70\" height=\"70\" src=\"" + item.Picture + "\" class=\"ui-li-thumb\"> ";
                str += " <h3 class=\"ui-li-heading\">" + item.TogoName + "</h3> ";
                str += " <p class=\"wxdemo-green-text ui-li-desc\">" + item.Remark + "元起送/" + item.Msn + "</p> ";

                str += "  </a></div><span class=\"ui-icon ui-icon-arrow-r ui-icon-shadow\">&nbsp;</span></div></li> ";

            })
            $("#shopContainer").append(str);
        }
        hideLoader();
    }


})

$("#showtype_button").click(function () {
    $("#moresortcontainer").removeClass("hide");
    var height = $("#moresortcontainer").height();
    var width = 100;
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    $("#moresortcontainer").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
})
