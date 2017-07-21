/*
zheng_jianfeng 2009-08-29 ihangjing
改善asp.net的验证控件的显示效果，用比较人性化的气泡提示来替换掉原来简单的提示信息显示效果
*/

if(typeof(__Win__IsAutoClose)=="undefined" || typeof(__Win__IsFilterClose)=="undefined" || typeof(__Win__CloseWaitTime)=="undefined")
{
    alert("初始化Js失败，请确定正确使用了气泡提示验证控件！")
}
var __Win__BlockFlag = false;

//关闭气泡提示
function HideWinErrMsgTips(elementid)
{

    var ua = navigator.userAgent.toLowerCase();
    var isOpera = (ua.indexOf('opera') != -1);
    var isIE = (ua.indexOf('msie') != -1 && !isOpera);
    
    var objWinDiv = document.getElementById(elementid);

    if(isIE&&typeof(__Win__IsFilterClose)!="undefined"&&__Win__IsFilterClose)
    {
       __Win__BlockFlag = false;
       HideIEWinErrMsgTips(objWinDiv.id);
    }
    else
    {
        objWinDiv.style.display = "none";
	    document.getElementById(elementid).style.display = "none";//zheng_jianfeng 2009-08-31 add this line for fix bug
	}
}

function HideIEWinErrMsgTips(elementid)
{
     var obj___ = document.getElementById(elementid+"____")
     var  opacty=obj___.filters.alpha.opacity;   
     obj___.filters.alpha.opacity-=9;
     if(obj___.filters.alpha.opacity>0)
     {
        if(!__Win__BlockFlag)
            setTimeout("HideIEWinErrMsgTips('"+elementid+"')",100);
     }
     else
     {
         document.getElementById(elementid).style.display = "none";;
        document.getElementById(elementid).style.display = "none";//zheng_jianfeng 2009-08-31 add this line for fix bug
     }
    
}

//得到某obj的x,y坐标,兼容大部分的浏览器
function getWinElementPos(obj)
{

    var ua = navigator.userAgent.toLowerCase();
    var isOpera = (ua.indexOf('opera') != -1);
    var isIE = (ua.indexOf('msie') != -1 && !isOpera); // not opera spoof

    var el = obj;

    if(el.parentNode === null || el.style.display == 'none') 
    {
        return false;
    }

    var parent = null;
    var pos = [];
    var box;

    if(el.getBoundingClientRect) //IE
    {
        box = el.getBoundingClientRect();
        var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
        var scrollLeft = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);

        return {x:box.left + scrollLeft, y:box.top + scrollTop};
    }
    else if(document.getBoxObjectFor) // gecko
    {
        box = document.getBoxObjectFor(el);
         
        var borderLeft = (el.style.borderLeftWidth)?parseInt(el.style.borderLeftWidth):0;
        var borderTop = (el.style.borderTopWidth)?parseInt(el.style.borderTopWidth):0;

        pos = [box.x - borderLeft, box.y - borderTop];
    }
    else // safari & opera
    {
        pos = [el.offsetLeft, el.offsetTop];
        parent = el.offsetParent;
        if (parent != el) 
        {
            while (parent) 
            {
                pos[0] += parent.offsetLeft;
                pos[1] += parent.offsetTop;
                parent = parent.offsetParent;
            }
        }
        if (ua.indexOf('opera') != -1 || ( ua.indexOf('safari') != -1 && el.style.position == 'absolute' )) 
        {
            pos[0] -= document.body.offsetLeft;
            pos[1] -= document.body.offsetTop;
        } 
    }

    if (el.parentNode) { parent = el.parentNode; }
    else { parent = null; }

    while (parent && parent.tagName != 'BODY' && parent.tagName != 'HTML') 
    {
        pos[0] -= parent.scrollLeft;
        pos[1] -= parent.scrollTop;

        if (parent.parentNode) { parent = parent.parentNode; } 
        else { parent = null; }
    }
    return {x:pos[0], y:pos[1]};
}


