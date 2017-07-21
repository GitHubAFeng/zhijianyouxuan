// OutComplete.js
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-20

//自动搜索补全用户输入 js代码

//使用实例：

/*输入地址字符寻找一个城市 是城市的值 比如杭州
<input class="txt" id="workAddr" name="workAddr" maxlength="10" autocomplete="off" type="text" 
onkeyup="suggestCity(event,this)" 
onkeydown="responseInput(event,this)" 
onkeypress="return event.keyCode==13?false:true" >
*/

/*页面中放置弹出的搜索结果div:<div style="display: none; left: 526px; top: 236px;" id="address_drop"></div>*/

/*ajax获取返回的值格式：
<a onmouseover="mouseOverAddr(this)" onmouseout="mouseOutDiv(this)" href="javascript:void(0)" onclick="SetDivDisplayType(pulldownId,"none")">杭州</a>
<input name="杭州" value="1" type="hidden">

<a onmouseover="mouseOverAddr(this)" onmouseout="mouseOutDiv(this)" href="javascript:void(0)" onclick="SetDivDisplayType(pulldownId,"none")">海口</a>
<input name="海口" value="2" type="hidden">


<a class="off" href="javascript:void(0)" onclick="SetDivDisplayType('address_drop','none')">关闭</a>
*/
/*样式
#address_drop a:hover,
#address_drop a.drop_item{background:#f4f1f8;}
#address_drop a.off,
#address_drop a.off:hover{background:#eee;text-align:right;}
#address_drop a span{float:right;color:#333;}
*/
//need common.js
var pulldownId = "address_drop";//a div which take out complete

var pulldownAjax=null;
var isUserAddr = false;
var sugUserAddr =" ";
var curPosition = -1;
var totalResultNum = 0;//自动补全搜索到的结果数量
var curInput = null;

//onkeydown事件 (将弹出框中选中的结果填写到输入框中)
function responseInput(tEvent,tThis)//b is this input,c is event
{
    if(!document.getElementById(pulldownId))
    {
        return; 
    }
    var A=document.getElementById(pulldownId).getElementsByTagName("a");
    if(!A.length||document.getElementById(pulldownId).style.display=="none")
    {   
        return;
    }
    totalResultNum=A.length;
    
    response(tEvent,tThis?tThis:this,pulldownId)
}

//将弹出框中选中的结果填写到输入框中
function response(tEvent,tThis,divDrop)
{
    var A = document.getElementById(divDrop).getElementsByTagName("a");
    var InputList=document.getElementById(divDrop).getElementsByTagName("input");
    
    if(tEvent.keyCode==38)
    {
        curPosition -= 1;
        if(curPosition <= -1)
        {
            curPosition=totalResultNum-2;
            A[0].className="";
        }
        else
        {
            A[curPosition+1].className="";
        }
        //选中的值填入输入框
        tThis.value=InputList[curPosition]?InputList[curPosition].name:A[curPosition].innerHTML.replace(/<(.+)>.*<\/\1>/,"");
        
        A[curPosition].className="drop_item";
    }
    else
    {
        if(tEvent.keyCode==40)
        {
            curPosition+=1;
            if(curPosition >= totalResultNum-1)
            {
                A[curPosition-1].className="";
                curPosition=0
            }
            else
            {
                if(curPosition>0)
                {
                    A[curPosition-1].className=""
                }
            }
            
            tThis.value=InputList[curPosition]?InputList[curPosition].name:A[curPosition].innerHTML.replace(/<(.+)>.*<\/\1>/,"");

            A[curPosition].className="drop_item";
        }
   }
}



//自动补全开始
function suggestCity(eventThis)//(event,this)
{
    var objectInPut = eventThis.srcElement?eventThis.srcElement:eventThis.target;//event.srcElement就是指向触发事件的元素，什么对象就有什么的属性
    curInput = objectInPut;
    
    var input_value = curInput.value;
    
    if( input_value == "")
    {
        document.getElementById(pulldownId).style.display="none";//replace hide()
        return; 
    }
    //搜索的ajax执行页面地址
    //var AjaxUrl = "GetCity.aspx?query="+$E(B);
    
    suggestInput(eventThis,$E(curInput),objectInPut);
}
            
