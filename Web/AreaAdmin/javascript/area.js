
//添加楼号
function addBuildNum(paid) {
    var level = 5;
    var bid = document.getElementById("hidBid").value;
    var name = escape(document.getElementById("tbName").value);
    var py = document.getElementById("tbPy").value;
    var ddltype = $("#ddltype").val(); //1表示小区，2表示办公楼。
    var rblauto = $("input[name='rblauto']:checked").val(); //1自动生成，2不生成
    var tbnum = $("#tbnum").val() + ""; //最大楼号
    var patrn = /^[0-9]{1,3}$/;

    if (!patrn.test(tbnum)) {
        alert("请输入最大楼号");
        return false;
    }

    var url = "level=" + level + "&bid=" + bid + "&paid=" + paid + "&bname=" + name + "&py=" + py + "&num=" + tbnum;
    $.ajax({
        type: "get",
        url: "Ajax/AddAreaItemfix.aspx",
        cache: false,
        dataType: "text",
        data: url,

        beforeSend: function(XMLHttpRequest) {
            //$("#map_loading").show();
        },
        success: function(data, textStatus) {
            jtip('添加成功,已经自己生成'+tbnum+'号楼');
        },
        complete: function(XMLHttpRequest, textStatus) {
            //$("#loading").html("加载完成");
        },
        error: function() {
            //请求出错处理
        }

    });

}