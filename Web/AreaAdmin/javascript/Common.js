
// Common.js
// CopyRight (c) 2009 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2009-10-22
String.prototype.trim=function()
{
    return this.replace(/(\s*$)|(^\s*)/g, '');
} 

//function $(id)
//{
//    return document.getElementById(id);
//}

function $I(id)
{
    return document.getElementById(id);
}

function $N(name)
{
    return document.getElementsByName(name);
}

///获取url参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}


//页面跳转 zjf@ihangjing.com 2010-04-12
function setLocation(url)
{
    window.location.href = url;
}
//为元素增加样式 zjf@ihangjing.com 2010-04-12
function objectAddClass(object, className) {
    $(object).addClass(className);
    var sl = jQuery("select");
    var _version = $.browser.version;
    if (sl && (_version == 6.0)) {
        jQuery("select").hide();
    }
}

//为元素移除样式 zjf@ihangjing.com 2010-04-12
function objectRemoveClass(object, className) {
    $(object).removeClass(className);
    var sl = jQuery("select");
    var _version = $.browser.version;
    if (sl && (_version == 6.0)) {
        jQuery("select").show();
    }
}

//前台页面弹出消息对话框 一般是操作成功弹出一个提示 点击确定跳转到其他页面
//或者输入框填写不正确,弹出对话框提示 
//有遮蔽层
//msg:显示的信息
//type:提示信息是提示正确还是错误 正确 right 错误 error
//urlClose:点击关闭按钮跳转的页面地址 不跳转则为空
//urlOk:点击确定按钮跳转的页面地址 不跳转则为空
//timeClose:是否需要经过几秒后自动关闭 目前暂定3秒 取值 true false
//imgType:图片路径 和如调用这个函数页面是和Images在同一级下则 imgType设置1 如调用这个函数的页面在一个与images同一级的子目录里面则imgType 设置2
//imgType是解决图片路径问题的暂时替代方法,如有解决方案会及时更新
//示例:
//页面中最下方:
//<div class="mask" id="mask_info" style="display: none;"></div>
//<div class="div_notice" style="left: 373px; top: 304px; display: none;background-color: #f9f9f9;border: 2px solid #7db5de;position: absolute;width: 280px;z-index: 10;" id="div_show_info"></div>
//弹出按钮
//<input class="div_notice_button_ok"  id="Button1" type="button" onclick='ShowDivInfo("happy","true","index.aspx","index2.aspx","false",2);' />
//<input class="div_notice_button_ok"  id="Button2" type="button" onclick='ShowDivInfo("happy","error","","","false",2);' />
function ShowDivInfo(msg,type,urlClose,urlOk,timeClose,imgType)
{
    //弹出对话框
    //innerHTML += "<div class='div_notice' id='div_show_info'>";
    var innerHTML = "";
    innerHTML += "<div class='div_notice_title'>";
    innerHTML += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
    innerHTML += "<tr>";
    innerHTML += "<td style='padding-left: 10px;'><label id='lbDivTitle'>提示信息</label></td>";
    innerHTML += "<td style='text-align: right; padding-right: 5px;'>";
    if( imgType == "1" )
    {
        if( urlClose != "" )
        {
            innerHTML += "<a href='javascript:Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");window.location.href=\""+urlClose+"\";'>";
            innerHTML += "<img title='关闭窗口' src='Images/close.gif' alt='关闭窗口' onmouseover='this.src=\"Images/closeHover.gif\"' onmouseout='this.src=\"Images/close.gif\"' /></a>"
        }
        else 
        {
            innerHTML += "<a href='javascript:Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");'>";
            innerHTML += "<img title='关闭窗口' src='Images/close.gif'' alt='关闭窗口' onmouseover='this.src=\"Images/closeHover.gif\"' onmouseout='this.src=\"Images/close.gif\"' /></a>"
        }
    }
    else
    {
        if( urlClose != "" )
        {
            innerHTML += "<a href='javascript:Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");window.location.href=\""+urlClose+"\";'>";
            innerHTML += "<img title='关闭窗口' src='../Images/close.gif' alt='关闭窗口' onmouseover='this.src=\"../Images/closeHover.gif\"' onmouseout='this.src=\"../Images/close.gif\"' /></a>"
        }
        else 
        {
            innerHTML += "<a href='javascript:Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");'>";
            innerHTML += "<img title='关闭窗口' src='../Images/close.gif'' alt='关闭窗口' onmouseover='this.src=\"../Images/closeHover.gif\"' onmouseout='this.src=\"../Images/close.gif\"' /></a>"
        }
    }

    innerHTML += "</td></tr></table></div>";
    innerHTML += "<div style='padding: 10px; text-align: center;'>";
    if( imgType == "1")
    {
        if(type == "error")
        {
            innerHTML += "<label id='lbDivInfo'><img src='Images/cuo.gif' />"+msg+"</label>";
        }
        else
        {            
            innerHTML += "<label id='lbDivInfo'><img src='Images/dui.gif' />"+msg+"</label>";
        }  
    }
    else
    {
        if(type == "error")
        {
            innerHTML += "<label id='lbDivInfo'><img src='../Images/cuo.gif' />"+msg+"</label>";
        }
        else
        {            
            innerHTML += "<label id='lbDivInfo'><img src='../Images/dui.gif' />"+msg+"</label>";
        }  
    }
    
    innerHTML += "</div>";
    innerHTML += "<div align='center' style='padding-bottom: 10px;'>";
    if( urlOk != "")
    {
        innerHTML += "<input class='div_notice_button_ok' onmouseover='this.className=\"div_notice_button_ok_hover\"' onmouseout='this.className=\"div_notice_button_ok\"' id='Button1' type='button' onclick='Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");window.location.href=\""+urlOk+"\";' />";
    }
    else
    {
        innerHTML += "<input class='div_notice_button_ok' onmouseover='this.className=\"div_notice_button_ok_hover\"' onmouseout='this.className=\"div_notice_button_ok\"' id='Button1' type='button' onclick='Hidewindow(\"div_show_info\");Hidewindow(\"mask_info\");' />";

    }
    
    innerHTML += "</div>";
        
    $I("div_show_info").innerHTML = innerHTML;
    $I("div_show_info").style.display = "block"; 
    $I("mask_info").style.display = "block"; 
    
    if( timeClose == "true")
    {
        setTimeout(HiddenDivShowInfo,3000);
    }
}

