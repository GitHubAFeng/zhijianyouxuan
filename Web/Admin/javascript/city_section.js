//城市，区域选择
///mytype =1表示是选择大的区域，=2表示是后台执行，要选中来的选择的二级区域
function sectionchange(mytype) {
    var sid = document.getElementById("DDLArea").value;
    if (sid != "-1") {
        var url = "/Admin/Ajax/getsubsection.aspx";
        var objpath = window.location + "";
        if (objpath.toLowerCase().indexOf("localhost") > 0) {
            url = "/web/Admin/Ajax/getsubsection.aspx";
        }
        jQuery.ajax(
        {
            type: "post",
            url: url,
            data: "id=" + sid + "&t=" + new Date().getTime(),
            dataType: "json",
            success: function(msg) {
                parsexml(msg, mytype);
            }
        })
    }
    else {
        document.getElementById("ddlsection").options.length = 0;
        var id = "-1";
        var name = "请选择";
        $("#subsectiontid ").val(id)
        $I("ddlsection").options.add(new Option(name, id));
    }
}

function parsexml(smg, mytype) {
    document.getElementById("ddlsection").options.length = 0;
    if (mytype == 1) {
        $("#subsectiontid ").val("-1");
    }
    $I("ddlsection").options.add(new Option("请选择", "-1"));
    if (typeof (smg) == "object" && typeof (smg.sort) == "object") {
        var length = smg.sort.length;
        for (var i = 0; i < length; i++) {
            var id = smg.sort[i].SectionID;
            var name = smg.sort[i].SectionName;
            $I("ddlsection").options.add(new Option(name, id));
            if (mytype == 1 && i == 0) {
                $("#subsectiontid ").val(id)
            }
        }
    }
    //选中
    if (mytype == 2) {
        var subsid = $("#subsectiontid").val() + "";
        $("#ddlsection ").val(subsid);
    }
}

function $I(ele) {
    return document.getElementById(ele);
}

function clearNames() {
    document.getElementById("ddlsection").options.length = 0;
}

function getsubsort() {
    $("#subsectiontid").val(document.getElementById("ddlsection").value);
}
//分类

//分类改变
function sortchange() {
    var value = $("#ddlfirstsort").val() + "";
    if (value != "0") {
        //getTogoTypes();
    }
    else {

    }
}


function gettypeval() {
    var str = "";
    $('input:checkbox[name="cblqq"]:checked').each(function() {
        str += "" + $(this).val() + ",";
    })
    var temp = str.replace(/,$/, "");
    $("#hfsecondsort").val(temp);

}

function settypeval() {
    var str = $("#hfsecondsort").val();
    if (str != null && str != "") {
        var strs = new Array();
        strs = str.split(',');
        for (var i = 0; i < strs.length; i++) {
            $(".cblqq").each(function() {
                if ($(this).val() == strs[i]) {
                    $(this).attr("checked", "true");
                }
            })
        }
    }
}
