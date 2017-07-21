
function Show(sender) {
    var togoid = document.getElementById("hidTogoId").value;
    jQuery.ajax(
            {
                type: "post",
                url: "../Ajax/gettogoinof.aspx",
                data: "togoid=" + togoid + "&t=" + new Date().getTime(),
                success: function(msg) {
                    if (msg == "-1") {
                        jQuery("#test").html("获取商家信息失败。");
                    }
                    else {
                        jQuery("#test").html(msg);
                    }
                }
            })
    document.getElementById("test").style.display = "block";
    var leftpos = 0, toppos = 0;
    var pObject = sender.offsetParent;
    if (pObject) {
        leftpos += pObject.offsetLeft;
        toppos += pObject.offsetTop;
    }
    while (pObject = pObject.offsetParent) {
        leftpos += pObject.offsetLeft;
        toppos += pObject.offsetTop;
    };
    document.getElementById("test").style.left = (sender.offsetLeft + leftpos) + "px";
    document.getElementById("test").style.top = (sender.offsetTop + toppos + sender.offsetHeight - 2) + "px";

}
function Hide(sender) {
    document.getElementById("test").style.display = "none";
}

function getorderInfo() {
    var v = document.getElementById("ttorderInof").value;
    copy(v);
}