function HiddenDivShowInfo()
{
    var div_show_info=document.getElementById("div_show_info");
    
	if(div_show_info)
	{
	    div_show_info.style.display="none";
	}
	var mask_info=document.getElementById("mask_info");
    
	if(mask_info)
	{
	    mask_info.style.display="none";
	}
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function Showwindow(obj,objdiv,addx,addy)
{
	var x=getposOffset_top(obj,'left');
    var y=getposOffset_top(obj,'top');
    var div_obj=document.getElementById(objdiv);
	div_obj.style.left=(x+addx)+'px';
	div_obj.style.top=(y+addy)+'px';
	div_obj.style.display="inline";
	return;
}

function ShowWindow(msg, type) {
    var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
    innerHTML += "<div style='float:left;font-size:12px;'>提示信息</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='Images/window_close.gif' alt='关闭窗口' /> </a>";
    innerHTML += "</div></div>";
    innerHTML += "<div style='text-align:center;font-size:12px;' id='divMassage'>";
    if (type == "error") {
        innerHTML += "<p><img src='images/error.gif' style='margin-top:10px;margin-bottom:10px;' /></p><p>";
    }
    else {
        innerHTML += "<p><img src='images/right.gif' style='margin-top:10px;margin-bottom:10px;' /></p><p>";
    }
    innerHTML += msg;
    innerHTML += "</p></div></div>";

    if (!document.getElementById("divMsg")) {


        var div = document.createElement('div');
        div.id = 'divMsg';
        div.setAttribute('style', 'bottom:0px; right:20px; width:200px; height:100px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;');
        div.setAttribute('innerHTML', innerHTML);
        div.innerHTML = innerHTML;
        document.body.appendChild(div);
        with (document.getElementById("divMsg").style) {
            bottom = "0px";
            right = "20px";
            width = "200px";
            height = "100px";
            position = "absolute";
            background = "#FFFFFF";
            border = "2px solid #447AA9";
        }
    }
    else {
        document.getElementById("divMsg").style.display = "block";
        document.getElementById("divMsg").setAttribute('innerHTML', innerHTML);
        document.getElementById("divMsg").innerHTML = innerHTML;
    }
    setTimeout(HiddenWindow, 3000);
}

function HiddenWindow() {
    if (document.getElementById("divMsg")) {
        document.getElementById("divMsg").style.display = "none";
    }
}

//隐藏一个div
function Hidewindow(objdiv)
{
    var div_obj=document.getElementById(objdiv);
	if(div_obj)
	{
	    div_obj.style.display="none";
	}
}

//隐藏一个div
function ShowMask(objdiv)
{
    var div_obj=document.getElementById(objdiv);
	if(div_obj)
	{
	    div_obj.style.display="block";
	}
}

//获取偏移量
function getposOffset_top(what, offsettype)
{ 
    var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop; 
    var parentEl=what.offsetParent; 
    while (parentEl!=null)
    { 
        totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop; 
         parentEl=parentEl.offsetParent; 
    } 
    return totaloffset;   
}

//屏幕中间弹出
function ShowAtCenter(objdiv)
{
    var scrollHeight;
    var scrollWidth;
    if (typeof window.pageYOffset != 'undefined') 
    {
       scrollHeight = window.pageYOffset;
       scrollWidth = window.pageXOffset;
    }
    else if (typeof document.compatMode != 'undefined' &&document.compatMode != 'BackCompat') 
    {
       scrollHeight = document.documentElement.scrollTop;
       scrollWidth = document.documentElement.scrollLeft;
    }
    else if (typeof document.body != 'undefined') 
    {
       scrollHeight = document.body.scrollTop;
       scrollWidth = document.body.scrollLeft;
    } 
    scrollHeight += 250;
    //scrollWidth += 450;
    //scrollHeight += document.body.clientHeight/2;
    scrollWidth += document.body.clientWidth/2 - 100;
    var div_obj=document.getElementById(objdiv);
	div_obj.style.left=scrollWidth+'px';
	div_obj.style.top=scrollHeight+'px';
	div_obj.style.display="inline";
}

//屏幕中间弹出
function ShowAtCenterFix(objdiv)
{
    var scrollHeight;
    var scrollWidth;
    if (typeof window.pageYOffset != 'undefined') 
    {
       scrollHeight = window.pageYOffset;
       scrollWidth = window.pageXOffset;
    }
    
    else if (typeof document.compatMode != 'undefined' &&document.compatMode != 'BackCompat') 
    {
       scrollHeight = document.documentElement.scrollTop;
       scrollWidth = document.documentElement.scrollLeft;
    }
    else if (typeof document.body != 'undefined') 
    {
       scrollHeight = document.body.scrollTop;
       scrollWidth = document.body.scrollLeft;
    } 
    var div_obj=document.getElementById(objdiv);
    div_obj.style.display="inline";
    //scrollHeight += document.body.clientHeight/2 - div_obj.offsetHeight/2;
    scrollWidth += document.body.clientWidth/2 - div_obj.offsetWidth/2;
    scrollHeight += 100;
    //scrollWidth += 300;
    
	div_obj.style.left=scrollWidth+'px';
	div_obj.style.top=scrollHeight+'px';
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

//非异步AJAX提交，调用方法如下
//Ajax ajax = new Ajax("Ajax/AjaxCheck.aspx") //新建对象
//var r = ajax.post("id=1234");   //发送对象，并返回结果
function Ajax(url)
{
    var m_xmlReq=null;
    if(window.ActiveXObject)
    {
        try
        {
            m_xmlReq = new ActiveXObject('Msxml2.XMLHTTP');
        }
        catch(e)
        {
            try{m_xmlReq = new ActiveXObject('Microsoft.XMLHTTP');}catch(e){}
        }
    }
    else if(window.XMLHttpRequest)
    {
        m_xmlReq = new XMLHttpRequest();
    }

    this.post=function(d)
    {
        if(!m_xmlReq) return;
        m_xmlReq.open('POST',url,false);
        m_xmlReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded;charset=gb2312');
        m_xmlReq.send(d);
        return m_xmlReq.responseText;
    }
}
//获得URL参数
function Request(){
    var Url=top.window.location.href.toLowerCase();
    var u,g,gg,strRt='';
    if(arguments[arguments.length-1]=="#")
       u=Url.split("#");
    else
       u=Url.split("?");
    if (u.length==1) g='';
    else g=u[1];

    if(g!=''){
       gg=g.split("&");
       var MaxI=gg.length;
       var str = arguments[0].toLowerCase()+"=";
       for(i=0;i<MaxI;i++){
          if(gg[i].indexOf(str)==0) {
            strRt=gg[i].replace(str,"");
            break;
          }
       }
    }
    return strRt;
}
//只允许输入整数，用例：<input onkeydown="onlyNum();" style="ime-mode:Disabled">
function onlyNum()
{
if(!(event.keyCode==46)&&!(event.keyCode==8)&&!(event.keyCode==37)&&!(event.keyCode==39))
if(!((event.keyCode>=48&&event.keyCode<=57)||(event.keyCode>=96&&event.keyCode<=105)))
event.returnValue=false;
} 
//只允许输入浮点数，用例：<input onkeydown="onlyFloat(this);" style="ime-mode:Disabled">
function onlyFloat(ipt)
{
    if(!(event.keyCode==46)&&!(event.keyCode==8)&&!(event.keyCode==37)&&!(event.keyCode==39))
    {
        if(event.keyCode==190)
        {
            if(/^\d*\.\d*$/.test(ipt.value))
                event.returnValue=false;
        }
        else if(!((event.keyCode>=48&&event.keyCode<=57)||(event.keyCode>=96&&event.keyCode<=105)))
        {
            event.returnValue=false;
        }
    }
}

//获取偏移量
function getposOffset_top(what, offsettype)
{ 
    var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop; 
    var parentEl=what.offsetParent; 
    while (parentEl!=null)
    { 
        totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop; 
         parentEl=parentEl.offsetParent; 
    } 
    return totaloffset; 
   
}
//判断是否为数字
function IsNum(s)
{
    if(s!=null)
    {

        if(/^[0-9]+$/.test(s))
            return true;
        else
            return false;
    }
    return false;
}
//用户输入时间与当前时间比较，小于系统时间返回less，大于返回big，等于返回equal
function equal(mydate)
{
	var d=new Date();
	var str_now=d.toString();
	var d_now = new Date(str_now.replace("-",",")).getTime();
	var d_mydate = new Date(mydate.replace("-",",")).getTime();
	if(d_mydate < d_now)
	{
		return "less";
	}
	else if(d_mydate > d_now)
	{
	    return "big"
	}
	return "equal";
}

//用户输入时间与当前日期比较
function DateEqual(mydate)
{
	var d=new Date();
	var str_now=d.toString();

	if( Date.parse(mydate.replace(/-/g,"/")) - Date.parse(str_now.replace(/-/g,"/")) >= 0 )
	{
	    return true;
	}
	else
	{
	    return false;
	}
}

//用户输入的两个时间比较
function DateCompare(date1,date2)
{
	if( Date.parse(date1.replace(/-/g,"/")) - Date.parse(date2.replace(/-/g,"/")) >= 0 )
	{
	    return true;
	}
	else
	{
	    return false;
	}
}

//限制多行输入框的输入字数<textarea maxlength="20" onkeyup="return isMaxLen(this)"></textarea>  
function isMaxLen(o){  debugger
var nMaxLen=o.getAttribute? parseInt(o.getAttribute("maxlength")):"";  
    if(o.getAttribute && o.value.length>nMaxLen){  
        o.value=o.value.substring(0,nMaxLen);
    }
}
//添加启动事件
function AddLoadFun(evnt, fun) {
    if(window.attachEvent)
    {
        window.attachEvent('on'+evnt,fun);
    }
    else
    {
        window.addEventListener(evnt,fun,false);
    }
}


function ShowMSg(obj,msg,iserr)
{
    obj.innerHTML = msg;
    if(iserr == true)
    {
        obj.className = "notice_error";
    }
    else
    {
        obj.className = "notice";
    }
    obj.style.display = "inline";
}

function email_check(objEmail)
{
    var email = false;
    var val = $I(objEmail).value;
    var err = $I('msgEmail');

    if(val.trim()=="")
    {
        ShowMSg(err,'此项为必填项，请输入您的Email。',true);
        email = false;
        return false;
    }
    if(!/^[.\-_a-zA-Z0-9]+@[\-_a-zA-Z0-9]+\.[a-zA-Z0-9]/.test(val))
    {
        ShowMSg(err,'Email格式错误，请重新填写',true);
        email = false;
        return false;
    }
    return true;
}


//缺书登记建议

function check(objBookName,objBookSort,objName,objTelephone,objEmail,objCellphone)
{
    var valbookname=$I(objBookName).value;
    var err1=$I('msgBookname');
    
    var valbooksort=$I(objBookSort).value;
    var err2=$I('msgBooksort');
    
    var valname=$I(objName).value;
    var err3=$I('msgName');
    
    var valtelephone=$I(objTelephone).value;
    var err4=$I('msgTelephone');
    
    var valemail=$I(objEmail).value;
    var err5=$I('msgEmail');
    
    var valcellphone=$I(objCellphone).value;
    var err6=$I('msgCellphone');
    
    var b1=true,b2=true,b3=true,b5=true,b6=true;
    
    if(valbookname.trim()=="")
    {
        ShowMSg(err1,'此项为必填项!',true);
        b1=false;
    }
    
    
    if(valbooksort.trim()=="")
    {
        ShowMSg(err2,'此项为必填项!',true);
        b2=false;
    }
    
    if(valname.trim()=="")
    {
        ShowMSg(err3,'此项为必填项,请填写你的姓名!',true);
        b3=false;
    }
    
    if(valtelephone.trim()=="")
    {
        ShowMSg(err4,'此项为必填项,请填写你的电话!',true);
        b4=false;
    }
    
    if(valemail.trim()=="")
    {
        ShowMSg(err5,'此项为必填项,请填写你的邮箱地址!',true);
        b5=false;
    }
    else
    {
        if(!/^[.\-_a-zA-Z0-9]+@[\-_a-zA-Z0-9]+\.[a-zA-Z0-9]/.test(valemail))
        {
            ShowMSg(err5,'Email格式错误，请重新填写',true);
            b5=false;
        }
        else
        {
            b5=true;
        }
    }
    
     if(valcellphone.trim()=="")
    {
        ShowMSg(err6,'此项为必填项,请填写你的手机号码!',true);
        b6=false;
    }
    
    if(b1&&b2&&b3&&b4&&b5&&b6)
    {
        return true;
    }
    else
    {
        return false;
    }
}


//荣誉列表，点击显示大图片

 function ShowBig(src,title,intro)
    {
        var scrollHeight;
        var scrollWidth;
        if (typeof window.pageYOffset != 'undefined') 
        {
           scrollHeight = window.pageYOffset;
           scrollWidth = window.pageXOffset;
        }
        else if (typeof document.compatMode != 'undefined' &&document.compatMode != 'BackCompat') 
        {
           scrollHeight = document.documentElement.scrollTop;
           scrollWidth = document.documentElement.scrollLeft;
        }
        else if (typeof document.body != 'undefined') 
        {
           scrollHeight = document.body.scrollTop;
           scrollWidth = document.body.scrollLeft;
        }
        var div_obj=document.getElementById("divBig");
        scrollHeight += 190;
        scrollWidth += document.body.clientWidth/2 - 320;
        
	    div_obj.style.left=scrollWidth+'px';
	    div_obj.style.top=scrollHeight+'px';
	    div_obj.style.display="inline";
	    
        var image=new Image(); 
        image.src=src; 
        var temp = image.width;
        if(temp>600)
            document.getElementById("imgBig").width = 600;
        else
            document.getElementById("imgBig").width = temp;
	    document.getElementById("imgBig").src = src;
	    document.getElementById("title").innerText = title;
	    document.getElementById("intro").innerText = intro;
	}
	
	
	function CloseBig()
	{
	    document.getElementById("divBig").style.display="none";
	}

    function BookPool(Id,booltype)
    {
        var radio= $I(Id);
        if(radio.checked == true)
        {
            $I(booltype).value = radio.value;
        }
    }
    
    
    /**************************** 图书详细页面，鼠标移过图书，现实大图片 *******************************************/
      function show(event,_this,mess) 
      {
        event = event || window.event;
        var t1="<div   cellspacing='1' cellpadding='10' style='border-color:#CCCCCC;background-color:#FFFFFF;font-size:12px;border-style:solid;    border-width:thin;text-align:center; width:300px; line-height:22px; padding-bottom:10px;*margin-top:-10px;'><tr><td><img src='" + _this   + "' width='300' height='400' >    <br>"+mess+"</td></tr></table>";
        document.getElementById("divBigPic").innerHTML =t1;
        document.getElementById("divBigPic").style.top   = document.body.scrollTop + event.clientY + 10 + "px";
        document.getElementById("divBigPic").style.left = document.body.scrollLeft + event.clientX + 10 + "px";
        document.getElementById("divBigPic").style.display = "block";
    }
    
    function hide(_this) 
    {
        document.getElementById("divBigPic").innerHTML = "";
        document.getElementById("divBigPic").style.display = "none";
    }
    
    
    
//    function SetFocus(id)
//    {
//        document.getElementById(id).focus();
//          alert(id');
//    }
  /*****************************************************************/
//操作cookie
//value:不为空时,表示设置cookie
//value:为空时,表示得到这个名字的cookie
// add by jijunjian 2010-03-26;
function handlecookie(name, value, options) 
{ 
    if (typeof value != 'undefined') 
	{ // name and value given, set cookie 
        options = options || {}; 
        if (value === null || value == "")
		{ 
            value = ''; 
            options.expires = -1; 
        } 
        var expires = ''; 
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) 
        { 
            var date; 
            if (typeof options.expires == 'number') 
			{ 
                date = new Date(); 
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000)); 
            } 
			else 
			{ 
                date = options.expires; 
            } 
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE 
        } 
        var path = options.path ? '; path=' + options.path : ''; 
        var domain = options.domain ? '; domain=' + options.domain : ''; 
        var secure = options.secure ? '; secure' : ''; 
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join(''); 
    } 
	else 
	{ // only name given, get cookie 
        var cookieValue = null; 
        if (document.cookie && document.cookie != '') 
		{ 
            var cookies = document.cookie.split(';'); 
            for (var i = 0; i < cookies.length; i++) 
			{ 
                var cookie = jQuery.trim(cookies[i]); 
                // Does this cookie string begin with the name we want? 
                if (cookie.substring(0, name.length + 1) == (name + '='))
				{ 
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1)); 
                    break; 
                } 
            } 
        } 
        return cookieValue; 
    } 
};


