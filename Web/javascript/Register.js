/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />

String.prototype.trim = function () {
    return this.replace(/(\s*$)|(^\s*)/g, '');
} 

function $I(id)
{
    return document.getElementById(id);
}

function $N(name)
{
    return document.getElementsByName(name);
}

var net=new Object();

//请求状态属性
net.READY_STATE_UNINITIALIZED=0;    //未初始化
net.READY_STATE_LOADING=1;          //正在加载
net.READY_STATE_LOADED=2;           //已加载
net.READY_STATE_INTERACTIVE=3;      //交互中
net.READY_STATE_COMPLETE=4;         //完成

net.ContentLoader=function(url,params,method,onload,onerror)   
{
    this.req=null;
    this.onload=onload;
    this.onerror=(onerror) ? onerror : this.defaultError;
    this.loadXMLDoc(url,method,params);
}

net.ContentLoader.prototype.loadXMLDoc=function(url,method,params)
{
    if (!method)
    {
        method="GET";
    }
    if (method=="POST")
    {
        contentType='application/x-www-form-urlencoded;charset=gb2312';
    }
    if (window.XMLHttpRequest)
    {
        this.req=new XMLHttpRequest();
    }
    else if (window.ActiveXObject)
    {
        try
        {
            this.req = new ActiveXObject('MSXML2.XMLHTTP');
        }
        catch(e)
        {
            try
            {
                this.req = new ActiveXObject('Microsoft.XMLHTTP');
            }
            catch(e)
            {
                this.onerror.call(this);
            }
        }
    }
    if (this.req)
    {
        try
        {
            var loader=this;
            this.req.onreadystatechange=function()
            {
                loader.onReadyState.call(loader);
            }
            this.req.open(method,url,true);
            if (contentType)
            {
                this.req.setRequestHeader('Content-Type', contentType);
            }
            this.req.send(params);
        }
        catch (err)
        {
            this.onerror.call(this);
        }
    }
}


net.ContentLoader.prototype.onReadyState = function()
{
    var req=this.req;
    if (req.readyState==net.READY_STATE_COMPLETE)
    {
        if (req.status==200 || req.status==0)
        {
            this.onload.call(this,req.responseText);
        }
        else
        {
            this.onerror.call(this,req.responseText);
        }
    }
}

net.ContentLoader.prototype.defaultError=function(){
  alert("error fetching data!"
    +"\n\nreadyState:"+this.req.readyState
    +"\nstatus: "+this.req.status
    +"\nheaders: "+this.req.getAllResponseHeaders());
}


var loading="正在验证...";
var servererr="对不起，服务器忙，请您稍候再试。";
var msg=new Array();
msg[0]="<img style='margin-top:10px;' src='images/reg_ok.png' />"
msg[1]="会员帐号可由英文、中文、数字、下划线组成，长度2-20个字符,。";
msg[2]="此项为必填项，请输入会员帐号。";
msg[3]="会员帐号格式错误，请重新填写。";
msg[4]="此会员帐号已被注册，请重新填写。";

msg[5] = "请输入6-20个英文字母、数字组成的字符。";
msg[6]="此项为必填项，请设置您的密码。";
msg[7] = "格式错误，请输入6-20个字母、数字组成的字符。";
msg[8]="再输入一遍上面所填的密码";
msg[9]="此项为必填项，请再次输入您的密码。";
msg[10]="两次密码输入不一致，请重新填写。";

msg[11]="用于密码找回，并可定期收到我社推荐书目。";
msg[12]="此项为必填项，请输入您的Email。";
msg[13]="Email格式错误，请重新填写。";
msg[14]="此Email已被注册，请重新填写。";

msg[15]="用于密码找回，例如：\"我宠物的名字\"。";
msg[16]="此项为必填项，请输入您的密码提示。";

msg[17]="用于密码找回，例如：\"旺财\"。";
msg[18]="此项为必填项，请输入您的提示答案。";

msg[19] = "抱歉，您必须同意食欲网服务条款后，才能注册。";

msg[20]="此项为必填项，请输入验证码。";
msg[21]="验证码错误，请重新输入。";
msg[22]="验证码已过期，请重新输入。"
msg[23] = "为了防止恶意注册，请输入验证码。";

//手机
msg[24] = "请输入您常用的手机号(此号码注册完成后无法修改，请认真填写)";
msg[25] = "手机号码格式错误，请输入11位数字";
msg[26] = "此手机号码已经被注册了，请重新输入";

