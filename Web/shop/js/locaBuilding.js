
function show_province() {

    document.getElementById("tabpopup4").style.display = "block";
    ShowTip(document.getElementById("aselect"), "tabpopup4", 0,0);
}

function closediv()
{
    document.getElementById("tabpopup4").style.display = "none";
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