///浏览器对象
var ABBrowser = 
{
    navi:navigator.userAgent.toLowerCase(),
    isIE:function()
    {
        var A = this;
        return(A.navi.indexOf("msie")!=-1)&&(A.navi.indexOf("opera")==-1)&&(A.navi.indexOf("omniweb")==-1)
    },
    getBody:function()
    {
        return(document.compatMode&&document.compatMode!="BackCompat")?document.documentElement:document.body
    },
    getScrollTop:function()
    {
        return this.isIE()?this.getBody().scrollTop:window.pageYOffset
    },
    getScrollLeft:function()
    {
        return this.isIE()?this.getBody().scrollLeft:window.pageXOffset
    },
    getAvailableHeight:function()
    {
        return this.getBody().offsetHeight>this.getBody().scrollHeight?this.getBody().offsetHeight:this.getBody().scrollHeight
    },
    getAvailableWidth:function()
    {
        return this.getBody().offsetWidth>this.getBody().scrollWidth?this.getBody().offsetWidth:this.getBody().scrollWidth
    },
    getViewWidth:function()
    {
        return self.innerWidth||(document.documentElement.clientWidth||document.body.clientWidth)
    },
    getViewHeight:function()
    {
        return self.innerHeight||(document.documentElement.clientHeight||document.body.clientHeight)
    },
    getPointerPositionInDocument:function(C)
    {
        var B=C;
        var A=B.pageX||(B.clientX+ABBrowser.getBody().scrollLeft);
        var D=B.pageY||(B.clientY+ABBrowser.getBody().scrollTop);
        return{"x":A,"y":D}
    },
    getElementPosition:function(C)
    {
        if(typeof C.offsetParent!="undefined")
        {
            for(var B=0,A=0;C;C=C.offsetParent)
            {
                B+=C.offsetLeft;
                A+=C.offsetTop;
            }
            return{"x":B,"y":A}
        }
        else
        {
            return{"x":B,"y":A}
        }
     }
};


