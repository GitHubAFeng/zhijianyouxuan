
var huKou = new Array();
var xmlHttp;
var btv = '';
var id = '';
var cot_huk_id = '';

function show_province(cot_huk ,T)//evt, 
{

   document.getElementById("tabpopup1").style.display = "";
   ShowTip(document.getElementById("tbBuilding"),"tabpopup1",-100,30);
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
function InitAddress(name, dataid) {
   //判断有没有
    var ids = document.getElementById("tbBuildingId").value+"";
     var idGrade=document.getElementById("BuildingGrade").value+"";

    var idarray = ids.split(',');
    var dataidd = "" + dataid + ""; //建筑事
    for (var i = 0; i < idarray.length; i++) {
        if (dataidd ==idarray[i]) {
            alert('您已经选择过了此建筑物');
            return;
        }
    }
    var textBox = document.getElementById("tbBuilding");
    if (textBox.value == "") {
        textBox.value = name;
    }
    else {
        textBox.value = document.getElementById("tbBuilding").value + "," +name;
    }
    var hidBuildingId = document.getElementById("tbBuildingId");
    if (hidBuildingId.value == "") {
        hidBuildingId.value = dataidd;
    }
    else {
        hidBuildingId.value = document.getElementById("tbBuildingId").value + "," +""+dataid+"";
    }
   
}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function ShowTip(obj,objdiv,addx,addy)
{
	var x=getposOffset_top(obj,'left');
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