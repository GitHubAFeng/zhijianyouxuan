
var huKou = new Array();
var xmlHttp;
var btv = '';
var id = '';
var cot_huk_id = '';

function show_province(evt, cot_huk, T)
{
   ShowTip(document.getElementById("tbaCode"),"tabpopup1",-100,30);
}

function closediv()
{
    document.getElementById("tabpopup1").style.display = "none";
}
function InitAddress(name, bid) {

    var hfbid = document.getElementById("hfbid");
    if (hfbid && bid) {
        hfbid.value = bid;
    }
    var textBox = document.getElementById("tbaCode");
    textBox.value = name;
    closediv();
}

//��ʾһ��div
//��ʾ��Ҫ��λ
//obj����Ҫ��ʾ��div��ԵĶ���һ����һ����ť���������� this���� 
//addx��addy�������obj��ƫ����������div��ʾ��λ��
function ShowTip(obj,objdiv,addx,addy)
{
	var x=getposOffset_top(obj,'left');
    var y=getposOffset_top(obj,'top');
    
    var div_obj=document.getElementById(objdiv);
	div_obj.style.left=(x+addx)+'px';
	div_obj.style.top=(y+addy)+'px';
	div_obj.style.display="inline";
}

//��ȡƫ����
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