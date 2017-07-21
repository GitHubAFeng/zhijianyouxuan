/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
//首页js

$(function () {
    initnav(1);
    //加载微博模块
    var url = $("#hfweiboshowurl").val();
    var $wbContent = $('<iframe width="100%" height="65" class="share_self"  frameborder="0" scrolling="no" src="' + url + '"></iframe>');
    $('#wbBox').html($wbContent);

    $(".tiptarget").click(function () {
        $.jBox.info('请用扫描关注右边微信二维码', '温馨提示');
    })
})

///首页搜索地址
function indexsearchAddress()
{
    var key = $("#tbindexsearchbox").val();
    if (key == "" || key == "请确定您的配送地址") {
        alert("请确定您的配送地址");
        return ;
    }
    window.location = "shoplist.aspx?addr=" + encodeURI(encodeURI(key)) + "&type=search";
}