//隐藏指定元素位置下方的"下拉列表框",该函数主题解决ie6以前(包括ie6)的下拉列表框遮挡显示层的问题
function HideOverSels(objID)
{
    var sels = document.getElementsByTagName('select'); 
  
    for (var i = 0; i < sels.length; i++) 
    {
         if (Obj1OverObj2(document.getElementById(objID), sels[i]))
         {
            
            sels[i].style.visibility = 'hidden';  
         }
         else
         {
            sels[i].style.visibility = 'visible';
         }
    }
}


function getLeftPosition(Obj) 
{
    try
    {
        for (var sumLeft=0;Obj!=document.body;sumLeft+=Obj.offsetLeft,Obj=Obj.offsetParent);
        return sumLeft;
    }
    catch(e)
    {}
}

function getTopPosition(Obj) 
{
    try
    {
        for (var sumTop=0;Obj!=document.body;sumTop+=Obj.offsetTop,Obj=Obj.offsetParent);
        return sumTop;
    }
    catch(e)
    {}
}

//判断obj1是否遮挡了obj2
function Obj1OverObj2(obj1, obj2)
{ 
  var result = true; 
  
  var obj1Left = getLeftPosition(obj1) - document.body.scrollLeft; 
  var obj1Top = getTopPosition(obj1)  - document.body.scrollTop; 
  var obj1Right = obj1Left + obj1.offsetWidth; 
  var obj1Bottom = obj1Top + obj1.offsetHeight;
  var obj2Left = getLeftPosition(obj2) - document.body.scrollLeft; 
  var obj2Top = getTopPosition(obj2) - document.body.scrollTop; 
  var obj2Right = obj2Left + obj2.offsetWidth; 
  var obj2Bottom = obj2Top + obj2.offsetHeight;
 

  if (obj1Right <= obj2Left || obj1Bottom <= obj2Top || obj1Left >= obj2Right || obj1Top >= obj2Bottom) 
  {
     result = false; 
  }
    
  return result; 
}