msg[27]="此项为必填项，请选择您的生日。";

var userid = false;
var pwd = false;
var again = false;
var email = false;
var question = false;
var answer = false;
var code = false;
var nike = false;
var chekcbox = false;
var phone = false;
var birthday=true;
var radiolist=false;

function checkfrom() {
    //email_check();
    phone_check();
    //nikename_check();
    pwd_check();
    again_check();
    // checkbox_check();
    if (pwd && again && phone) {
        //if (pwd && again && nike && phone) {
        showload_super();
        return true;
    }
    return false;
}

function ShowMSg(obj,msg,iserr)
{
    obj.innerHTML = msg;
    if(iserr == false)
    {
        obj.className = "notice_error text_heddin_style";
    }
    else
    {
        obj.className = "text_heddin_style";
    }
    obj.style.display = "inline-block";
}

function userid_focus()
{
    ShowMSg($I('msgUserID'),msg[1],false);
}

function userid_check()
{
    var val = $I('tbUserID').value;
    var err = $I('msgUserID');

    if(val.trim()=="")
    {
        ShowMSg(err,msg[2],true);
        userid = false;
        return false;
    }
    if(!/^(\w|[\u4e00-\u9fa5]){2,20}$/.test(val))
    {
        ShowMSg(err,msg[3],true);
        userid = false;
        return false;
    }
    
    ShowMSg(err,loading,false);
    new net.ContentLoader("Ajax/AjaxCheck.aspx","type=customername&value="+val,"POST",userid_deal,userid_err);
}

function userid_deal()
{
    if(this.req.responseText == "1")
    {
        ShowMSg($I('msgUserID'),msg[0],false);
        userid = true;
    }
    else
    {
        ShowMSg($I('msgUserID'),msg[4],true);
        userid = false;
    }
}

function userid_err()
{
    ShowMSg($I('msgUserID'),servererr,true);
    userid = false;
}

function pwd_focus()
{
    ShowMSg($I('msgPwd'),msg[5],false);
}

function pwd_check()
{
    var val = $I('TBpassword').value;
    var err = $I('msgPwd');
    if(val.trim() == "")
    {   
        err.style.color = 'red'
        ShowMSg(err,msg[6],true);
        pwd = false;
        return ;
    }
    if(!/^[a-zA-Z0-9]{6,20}$/.test(val))
    {
        err.style.color = 'red'
        ShowMSg(err,msg[7],true);
        pwd = false;
        return ;
    }
    if($I('tbAgain').value != "")
    {
        again_check()
    }
    ShowMSg(err,msg[0],false);
    pwd = true;
    return ;
}

function again_focus()
{
    ShowMSg($I('msgAgain'),msg[8],false);
}

function again_check()
{
    var val = $I('tbAgain').value;
    var err = $I('msgAgain');

    if(val.trim() == "")
    {
        $I('msgAgain').style.color = 'red'
        ShowMSg(err,msg[9],true);
        again = false;
        return false;
    }
    if(val != $I('TBpassword').value)
    {
        $I('msgAgain').style.color = 'red'
        ShowMSg(err,msg[10],true);
        again = false;
        return false;
    }
    ShowMSg(err,msg[0],false);
    again = true;
    return true;
}

function email_focus()
{
    $I("emailmsg").className = "text_heddin_style";
    ShowMSg($I('emailmsg'),msg[12],false);
}

function email_check() {
    var val = document.getElementById('tbEmail').value;
    var err = document.getElementById('emailmsg');

    if(val.trim()=="")
    {
        err.style.color = "red";
        ShowMSg(err,msg[12],true);
        email = false;
        return ;
    }
    if(!/^[.\-_a-zA-Z0-9]+@[\-_a-zA-Z0-9]+\.[a-zA-Z0-9]/.test(val))
    {
        err.style.color = "red";
        ShowMSg(err,msg[13],true);
        email = false;
        return ;
    }
    ShowMSg(err,loading,false);
    new net.ContentLoader("Ajax/AjaxCheck.aspx","type=email&value="+val,"POST",email_deal,email_err);
}

function email_deal()
{
    if(this.req.responseText == "1")
    {
        ShowMSg($I('emailmsg'),msg[0],false);
        email = true;
    }
    else//已经被注册
    {
        ShowMSg($I('emailmsg'),msg[14],true);
        $I('emailmsg').style.color = 'red'
        email = false;
    } 
}

