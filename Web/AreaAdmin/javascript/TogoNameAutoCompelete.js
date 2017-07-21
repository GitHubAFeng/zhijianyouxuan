// TogoNameAutoCompelete.js
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-15

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
        var RetValue =  new Array();
        RetValue = InputList[curPosition]?InputList[curPosition].name.split("|"):A[curPosition].innerHTML.replace(/<(.+)>.*<\/\1>/,"").split("|");
        //alert(RetValue);
        //选中的值填入输入框
        tThis.value= RetValue[0];
        //此处需要保存商家的编号ID zjf@ihangjing.com 2010-07-15
        //"点一份{|}1" 分隔符为：{|}
        document.getElementById("hidTogoId").value = RetValue[1];
        
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
            var RetValue1 =  new Array();
            RetValue1 = InputList[curPosition]?InputList[curPosition].name.split("|"):A[curPosition].innerHTML.replace(/<(.+)>.*<\/\1>/,"").split("|");
            tThis.value=RetValue1[0];
            document.getElementById("hidTogoId").value = RetValue1[1];
            
            A[curPosition].className="drop_item";
        }
   }
}

//自动补全开始
function suggestTogoName(eventThis)//(event,this)
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
    var input_tbkeyword = eventThis.srcElement?eventThis.srcElement:eventThis.target;
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

    var _name = document.getElementById("tbTogoName");
    var _value = _name.value;
     
	$.ajax({  
	type: "GET",  
	url: "../ajax/TogoNameCommplit.aspx",  
	data: { names:escape( _value) ,time:new Date().getTime()},  
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
        
        
        if($("ssss")&&V!="")
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
    var RetValue =  new Array();
    RetValue = A.split("|");
    
    //此处需要保存商家的编号ID zjf@ihangjing.com 2010-07-15
    //"点一份{|}1" 分隔符为：{|}
    document.getElementById("hidTogoId").value = RetValue[1];
        
    curInput.value = RetValue[0];
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

function parseXml(resultxml)
{
    var size = resultxml.length;
    var row, cell, txtNode;
    for (var i = 0; i < size; i++) 
    {
        var nextNode = resultxml[i].firstChild.data;
        row = document.createElement("tr");
        cell = document.createElement("td");

        cell.onmouseout = function() {this.className='mouseOver';};
        cell.onmouseover = function() {this.className='mouseOut';};
        cell.setAttribute("bgcolor", "#FFFAFA");
        cell.setAttribute("border", "0");
        cell.onclick = function() { populateName(this); } ;

        txtNode = document.createTextNode(nextNode);
        cell.appendChild(txtNode);
        row.appendChild(cell);
        nameTableBody.appendChild(row);
    }
}


function divnone()
{
    var _div = document.getElementById("address_drop");
    if (_div.style.display == 'block')
    {
        _div.style.display = 'none';
        return;
    }
    else
    {
        return;
    }
}

//获取客户信息库中的客户信息
function GetCustomerInfo()
{
    var _name = document.getElementById("tbTel");
    var _value = _name.value;
     
	$.ajax({  
	type: "GET",  
	url: "../ajax/CustomerDB.aspx",  
	data: { tel:escape( _value) ,time:new Date().getTime()},  
	success: function(theResponse) 
	{
	    //返回的信息 姓名|地址|备注
	    var V = theResponse;
	    
	    //无记录
	    if( V == "0")
	    {
	        document.getElementById("hidHaveCustomer").value ="0";
	        return;
	    }
	    
        var RetValue = new Array();
        RetValue = V.split('|');
        
        document.getElementById("tbCustomerName").value = RetValue[0];
        document.getElementById("tbAddress").value = RetValue[1];
        document.getElementById("tbRemark").value = RetValue[2];
        
    }//success end
	});//$.ajax({   end
}