//处理验证控件显示,重写系统原来的显示方法
function ValidatorUpdateDisplay(val)
{
     if (typeof(val.display) == "string")
     {
        if (val.display == "None")
        {
            return;
        }
    }
    if ((navigator.userAgent.indexOf("Mac") > -1) &&(navigator.userAgent.indexOf("MSIE") > -1))
    {
        val.style.display = "inline";
    }
    val.style.position = "absolute";
    val.style.className = "";
    var closestr = " <span style=\"cursor:pointer;color:red;\" title=\"关闭提示\" onclick=\"javascript:HideWinErrMsgTips('"+val.id+"')\">[关闭]</span>&nbsp;";
    val.innerHTML="<div style=\"position:absolute;z-index:9999;filter:alpha(opacity=100)\" class='tip-bg' id='"+val.id+"____'><div class=\"tip-bulb\">&nbsp;&nbsp;&nbsp;"+val.errormessage+closestr+"</div></div>";

    obj = document.getElementById(val.controltovalidate)
    var WinElementPos = getWinElementPos(obj)
	val.style.left = (parseInt(WinElementPos.x+obj.offsetWidth+10)).toString() + "px";
	val.style.top = (parseInt(WinElementPos.y)).toString() + "px";
    val.style.display = val.isvalid ? "none" : "";
    val.style.display = val.isvalid ? "none" : "block"; //zheng_jianfeng 2009-08-31 add this line for fix bug

    if (val.isvalid) {
        //
    }
    else {
        hideload_super();//显示加载层，防止多交提交
    }

    __Win__BlockFlag = true;
    
    if (typeof (__Win__IsAutoClose) != "undefined" && __Win__IsAutoClose && typeof (__Win__CloseWaitTime) != "undefined") {
        setTimeout("HideWinErrMsgTips('" + val.id + "')", __Win__CloseWaitTime);

    }
}

/*自定义textbox*/
function showhint(iconid, str)
{
	var imgUrl='images/Control/hint.gif';
	if (iconid != 0)
	{
		imgUrl = 'images/Control/warning.gif';
	}
	document.write('<div style="background:url(' + imgUrl + ') no-repeat 20px 10px;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin-bottom:10px; padding:10px 10px 10px 56px; text-align: left; font-size: 12px;">');
	document.write(str + '</div><div style="clear:both;"></div>');
}

function showloadinghint(divid, str)
{
	if (divid=='')
	{
		divid='PostInfo';
	}
	document.write('<div id="' + divid + ' " style="display:none;position:relative;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin:auto;padding:10px" width="90%"  ><img border="0" src="images/Control/ajax_loading.gif" /> ' + str + '</div>');
}

function getposition(obj)
{
	var r = new Array();
	r['x'] = obj.offsetLeft;
	r['y'] = obj.offsetTop;
	while(obj = obj.offsetParent)
	{
		r['x'] += obj.offsetLeft;
		r['y'] += obj.offsetTop;
	}
	return r;
}
var m_obj;
//显示提示层
function showhintinfo(obj, objleftoffset,objtopoffset, title, info , objheight, showtype ,objtopfirefoxoffset)
{
   m_obj = obj;
   var p = getposition(obj);
   
   if((showtype==null)||(showtype =="")) 
   {
       showtype =="up";
   }
   document.getElementById('hintiframe'+showtype).style.height= objheight + "px";
   document.getElementById('hintinfo'+showtype).innerHTML = info;
   document.getElementById('hintdiv'+showtype).style.display='block';
   
   if(objtopfirefoxoffset != null && objtopfirefoxoffset !=0 && !isie())
   {
        document.getElementById('hintdiv'+showtype).style.top=p['y']+parseInt(objtopfirefoxoffset)+"px";
   }
   else
   {
        if(objtopoffset == 0)
        { 
			if(showtype=="up")
			{
				 document.getElementById('hintdiv'+showtype).style.top=p['y']-document.getElementById('hintinfo'+showtype).offsetHeight-40+"px";
			}
			else
			{
				 document.getElementById('hintdiv'+showtype).style.top=p['y']+obj.offsetHeight+5+"px";
			}
        }
        else
        {
			document.getElementById('hintdiv'+showtype).style.top=p['y']+objtopoffset+"px";
        }
   }
   document.getElementById('hintdiv'+showtype).style.left=p['x']+objleftoffset+"px";
   
   
   obj.className="FormMouseOver";

}

//隐藏提示层
function hidehintinfo()
{
    document.getElementById('hintdivup').style.display='none';
    m_obj.className="FormBase";
    //document.getElementById('hintdivdown').style.display='none';
}