//获取URL参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}
//去除空格（不能去除中间的空格，可以去除前后空格）
function RemoverSpace(str) {
    return str.replace(/(\s*$)|(^\s*)/g, '');
}
//验证是否为空||空字符串(ture为空 false不为空)
function CheckNull(str) {
    if (str == null) {
        return true;//为空
    }
    if (str.replace(/(\s*$)|(^\s*)/g, '').length <= 0)
    {
        return true;//为空
    }
    else {
        return false;
    }
}
//验证汉字
function CheckHanZi(str) {
    var reg = /.*[\u4e00-\u9fa5]+.*$/;
    return reg.test(str);
}
//验证正整数
function CheckInteger(str) {
    var reg = /^([0-9]|[1-9])*\d$/;
    return reg.test(str);
}
//验证正数
function CheckPositive(num) {
    var reg = /^\d+(?=\.{0,1}\d+$|$)/
    return reg.test(num);
}
//验证邮箱
function CheckEmail(str) {
    var reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/;
    return reg.test(str);
}
//验证用户名
function CheckUserName(str) {
    var reg = /^[a-zA-Z0-9\u4E00-\u9FA5]{4,16}$/;
    return reg.test(str);
}
//验证登录名
function CheckAdminName(str) {
    var reg = /^[a-zA-Z0-9]{4,16}$/;
    return reg.test(str);
}
//验证密码
function CheckPwd(str) {
    var reg = /^[a-zA-Z0-9_!@#$%^&*]{6,20}$/;
    return reg.test(str);
}
//验证输入的为中文
function CheckZW(str)
{
    var reg = /[\u4E00-\u9FA5]|[\uFE30-\uFFA0]/gi;
    return reg.test(str);
}
//验证手机号
function CheckPhone(str) {
    //var reg = /^0?(13[0-9]|15[0-9]|18[0-9]|14[0-9]|17[0-9])[0-9]{8}$/;
    var reg = /^1\d{10}$/;//为了后期少维护 2015-12-17
    return reg.test(str);
}
//验证手机号（简单版）
function checkMobile(mobile) {
    var reg0 = /^1\d{10}$/; 
    var my = false;
    if (reg0.test(mobile)) my = true;
    if (!my) {
        alert('手机号码格式错误！')
        return false;
    } else {
        return true;
    }
}
//验证身份证号
function CheckCard(str)
{
    var reg = /^[1-9]{1}[0-9]{14}$|^[1-9]{1}[0-9]{16}([0-9]|[xX])$/;
    return reg.test(str);
}
//保留两位小数
function returnFloat(value) {
    value = Math.round(parseFloat(value) * 100) / 100;
    if (value.toString().indexOf(".") < 0)
        value = value.toString() + ".00";
    return value;
}
//兼容多浏览器加入收藏
function addfavor(url, title) {
    if (confirm("网站名称：" + title + "\n网址：" + url + "\n确定添加收藏?")) {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.indexOf("msie 8") > -1) {
            external.AddToFavoritesBar(url, title, '');//IE8
        } else {
            try {
                window.external.addFavorite(url, title);
            } catch (e) {
                try {
                    window.sidebar.addPanel(title, url, "");//firefox
                } catch (e) {
                    alert("您的浏览器不支持快捷，请使用Ctrl+D进行添加！");
                }
            }
        }
    }
    return false;
}

//页面跳转
function locationHref(a) {
    location.href = a;
}
//日期大小判断
function compareDate(DateOne, DateTwo, model) {
    var str = "-";
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf(str));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf(str) + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf(str));
    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf(str));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf(str) + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf(str));
    switch (model) {
        case "dy":
            if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) >
                Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
                return true;
            }
            else {
                return false;
            }
            break;
        case "dd":
            if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) >=
                Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
                return true;
            }
            else {
                return false;
            }
            break;
    }
}
//日期天数差，日期格式yyyy-MM-dd
function daysBetween(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf('-'));
    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));
    var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
    return Math.abs(cha);
}