function CheckTable(tableName)
{
    if(!document.getElementById(tableName)){return false;}
    var AllRows = document.getElementById(tableName).getElementsByTagName("tr");
    for(var i=1; i<AllRows.length; i++){
        //AllRows[i].onmouseover = function(){this.className = "on-mouse";};
        //AllRows[i].onmouseout = function(){this.className = "list";};
        AllRows[i].checkbox = AllRows[i].cells[0].getElementsByTagName("input")[0];
        AllRows[i].onclick = function()
        {        
            if(this.checkbox.checked == true)
            {
                this.SetCheck(false);
            }
            else
            {
                this.SetCheck(true);
            }            
        }
        AllRows[i].SetCheck = function(bool)
        {
            if(bool == true)
            {
                this.checkbox.checked = true;
                //增加一个样式 2010-04-12 zjf@ihangjing.com update
                $(this).addClass("invalid");
            }
            else
            {    
                this.checkbox.checked = false;
                $(this).removeClass("invalid");

            }
        }
        AllRows[i].checkbox.onclick = function()  //解决点击checkbox引发时间冲突，有待改善
        {
            if(document.all)
            {
                this.parentNode.parentNode.click();
            }
            else
            {
//                var evt = document.createEvent("MouseEvents");
//                evt.initEvent("click", true, true);
//                this.dispatchEvent(evt); 
               if (this.checked == false)
               {
                    this.checked = true;
               }
               else
               {
                   this.checked = false;
               }
            }
        }; 
    }
    
   this.CheckAll = function()
   {
    for(i=1;i<AllRows.length;i++)
    {
     AllRows[i].SetCheck(true);
    }

   }
   this.CheckNo= function()
    {
        for(i=1;i<AllRows.length;i++)
        {
           AllRows[i].SetCheck(false);
        }

    }
   this.ReCheck = function()
   {
       for(i=1;i<AllRows.length;i++)
       {
           if(AllRows[i].checkbox.checked == true)
           {
                AllRows[i].SetCheck(false);
           }
           else
           {
                AllRows[i].SetCheck(true);
           }
       }

   }
   this.GetChecks = function()
   {
        var nums = new Array();
        var j = 0;
        for(var i=1; i<AllRows.length; i++){
            if(AllRows[i].checkbox.checked == true){nums[j] = AllRows[i].checkbox.value;j++;}
        }
        return nums;
    }
   /*客服系统提交订单时对购物车的菜单进行遍历得到 编号 价格 折扣*/  
   this.GetFoodListChecks = function()
   {
        var nums = new Array();    //编号
        var Prices = new Array();  //价格
        var Discout = new Array(); //折扣
        var FoodNum = new Array(); //数量
        
        var TotalPrice = parseFloat("0.0");
        var CurrentPrice =parseFloat("0.0");
        
        var j = 0;
        for(var i=1; i<AllRows.length; i++)
        {
            if(AllRows[i].checkbox.checked == true)
            {
                nums[j] = AllRows[i].checkbox.value;
                Prices[j] = AllRows[i].cells[0].getElementsByTagName("input")[1].value ;//AllRows[i].getElementById("Price").value
                Discout[j] = AllRows[i].cells[0].getElementsByTagName("input")[2].value ;// AllRows[i].getElementById("Discout").value;
                FoodNum[j] = AllRows[i].cells[3].getElementsByTagName("input")[0].value ;// AllRows[i].getElementById("Discout").value;
                j++;
            }
        }
        
        document.getElementById("hidPrices").value = ArrayToString(Prices);
        document.getElementById("hidDiscounts").value = ArrayToString(Discout);
        document.getElementById("hidFoodNums").value = ArrayToString(FoodNum)
        return nums;
    }
}

