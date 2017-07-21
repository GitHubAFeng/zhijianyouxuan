

/***************************************
* 弹出写字楼选择框 确认位置
*
*
****************************************/
var huKou = new Array();
var xmlHttp;
var btv = '';
var id = '';
var cot_huk_id = '';

function show_province(evt, cot_huk, T)
{
   ShowTip(document.getElementById("tbaCode"),"tabpopup1",-200,30);
}



function closediv()
{
    document.getElementById("tabpopup1").style.display = "none";
    var btsubmitorder = document.getElementById("submitorder");
    if (btsubmitorder != null & btsubmitorder != ''& btsubmitorder != 'undefined')
    {
        btsubmitorder.value = "提交订单";
        btsubmitorder.disabled = false;
    }
}

function InitAddress(name, bid) {

    var hfbid = document.getElementById("hfbid");
    if (hfbid && bid) {
        hfbid.value = bid;
    }
    var textBox = document.getElementById("tbaCode");
    textBox.value = name;
    closediv();
    var btsubmitorder = document.getElementById("submitorder");
    if (btsubmitorder != null & btsubmitorder != ''& btsubmitorder != 'undefined')
    {
        btsubmitorder.value = "提交订单";
        btsubmitorder.disabled = false;
    }
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function ShowTip(obj,objdiv,addx,addy)
{
	var x=getposOffset_top(obj,'left')+100;
    var y=getposOffset_top(obj,'top');
    
    var div_obj=document.getElementById(objdiv);
	div_obj.style.left=(x+addx)+'px';
	div_obj.style.top=(y+addy)+'px';
	div_obj.style.display="inline";
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