function suggestInput(eventThis,Query,inPut)//c 是event Query 参数（输入框中的值） B当前输入框对象 
{
    var dropObject = document.getElementById(pulldownId);
    if(!dropObject)
    {
        return 
    }

    if(eventThis.keyCode==38||eventThis.keyCode==40)//up or dowm
    {
        if(dropObject.innerHTML&&dropObject.style.display=="none")
        {
	        dropObject.innerHTML=""
	    }
	        return 
    }
    else//not up or down
    {
        if(eventThis.keyCode==13)//enter key
        {
            dropObject.style.display="none";
            return;
        }
        else
        {
            if(eventThis.keyCode==9||eventThis.keyCode==37||eventThis.keyCode==39)//tab、left or right
            {
                return 
            }
        }
    }
    //ajax获取搜索结果并显示 jquery
	$.ajax({  
	type: "GET",  
	url: "Ajaxorder.aspx",  
	data: { names:$E(document.getElementById("tbKeyword").value),time:new Date().getTime(),userid:document.getElementById("tbuserids").value},  
	success: function(theResponse) 
	{
	    var V = theResponse;
        if(V)
        {
            dropObject.innerHTML = "<iframe id='adir' frameborder='0' height='0'></iframe><div id='ssss'>"+V+"</div>";
        }
        else
        {
            dropObject.innerHTML = "";
        }
        
        var P = dropObject.getElementsByTagName("INPUT");
        
        if($("ssss"))
        {
            var K = $("ssss").clientHeight;
            K = K?K:18*(P.length);
            document.getElementById("adir").style.height = K + "px";
        }
        if(V)
        {
            var U = dropObject.style;

            if((dropObject.style.display != "none") == false)
            {
                U.left = "-1000px";
            }
            
            dropObject.style.display = "block";
            var N = inPut;
            var Q = inPut.offsetTop,Z = inPut.clientHeight,W = inPut.offsetLeft, T = inPut.type;
            var X = false;
            while(inPut = inPut.offsetParent)
            {
                Q += inPut.offsetTop;
                W += inPut.offsetLeft;
                if(ABBrowser.isIE())
                {
                    var J = inPut.style.paddingLeft;
                    J = J?parseInt(J,10):0;
                    var G = inPut.style.paddingTop; G = G?parseInt(G,10):0;
                    var S = inPut.style.borderLeft; S = S?parseInt(S,10):0;
                    var L = inPut.style.borderTop;  L = L?parseInt(L,10):0;
                    var H = inPut.style.marginLeft; H = H?parseInt(H,10):0;
                    var E = inPut.style.marginTop;  E = E?parseInt(E,10):0;
                    var I = J+S+H;
                    var F = G+L+E;
                    
                    if("msg_addr_down" == inPut.id)
                    {
                        X = true;
                    }
                    Q -= F;
                    if(!X)
                    {
                        W -= I;
                    }
                }//if(IE()) end
            }//while end
            
            var D = N.offsetWidth;
            var M = document.getElementById("ssss").offsetHeight;
            var O = ABBrowser.getViewWidth(),Y = ABBrowser.getScrollLeft(),R = ABBrowser.getScrollTop();
            if(ABBrowser.getViewHeight()+R-Q-Z>=M)
            {
                U.top = ((T=="image")?Q+Z:Q+Z+6)+"px";
            }
            else
            {
                U.top = ((Q-R<M)?((T=="image")?Q+Z:Q+Z+6):Q-M-2)+"px";
            }

            if(O+Y-W>=D)
            {
                U.left=W+"px";
            }
            else
            {
                U.left=((O>=D)?O-D+Y:Y)+"px";
            }
        }//if(V) end
        else
        {
            SetDivDisplayType(pulldownId,"none")
        }
        
        curPosition=-1;
        totalResultNum=0
        }//success end
		});//$.ajax({   end
   
}//suggestInput

function selectItem(A)
{
    curInput.value = A;
    SetDivDisplayType(pulldownId,"none");
}

function SetDivDisplayType(objectDivId,displayType)
{
    if("object" != typeof document.getElementById(objectDivId) )
    {
	    return 
    }
    if(displayType)
    {
        if(document.getElementById(objectDivId))
        {
	        document.getElementById(objectDivId).style.display=""+displayType
	    }
	    return 
    }
    
    if("none"==document.getElementById(objectDivId).style.display)
    {
	    document.getElementById(objectDivId).style.display="block"
    }
    else
    {
	    document.getElementById(objectDivId).style.display="none"
    }
}

function mouseOutDiv(objectDiv)
{
    objectDiv.className="";
}

function mouseOverAddr(objectDiv)
{
    mouseOverDiv(objectDiv,pulldownId)
}

function mouseOverWhat(objectDiv)
{
    mouseOverDiv(objectDiv,pulldownId)
}

function mouseOverDiv(objectDiv,B)
{
    if(!B)
    {
        B = pulldownId;
    }
    var A = document.getElementById(B).getElementsByTagName("a");
    for(var C=0;C<totalResultNum-1;C++)
    {
        if(A[C]==objectDiv)
        {
	        curPosition=C;A[C].className="drop_item"
        }
        else
        {
            A[C].className="";
        }
    }
}

function $E(A)
{
    if( A==null || A==undefined)
    {
        return A;
    }
    return encodeURIComponent(A);
}

//function suggestRoadAddrInCity(B,C)
//{
//    var A=C?C:getCookie("city");
//    suggestAddr(B,A,6)
//}