/*客服下订单系统*/
function CheckFoodListTable(tableName)
{
    if(!document.getElementById(tableName)){return false;}
    var AllRows = document.getElementById(tableName).getElementsByTagName("tr");
    for(var i=1; i<AllRows.length; i++){
        //AllRows[i].onmouseover = function(){this.className = "on-mouse";};
        //AllRows[i].onmouseout = function(){this.className = "list";};
        AllRows[i].checkbox = AllRows[i].cells[0].getElementsByTagName("input")[0];
//        AllRows[i].onclick = function()
//        {        
//            if(this.checkbox.checked == true)
//            {
//                this.SetCheck(false);
//            }
//            else
//            {
//                this.SetCheck(true);
//            }            
//        }
        AllRows[i].SetCheck = function(bool)
        {
            if(bool == true)
            {
                this.checkbox.checked = true;
                //增加一个样式 2010-04-12 zjf@ihangjing.com update
                $(this).addClass("invalid");
            }
            else
            {    
                this.checkbox.checked = false;
                $(this).removeClass("invalid");

            }
        }
        /*zjf@ihangjing.com 2010-07-17*/
        AllRows[i].checkbox.SetCheckFix = function(bool)
        {
            if(bool == true)
            {
                this.checked = true;
                //增加一个样式 2010-04-12 zjf@ihangjing.com update
                $(this).parent().parent().addClass("invalid");
            }
            else
            {    
                this.checked = false;
                $(this).parent().parent().removeClass("invalid");

            }
        }
        AllRows[i].checkbox.onclick = function()  //解决点击checkbox引发时间冲突，有待改善
        {
            if(document.all)
            {
                this.parentNode.parentNode.click();
            }
            else
            {
//                var evt = document.createEvent("MouseEvents");
//                evt.initEvent("click", true, true);
//                this.dispatchEvent(evt); 
//               if (this.checked == false)
//               {
//                    this.checked = true;
//               }
//               else
//               {
//                   this.checked = false;
//               }
               
            if(this.checked == true)
            {
                this.SetCheckFix(true);
            }
            else
            {
                this.SetCheck(false);
            }     
            }
        }; 
    }
    
   this.CheckAll = function()
   {
    for(i=1;i<AllRows.length;i++)
    {
     AllRows[i].SetCheck(true);
    }

   }
   this.CheckNo= function()
    {
        for(i=1;i<AllRows.length;i++)
        {
           AllRows[i].SetCheck(false);
        }

    }
   this.ReCheck = function()
   {
       for(i=1;i<AllRows.length;i++)
       {
           if(AllRows[i].checkbox.checked == true)
           {
                AllRows[i].SetCheck(false);
           }
           else
           {
                AllRows[i].SetCheck(true);
           }
       }

   }
   this.GetChecks = function()
   {
        var nums = new Array();
        var j = 0;
        for(var i=1; i<AllRows.length; i++){
            if(AllRows[i].checkbox.checked == true){nums[j] = AllRows[i].checkbox.value;j++;}
        }
        return nums;
    }
   /*客服系统提交订单时对购物车的菜单进行遍历得到 编号 价格 折扣*/  
   this.GetFoodListChecks = function()
   {
        var nums = new Array();    //编号
        var Prices = new Array();  //价格
        var Discout = new Array(); //折扣
        var FoodNum = new Array(); //数量
        
        var TotalPrice = parseFloat("0.0");
        var CurrentPrice =parseFloat("0.0");
        
        var j = 0;
        for(var i=1; i<AllRows.length; i++)
        {
            if(AllRows[i].checkbox.checked == true)
            {
                nums[j] = AllRows[i].checkbox.value;
                Prices[j] = AllRows[i].cells[0].getElementsByTagName("input")[1].value ;//AllRows[i].getElementById("Price").value
                Discout[j] = AllRows[i].cells[0].getElementsByTagName("input")[2].value ;// AllRows[i].getElementById("Discout").value;
                FoodNum[j] = AllRows[i].cells[3].getElementsByTagName("input")[0].value ;// AllRows[i].getElementById("Discout").value;
                j++;
            }
        }
        
        document.getElementById("hidPrices").value = ArrayToString(Prices);
        document.getElementById("hidDiscounts").value = ArrayToString(Discout);
        document.getElementById("hidFoodNums").value = ArrayToString(FoodNum)
        return nums;
    }
}

