$(function () {

    var tbtel = request("hasLogUpload");
    if (tbtel == "" || tbtel==undefined) {
        $(".mainbox1").show();
        $(".mainbox").hide();
    }
    else
    {
        var myreg = /^1\d{10}$/;
        if (!myreg.test(tbtel)) {
            $(".mainbox").hide();
            $(".mainbox1").show();
        }
        else
        {
            $(".mainbox1").hide();
            $(".mainbox").show();

            var msg = $("#hidtel").val();
            if (msg == "") {
                sweetAlert("", "您来晚了，红包已经被抢光了", "warning");
            }

        }
    }

})

function checkphone()
{
    var tel = $("#tbphone").val();
    if (tel == "" || tel == "请输入您的手机号") {
        sweetAlert("", "请输入您的手机号", "warning");
        return false;
    }
    else
    {

        var myreg = /^1\d{10}$/;
        if (!myreg.test(tel)) {
            sweetAlert("", "您的手机号格式有误，请重新输入", "warning");
            return false;
        }


        return true;
    }
}