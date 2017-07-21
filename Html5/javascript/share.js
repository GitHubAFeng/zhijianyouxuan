
var shareData = {
    title: '',
    desc: '',
    link: '',
    imgUrl: ''
};



wx.ready(function () {


    alert(22);

    var title = $("#hfsharetitle").val();
    var desc = $("#hfsharecontent").val();
    var link = $("#shareurl").val();
    var imgurl = "http://" + location.host + "/images/logo.png";


    shareData = {
        title: title,
        desc: desc,
        link: link,
        imgUrl: imgurl
    };


    wx.onMenuShareAppMessage({
        title: title,
        desc: desc,
        link: link,
        imgUrl: imgurl,
        trigger: function (res) {
            // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
            //alert('用户点击发送给朋友');
        },
        success: function (res) {
            //alert('已分享');
            useSharepackage();
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareTimeline({
        title: title,
        desc: desc,
        link: link,
        imgUrl: imgurl,
        trigger: function (res) {
            // 不要尝试在trigger中使用ajax异步请求修改本次分享的内容，因为客户端分享操作是一个同步操作，这时候使用ajax的回包会还没有返回
            //alert('用户点击发送给朋友');
        },
        success: function (res) {
            //alert('已分享');
            useSharepackage();
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });

});

wx.error(function (res) {
    alert(res.errMsg);
});


//分享红包数量减少
function useSharepackage() {
    jQuery.ajax(
    {
        type: "post",
        url: "/ajaxHandler.ashx",
        data: "method=useSharepackage&id=" + request("id") + "&_=" + new Date().getTime() + "",
        success: function (msg) {
            var count = parseInt($("#pcount").html());
            if (count <= 1) {
                $(".my_order_list").hide();
            }
            $("#pcount").html(count - 1);

        }
    })
}
