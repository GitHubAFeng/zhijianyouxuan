//保存在spanbuild中,aid1,aid2,...
function mychange(aid, tag, name) {
    var spanbuild = $("#spanbuild").html() + "";
    var ids = $("#hfids").val() + "";
    var check = tag.checked;
    if (check == true) {
        if (ids == "") {
            $("#hfids").val(aid);
            $("#spanbuild").html(name)
        }
        else {
            $("#hfids").val(ids + "," + aid);
            $("#spanbuild").html(spanbuild + "," + name)
        }
    }
    else {
        var bnames = spanbuild.split(',');
        var bids = ids.split(',');
        var m = 0;
        var tempnams = "";
        var tempids = "";
        for (var i = 0; i < bids.length; i++) {
            if (aid == bids[i]) {
                m = i;
            }
            else {
                tempnams += bnames[i] + ",";
                tempids += bids[i] + ","
            }
        }
        var tn1 = tempnams.replace(/,$/, "");
        var ti1 = tempids.replace(/,$/, "");
        $("#hfids").val(ti1);
        $("#spanbuild").html(tn1)
    }
}

function checkb() {
    var ids = $("#hfids").val() + "";
    if (ids == "") {
        alert("请选择建筑物.");
        return false;
    }
    return true;
}


function getkeyFix() {
    var key = $("#bkey").val().trim();
    get4buildkey(key)
}

//名称，编号（自增） , 编号(100000000)
function get4buildkey(name) {
    jQuery.ajax(
    {
        type: "post",
        url: '../ajax/get4buildkey_fix.aspx',
        data: "key=" + name,
        success: function(msg) {
            $("#div_build").html(msg);
            var v = "";
        }
    })
}

function BuildCheckAll() {
    $(".mychck").each(function() {
        $(this).attr("checked", true);
        var aid = $(this).attr("value");
        var aname = $(this).attr("bname");
        mychange(aid, $(this)[0], aname);
    });
}

function BuildRiCheck() {

    $(".mychck").each(function() {
        var flag = $(this).attr("checked");
        if (flag == true) {
            $(this).attr("checked", false);
        }
        else {
            $(this).attr("checked", true);
        }
    });
}