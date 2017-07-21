
$(document).ready(function () {
    var collect = $("#collect").val();
    if (collect == "1") {
        $("#iscollect").addClass("top_stop");
    }
    else {
        $("#iscollect").addClass("top_star");
    }

    $(".top_right").click(function () {
        var tid = $("#tid").val();
        var collect = $("#collect").val();
        addtogo(tid, collect);
    })
    function addtogo(togoid, bid) {
        var uid = $("#hidUid").val();
        if (uid == "0") {
            window.location.href = "login.aspx?returnurl=" + window.location.href;
        }

        jQuery.ajax(
        {
            type: "post",
            url: "ajaxHandler.ashx",
            data: "method=addtogo&userid=" + uid + "&togoid=" + togoid + "&bid=" + bid,
            success: function (msg) {
                if (msg == "1") {
                  
                    sweetAlert("", "收藏成功!", "success");

                    $("#collect").val(1);
                    $("#iscollect").removeClass("top_star").addClass("top_stop");
                }
                else if (msg == "0") {
                    sweetAlert("", "收藏失败!", "error");
                }
                else if (msg == "2") {
                    sweetAlert("", "取消收藏成功!", "success");
                    $("#collect").val(0);
                    $("#iscollect").addClass("top_star").removeClass("top_stop");
                }
                else {
                    alert(msg);
                }
            }
        })
    }
})
