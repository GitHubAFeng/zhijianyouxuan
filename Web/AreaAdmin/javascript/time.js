//页面里要有 div id=timediv
//传入参数  input
String.prototype.trim = function() {
    return this.replace(/(\s*$)|(^\s*)/g, '');
} 

var myinput = null;

function timeinit(id) {
    myinput = $("#" + id);
    var input = document.getElementById(id);
    ShowTip(input, "timediv", 0, myinput.height() + 5);
    $("#timediv").show();
}

function sethour(target) {
    myinput.val(target.innerHTML);
}

function setminute(target) {
    var v = myinput.val().trim();
    if (v == "") {
        return;
    }
    else {
        var timestr_mm = v + ":" + target.innerHTML;
        myinput.val(timestr_mm.trim());
        $("#timediv").hide();
    }
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function ShowTip(obj, objdiv, addx, addy) {

    var x = getposOffset_top(obj, 'left');
    var y = getposOffset_top(obj, 'top');

    var div_obj = document.getElementById(objdiv);
    div_obj.style.left = (x + addx) + 'px';
    div_obj.style.top = (y + addy) + 'px';
}


//获取偏移量
function getposOffset_top(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;

}
 