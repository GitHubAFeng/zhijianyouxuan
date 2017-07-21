/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
function CheckForm() {
    var name = $("#tbname").val() + "";
    if (name == "") {
        $("#nikenamemsg").show();
        return false;
    }
    else {
        $("#nikenamemsg").hide();
    }
    var myemail = $("#tbemail1").val();
    if ($("#tbemail1").val() == "") {
       
        $("#emailmsg").show();
        return false;
    }
    if (!/^[.\-_a-zA-Z0-9]+@[\-_a-zA-Z0-9]+\.[a-zA-Z0-9]/.test(myemail)) {
        alert("邮箱格式错误。");
        return false;
    }
    else {
        $("#emailmsg").hide();
    }
    $I("btOK").disabled = "disabled";

    showload_super();

    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/SendPassword.aspx",
        data: 'email=' + myemail + "&name=" + escape(name),
        success: function (msg) {
            hideload_super();
            if (msg == "1") {
                alert("找回密码邮件发送到您的邮箱，请注意查看。");
            }
            else if (msg == '-1') {
                alert("邮箱不存在,请重新输入.");
                $I("btOK").disabled = "";
            }
            else {
                $I("btOK").disabled = "";
                alert("发送失败,请稍后再试。.");
            }
        }
    })
    
 
    return false;
}


function $I(ele) {
    return document.getElementById(ele);
}