function DelConfirm()
{
    return confirm("确定要删除吗？");
}

function ArrayToString(arr)
{
    var temp = "";
    for(var i = 0; i< arr.length;i++)
    {
        temp += arr[i];
        if(i != arr.length - 1)
            temp += ",";
    }
    return temp;
}

// build/show the dialog box, populate the data and call the fadeDialog function //
// 2010－01－12 zhengjianfeng add .error-msg .success-msg .notice-msg .warning-msg
// 显示操作后成功、失败、警告信息，代替弹出框，更简便的实现消息提示
function showMessage(message,type,autoclose,second)
{
    var obj = document.getElementById("divMessages");
    var message_info = "<ul class='messages'>";
    
    if( type == "success")
    {
        message_info += "<li class='success-msg'><ul><li>"+message+"</li></ul></li></ul>";
    }
    else if(type=="warning")
    {
         message_info += "<li class='warning-msg'><ul><li>"+message+"</li></ul></li></ul>";
    }
    else if( type == "notice")
    {
        message_info += "<li class='notice-msg'><ul><li>"+message+"</li></ul></li></ul>";
    }
    else
    {
        message_info += "<li class='error-msg'><ul><li>"+message+"</li></ul></li></ul>";
    }

    obj.innerHTML = message_info;
    if( autoclose == "true")
    {
        window.setTimeout("closeDiv()", (second * 1000));
    }
    hideload_super();
}

function closeDiv()
{
    var obj = document.getElementById("divMessages");
    obj.style.visibility = "hidden";
    obj.innerHTML = "";
}

function WhitchActive(index) {
    var n = parseInt(index) - 1;
    $("#nav>li:gt(-1)").addClass("");
    $("#nav>li:eq(" + n + ")").addClass("active");
}

function loading() {
    $("#loading-mask").show();
}

function loadover() {
    $("#loading-mask").hide();
}

///显示加载中，防止多次点
///msg现在用，time,也没用，btid按钮的id
function showload(msg, time, btid) {
    var width = 125;
    var height = 96;
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading-mask").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading-mask").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };

    var moveX = 0, moveY = 0, moveTop, moveLeft = 0, moveable = false;
    if (_version == 6.0) {
        moveTop = est;
    } else {
        moveTop = 0;
    }
    $("#loading-mask").show();
    //定时器 事件
    var closeWindown = function() {
        $("#loading-mask").fadeOut("slow", function() { $(this).remove(); });
    }

    if (time == "" || typeof (time) == "undefined") {

    }
    else {
        setTimeout(closeWindown, time);
    }
    $("#" + btid).hide();
}
//隐藏加载中，设置按钮有郊
function hideload(btid) {
    $("#loading-mask").remove();
    $("#" + btid).show();
}

// onkeypress="return only_num(event)"
function only_num(e) {
    ee = e ? e : window.event ? event : null;
    var keyNum = ee.keyCode == 0 ? ee.which : ee.keyCode;
    if ((keyNum >= 48 && keyNum <= 57) || keyNum == 8) {
        return true;
    }
    else {
        return false;
    }
}

///获取url参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

function showload_super(msg, time, ele) {
    var width = 125;
    var height = 96;
    var logindiv = new String;
    logindiv = "<div id=\"loading_mask_my\">";
    logindiv += "<p class=\"loader\" id=\"loading_mask_loader\">";
    logindiv += "<img src=\"../images/ajax-loader-tr.gif\" /><br />";
    logindiv += "请稍候...<br></p></div>";
    logindiv += "<div id=\"windownbg_notice\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.2);opacity:0.2;z-index: 99\"></div>"
    $("body").append(logindiv);
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("loading_mask_my").show();
}

//隐藏加载中，设置按钮有郊
function hideload_super(btid) {
    $("#loading_mask_my").remove();
    $("#windownbg_notice").remove();
}
function gourl(url) {
    window.location = url;
}

//开始提交数据：mycontainter 是一个Class,表示中只验证这个元素内的控件
function j_submitdata(mycontainter) {
    showload_super();
    var comflag = true;
    $("." + mycontainter).find("[reg],[url]:not([reg])").each(function() {
        if ($(this).attr("reg") != undefined) {
            if (!validate($(this))) {
                hideload_super();
                comflag = false;
                return false;
            }
        }
    });

    if (comflag == false) {
        hideload_super();
        return false;
    }
    return true;
}

