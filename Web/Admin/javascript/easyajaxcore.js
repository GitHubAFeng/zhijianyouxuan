function Ajax(){
	var loading_html = '<img src="http://www.dianyifen.com/images/ajaxloading.gif" border="0"/>';
	var tip = null; 
	var ResponseText = null; 
	var BackFuc = null; //服务器返回后，调用的方法。
	
	this.callServer = function(reqUrl,postStr,_ResponseText,callBack,flag,synchronize)
	{
		ResponseText = _ResponseText;
		BackFuc = callBack;
		if(flag)
		{
			setWaitInfo();
        }
		service(reqUrl,postStr,synchronize);
	}
	
	var service = function(reqUrl,postStr,synchronize)
	{
		var req = getRequest();
		req.onreadystatechange = function() 
		{
			if (req.readyState == 4)
			{
				handleResponse(req.responseXML, req.responseText);
	        }
		}
		var method = null==postStr? "get":"post";
		if(!synchronize)
		{
		    req.open(method,reqUrl,true);
		}
		else
		{
		    req.open(method,reqUrl,false);
		}
		
		if("post"==method)
		{
			req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
			req.setRequestHeader("Content-Length",postStr.length);	
			req.send(postStr);
		}
		try 
		{
			req.send(null);
		} 
		catch (e) {}
	}
	var handleResponse = function(xml,txt)
	{
		if(null!=tip)
		{
			document.body.removeChild(tip);
	    }
		if(null!=ResponseText)
		{
			ResponseText.innerHTML = txt;
	    }
		if(null!=BackFuc)//如果回调函数存在，则调用。
		{
			BackFuc(txt,xml);
	    }
	}
	var getRequest = function()
	{
		var req = null
		if (window.XMLHttpRequest) //for firefox etc..	
			req = new XMLHttpRequest();
		else if (window.ActiveXObject)//for IE
			req = new ActiveXObject("Microsoft.XMLHTTP");
		return req;	          	
	}
	var setWaitInfo = function()
	{
		tip = document.createElement("span");
		tip.innerHTML= loading_html+"在获取数据,请稍候 ............";
		document.body.appendChild(tip);
		tip.setAttribute('style','position:absolute;left:40%;top:360px;margin:0 0 0 -50px; display:block; padding:10px 20px; background:#E5E5E5; border:1px #999 solid;');
	}
}

Ajax.doGet = function(reqUrl,disResponseObj,callBack,flag,synchronize)
{
	var ajax = new Ajax();
	var ur=reqUrl+(reqUrl.indexOf("?")>=0?"&t=":"?t=")+new Date();
	ajax.callServer(reqUrl,null,disResponseObj,callBack,flag,synchronize);
}

Ajax.doPost = function(reqUrl,postStr,disResponseObj,callBack,flag,synchronize)
{
	var ajax = new Ajax();
	ajax.callServer(reqUrl,postStr,disResponseObj,callBack,flag,synchronize);
}

var $id = function( id )
{
	return document.getElementById(id);
}

var $name = function(name)
{
	return document.getElementsByName(name);
}

var $hidden=function(id)
{
	$id(id).style.display="none";
}

var $display=function(id)
{
	$id(id).style.display="block";
}

var $names = function(name)
{
	return document.getElementsByName(name);
}