function email_err()
{
    ShowMSg($I('emailmsg'),servererr,true);
}


function code_focus()
{
    ShowMSg($I('msgCode'),msg[23],false);
}

function code_check()
{
    if ($I('tbCode').value.trim() == "") {
        $I('msgCode').style.color = 'red'
        ShowMSg($I('msgCode'), msg[20], true);
        code = false;
    }
    else {
        ShowMSg($I('msgCode'), loading, false);
        new net.ContentLoader("Ajax/AjaxCheck.aspx", "type=code&value=" + $I('tbCode').value, "POST", code_deal, code_err);
    }
}

function code_deal()
{
    if(this.req.responseText == "1")
    {
        ShowMSg($I('msgCode'),msg[0],false);
        code = true;
    }
    else
    {
        $I('msgCode').style.color = 'red'
        ShowMSg($I('msgCode'),msg[21],true);
        code = false;
    }
}

function code_err()
{
    ShowMSg($I('msgCode'),servererr,true);
}

function detail_check()
{
    if($I("trDetail").style.display == "none")
        $I("trDetail").style.display = "block";
    else
        $I("trDetail").style.display = "none";
}

function checkbox_check() {
    if ($I("Jchagree").checked == false) {
        document.getElementById('checkboxmsg').style.color = "red";
        ShowMSg($I('checkboxmsg'), msg[19], false);
        $I("checkboxmsg").style.display = "";
        chekcbox = false;
    }
    else {
        chekcbox = true;
        $I("checkboxmsg").style.display = "none";
    }    
}

function radiolist_check(){
   var value = $I("rblsex").val();
   if(value.trim()==""){
       document.getElementById('sexmsg').style.color = "red";
       radiolist=false;
   }
   else
   {
      radiolist=true;
      $I("rblsex").style.display="none";
   }
}

function nikename_focus() {
    ShowMSg($I('nikenamemsg'), "昵称不能为空,注册不能修改", true);
}

function nikename_check() {
    if ($I('tbnikename').value.trim() == "") {
        $I('nikenamemsg').style.color = 'red'
        nike = false;
    }
    else {

        //不能全为数字,为了用手机号登录
        
        ShowMSg($I('nikenamemsg'), loading, false);
        new net.ContentLoader("Ajax/AjaxCheck.aspx", "type=nike&value=" + escape( $I('tbnikename').value), "POST", nike_deal, nike_err);
    }

}

function nike_deal()
{
    if(this.req.responseText == "1")
    {
        ShowMSg($I('nikenamemsg'),msg[0],false);
        nike = true;
    }
    else
    {
        $I('nikenamemsg').style.color = 'red'
        ShowMSg($I('nikenamemsg'), '此会员名已经被注册了,你重新填写.', true);
        nike = false;
    }
}

function nike_err()
{
    ShowMSg($I('nikenamemsg'),servererr,true);
}

//手机号码
function phone_focus() {
    $I("phonemsg").className = "text_heddin_style";
    ShowMSg($I('phonemsg'), msg[24], false);
}

function phone_check() {
    var val = document.getElementById('tbphone').value;
    var err = document.getElementById('phonemsg');

    if (val.trim() == "") {
        err.style.color = "red";
        ShowMSg(err, msg[25], true);
        phone = false;
        return;
    }
    var patrn = /^[0-9]{11}$/;
    if (!patrn.test(val)) {
        err.style.color = "red";
        ShowMSg(err, msg[25], true);
        phone = false;
        return;
    }
    ShowMSg(err, loading, false);
    new net.ContentLoader("Ajax/AjaxCheck.aspx", "type=phone&value=" + val, "POST", phone_deal, phone_err);
}

function phone_deal() {
    if (this.req.responseText == "1") {
        ShowMSg($I('phonemsg'), msg[0], false);
        phone = true;
    }
    else//已经被注册
    {
        ShowMSg($I('phonemsg'), msg[26], true);
        $I('phonemsg').style.color = 'red'
        phone = false;
    }
}

function phone_err() {
    ShowMSg($I('phonemsg'), servererr, true);
}

$(document).ready(function ()
{
    $(".text_style").focus(function()
    {
        $(this).addClass("text_style_hover");
    })
    $(".text_style").blur(function () {
        $(this).removeClass("text_style_hover");
    })
})