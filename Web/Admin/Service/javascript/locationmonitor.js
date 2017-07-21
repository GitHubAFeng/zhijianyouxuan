/// <reference path="../../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />

var shopjson = null;

//显示所有商家
function showallshop()
{
    var cbshop = $("#cbshop").attr("checked");
    if (cbshop) {
        LoadShops();
    }
    else {
        delShopMaker();
    }
}

//
function showalldeliver()
{
    var cbdeliver = $("#cbdeliver").attr("checked");
    if (cbdeliver) {
        LoadDeliver(9);
    }
    else {
        delDeliverMaker();
    }
}

//删除所有配送员标注
function delDeliverMaker()
{
    for (var i in markerlist) {
        map.removeOverlay(markerlist[i]);
    }
    
}

//删除所有商家标注
function delShopMaker() {
    for (var i in shopmarkerlist) {
        map.removeOverlay(shopmarkerlist[i]);
    }

}


function LoadShops() {
    var hfcityid = $("#hfcityid").val();
    shopmarkerlist.length = 0;
    $.ajax({
        type: "get",
        url: "../ajax/getallshops.aspx",
        cache: false,
        dataType: "html",
        data: "cid=" + hfcityid,
        beforeSend: function (XMLHttpRequest) {
            loading();
        },
        success: function (data, textStatus) {
           
            //显示所有配送员
            var shops = eval("(" + data + ")");

            shopjson = shops;

            for (var i = 0; i < shops.length; i++) {

                var shoppoint = new BMap.Point(shops[i].Lng, shops[i].Lat); // 创建点坐标
                var shopmarker = new BMap.Marker(shoppoint);
                var shoplabel = new BMap.Label("商家：" + shops[i].TogoName, { offset: new BMap.Size(-28, -22) });
                shoplabel.setStyle({ color: "blue", fontSize: "12px", padding: "2px 5px 2px 5px" });
                shopmarker.setLabel(shoplabel);

                shopmarker.setTitle(shops[i].Unid);
                map.addOverlay(shopmarker);
            

                shopmarker.addEventListener("click", function (e) {
                    showShopBox(this.getTitle(), this);
                });

                shopmarkerlist.push(shopmarker);


               
            }

        },
        complete: function (XMLHttpRequest, textStatus) {
            //$("#loading").html("加载完成");
        },
        error: function () {
            //请求出错处理
        }

    });

    return false;

}


//显示商家信息框
function showShopBox(shopid, shopmarker) {
    for (var i in shopjson)
    {
        if (shopjson[i].Unid == shopid)
        {
            var d_point = new BMap.Point(shopjson[i].Lng, shopjson[i].Lat); // 创建点坐标
            var opts = {
                width: 350,     // 信息窗口宽度  
                height: 100    // 信息窗口高度
            }

            var winhtml = "<div>";
            winhtml += ' <table width="330px" border="0" cellpadding="0" cellspacing="0" class="delivertipbox">'
            winhtml += ' <tr><td colspan="2">' + shopjson[i].TogoName + '(' + shopjson[i].State + ')' + '</td></tr>'
            winhtml += ' <tr><td colspan="2">电话：' + shopjson[i].Comm + '</td></tr>'
            winhtml += ' <tr><td colspan="2">地址：' + shopjson[i].Address + '</td></tr>'
            winhtml += " </table></div>";

            var infoWindow = new BMap.InfoWindow(winhtml, opts);  // 创建信息窗口对象

            shopmarker.openInfoWindow(infoWindow)

            map.panTo(d_point);
        }
    }
}




//通知事件
function loadData() {
    //intidata;

    LoadDeliver(9);

    var url = "/ajaxHandler.ashx";
    var para = "t=" + new Date().getTime() + "&method=businessmonitor&cityid=" + $("#hfcityid").val();
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function (msg) {
            
            var json = eval("(" + msg + ")");

            //id          开始 结束 小数位数 延迟
            var demo = new CountUp("riderCount", 0, json.rider.quota, 0, 2);
            demo.start();
            riderCount1.html(json.rider.quota1 + "");

            demo = new CountUp("timeoutCount", 0, json.timeout.quota, 1, 2);
            demo.start();
            timeoutCount1.html(json.timeout.quota1 + "");

            demo = new CountUp("loadCount", 0, json.loadCount.quota, 2, 2);
            demo.start();
            loadCount1.html(json.loadCount.quota1 + "");

            demo = new CountUp("totalCount", 0, json.total.quota, 0, 2);
            demo.start();
            totalCount1.html(json.total.quota1 + "");
        }
    })


}