//验证,根据当前对像设置的正则表达式，判断是否通，不通过alert(当前对像的tip属性)
//CanBeNull="n" 表示不可为空，格式按reg 
//CanBeNull="y" 表示可为空，如果不空，格式按reg 
function validate(obj) {
    var reg = new RegExp(obj.attr("reg"));
    var objValue = obj.attr("value");
    var CanBeNull = "";
    if (obj.attr("canbenull") != undefined && obj.attr("canbenull") == "y") {

        CanBeNull = "y"; //可为空，如果不空，才判断
        if (objValue != "") {
            if (!reg.test(objValue)) {
                alert(obj.attr("tip"));
                return false;
            }
            return true;
        }
        else {
            return true;
        }
    }
    else {
        CanBeNull = "n"; //不能为空
        if (!reg.test(objValue)) {
            alert(obj.attr("tip"));
            return false;
        }
        else {

        }
        return true;
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
    div_obj.style.display = "inline";
}


//显示城市选择框
function show_citytable() {
    var url = "/AreaAdmin/buildtable_1.aspx";
    showBuild_Box('url:get?' + url, 702, 450, 'true');
}


/*弹出选择写字楼的table*/
function showBuild_Box(content, width, height, showbg) {
    $("#build-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 999901;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
        simpleWindown_html += "<div id=\"build-box\" style=' display:none'>";
        simpleWindown_html += "<div id=\"build-content\"><img src='http://www.ihangjing.com/images/loading.gif' class='loading' /></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;
    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html(html);
                }
            });
            break;
        case "iframe":
            $.ajax({
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + parseInt(height) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
                }
            });
    }
    if (showbg == "true") {
        $("#windownbg").show();
    }
    else {
        $("#windownbg").remove();
    };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度  
    if (height >= 527) {
        $("#build-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#build-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#build-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        var _scrollHeight = $(document).scrollTop(); //获取当前窗口距离页面顶部高度
        var _windowHeight = $(window).height(); //获取当前窗口高度
        var _windowWidth = $(window).width(); //获取当前窗口宽度
        var _popupHeight = height;  //获取弹出层高度
        var _popupWeight = width; //获取弹出层宽度

        var _posiTop = (_windowHeight - _popupHeight) / 2 + _scrollHeight + 200;

        $("#build-box").css({ left: "50%", top: _posiTop + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });

    };
    $("#build-box").show();
}

function showload_super(msg, time, ele) {
    var width = 125;
    var height = 96;
    var logindiv = new String;
    logindiv = "<div id=\"loading_mask_my\">";
    logindiv += "<p class=\"loader\" id=\"loading_mask_loader\">";
    logindiv += "<img src=\"/images/ajax-loader-tr.gif\" /><br />";
    logindiv += "请稍候...<br></p></div>";
    logindiv += "<div id=\"windownbg_notice\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.2);opacity:0.2;z-index: 99\"></div>"
    $("body").append(logindiv);
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#loading_mask_my").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("loading_mask_my").show();
}

//隐藏加载中，设置按钮有郊
function hideload_super(btid) {
    $("#loading_mask_my").remove();
    $("#windownbg_notice").remove();
}


function j_ShowWindow(msg) {
    var innerHTML = "<div style='background-color: #458BC9;height: 19px;padding: 3px 4px 0 10px;'>";
    innerHTML += "<div style='float:left;font-size:12px;color:#fff'>温馨提醒</div>";
    innerHTML += "<div style='float:right;'>";
    innerHTML += "<a href='javascript:HiddenWindow();'><img src='/Admin/Images/window_close.gif' alt='关闭窗口' /> </a>";
    innerHTML += "</div></div>";
    innerHTML += "<div style='text-align:left;font-size:12px;width:100%;overflow:hidden;padding-left:5px;' id='divMassage'>";
    innerHTML += msg;
    innerHTML += "</div></div></div>";

    if (!document.getElementById("divMsg")) {
        var div = document.createElement('div');
        div.id = 'divMsg';
        div.setAttribute('style', 'bottom:0px; right:20px; width:330px; height:120px; position: absolute;z-index: 100; background-color:#FFFFFF;border: 2px solid #447AA9;');
        div.setAttribute('innerHTML', innerHTML);
        div.innerHTML = innerHTML;
        document.body.appendChild(div);
        with (document.getElementById("divMsg").style) {
            bottom = "0px";
            right = "5px";
            width = "350px";
            height = "240px";
            position = "absolute";
            background = "#FFFFFF";
            border = "2px solid #447AA9";
        }
    }
    else {
        document.getElementById("divMsg").style.display = "block";
        document.getElementById("divMsg").setAttribute('innerHTML', innerHTML);
        document.getElementById("divMsg").innerHTML = innerHTML;
    }
    //setTimeout(HiddenWindow, 3000);
}
function HiddenWindow() {
    if (document.getElementById("divMsg")) {
        document.getElementById("divMsg").style.display = "none";
    }
}


var Loader =
    {
        show: function (btn) {
            if (btn == null || btn == undefined || $(btn).length == 0) return;
            var left = $(btn).offset().left;
            var top = $(btn).offset().top;
            var width = $(btn).outerWidth();
            var height = $(btn).height();
            var opts = {
                lines: 9, // The number of lines to draw
                length: 0, // The length of each line
                width: 10, // The line thickness
                radius: 15, // The radius of the inner circle
                corners: 1, // Corner roundness (0..1)
                rotate: 0, // The rotation offset
                direction: 1, // 1: clockwise, -1: counterclockwise
                color: '#000', // #rgb or #rrggbb or array of colors
                speed: 1, // Rounds per second
                trail: 81, // Afterglow percentage
                shadow: false, // Whether to render a shadow
                hwaccel: false, // Whether to use hardware acceleration
                className: 'spinner', // The CSS class to assign to the spinner
                zIndex: 2e9, // The z-index (defaults to 2000000000)
                top: '50%', // Top position relative to parent
                left: '50%' // Left position relative to parent
            };
            $('#ajax_spin').remove();
            $('body').append('<div id="ajax_spin" style="position:absolute;background:#FFF;filter:alpha(opacity=30);opacity:0.3"><div id="ajax_spin_inner" style="position:relative;height:20px;"></div></div>');
            $('#ajax_spin').css({
                'top': top,
                'left': left,
                'width': width,
                'height': height
            });
            var target = document.getElementById('ajax_spin_inner');
            var spinner = new Spinner(opts).spin(target);
        },
        hide: function (btn) {
            $('#ajax_spin').remove();
        }
}